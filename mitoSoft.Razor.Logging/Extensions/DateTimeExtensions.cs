using System;

namespace mitoSoft.Razor.Logging.Extensions
{
    internal static class DateTimeExtensions
    {
        internal static DateTime ToSelectedKind(this DateTime date, DateTimeKind kind)
        {
            if (date.Kind == kind)
            {
                return date;
            }
            else if (date.Kind == DateTimeKind.Local && kind == DateTimeKind.Utc)
            {
                return date.ToUniversalTime();
            }
            else if (date.Kind == DateTimeKind.Utc && kind == DateTimeKind.Local)
            {
                return date.ToLocalTime();
            }
            else
            {
                throw new InvalidOperationException($"invalid DateTimeKind '{date.Kind}'.");
            }
        }

        public static string ToFormatted(this DateTime date, string text, string defaultFormat = "yyyy-MM-dd HH:mm:ss fff")
        {
            text = text.Replace("{date}", date.ToString(defaultFormat));  //default format 

            text = ToCustom(date, text);  //individual format

            return text;
        }

        private static string ToCustom(DateTime date, string text)
        {
            foreach (string match in text.FindBetween("{date:", "}"))
            {
                var format = match.Replace("{date:", "").TrimEnd('}');
                string dateString = date.ToString(format);
                text = text.Replace(match, dateString);
            }

            return text;
        }
    }
}