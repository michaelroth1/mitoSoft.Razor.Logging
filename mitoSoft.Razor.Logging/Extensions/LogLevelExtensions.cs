using Microsoft.Extensions.Logging;
using System;

namespace mitoSoft.Razor.Logging.Extensions
{
    internal static class LogLevelExtensions
    {
        public static string ToShortString(this LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => "trce",
                LogLevel.Debug => "dbug",
                LogLevel.Information => "info",
                LogLevel.Warning => "warn",
                LogLevel.Error => "fail",
                LogLevel.Critical => "crit",
                LogLevel.None => "none",
                _ => throw new InvalidOperationException("invalid logLevel"),
            };
        }
    }
}