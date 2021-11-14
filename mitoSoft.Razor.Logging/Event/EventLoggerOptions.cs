using System;

namespace mitoSoft.Razor.Logging.Event
{
    public class EventLoggerOptions : LoggerOptions<EventLogger>
    {
        public Action<object, EventLoggerEventArgs> LogCallback { get; set; }
    }
}