using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bodegas
{
    internal static class StringExtensions
    {
        private static Regex regex = new Regex("([A-Z])");

        internal static string SplitByUpperCase(this string text)
        {
            var name = new StringBuilder();
            var par = false;

            foreach (var word in regex.Split(text).Where(x => x.Length > 0))
            {
                name.Append(word);
                if (par)
                {
                    name.Append(" ");
                }
                par = !par;
            }

            return name.ToString().Trim();
        }
    }
}
