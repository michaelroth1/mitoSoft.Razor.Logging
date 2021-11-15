using Microsoft.Extensions.Logging;
using System;

namespace mitoSoft.Razor.Logging
{
    public class LoggerOptions<TLogger> where TLogger : ILogger
    {
        public virtual DateTimeKind DateTimeKind { get; set; } = DateTimeKind.Utc;

        public virtual Action<object, LoggerRegisterEventArgs<TLogger>> RegisterCallback { get; set; }
    }
}