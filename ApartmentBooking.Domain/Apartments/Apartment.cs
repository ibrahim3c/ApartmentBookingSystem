using ApartmentBooking.Domain.Abstraction;
using ApartmentBooking.Domain.Shared;

namespace ApartmentBooking.Domain.Apartments
{
    // make the class sealed
    // if I don't intend to inherit them at any point.
    // It might give you a small performance improvement
    // this is anemic Entity
    public sealed class Apartment:Entity
    {
        public Apartment(Guid id, Name name,Description description,
            Address address, List<Amenty> amenties,
            Money price, Money cleaningFee) : base(id)
        {
            Name = name;
            Description = description;
            Address = address;
            Amenties = amenties;
            Price = price;
            CleaningFee = cleaningFee;
        }
        private Apartment()
        {

        }

        /*
         * why he make setter private? 
            This approach follows Clean Architecture and DDD best practices 
                by ensuring that the only way to modify an entity is through controlled domain logic methods
                rather than direct property assignments.
            Encapsulation           → Prevents direct modification from outside the entity.
            Enforces Business Rules → Updates must follow controlled logic.
            Ensures Data Integrity  → Prevents invalid or null values.
            Promotes Immutability   → Read-only properties make the entity more predictable.
            Prevents Bugs & Side Effects → No unexpected changes from external code.
         */


        //public Guid Id { get; private set; }
        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public Address Address { get; private set; }
        public Money Price { get; private set; }
        public Money CleaningFee { get; private set; }
        public DateTime? LastBookedOnUTC { get; internal set; }


        // represent the benefits this apartment offers
        public List<Amenty> Amenties { get; private set; } = new();

    }
}
