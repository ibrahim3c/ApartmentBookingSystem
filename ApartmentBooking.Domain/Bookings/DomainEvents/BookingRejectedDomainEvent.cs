using ApartmentBooking.Domain.Abstraction;

namespace ApartmentBooking.Domain.Bookings.DomainEvents;

public sealed record BookingRejectedDomainEvent(Guid id):IDomainEvent;
