using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace netcore__Assignment_4.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserRestrictionmiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserRestrictionmiddleware> _logger;

        public UserRestrictionmiddleware(RequestDelegate next, ILogger<UserRestrictionmiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext.Request.Headers.TryGetValue("username", out var username))
            {
                if(username.ToString().Contains("bacancy", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogWarning("Access denied for user: bacancy");
                    httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await httpContext.Response.WriteAsync("Access denied");
                    return;
                }
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UserRestrictionmiddlewareExtensions
    {
        public static IApplicationBuilder UseUserRestrictionmiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserRestrictionmiddleware>();
        }
    }
}
