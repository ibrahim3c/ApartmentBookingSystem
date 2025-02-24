using ApartmentBooking.Domain.Bookings;
using ApartmentBooking.Domain.Bookings.DomainEvents;
using ApartmentBooking.Domain.Users;
using MediatR;

namespace ApartmentBooking.Application.Bookings.ReserveBooking
{
    internal sealed class ReserveBookingDomainEventHandler : INotificationHandler<BookingReservedDomainEvent>
    {
        IBookingRepository _bookingRepository;
        IUserRepository _userRepository;

        public ReserveBookingDomainEventHandler(IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
        {
            var booking =await _bookingRepository.GetByIdAsync(notification.id,cancellationToken);
            if (booking == null)
                return;

            var user = await _userRepository.GetByIdAsync(booking.UserId);
            if (user == null)
                return;

            // TODO: send the notification using EmailService => it Abstractions/Email
            return;

        }
    }
}
