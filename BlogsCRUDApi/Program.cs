using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var blogs = new List<Blog>
{
    new Blog { Title = "My First Post", Body = "This is my first post" },
    new Blog { Title = "My Second Post", Body = "This is my second post" },
};
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use(async (context, next) =>
{
    var startTime = DateTime.UtcNow;
    Console.WriteLine($"Start Time: {DateTime.UtcNow}");
    await next.Invoke();
    var duration = DateTime.UtcNow - startTime;
    Console.WriteLine($"Response Time: {duration.TotalMilliseconds} ms");
});

app.Use(async (context, next) =>
{
    Console.WriteLine(context.Request.Path);
    await next();
    Console.WriteLine(context.Response.StatusCode);
});

app.UseWhen(context => context.Request.Method != "GET",
    appBuilder => appBuilder.Use(async (context, next) =>
    {
        var extractedPassword = context.Request.Headers["X-Api-Key"];
        if (extractedPassword == "thisIsABadPassword")
        {
            await next.Invoke();
        }
        else
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Invalid API Key");
        }
    })
);
app.MapGet("/", () => "I am root!").ExcludeFromDescription();
app.MapGet("/blogs", () =>
{
    return blogs;
});

app.MapGet("/blogs/{id}", Results<Ok<Blog>, NotFound> (int id) =>
{
    if(id < 0 || id >= blogs.Count)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(blogs[id]); 
}).WithOpenApi(operation =>
{
    operation.Parameters[0].Description = "This is id of a blog to retrieve";
    operation.Summary = "Get single blog";
    operation.Description = "Returns a single blog";
    return operation;
});

app.MapPost("/blog", (Blog blog) =>
{
    blogs.Add(blog);
    return Results.Created($"/blogs/{blogs.Count - 1}", blog);
});

app.MapDelete("/blogs/{id}", (int id) =>
{
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    else
    {
        var blog = blogs[id];
        blogs.RemoveAt(id);
        return Results.NoContent();
    }
});

app.MapPut("/blogs/{id}", (int id, Blog blog) =>
{
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    blogs[id] = blog;
    return Results.Ok(blog);
});
app.Run();

public class Blog
{
    public required string Title { get; set; }
    public required string Body { get; set; }
}