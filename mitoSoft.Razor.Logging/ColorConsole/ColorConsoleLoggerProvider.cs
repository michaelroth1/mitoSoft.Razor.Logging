using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace mitoSoft.Razor.Logging.ColorConsole
{
    [ProviderAlias("ColorConsole")]
    public sealed class ColorConsoleLoggerProvider : ILoggerProvider
    {
        public readonly ColorConsoleLoggerOptions Options;

        public ColorConsoleLoggerProvider() : this(new ColorConsoleLoggerOptions())
        {
        }

        public ColorConsoleLoggerProvider(IOptions<ColorConsoleLoggerOptions> options) : this(options.Value)
        {
        }

        public ColorConsoleLoggerProvider(ColorConsoleLoggerOptions options)
        {
            this.Options = options;
        }

        public ILogger CreateLogger(string category)
        {
            var logger = new ColorConsoleLogger(this, category);
            this.Options.RegisterCallback?.Invoke(this, new LoggerRegisterEventArgs<ColorConsoleLogger>(category, logger));
            return logger;
        }

        public void Dispose()
        {
        }
    }
}