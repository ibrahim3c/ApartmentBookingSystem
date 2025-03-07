﻿using ApartmentBooking.Application.Abstractions.Data;
using ApartmentBooking.Application.Abstractions.Messaging;
using ApartmentBooking.Domain.Abstraction;
using ApartmentBooking.Domain.Bookings;
using Dapper;

namespace ApartmentBooking.Application.Bookings.GetBooking
{
    // i think internal cuz we will not use it in controller 
    // will we use mediatR and he will route request to its hander
    internal sealed class GetBookingQueryHanlder : IQueryHanlder<GetBookingQuery, BookingResponse>
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;

        public GetBookingQueryHanlder(ISqlConnectionFactory sqlConnectionFactory)
        {
            this.sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<BookingResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            // we will use dapper to query data
            using var connection= sqlConnectionFactory.CreateConnection();
            const string sql = """
                                SELECT 
                                    id AS Id,
                                    apartment_id AS ApartmentId,
                                    user_id AS UserId,
                                    status AS Status,
                                    price_for_period_amount AS PriceAmount,
                                    price_for_period_currency AS PriceCurrency,
                                    cleaning_fee_amount AS CleaningFeeAmount,
                                    cleaning_fee_currency AS CleaningFeeCurrency,
                                    amenities_up_charge_amount AS AmenitiesUpChargeAmount,
                                    amenities_up_charge_currency AS AmenitiesUpChargeCurrency,
                                    total_price_amount AS TotalPriceAmount,
                                    total_price_currency AS TotalPriceCurrency,
                                    duration_start AS DurationStart,
                                    duration_end AS DurationEnd,
                                    created_on_utc AS CreatedOnUtc
                                FROM bookings
                                WHERE id = @BookingId
                                """;


            var booking = await connection.QueryFirstOrDefaultAsync<BookingResponse>(sql, new
            {
                request.BookingId
            });

            return booking;
           
        }
    }
}
