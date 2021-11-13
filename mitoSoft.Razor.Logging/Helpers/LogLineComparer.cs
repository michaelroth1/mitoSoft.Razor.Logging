using System;
using System.Collections.Generic;

namespace mitoSoft.Razor.Logging.Helpers
{
    public class LogLineComparer : IComparer<LogLine>
    {
        public int Compare(LogLine x, LogLine y)
        {
            return DateTime.Compare(x.Timestamp, y.Timestamp);
        }
    }
}