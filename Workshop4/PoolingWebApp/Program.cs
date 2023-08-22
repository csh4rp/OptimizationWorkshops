using System.Buffers;
using Microsoft.IO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var manager = new RecyclableMemoryStreamManager();

app.MapGet("/non-buffered", () =>
{
    using var ms = new MemoryStream();
    using (var fs = File.OpenRead("index.html"))
    {
        fs.CopyTo(ms);
    }

    return Results.Ok();
});

app.MapGet("/buffered", () =>
{
    using var fs = File.OpenRead("index.html");
    var length = (int)fs.Length;
    using var pool = MemoryPool<byte>.Shared.Rent(length);
    
    while (length > 0)
    {
        pool.Memory.Span[length] = (byte)fs.ReadByte();
        length--;
    }
    
    return Results.Ok();
});

app.MapGet("/recyclable", () =>
{
    using var ms = manager.GetStream();
    using (var fs = File.OpenRead("index.html"))
    {
        fs.CopyTo(ms);
    }

    return Results.Ok();
});

app.Run();
