using ApartmentBooking.Application.Abstractions.Data;
using ApartmentBooking.Domain.Apartments;
using ApartmentBooking.Infrastructure.Data;
using Bogus;
using Dapper;

namespace ApartmentBooking.Api.Extensions
{
    public static class SeedDataExtension
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();

            using var connection = sqlConnectionFactory.CreateConnection();

            // Check if there are existing records
            var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Apartments");
            if (count > 0) return; // Stop if data already exists

            var apartments = GenerateApartments(10);

            // Insert data into the database
            const string sql = @"
            INSERT INTO Apartments (Id, Name, Description, Address, Price, CleaningFee, LastBookedOnUTC)
            VALUES (@Id, @Name, @Description, @Address, @Price, @CleaningFee, @LastBookedOnUTC)";

            connection.Execute(sql, apartments);
        }

        private static List<dynamic> GenerateApartments(int count)
        {
            var faker = new Faker();

            var apartments = new List<dynamic>();

            for (int i = 0; i < count; i++)
            {
                apartments.Add(new
                {
                    Id = Guid.NewGuid(),
                    Name = faker.Company.CompanyName(),
                    Description = faker.Lorem.Paragraph(),
                    Address = faker.Address.FullAddress(),
                    Price = faker.Random.Decimal(50, 500),
                    CleaningFee = faker.Random.Decimal(10, 50),
                    LastBookedOnUTC = faker.Date.Past()
                });
            }

            return apartments;
        }
    }
}