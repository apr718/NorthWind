using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace NorthWind.Filters
{
    public class LogActionFilterAttribute : TypeFilterAttribute
    {
        public LogActionFilterAttribute() : base(typeof(LogActionFilterImpl))
        {
        }

        private class LogActionFilterImpl : IActionFilter
        {
            private readonly ILogger _logger;
            public LogActionFilterImpl(ILoggerFactory loggerFactory)
            {
                _logger = loggerFactory.CreateLogger<LogActionFilterAttribute>();
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                _logger.LogInformation("Business action starting...");
                // perform some business logic work

            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                // perform some business logic work
                _logger.LogInformation("Business action completed.");
            }
        }
    }
}
