namespace mitoSoft.Razor.Logging.ColorConsole
{
    public class ColorConsoleLoggerOptions : LoggerOptions<ColorConsoleLogger>
    {
        public virtual string OutputFormat { get; set; } = "<<{date:yyyy-MM-dd HH:mm:ss fff}\t[{shortlevel}]>>\n\t{message}";

        public virtual IColorSchema ColorSchema { get; set; } = new StandardColorSchema();
    }
}