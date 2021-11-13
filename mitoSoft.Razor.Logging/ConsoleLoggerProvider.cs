using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace mitoSoft.Razor.Logging
{
    public sealed class ConsoleLoggerProvider : ILoggerProvider
    {
        public ConcurrentDictionary<string, ConsoleLogger> Loggers { get; private set; } = new();

        public ILogger CreateLogger(string categoryName)
        {
            var logger = new ConsoleLogger();
            return this.Loggers.GetOrAdd(categoryName, logger);
        }

        public void Dispose() => this.Loggers.Clear();
    }
}