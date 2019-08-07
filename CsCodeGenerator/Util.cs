using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeGenerator
{
    public static class Util
    {
        #region Static Fields

        public static string Using = "using";
        public static string Namespace = "namespace";
        public static string Class = "class";
        public static string Enum = "enum";
        public static string Interface = "interface";
        public static string Base = "base";
        public static string CsExtension = "cs";
        public static string TxtExtension = "txt";
        public static string NewLine = System.Environment.NewLine;
        public static string NewLineDouble = NewLine + NewLine;

        #endregion Static Fields

        #region Extensions

        public static string ToCamelCase(this string inputString)
        {
            inputString = inputString.ToTitleCase();
            return char.ToLowerInvariant(inputString[0]).ToString() + inputString.Substring(1);
        }

        public static string ToTitleCase(this string inputString)
        {
            var words = new List<string>();

            if (inputString.Contains(" "))
            {
                words = inputString?.Split(' ').ToList();
            }
            else
            {
                words = inputString.SplitOnCase().ToList();
            }

            var fieldName = new StringBuilder();

            bool firstWordSet = false;

            if (null != words)
            {
                foreach (var word in words)
                {
                    if (word == words.FirstOrDefault())
                    {
                        if (!firstWordSet && !string.IsNullOrWhiteSpace(word))
                        {
                            fieldName.Append(char.ToUpperInvariant(word.FirstOrDefault()) + word.Substring(1));
                            firstWordSet = true;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(word))
                        {
                            fieldName.Append
                                (word.FirstOrDefault().ToString().ToUpperInvariant()?.Replace("_", string.Empty) +
                                word?.Substring(1)?.Replace("_", string.Empty));
                        }
                    }
                }
            }

            return fieldName.ToString();
        }

        public static string[] SplitOnCase(this string inputString)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            return r.Replace(inputString, " ").Split(' ');
        }

        #endregion Extensions
    }
}