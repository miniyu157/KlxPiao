using System.Text.RegularExpressions;
using System.Text;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供了一系列用于字符串处理的扩展方法。
    /// </summary>
    public static partial class StringExtensions
    {
        /// <summary>
        /// 执行批量替换操作，将字符串中的指定内容替换为新的值。
        /// </summary>
        /// <param name="format">要进行替换操作的原始字符串。</param>
        /// <param name="replacements">用于替换的键值对，键表示要替换的内容，值表示替换后的新值。</param>
        public static string ReplaceMultiple(this string format, Dictionary<string, string> replacements)
        {
            foreach (var replacement in replacements)
            {
                format = format.Replace(replacement.Key, replacement.Value);
            }
            return format;
        }

        /// <summary>
        /// 将指定范围内的字符转换为小写形式。
        /// </summary>
        /// <param name="input">要转换的字符串。</param>
        /// <param name="start">转换范围的起始索引（包括）。</param>
        /// <param name="end">转换范围的结束索引（包括）。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="ArgumentException">当起始索引小于0、结束索引大于等于字符串长度、或者起始索引大于结束索引时抛出。</exception>
        public static string ToLowerRange(this string input, int start, int end)
        {
            if (input.Length == 1)
            {
                return input.ToLower();
            }

            if (start < 0 || end >= input.Length || start > end)
            {
                throw new ArgumentException("Invalid start or end index.");
            }

            string prefix = input[..start];
            string range = input[start..(end + 1)].ToLower();
            string suffix = input[(end + 1)..];

            return prefix + range + suffix;
        }

        /// <summary>
        /// 将指定范围内的字符转换为大写形式。
        /// </summary>
        /// <param name="input">要转换的字符串。</param>
        /// <param name="start">转换范围的起始索引（包括）。</param>
        /// <param name="end">转换范围的结束索引（包括）。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="ArgumentException">当起始索引小于0、结束索引大于等于字符串长度、或者起始索引大于结束索引时抛出。</exception>
        public static string ToUpperRange(this string input, int start, int end)
        {
            if (input.Length == 1)
            {
                return input.ToUpper();
            }

            if (start < 0 || end >= input.Length || start > end)
            {
                throw new ArgumentException("Invalid start or end index.");
            }

            string prefix = input[..start];
            string range = input[start..(end + 1)].ToUpper();
            string suffix = input[(end + 1)..];

            return prefix + range + suffix;
        }

        /// <summary>
        /// 处理字符串的第一个字符，根据其大小写转换为相反的大小写。
        /// </summary>
        /// <param name="input">要处理的字符串。</param>
        /// <param name="failstring">处理无效时追加的字符。</param>
        /// <returns>返回处理后的字符串。</returns>
        /// <exception cref="ArgumentException">参数无效时抛出。</exception>
        public static string ProcessFirstChar(this string input, string failstring = "")
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("参数 'input' 不能为空");
            }

            char firstChar = input[0];

            if (char.IsUpper(firstChar))
            {
                var 小写副本 = char.ToLower(firstChar) + input[1..];

                if (小写副本 == input)
                {
                    return input + failstring;
                }
                return 小写副本;
            }
            else
            {
                var 大写副本 = char.ToUpper(firstChar) + input[1..];

                if (大写副本 == input)
                {
                    return input + failstring;
                }
                return 大写副本;
            }
        }

        /// <summary>
        /// 将字符串转换为 Unicode 形式的字符串。
        /// </summary>
        /// <param name="chineseString">提供的源文本。</param>
        /// <returns>转换后的 Unicode 字符串。</returns>
        /// <exception cref="ArgumentException">参数无效时抛出。</exception>
        public static string ToUnicode(this string chineseString)
        {
            if (string.IsNullOrEmpty(chineseString))
            {
                throw new ArgumentException("Input string cannot be null or empty", nameof(chineseString));
            }

            StringBuilder unicodeString = new();
            foreach (char c in chineseString)
            {
                unicodeString.AppendFormat("\\u{0:X4}", (int)c);
            }
            return unicodeString.ToString();
        }

        /// <summary>
        /// 将形如 \uXXXX 的 Unicode 字符串转换为可读的文本。
        /// </summary>
        /// <param name="unicodeString">提供的源文本。</param>
        /// <returns>转换后的字符串。</returns>
        /// <exception cref="ArgumentException">参数无效时抛出。</exception>
        public static string ToChinese(this string unicodeString)
        {
            if (string.IsNullOrEmpty(unicodeString))
            {
                throw new ArgumentException("Input string cannot be null or empty", nameof(unicodeString));
            }

            //匹配所有的Unicode转义序列
            Regex regex = MyRegex();
            return regex.Replace(unicodeString, m => ((char)Convert.ToInt32(m.Groups["Value"].Value, 16)).ToString());
        }

        [GeneratedRegex(@"\\u(?<Value>[a-fA-F0-9]{4})", RegexOptions.Compiled)]
        private static partial Regex MyRegex();
    }
}