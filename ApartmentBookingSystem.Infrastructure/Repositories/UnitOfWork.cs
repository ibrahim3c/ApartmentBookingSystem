using ApartmentBooking.Domain.Abstraction;
using ApartmentBooking.Domain.Apartments;
using ApartmentBooking.Domain.Bookings;
using ApartmentBooking.Domain.Reviews;
using ApartmentBooking.Domain.Users;
using ApartmentBooking.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApartmentBooking.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IPublisher publisher;

        private readonly AppDbContext appDbContext;   
        public UnitOfWork(AppDbContext appDbContext,IPublisher publisher)
        {
            this.appDbContext = appDbContext;
            this.publisher = publisher;
            ReviewRepository = new ReviewRepository(appDbContext);
            ApartmentRepository = new ApartmentRepository(appDbContext);
            BookingRepository = new BookingRepository(appDbContext);
            UserRepository = new UserRepository(appDbContext);
        }

        public IReviewRepository ReviewRepository      {get; private set;}
        public IApartmentRepository ApartmentRepository{get; private set;}
        public IBookingRepository BookingRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result= await appDbContext.SaveChangesAsync(cancellationToken);
            await PublishDomainEventsAsync();
            return result;
        }

        private async Task PublishDomainEventsAsync()
        {
            // get domain events from each domain model
            var domainEvents = appDbContext.ChangeTracker.Entries<Entity>()
                .Select(e => e.Entity)
                .SelectMany(e =>
                {
                    var domainEvents = e.GetDomainEvents();
                    e.ClearDomainEvents();
                    return domainEvents;
                }).ToList();

            foreach (var domainEvent in domainEvents)
            {
                await publisher.Publish(domainEvent);
            }
        }
    }
}
