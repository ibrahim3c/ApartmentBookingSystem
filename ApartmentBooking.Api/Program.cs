
using ApartmentBooking.Api.Extensions;
using ApartmentBooking.Api.Middleware;
using ApartmentBooking.Application;
using ApartmentBooking.Infrastructure;

namespace ApartmentBooking.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        #region myConfigs
        // Add configuration from the secret.json file
        builder.Configuration.AddJsonFile("Secret.json", optional: false, reloadOnChange: true);

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.SeedData();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<GlobalExceptionHandler>();
        app.MapControllers();

        app.Run();
    }
}
