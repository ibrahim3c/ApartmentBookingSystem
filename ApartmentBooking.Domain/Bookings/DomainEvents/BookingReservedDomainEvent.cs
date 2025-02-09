using ApartmentBooking.Domain.Abstraction;

namespace ApartmentBooking.Domain.Bookings.DomainEvents;

public sealed record BookingReservedDomainEvent(Guid id):IDomainEvent;
