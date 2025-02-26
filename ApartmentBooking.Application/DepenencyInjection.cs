using ApartmentBooking.Application.Behaviors;
using ApartmentBooking.Domain.Bookings;
using Microsoft.Extensions.DependencyInjection;

namespace ApartmentBooking.Application
{
    //it's going to be responsible for registering the services related to the Application Layer
    public static class DepenencyInjection
    {
       
        public static IServiceCollection AddApplication(this IServiceCollection services) {

            services.AddMediatR(conf =>
            {
                // It registers all MediatR handlers from the same assembly where DepenencyInjection is located.
                conf.RegisterServicesFromAssemblies(typeof(DepenencyInjection).Assembly);
                //It tells MediatR to apply LoggingBehavior<TRequest, TResponse> for all request-response types automatically.
                conf.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            //A service related to booking pricing logic (probably used in business rules).
            services.AddTransient<PricingService>();
            return services;
        }

    }
}
