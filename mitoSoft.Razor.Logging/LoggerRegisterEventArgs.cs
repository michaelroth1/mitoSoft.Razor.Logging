using Microsoft.Extensions.Logging;
using System;

namespace mitoSoft.Razor.Logging
{
    public class LoggerRegisterEventArgs<TLogger> : EventArgs where TLogger : ILogger
    {
        public string Category { get; set; }

        public TLogger Logger { get; set; }

        public LoggerRegisterEventArgs(string category, TLogger logger)
        {
            this.Category = category;
            this.Logger = logger;
        }
    }
}