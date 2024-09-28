using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;

namespace Utilities.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TimeSpan _timeWindow = TimeSpan.FromMinutes(1);
        private readonly int _maxRequests = 100;
        private readonly ConcurrentDictionary<string, (DateTime startTime, int requestCount)> _clients = new();

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString();
            if (clientIp == null)
            {
                await _next(context);
                return;
            }

            var currentTime = DateTime.UtcNow;

            var clientInfo = _clients.GetOrAdd(clientIp, _ => (currentTime, 0));
            if (clientInfo.startTime + _timeWindow < currentTime)
            {
                clientInfo = (currentTime, 0);
                _clients[clientIp] = clientInfo;
            }

            if (clientInfo.requestCount >= _maxRequests)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded.");
                return;
            }

            _clients[clientIp] = (clientInfo.startTime, clientInfo.requestCount + 1);
            await _next(context);
        }
    }
}