using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LoginMiddleware.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomAuthorization
    {
        private readonly RequestDelegate _next;
        private string correctEmail = "admin@example.com";
        private string correctPassword = "admin1234";

        public CustomAuthorization(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Query["email"] == correctEmail &&
                context.Request.Query["password"] == correctPassword)
            {
                await context.Response.WriteAsync("Successful login");   
            } else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid login");
            }
            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomAuthorizationExtensions
    {
        public static IApplicationBuilder UseCustomAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthorization>();
        }
    }
}
