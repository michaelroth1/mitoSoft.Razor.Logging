using Microsoft.Extensions.Logging;
using mitoSoft.Common.Extensions;
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

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
                                Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }

            var timestamp = DateTime.UtcNow.ToSelectedKind(this._provider.Options.DateTimeKind);

            var exceptionText = exception != null ? $" {exception.StackTrace}" : string.Empty;
            var message = $"{formatter(state, exception)}{exceptionText}";

            string text = this._provider.Options.OutputFormat;
            text = text.ReplaceFormattedDate(timestamp, "yyyyMMdd");
            text = text.ReplaceBetweenBrackets("shortloglevel", logLevel.ToShortString());
            text = text.ReplaceBetweenBrackets("shortlevel", logLevel.ToShortString());
            text = text.ReplaceBetweenBrackets("loglevel", logLevel.ToString());
            text = text.ReplaceBetweenBrackets("level", logLevel.ToString());
            text = text.ReplaceBetweenBrackets("categoryname", this.Category);
            text = text.ReplaceBetweenBrackets("category", this.Category);
            text = text.ReplaceBetweenBrackets("message", message);
            text += "\n";

#pragma warning disable CA1416 // Validate platform compatibility
            var originalColor = Console.ForegroundColor;
            var color = this._provider.Options.ColorSchema.GetColor(logLevel);

            foreach (var part in text.Split("<<"))
            {
                if (!string.IsNullOrEmpty(part) && part.IndexOf(">>") > 0)
                {
                    var colored = part.Substring(0, part.IndexOf(">>"));
                    Console.ForegroundColor = color;
                    Console.Write(colored);

                    var nonecolored = part.Substring(part.IndexOf(">>") + 2);
                    Console.ForegroundColor = originalColor;
                    Console.Write(nonecolored);
                }
                else
                {
                    Console.Write(part);
                }
            }

            Console.ForegroundColor = originalColor;
#pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}