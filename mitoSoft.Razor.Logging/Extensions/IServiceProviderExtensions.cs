using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace mitoSoft.Razor.Logging.Extensions
{
    public static class IServiceProviderExtensions
    {
        public static ConsoleLogger GetConsoleLogger(this IServiceProvider provider, string context)
        {
            var loggerProvider = (ConsoleLoggerProvider)provider.GetRequiredService<ILoggerProvider>();
            return (ConsoleLogger)loggerProvider.Loggers.First(l => l.Key == context).Value;
        }
    }
}