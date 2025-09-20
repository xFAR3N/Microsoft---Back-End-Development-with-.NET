var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5294);
});
var app = builder.Build();
app.Use(async (context, next) =>
{
    if (context.Request.Query["secure"] != "true")
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Simulated HTTPS Required");
        return;
    }
    await next();
});

app.Use(async (context, next) =>
{
    var input = context.Request.Query["input"];
    if (!IsValidInput(input))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid input");
        return;
    }
    await next();

});

app.Use(async (context, next) =>
{
    if(context.Request.Path == "/unauthorized")
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized acces");
        return;
    }
    await next();
});

app.Use(async (context, next) =>
{
    var isAuthenticated = context.Request.Query["authenticated"] == "true";
    if (isAuthenticated)
    {
        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("Acced denied");
        return;
    }
    context.Response.Cookies.Append("SecureCookie", "SecureData", new CookieOptions
    {
        HttpOnly = true,
        Secure = true
    });
    await next();
});

app.Use(async (context, next) =>
{
    await Task.Delay(100);
    await context.Response.WriteAsync("Processed Asynchronously\n");
    await next();
});

static bool IsValidInput(string input)
{
    return string.IsNullOrEmpty(input) || (input.All(char.IsLetterOrDigit) && !input.Contains("<script>"));
}


app.Run();


