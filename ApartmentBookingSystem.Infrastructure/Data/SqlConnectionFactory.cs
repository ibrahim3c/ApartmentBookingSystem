using ApartmentBooking.Application.Abstractions.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Data;

namespace ApartmentBooking.Infrastructure.Data;

internal class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public IDbConnection CreateConnection()
    {
        var connection = new SqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}
