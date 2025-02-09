using ApartmentBooking.Domain.Abstraction;

namespace ApartmentBooking.Domain.Bookings.DomainEvents;

public sealed record BookingCancelledDomainEvent(Guid id):IDomainEvent;
