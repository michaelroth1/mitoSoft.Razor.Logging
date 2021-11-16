using Microsoft.Extensions.Logging;
using System;

namespace mitoSoft.Razor.Logging.ColorConsole
{
    public class StandardColorSchema : IColorSchema
    {
        public ConsoleColor GetColor(LogLevel logLevel)
        {
            if (logLevel >= LogLevel.Error)
            {
                return ConsoleColor.DarkRed;
            }
            else if (logLevel >= LogLevel.Warning)
            {
                return ConsoleColor.DarkMagenta;
            }
            else
            {
                return ConsoleColor.Green;
            }
        }
    }
}