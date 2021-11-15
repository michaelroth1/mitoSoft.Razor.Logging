using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace mitoSoft.Razor.Logging.Extensions
{
    internal static class StringExtensions
    {
        public static List<string> FindInBrackets(this string value)
        {
            var result = new List<string>();
            var pattern = @"\{(.*?)\}";
            var matches = Regex.Matches(value, pattern);

            foreach (Match match in matches)
            {
                result.Add(match.Value);
            }

            return result;
        }
    }
}