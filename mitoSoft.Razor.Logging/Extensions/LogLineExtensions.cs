using mitoSoft.Razor.Logging.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace mitoSoft.Razor.Logging.Extensions
{
    internal static class LogLineExtensions
    {
        public static LogLine GetFirstTimestamp(this List<LogLine> lines)
        {
            var temp = lines.ToList();
            temp.Sort(new LogLineComparer());
            return temp.First();
        }
    }
}