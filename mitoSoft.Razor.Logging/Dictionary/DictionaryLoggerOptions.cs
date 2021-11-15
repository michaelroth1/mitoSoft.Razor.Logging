namespace mitoSoft.Razor.Logging.Dictionary
{
    public class DictionaryLoggerOptions : LoggerOptions<DictionaryLogger>
    {
        public virtual int MaxRows { get; set; } = 100;
    }
}