namespace ApartmentBooking.Domain.Abstraction
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents=new();  
        protected Entity(Guid id)
        {
            Id = id;
        }
        protected Guid Id { get; init; }

        public IReadOnlyList<IDomainEvent> GetDomainEvents() {
            return _domainEvents.ToList();
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        public void RaiseDomainEvent(IDomainEvent domainEvent) { 
            _domainEvents.Add(domainEvent);
        }
    }
}
