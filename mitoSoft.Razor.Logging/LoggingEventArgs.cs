using System;
using System.Collections.Generic;

namespace mitoSoft.Razor.Logging
{
    public class LoggingEventArgs : EventArgs
    {
        public List<LogLine> AllLines { get; }

        public LogLine SingleLine { get; }

        public LoggingEventArgs(List<LogLine> allLines, LogLine singleLine)
        {
            this.AllLines = allLines;
            this.SingleLine = singleLine;
        }
    }
}