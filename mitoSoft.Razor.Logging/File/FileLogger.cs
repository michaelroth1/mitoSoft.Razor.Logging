using Microsoft.Extensions.Logging;
using mitoSoft.Common.Extensions;
using mitoSoft.Razor.Logging.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace mitoSoft.Razor.Logging.File
{
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

            var timestamp = DateTime.UtcNow.ToSelectedKind(this._provider.Options.DateTimeKind);

            var exceptionText = exception != null ? $" {exception.StackTrace}" : string.Empty;
            var message = $"{formatter(state, exception)}{exceptionText}";

            string path = this._provider.Options.Path;
            path = path.ReplaceFormattedDate(timestamp, "yyyyMMdd");
            path = path.ReplaceBetweenBrackets("loglevel", logLevel.ToShortString());
            path = path.ReplaceBetweenBrackets("level", logLevel.ToShortString());
            path = path.ReplaceBetweenBrackets("categoryname", this.Category);
            path = path.ReplaceBetweenBrackets("category", this.Category);
            path = path.ReplaceBetweenBrackets("message", message);

            var dir = new FileInfo(path).DirectoryName;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var file = new FileInfo(path).Name;

            var fullPath = Path.Combine(dir, file);

            var line = new LogLine(timestamp, logLevel, message, this.Category);

            lock (_lock)
            {
                System.IO.File.AppendAllLines(fullPath, new List<string>() { line.ToString(this._provider.Options.Format) });
            }
        }
    }
}