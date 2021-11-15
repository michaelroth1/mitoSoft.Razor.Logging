using Microsoft.Extensions.Logging;
using mitoSoft.Razor.Logging.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

//https://docs.microsoft.com/de-de/dotnet/core/extensions/custom-logging-provider
//https://andrewlock.net/creating-a-rolling-file-logging-provider-for-asp-net-core-2-0/
namespace mitoSoft.Razor.Logging.ColorConsole
{
    public class ColorConsoleLogger : ILogger
    {
        protected readonly ColorConsoleLoggerProvider _provider;

        public string Category { get; private set; }

        public ColorConsoleLogger([NotNull] ColorConsoleLoggerProvider provider, string category)
        {
            this._provider = provider;
            this.Category = category;
        }

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

            var timestamp = DateTime.UtcNow.ToSelectedKind(this._provider.Options.DateTimeKind).ToDefault();

#pragma warning disable CA1416 // Validate platform compatibility
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ColorConsoleLogger.GetColor(logLevel);
            Console.WriteLine($"{timestamp}: [{logLevel.ToShortString()}]");
            Console.ForegroundColor = originalColor;
#pragma warning restore CA1416 // Validate platform compatibility

            Console.WriteLine($"{formatter(state, exception)}");

            if (!string.IsNullOrEmpty(exception?.Message))
            {
                Console.WriteLine($"Message: {exception.Message}");
            }

            if (exception != null)
            {
                Console.WriteLine($"Type: {exception.GetType()}");
            }

            if (exception?.StackTrace != null)
            {
                Console.WriteLine($"StackTrace: {exception.StackTrace}");
            }
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