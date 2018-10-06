using Microsoft.Extensions.Configuration;
using UI.Services.Interfaces;

namespace UI.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;
        private const string PageSizeKey = "PageSize";

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int PageSize => _configuration.GetValue<int>(PageSizeKey);
    }
}
