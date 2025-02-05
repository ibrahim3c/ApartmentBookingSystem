using ApartmentBooking.Domain.Abstraction;

namespace ApartmentBooking.Domain.Users.Events;

// make it record cuz he is immutable
public record UserCreatedDomainEvent(Guid UserId):IDomainEvent;

