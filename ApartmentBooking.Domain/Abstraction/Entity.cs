namespace ApartmentBooking.Domain.Abstraction
{
    public abstract class Entity
    {
        //You can modify the content of a readonly list like RaiseDomainEvent method,
        //but you cannot reassign the list itself after it is initialized except initialization
                                                            // no =>_domainEvents=new IDomainEvent .
        private readonly List<IDomainEvent> _domainEvents=new();  
        protected Entity(Guid id)
        {
            Id = id;
            //_domainEvents=new List<IDomainEvent>(); // allowed cuz it is initialization
        }

        protected Entity()
        {        }
        public Guid Id { get; init; }

        public IReadOnlyList<IDomainEvent> GetDomainEvents() {
            //_domainEvents=new List<IDomainEvent>(); // not allowed cuz it is reassigning
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
