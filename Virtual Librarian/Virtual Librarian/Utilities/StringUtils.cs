using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Text.RegularExpressions;

namespace Virtual_Librarian
{
    static class StringUtils
    {
        /// <summary>
        /// Helper for getting string from specified string to end of line.
        /// </summary>
        public static string FromToNewline(this string value, string from)
        {
            int posFrom = value.IndexOf(from + ',');
            Regex newLineRegex = new Regex("(" + from + @".+?(?=\r\n))");

            //Check if not last entry
            if (newLineRegex.IsMatch(value)){
                int posTo = value.IndexOf("\r\n");
                return value.Substring(posFrom, posTo);
            }
            else
            {
                return value.Substring(posFrom);
            }
        }

        /// <summary>
        /// Helper to debug strings (to see escape symbols, etc).
        /// </summary>
        private static string ToLiteral(string input)
        {
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
                    return writer.ToString();
                }
            }
        }
    }
}
