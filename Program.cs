using LoginMiddleware.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Method == "POST" &&
        context.Request.Path == "/")
        await next(context);
});

app.UseCustomExceptionHandler();

app.UseCustomAuthorization();

app.Run();
