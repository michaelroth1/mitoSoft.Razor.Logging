﻿using Microsoft.Extensions.Logging;
using mitoSoft.Common.Extensions;
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
            var s = format;
            s = s.ReplaceFormattedDate(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc));
            s = s.ReplaceBetweenBrackets("shortloglevel", this.LogLevel.ToShortString());
            s = s.ReplaceBetweenBrackets("shortlevel", this.LogLevel.ToShortString());
            s = s.ReplaceBetweenBrackets("loglevel", this.LogLevel.ToString());
            s = s.ReplaceBetweenBrackets("level", this.LogLevel.ToString());
            s = s.ReplaceBetweenBrackets("categoryname", this.Category);
            s = s.ReplaceBetweenBrackets("category", this.Category);
            s = s.ReplaceBetweenBrackets("message", this.Message);
            return s;
        }
    }
}
