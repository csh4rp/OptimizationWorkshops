using Microsoft.AspNetCore.Mvc;
using WebBackendApp.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/get-async", async () =>
{
    await Task.Delay(100);
    return Results.NoContent();
});

app.MapGet("/get-sync", () =>
{
    Thread.Sleep(100);
    return Results.NoContent();
});

app.MapPost("/set-threadpool", ([FromBody] SetThreadPoolRequest request) =>
{
    ThreadPool.SetMinThreads(request.Threads, request.Threads);
    return Results.NoContent();
});

await app.RunAsync();
