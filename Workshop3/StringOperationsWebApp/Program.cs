using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using StringOperationsWebApp.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/hash", ([FromBody] HashRequest request) =>
{
    var hashedText = "";

    for (int i = 0; i < request.Input.Length; i++)
    {
        var characterToHash = request.Input.Substring(i, 1);
        
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(characterToHash));
        
        hashedText += Convert.ToBase64String(bytes);
    }

    return Results.Ok(hashedText);
});

app.Run();
