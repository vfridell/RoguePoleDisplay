using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Helpers
{
    public static class ExtensionMethods
    {
        public static T Mode<T>(this IEnumerable<T> seq)
            where T : struct
        {
            return seq.GroupBy(s => s).OrderByDescending(g => g.Count()).First().First();
        }
    }
}
