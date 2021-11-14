using Microsoft.Extensions.Logging;
using mitoSoft.Razor.Logging.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace mitoSoft.Razor.Logging.Event
{
    public class EventLogger : ILogger
    {
        protected readonly EventLoggerProvider _provider;

        public string Category { get; private set; }

        public EventLogger([NotNull] EventLoggerProvider provider, string category)
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

            var callback = this._provider.Options.LogCallback;

            var exceptionText = exception != null ? $" {exception.StackTrace}" : string.Empty;
            var message = $"{formatter(state, exception)}{exceptionText}";

            callback?.Invoke(this, new EventLoggerEventArgs(new LogLine(
                DateTime.UtcNow.ToSelectedKind(this._provider.Options.DateTimeKind),
                logLevel,
                message,
                this.Category)));
        }
    }
}