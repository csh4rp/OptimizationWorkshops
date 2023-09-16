var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/get-async", async () =>
{
    await Task.Delay(1000);
    return Results.NoContent();
});

app.MapGet("/get-sync", () =>
{
    Thread.Sleep(1000);
    return Results.NoContent();
});

await app.RunAsync();
