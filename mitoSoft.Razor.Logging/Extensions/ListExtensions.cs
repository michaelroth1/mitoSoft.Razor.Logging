using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mitoSoft.Razor.Logging.Extensions
{
   public static class ListExtensions
    {
        public static IList<T> GetLast<T>(this IList<T> items, int amount)
        {
            var values = items.ToList();
            values.Reverse();

            List<T> result = new();
            for (int i = 0; i < values.Count; i++)
            {
                result.Add(values[i]);
                if (i > amount)
                {
                    break;
                }
            }
            result.Reverse();
            return result;
        }
    }
}
