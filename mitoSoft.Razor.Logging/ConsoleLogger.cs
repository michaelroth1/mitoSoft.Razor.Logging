using Microsoft.Extensions.Logging;
using mitoSoft.Razor.Logging.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

//https://docs.microsoft.com/de-de/dotnet/core/extensions/custom-logging-provider
//https://andrewlock.net/creating-a-rolling-file-logging-provider-for-asp-net-core-2-0/
namespace mitoSoft.Razor.Logging
{
    public class ConsoleLogger : ILogger
    {
        public List<LogLine> Lines { get; private set; } = new();

        public event EventHandler<LoggingEventArgs> Logged;

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }

            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleLogger.GetColor(logLevel);
            Console.WriteLine($"{DateTime.Now:g}: {logLevel,-12}");
            Console.ForegroundColor = originalColor;

            this.LogLine($"{formatter(state, exception)}");

            if (!string.IsNullOrEmpty(exception?.Message))
            {
                this.LogLine($"Message: {exception.Message}");
            }

            if (exception != null)
            {
                this.LogLine($"Type: {exception.GetType()}");
            }

            if (exception?.StackTrace != null)
            {
                this.LogLine($"StackTrace: {exception.StackTrace}");
            }
        }

        private void LogLine(string text)
        {
            Console.WriteLine(text);

            var line = new LogLine(DateTime.Now, text);
            this.Lines.Add(line);
            this.Lines = this.Lines.GetLast(100).ToList();
            Logged?.Invoke(this, new LoggingEventArgs(this.Lines, line));
        }

        private static ConsoleColor GetColor(LogLevel logLevel)
        {
            if (logLevel >= LogLevel.Error)
            {
                return ConsoleColor.DarkRed;
            }
            else if (logLevel >= LogLevel.Warning)
            {
                return ConsoleColor.DarkMagenta;
            }
            else
            {
                return ConsoleColor.Green;
            }
        }
    }
}