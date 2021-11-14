using System;

namespace mitoSoft.Razor.Logging.Event
{
    public class EventLoggerEventArgs : EventArgs
    {
        public LogLine LogLine { get; }

        public EventLoggerEventArgs(LogLine logLine)
        {
            this.LogLine = logLine;
        }
    }
}