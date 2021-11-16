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
    }
}