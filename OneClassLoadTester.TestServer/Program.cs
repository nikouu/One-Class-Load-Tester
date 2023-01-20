var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/test", async () =>
{
    Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} Request recieved.");
    await Task.Delay(Random.Shared.Next(100, 1000));
});

app.Run("http://localhost:3000");