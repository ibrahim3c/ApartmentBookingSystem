using ApartmentBooking.Application.Abstractions.Data;
using ApartmentBooking.Domain.Abstraction;
using ApartmentBooking.Domain.Apartments;
using ApartmentBooking.Domain.Bookings;
using ApartmentBooking.Domain.Users;
using ApartmentBooking.Infrastructure.Data;
using ApartmentBooking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


// install them from nuget packege
//"Microsoft.Extensions.Configuration.Abstractions"
//"Microsoft.Extensions.DependencyInjection.Abstractions" 
namespace ApartmentBooking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
    {
        // efCore
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException(nameof(configuration));
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        // respos
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IApartmentRepository, ApartmentRepository>();
        services.AddScoped<IUnitOfWork,UnitOfWork>();

        //dapper                                      // use same connectionString
        services.AddSingleton< ISqlConnectionFactory >(provider=> new SqlConnectionFactory(connectionString));

        return services;
    }
}
