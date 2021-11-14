using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using mitoSoft.Razor.Logging.ColorConsole;
using mitoSoft.Razor.Logging.Dictionary;
using mitoSoft.Razor.Logging.Event;
using mitoSoft.Razor.Logging.File;
using System;

namespace mitoSoft.Razor.Logging.Extensions
{
    public static class ILoggingBuilderExtensions
    {
        public static ILoggingBuilder AddColorConsole(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, ColorConsoleLoggerProvider>();
            return builder;
        }

        public static ILoggingBuilder AddColorConsole(this ILoggingBuilder builder, Action<ColorConsoleLoggerOptions> configure)
        {
            builder.AddColorConsole();
            builder.Services.Configure(configure);
            return builder;
        }

        public static ILoggingBuilder AddFile(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
            return builder;
        }

        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, Action<FileLoggerOptions> configure)
        {
            builder.AddFile();
            builder.Services.Configure(configure);
            return builder;
        }

        public static ILoggingBuilder AddEvent(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, EventLoggerProvider>();
            return builder;
        }

        public static ILoggingBuilder AddEvent(this ILoggingBuilder builder, Action<EventLoggerOptions> configure)
        {
            builder.AddEvent();
            builder.Services.Configure(configure);
            return builder;
        }

        public static ILoggingBuilder AddDictionary(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, DictionaryLoggerProvider>();
            return builder;
        }

        public static ILoggingBuilder AddDictionary(this ILoggingBuilder builder, Action<DictionaryLoggerOptions> configure)
        {
            builder.AddDictionary();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}