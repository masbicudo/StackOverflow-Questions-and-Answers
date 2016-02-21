using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication.Sopt_2014_Abr_28_RegexReplace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var result = Regex.Replace("dia dos pais", @"\b(\w)(\w*)\b", TitleCase);
        }

        private static readonly Dictionary<string, string> special =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"de", "de"},
                {"da", "da"},
                {"do", "do"},
                {"das", "das"},
                {"dos", "dos"},
            };

        private static string TitleCase(Match m)
        {
            string replacement;
            if (special.TryGetValue(m.Value, out replacement))
                return replacement;

            return m.Groups[1].Value.ToUpper() + m.Groups[2].Value.ToLower();
        }
    }
}