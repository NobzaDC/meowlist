using System.Net;
using Newtonsoft.Json;
using MeowCore.Helpers;

namespace MeowCore.Middlewares
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorLoggingMiddleware> _logger;
        private readonly LogHelper<object, object> _logHelper;

        public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _logHelper = new LogHelper<object, object>();
            _logHelper.CreateLogger(_logger);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var endpoint = context.GetEndpoint()?.DisplayName ?? "Unknown";
                var controller = endpoint?.Split('.')[0] ?? "Unknown";
                var statusCode = (int)HttpStatusCode.InternalServerError;

                var log = _logHelper.GetInitialLog(
                    controller,
                    endpoint,
                    new List<Dictionary<string, string>>(),
                    null
                );

                _logHelper.LogError(
                    log,
                    statusCode,
                    ex.Message,
                    ex.InnerException?.Message ?? "",
                    ex.StackTrace ?? "",
                    controller,
                    endpoint
                );

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                var errorResponse = new
                {
                    error = "An unexpected error occurred.",
                    details = ex.Message
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
    }
}