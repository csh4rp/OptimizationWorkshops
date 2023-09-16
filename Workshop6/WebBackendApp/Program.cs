var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

ThreadPool.SetMinThreads(500, 500);

app.MapGet("/get-async", async () =>
{
    await Task.Delay(50);
    return Results.NoContent();
});

app.MapGet("/get-sync", () =>
{
    Thread.Sleep(50);
    return Results.NoContent();
});

await app.RunAsync();
