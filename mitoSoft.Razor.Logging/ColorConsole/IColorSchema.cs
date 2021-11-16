using Microsoft.Extensions.Logging;
using System;

namespace mitoSoft.Razor.Logging.ColorConsole
{
    public interface IColorSchema
    {
        ConsoleColor GetColor(LogLevel logLevel);
    }
}