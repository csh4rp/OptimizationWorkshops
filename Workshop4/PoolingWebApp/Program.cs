using System.Buffers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/non-buffer", () =>
{
    using var ms = new MemoryStream();
    using var fs = File.OpenRead("index.html");
    
    fs.CopyTo(ms);

    Results.Ok();
});

app.MapGet("/buffer", () =>
{
    using var fs = File.OpenRead("index.html");
    var length = (int)fs.Length;
    using var pool = MemoryPool<byte>.Shared.Rent(length);
    
    while (length > 0)
    {
        pool.Memory.Span[length] = (byte)fs.ReadByte();
        length--;
    }
    
    Results.Ok();
});

app.Run();
