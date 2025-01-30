using System.Net;
using System.Text.Json;

namespace GamesAPI.Middlewares
{
    public class ExeptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionHandlingMiddleware> _logger;

        public ExeptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExeptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var statusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            response.StatusCode = statusCode;


            var errorResponse = new
            {
                StatusCode = statusCode,
                Message = exception.Message,
                Details = exception.StackTrace
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
