namespace mitoSoft.Razor.Logging.File
{
    public class FileLoggerOptions : LoggerOptions<FileLogger>
    {
        public virtual string Path { get; set; } = "{date}_log.txt";

        public virtual string Format { get; set; } = "[{date}]\t[{level}] [{category}] {message}";
    }
}