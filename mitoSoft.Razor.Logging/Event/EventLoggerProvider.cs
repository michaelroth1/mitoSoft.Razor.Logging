using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace mitoSoft.Razor.Logging.Event
{
    [ProviderAlias("Event")]
    public class EventLoggerProvider : ILoggerProvider
    {
        public readonly EventLoggerOptions Options;

        public EventLoggerProvider() : this(new EventLoggerOptions())
        {
        }

        public EventLoggerProvider(IOptions<EventLoggerOptions> options) : this(options.Value)
        {
        }

        public EventLoggerProvider(EventLoggerOptions options)
        {
            this.Options = options;
        }

        public ILogger CreateLogger(string category)
        {
            var logger = new EventLogger(this, category);
            this.Options.RegisterCallback?.Invoke(this, new LoggerRegisterEventArgs<EventLogger>(category, logger));
            return logger;
        }

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
        }
    }
}