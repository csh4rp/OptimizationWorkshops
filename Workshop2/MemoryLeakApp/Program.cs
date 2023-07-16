using MemoryLeakApp.Services;
using Microsoft.AspNetCore.Mvc;
using WebBackend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddScoped<DbInitializer>()
    .AddScoped<IDataService, DataService>()
    .AddMemoryCache();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await initializer.InitializeAsync();
}

app.MapGet("/data/summary", async ([FromServices] IDataService service,
    [FromQuery(Name = "tillTime")] DateTimeOffset? tillTime,
    CancellationToken cts) =>
{
    tillTime ??= DateTimeOffset.UtcNow;
    
    var summary = await service.GetSummaryAsync(tillTime.Value, cts);

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
