using InventorySales.Domain.Exceptions;
using InventorySales.Infrastructure.Exceptions;

namespace InventorySales.API.Middlewares.GlobalException
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
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
            catch (ConcurrencyException ex)
            {
                _logger.LogWarning(ex, "Concurrency conflict");

                context.Response.StatusCode = StatusCodes.Status409Conflict;
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Concurrency Conflict",
                    Detail = ex.Message,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                    Instance = context.Request.Path
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(ex, "Domain validation error");

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = ex.Message, 
                    Instance = context.Request.Path, 
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (ValidationException ex)
            {
                var problemDetails = new ValidationProblemDetails(ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray()))
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Error"
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Internal server error",
                    detail = isDevelopment ? ex.Message : null
                });
            }
        }
    }
}
