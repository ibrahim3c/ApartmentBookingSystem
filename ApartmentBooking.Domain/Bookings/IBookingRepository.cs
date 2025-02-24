using ApartmentBooking.Domain.Apartments;

namespace ApartmentBooking.Domain.Bookings
{
    public interface IBookingRepository
    {
        Task<bool> IsOverlappingAsync(Apartment apartment, DateRange dateRange, CancellationToken cancellationToken);
        Task<Booking> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
