using Microsoft.Extensions.Logging;

namespace mitoSoft.Razor.Logging.Extensions
{
    public static class ILoggingBuilderExtensions
    {
        public static ILoggingBuilder AddConsoleLogger(this ILoggingBuilder builder)
        {
            builder.AddProvider(new ConsoleLoggerProvider());
            return builder;
        }
    }
}