using Microsoft.Extensions.Logging;
using mitoSoft.Razor.Logging.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace mitoSoft.Razor.Logging.Dictionary
{
    public class DictionaryLogger : ILogger
    {
        protected readonly DictionaryLoggerProvider _provider;

        public List<LogLine> Lines = new();

        public string Category { get; private set; }

        public DictionaryLogger([NotNull] DictionaryLoggerProvider provider, string category)
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

            var exceptionText = exception != null ? $" {exception.StackTrace}" : string.Empty;
            var message = $"{formatter(state, exception)}{exceptionText}";

            var line = new LogLine(
                DateTime.UtcNow.ToSelectedKind(this._provider.Options.DateTimeKind),
                logLevel,
                message,
                this.Category);

            this.Lines.Add(line);
        }
    }
}