using ApartmentBooking.Application.Abstractions.Messaging;

namespace ApartmentBooking.Application.Bookings.GetBooking;

public record GetBookingQuery(Guid BookingId):IQuery<BookingResponse>
{
}
