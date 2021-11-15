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

            var pattern = @"\{(.*?)\}";
            var matches = Regex.Matches(logRecord, pattern);

            foreach (Match match in matches)
            {
                var value = match.Value;
                value = value.Trim('{', '}');
                value = value.Replace(" ", "");
                if (value.ToLower().StartsWith("date"))
                {
                    string dateString;
                    if (value.Split(':').Length > 1)
                    {
                        var dateFormat = match.Value.Substring(match.Value.IndexOf(':')).TrimStart(':').Trim().Trim('{', '}');

                        dateString = this.Timestamp.ToString(dateFormat);
                    }
                    else
                    {
                        dateString = this.Timestamp.ToFormattedString();
                    }

                    logRecord = logRecord.Replace(match.Value, dateString);
                }
                else if (value.ToLower() == "level"
                      || value.ToLower() == "logevel")
                {
                    logRecord = logRecord.Replace(match.Value, this.LogLevel.ToShortString());
                }
                else if (value.ToLower() == "message")
                {
                    logRecord = logRecord.Replace(match.Value, this.Message);
                }
                else if (value.ToLower() == "category"
                     || value.ToLower() == "categoryname")
                {
                    logRecord = logRecord.Replace(match.Value, this.Category);
                }
            }

            return logRecord;
        }
    }
}
