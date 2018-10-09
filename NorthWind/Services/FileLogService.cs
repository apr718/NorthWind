﻿using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace NorthWind.Services
{
    public class FileLogService : ILogger
    {
        private readonly string _filePath;
        private readonly object _lock = new object();

        public FileLogService(string path)
        {
            _filePath = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                 //   File.AppendAllText(_filePath, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}
