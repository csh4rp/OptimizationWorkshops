using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using OptimizationWorkshops.Workshop1.WebBackend;
using OptimizationWorkshops.Workshop1.WebBackend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddScoped<SqliteConnection>(_ =>
    {
        var connectionString = builder.Configuration.GetConnectionString("Sqlite");
        var connection = new SqliteConnection(connectionString);
        return connection;
    })
    .AddScoped<DbInitializer>();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await initializer.InitializeAsync();
}

app.MapGet("/data/latest-summary", async ([FromServices] SqliteConnection connection) =>
{
    await connection.OpenAsync();

    await using var command = connection.CreateCommand();
    command.CommandText = "SELECT SUM(X), SUM(Y), SUM(Z) FROM DataFrame ORDER BY Timestamp DESC LIMIT 100;";

    await using var reader = await command.ExecuteReaderAsync();
    await reader.ReadAsync();

    var x = reader.GetDouble(0);
    var y = reader.GetDouble(1);
    var z = reader.GetDouble(2);

    return Results.Ok(new { X = x, Y = y, Z = z });
});

app.MapPost("/data", async ([FromServices] SqliteConnection connection, [FromBody] DataFrameRequest request) =>
{
    await connection.OpenAsync();

    await using var insertCommand = connection.CreateCommand();
    insertCommand.CommandText = "INSERT INTO DataFrame (Timestamp, X, Y, Z) VALUES (@Timestamp, @X, @Y, @Z);";

    var timestampParameter = insertCommand.CreateParameter();
    timestampParameter.ParameterName = "@Timestamp";
    timestampParameter.Value = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    var xParameter = insertCommand.CreateParameter();
    xParameter.ParameterName = "@X";
    xParameter.Value = request.X;

    var yParameter = insertCommand.CreateParameter();
    yParameter.ParameterName = "@Y";
    yParameter.Value = request.Y;

    var zParameter = insertCommand.CreateParameter();
    zParameter.ParameterName = "@Z";
    zParameter.Value = request.Z;

    insertCommand.ExecuteNonQuery();
});

app.Run();
