using ApartmentBooking.Domain.Apartments;
using ApartmentBooking.Domain.Bookings;
using ApartmentBooking.Domain.Reviews;
using ApartmentBooking.Domain.Users;

namespace ApartmentBooking.Domain.Abstraction;

public interface IUnitOfWork
{
    IReviewRepository ReviewRepository { get; }
    IApartmentRepository ApartmentRepository { get; }
    IBookingRepository BookingRepository { get; }
    IUserRepository UserRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
