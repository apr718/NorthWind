namespace NorthWind.Services.Interfaces
{
    public interface IConfigurationService
    {
        int PageSize { get; }
        bool LoggingEnabled { get; }
    }
}
