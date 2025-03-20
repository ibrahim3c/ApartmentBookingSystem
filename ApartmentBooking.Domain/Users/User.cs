using ApartmentBooking.Domain.Abstraction;
using ApartmentBooking.Domain.Users.Events;

namespace ApartmentBooking.Domain.Users
{
    public sealed class User : Entity
    {
        // he will make constructor is private and use factory method to create instance
        //Just wrapping the constructor inside of a factory method.
        //There are a few benefits to using this approach. One is you are hiding your constructor which could have some other implementation details that you don't want to expose outside of the user entity.
        //Another benefit is encapsulation and the real reason I'm doing this is to be able to introduce some side effects inside of the factory method that don't naturally belong inside of a constructor. 
        //what i am talking about our domain events and I'm going to show you how to implement them.

        /*
         * why make constructor private and use factory method here?
         ✔ Private Constructor → Prevents direct instantiation, forces the use of Create().
         ✔ Factory Method (Create) → Centralized creation logic, allows future modifications.
         ✔ Supports Domain Events → Keeps constructor clean, avoids side effects inside constructor.
         */

        private User(Guid id, FirstName firstName, LastName lastName, Email email) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        private User()
        {
            
        }

        public FirstName FirstName { get; set; }
        public LastName LastName { get; set; }
        public Email Email { get; set; }

        // factory method
        public static User Create(FirstName firstName, LastName lastName, Email email)
        {
            var user= new User(new Guid(), firstName, lastName, email);
            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
            return user;
        }

    }
}
