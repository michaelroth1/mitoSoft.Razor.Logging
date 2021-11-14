using Microsoft.Extensions.Logging;
using mitoSoft.Razor.Logging.Extensions;
using System;

namespace mitoSoft.Razor.Logging
{
    public class LogLine
    {
        public LogLevel LogLevel { get; }

        public string Message { get; }

        public string Category { get; }

        public DateTime Timestamp { get; }

        public LogLine(DateTime timestamp, LogLevel logLevel, string message, string category)
        {
            this.Timestamp = timestamp;
            this.LogLevel = logLevel;
            this.Message = message;
            this.Category = category;
        }

        public string ToString(string format)
        {
            var timestamp = this.Timestamp.ToFormattedString();

            var logRecord = format;
            logRecord = logRecord.Replace("{date}", timestamp);
            logRecord = logRecord.Replace("{level}", this.LogLevel.ToShortString());
            logRecord = logRecord.Replace("{category}", this.Category);
            logRecord = logRecord.Replace("{message}", this.Message);

            return logRecord;
        }
    }
}
