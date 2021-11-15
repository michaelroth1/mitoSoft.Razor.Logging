namespace mitoSoft.Razor.Logging.ColorConsole
{
    public class ColorConsoleLoggerOptions : LoggerOptions<ColorConsoleLogger>
    {
        public virtual string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss fff";
    }
}