using Microsoft.AspNetCore.Http;

namespace Utilities.Middleware
{
    public class CustomCorsMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomCorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE,OPTIONS");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");

            if (context.Request.Method == "OPTIONS")
            {
                context.Response.StatusCode = StatusCodes.Status204NoContent;
                return;
            }

            await _next(context);
        }
    }
}
