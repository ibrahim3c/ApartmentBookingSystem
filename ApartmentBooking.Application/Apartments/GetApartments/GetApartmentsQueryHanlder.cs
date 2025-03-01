using ApartmentBooking.Application.Abstractions.Data;
using ApartmentBooking.Application.Abstractions.Messaging;
using ApartmentBooking.Domain.Abstraction;
using ApartmentBooking.Domain.Bookings;
using Dapper;

namespace ApartmentBooking.Application.Apartments.GetApartments
{
    internal sealed class GetApartmentsQueryHanlder : IQueryHanlder<GetAppartmentsQuery, IReadOnlyList<ApartmentResponse>>
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;


        private static readonly  int[] ActiveBookingStatuses =
           {
                    (int) BookingStatus.Reserved,
                    (int) BookingStatus.Completed,
                    (int) BookingStatus.Confirmed
           };
        public GetApartmentsQueryHanlder(ISqlConnectionFactory sqlConnectionFactory)
        {
            this.sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(GetAppartmentsQuery request, CancellationToken cancellationToken)
        {
          using var connn = sqlConnectionFactory.CreateConnection();
            var sql = """
                select id as Id,
                name as Name,
                price as Price,
                description as Description,
                currency as Currency ,
                address_country as Country,
                address_state as State,
                address_zipCode as ZipCode,
                address_city as City,
                address_street as Street
                from department
                where duration_start <= @EndDate and duration_end >= @startDate and status = ANY(@ActiveBookingStatuses);
                """;

            var result = await connn.QueryAsync<ApartmentResponse,AddressResponse,ApartmentResponse>(sql, (apartment, address) =>
            {
                apartment.Address = address;
                return apartment;
            },
            new
            {
                request.startDate,
                request.endDate,
                ActiveBookingStatuses
            },
            // we tell to dapper that the address object start from Country field 
            splitOn:"Country"
            );
            return result.ToList();
        }
    }
}
