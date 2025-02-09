using ApartmentBooking.Domain.Abstraction;

namespace ApartmentBooking.Domain.Bookings.DomainEvents;

public sealed record BookingConfirmedDomainEvent(Guid id):IDomainEvent;
