using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace netcore_Assignment3.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestResponseLogging
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLogging> _logger;



        public RequestResponseLogging(RequestDelegate next, ILogger<RequestResponseLogging> logger)

        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation($"Incoming Request: {httpContext.Request.Method} {httpContext.Request.Path}");

            var originalBodyStream = httpContext.Response.Body;
            using var responseBody = new MemoryStream();
            httpContext.Response.Body = responseBody;

            await _next(httpContext);

            _logger.LogInformation($"Outgoing Response: {httpContext.Response.StatusCode}");

            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestResponseLoggingExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLogging>();
        }
    }
}
