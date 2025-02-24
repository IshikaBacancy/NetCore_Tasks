using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace netcore_Assignment3.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;

        public ExceptionHandling(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }

        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred. Please try again later.",
                Error = exception.Message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandling>();
        }
    }
}
