using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddControllers();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Global exception caught: {ex}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected error occured. Try again later.");
    }
});


app.UseRouting();
app.MapControllers();
app.Run();

