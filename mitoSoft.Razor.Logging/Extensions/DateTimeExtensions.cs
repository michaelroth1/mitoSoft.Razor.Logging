using System;
using System.Text.RegularExpressions;

namespace mitoSoft.Razor.Logging.Extensions
{
    internal static class DateTimeExtensions
    {
        public static DateTime ToSelectedKind(this DateTime date, DateTimeKind kind)
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

        public static string ToFormattedString(this DateTime date, string format, string defaultFormat)
        {
            foreach (string match in format.FindInBrackets())
            {
                var value = match.Trim('{', '}');
                value = value.Replace(" ", "");
                if (value.ToLower().StartsWith("date"))
                {
                    string dateString;
                    if (value.Split(':').Length > 1)
                    {
                        // ..          = match.Substring(match.IndexOf(':')).TrimStart(':').Trim().Trim('{', '}');
                        var dateFormat = match[match.IndexOf(':')..].TrimStart(':').Trim().Trim('{', '}');

                        dateString = date.ToString(dateFormat);
                    }
                    else
                    {
                        dateString = date.ToString(defaultFormat);
                    }

                    format = format.Replace(match, dateString);
                }
            }

            return format;
        }
    }
}