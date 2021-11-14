using System;

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

        public static string ToFormattedString(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Utc)
            {
                return date.ToString("yyyy-MM-dd HH:mm:ss fffZ");
            }
            else if (date.Kind == DateTimeKind.Local)
            {
                return date.ToString("yyyy-MM-dd HH:mm:ss fff");
            }
            else
            {
                throw new InvalidOperationException($"invalid DateTimeKind '{date.Kind}'.");
            }
        }
    }
}