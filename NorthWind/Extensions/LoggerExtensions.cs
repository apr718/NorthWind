using Microsoft.Extensions.Logging;
using NorthWind.Services;

namespace NorthWind.Extensions
{
    public static class LoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));
            return factory;
        }
    }
}
