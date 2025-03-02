using ApartmentBooking.Domain.Apartments;
using ApartmentBooking.Domain.Bookings;
using ApartmentBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApartmentBooking.Infrastructure.Repositories;

internal class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    public BookingRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    private static readonly BookingStatus[] ActiveBookingStatuses =
    {
        BookingStatus.Reserved,
        BookingStatus.Completed,
        BookingStatus.Confirmed
    };
    public async Task<bool> IsOverlappingAsync(Apartment apartment, DateRange dateRange, CancellationToken cancellationToken)
    {
        return await appDbContext.Set<Booking>().AnyAsync(
            a =>
            a.Id == apartment.Id
            && a.Duration.Start <= dateRange.End
            && a.Duration.End >= dateRange.Start
            && ActiveBookingStatuses.Contains(a.Status)
            , cancellationToken);
    }
}
