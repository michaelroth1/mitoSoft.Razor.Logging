using Microsoft.Extensions.Logging;
using mitoSoft.Razor.Logging.Extensions;
using System;
using System.Text.RegularExpressions;

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
            var logRecord = format;

            logRecord = this.Timestamp.ToFormattedString(logRecord, "yyyy-MM-dd HH:mm:ss fff");

            foreach (string match in format.FindInBrackets())
            {
                var value = match.Trim('{', '}');
                value = value.Replace(" ", "");
                                
                if (value.ToLower() == "level"
                      || value.ToLower() == "loglevel")
                {
                    logRecord = logRecord.Replace(match, this.LogLevel.ToShortString());
                }
                else if (value.ToLower() == "message")
                {
                    logRecord = logRecord.Replace(match, this.Message);
                }
                else if (value.ToLower() == "category"
                     || value.ToLower() == "categoryname")
                {
                    logRecord = logRecord.Replace(match, this.Category);
                }
            }

            return logRecord;
        }
    }
}
