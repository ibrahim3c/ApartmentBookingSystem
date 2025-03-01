using ApartmentBooking.Application.Abstractions.Messaging;

namespace ApartmentBooking.Application.Apartments.GetApartments
{
    public record GetAppartmentsQuery(DateOnly startDate, DateOnly endDate)
        : IQuery<IReadOnlyList<ApartmentResponse>>;
}
