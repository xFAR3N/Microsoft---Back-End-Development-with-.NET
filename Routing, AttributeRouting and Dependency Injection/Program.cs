using Middleware;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddSingleton<IMyService, MyService>();
builder.Services.AddScoped<IMyService, MyService>();
//builder.Services.AddTransient<IMyService, MyService>();

var app = builder.Build();

app.Use(async (context, next) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>();

    myService.LogCreation("First Middleware");

    await next();
});

app.Use(async (context, next) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>();

    myService.LogCreation("Second Middleware");

    await next();
});
app.MapGet("/", (IMyService myService) =>
{
    myService.LogCreation("Root");
    return Results.Ok("Check the console for service creation logs.");
});

app.Run();

