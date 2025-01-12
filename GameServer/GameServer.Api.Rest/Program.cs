using System.Text.Json;
using Domain.Contract;
using GameServer;
using GameServer.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Game>();
builder.Services.AddSingleton<IPlaceHolderCommands, PlaceHolderCommands>();
builder.Services.AddControllers();

builder.WebHost.ConfigureKestrel(options => 
    options.ListenAnyIP(5555)
);

var app = builder.Build();

// Configure middleware to handle requests.
app.MapGet("/", () => "Welcome to the Simple HTTP Server!"); // Handles root GET requests.

app.MapGet("/village", () =>
{
    var village = app.Services.GetRequiredService<Game>().Village();
    return Results.Json(village.ToDto());
    
});
//app.MapGet("/village", () => app.Services.GetRequiredService<Game>().GetVillage());

// app.MapPost("/data", async (HttpContext context) =>
// {
//     using var reader = new StreamReader(context.Request.Body);
//     var body = await reader.ReadToEndAsync();
//     return $"You posted: {body}";
// }); // Handles POST requests and echoes the data.


var game = app.Services.GetRequiredService<Game>();
app.Lifetime.ApplicationStarted.Register(() =>
{
    Task.Run(() => game.Run());
});

await app.RunAsync(); 