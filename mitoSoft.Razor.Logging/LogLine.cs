using System;

namespace mitoSoft.Razor.Logging
{
    public class LogLine
    {
        public DateTime Timestamp { get; }

        public string Text { get; }

        public LogLine(DateTime timestamp, string text)
        {
            this.Timestamp = timestamp;
            this.Text = text;
        }

        public string GetFormatted()
        {
            return $"{Timestamp:yyyyMMdd HH:mm}\t{Text}";
        }
    }
}