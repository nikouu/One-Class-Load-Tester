var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/test", () =>
{
    Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} Request recieved.");
});

app.Run("http://localhost:3000");