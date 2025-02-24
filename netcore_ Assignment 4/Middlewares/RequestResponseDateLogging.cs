using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using netcore__Assignment_4.Services;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
namespace netcore__Assignment_4.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestResponseDateLogging
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseDateLogging> _logger;
        private readonly ITimeService _timeService;

        public RequestResponseDateLogging(RequestDelegate next, ILogger<RequestResponseDateLogging> logger, ITimeService timeservice)
        {

            _next = next;
            _logger = logger;
            _timeService = timeservice;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var request = httpContext.Request;
            var requestTime = _timeService.GetCurrentTime();
            _logger.LogInformation($"[Request] {request.Method} {request.Path} at {requestTime}");

            var originalBodyStream = httpContext.Response.Body;
            using var responseBody = new MemoryStream();
            httpContext.Response.Body = responseBody;

            await _next(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseBodyText = await new StreamReader(httpContext.Response.Body, Encoding.UTF8).ReadToEndAsync();
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

            var responseTime = _timeService.GetCurrentTime();
            _logger.LogInformation($"[Response] Status Code: {httpContext.Response.StatusCode} at {responseTime}");

            // Copy back the response body
            await responseBody.CopyToAsync(originalBodyStream);




        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestResponseDateLoggingExtensions
    {
        public static IApplicationBuilder UseRequestResponseDateLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseDateLogging>();
        }
    }
}
