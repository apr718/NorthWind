using Microsoft.Extensions.Configuration;
using NorthWind.Services.Interfaces;

namespace NorthWind.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;
        private const string PageSizeKey = "PageSize";
        private const string LoggingEnabledKey = "LoggingEnabled";

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int PageSize
        {
            get
            {
                var value = _configuration.GetValue<int>(PageSizeKey);
                // _logger.LogInformation("The PageSize configuration was read. PageSize= {0}", value);
                return value;
            }
        }

        public bool LoggingEnabled
        {
            get
            {
                var value = _configuration.GetValue<bool>(LoggingEnabledKey);
                return value;
            }
        }


    }
}
