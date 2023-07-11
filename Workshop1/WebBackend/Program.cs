using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using WebBackend.Models;
using WebBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddScoped<SqliteConnection>(_ =>
    {
        var connectionString = builder.Configuration.GetConnectionString("Sqlite");
        var connection = new SqliteConnection(connectionString);
        return connection;
    })
    .AddScoped<DbInitializer>()
    .AddScoped<IDataService, DataService>();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await initializer.InitializeAsync();
}

app.MapGet("/data/summary", async ([FromServices] IDataService service,
    CancellationToken cts) =>
{
    var summary = await service.GetSummaryAsync(cts);

    return Results.Ok(summary);
});

app.MapPost("/data", async ([FromServices] IDataService service, 
    [FromBody] DataFrameDto request,
    CancellationToken cts) =>
{
    await service.SaveAsync(request, cts);

    return Results.NoContent();
});

app.Run();
