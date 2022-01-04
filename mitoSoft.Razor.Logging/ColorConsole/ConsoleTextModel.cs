using System;

namespace mitoSoft.Razor.Logging.ColorConsole
{
    internal enum ConsoleWriteType
    {
        Write,
        WriteLine,
        WriteLineWithColor,
        WriteWithColor,
    }

    internal class ConsoleTextModel
    {
        public ConsoleWriteType Type { get; set; }

        public ConsoleColor Color { get; set; }

        public string Text { get; set; }

        public ConsoleTextModel(ConsoleWriteType type, string text) : this(type, ConsoleColor.White, text)
        {
        }

        public ConsoleTextModel(ConsoleWriteType type, ConsoleColor color, string text)
        {
            this.Type = type;
            this.Color = color;
            this.Text = text;
        }
    }
}