using System;
using System.Collections.Generic;
using System.Linq;

namespace Support
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitAndTrimOnAllNewlines(this string input)
        {
            return input.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries)
            .Select(c => c.Trim());
        }
    }
}
