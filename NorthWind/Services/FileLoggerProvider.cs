using Microsoft.Extensions.Logging;

namespace NorthWind.Services
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _path;
        public FileLoggerProvider(string path)
        {
            this._path = path;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogService(_path);
        }

        public void Dispose()
        {
        }
    }
}
