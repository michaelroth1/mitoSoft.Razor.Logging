using Microsoft.Extensions.Logging;
using mitoSoft.Razor.Logging.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace mitoSoft.Razor.Logging.File
{
    /*
    Usage in appsettings.json
     "Logging": {
       ...,
       "File": {
         "LogLevel": {
           "Default": "Information"
         }
       },
       ...
    */
    public class FileLogger : ILogger
    {
        private static readonly object _lock = new();

        protected readonly FileLoggerProvider _provider;

        public string Category { get; private set; }

        public FileLogger([NotNull] FileLoggerProvider provider, string category)
        {
            this._provider = provider;
            this.Category = category;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }

            var dir = new System.IO.FileInfo(this._provider.Options.Path).DirectoryName;
            var file = new System.IO.FileInfo(this._provider.Options.Path).Name;
            var fullPath = System.IO.Path.Combine(dir, file.Replace("{date}", DateTimeOffset.UtcNow.ToString("yyyyMMdd")));

            var timestamp = DateTime.UtcNow.ToSelectedKind(this._provider.Options.DateTimeKind);

            var exceptionText = exception != null ? $" {exception.StackTrace}" : string.Empty;
            var message = $"{formatter(state, exception)}{exceptionText}";

            var line = new LogLine(timestamp, logLevel, message, this.Category);

            lock (_lock)
            {
                System.IO.File.AppendAllLines(fullPath, new List<string>() { line.ToString(this._provider.Options.Format) });
            }
        }
    }
}