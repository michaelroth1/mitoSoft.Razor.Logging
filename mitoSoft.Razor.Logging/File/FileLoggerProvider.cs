using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace mitoSoft.Razor.Logging.File
{
    // RoundTheCodeFileLoggerProvider.cs
    [ProviderAlias("File")]
    public class FileLoggerProvider : ILoggerProvider
    {
        public readonly FileLoggerOptions Options;

        public FileLoggerProvider() : this(new FileLoggerOptions())
        {
        }

        public FileLoggerProvider(IOptions<FileLoggerOptions> options) : this(options.Value)
        {
        }

        public FileLoggerProvider(FileLoggerOptions options)
        {
            this.Options = options;
        }

        public ILogger CreateLogger(string category)
        {
            var logger = new FileLogger(this, category);
            this.Options.RegisterCallback?.Invoke(this, new LoggerRegisterEventArgs<FileLogger>(category, logger));
            return logger;
        }

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
        }
    }
}