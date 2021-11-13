using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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