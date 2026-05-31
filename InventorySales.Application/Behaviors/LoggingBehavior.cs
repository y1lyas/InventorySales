using InventorySales.Application.Abstractions.Services;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IUserService _userService;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var requestOwner = _userService.UserId ?? "Unknown User";
            var scopeProps = new Dictionary<string, object>
            {
             { "RequestOwner", requestOwner },
             { "RequestName", requestName }
            };
            using (_logger.BeginScope(scopeProps))
            {
                _logger.LogInformation(
                    "Handling {RequestName} Requested By {RequestOwner}: {@Request}",
                    requestName,
                    requestOwner,
                    request);

                var response = await next();

                _logger.LogInformation(
                    "Handled {RequestName} Requested By {RequestOwner}",
                    requestName,
                    requestOwner);

                return response;
            }
        }
    }
}
