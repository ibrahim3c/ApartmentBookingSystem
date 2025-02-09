using ApartmentBooking.Domain.Abstraction;

namespace ApartmentBooking.Domain.Reviews.DomainEvents;

public record ReviewCreatedDomainEvent(Guid id):IDomainEvent;
