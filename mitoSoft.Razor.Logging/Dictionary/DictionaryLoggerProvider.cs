using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace mitoSoft.Razor.Logging.Dictionary
{
    [ProviderAlias("Dictionary")]
    public class DictionaryLoggerProvider : ILoggerProvider
    {
        public readonly DictionaryLoggerOptions Options;

        public DictionaryLoggerProvider() : this(new DictionaryLoggerOptions())
        {
        }

        public DictionaryLoggerProvider(IOptions<DictionaryLoggerOptions> options) : this(options.Value)
        {
        }

        public DictionaryLoggerProvider(DictionaryLoggerOptions options)
        {
            this.Options = options;
        }

        public ILogger CreateLogger(string category)
        {
            var logger = new DictionaryLogger(this, category);
            this.Options.RegisterCallback?.Invoke(this, new LoggerRegisterEventArgs<DictionaryLogger>(category, logger));
            return logger;
        }

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
        }
    }
}