using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LoginMiddleware.Middlewares
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Query.ContainsKey("email") &&
                context.Request.Query.ContainsKey("password"))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 400;
                if (!context.Request.Query.ContainsKey("email"))
                {
                    await context.Response.WriteAsync("Invalid input for 'email'\n");
                }
                if (!context.Request.Query.ContainsKey("password"))
                {
                    await context.Response.WriteAsync("Invalid input for 'password'");
                }
            }
        }
    }

    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    }
}
