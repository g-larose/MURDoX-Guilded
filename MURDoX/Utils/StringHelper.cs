using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MURDoX.Utils
{
    public static class StringHelper
    {
        public static string Sanitize(this string input)
        {
            string pattern = "<.*?>";
            string result = Regex.Replace(input, pattern, string.Empty);
            return result;
        }
    }
}
