namespace NorthWind.Api.Service.Interfaces
{
    public interface IConfigurationService
    {
        int PageSize { get; }
        bool LoggingEnabled { get; }
    }
}
