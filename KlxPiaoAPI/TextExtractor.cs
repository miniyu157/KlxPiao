using System.Text.RegularExpressions;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 匹配模式的枚举。
    /// </summary>
    public enum MatchMode
    {
        /// <summary>
        /// 正则表达式匹配模式。
        /// </summary>
        Regex,
        /// <summary>
        /// 字符串索引匹配模式。
        /// </summary>
        StringIndex
    }

    /// <summary>
    /// 用于从文本中提取子字符串的实用工具类。
    /// </summary>
    public static class TextExtractor
    {
        /// <summary>
        /// 从原始文本中提取位于前导文本和尾随文本之间的中间文本。
        /// </summary>
        /// <param name="originalText">原始文本。</param>
        /// <param name="leadingText">前导文本。</param>
        /// <param name="trailingText">尾随文本。</param>
        /// <returns>提取的中间文本。</returns>
        public static string 提取中间文本(this string originalText, string leadingText, string trailingText)
        {
            int startIndex = originalText.IndexOf(leadingText);
            if (startIndex == -1)
            {
                return string.Empty;
            }

            startIndex += leadingText.Length;
            int endIndex = originalText.IndexOf(trailingText, startIndex);
            if (endIndex == -1)
            {
                return string.Empty;
            }

            return originalText[startIndex..endIndex];
        }

        /// <summary>
        /// 提取所有位于前导文本和尾随文本之间的子字符串。
        /// </summary>
        /// <param name="inputText">要提取的输入文本。</param>
        /// <param name="leadingText">要搜索的前导文本。</param>
        /// <param name="trailingText">要搜索的尾随文本。</param>
        /// <param name="mode">要使用的匹配模式（默认为 Regex）。</param>
        /// <returns>提取的子字符串列表。</returns>
        public static List<string> 提取所有中间文本(this string inputText, string leadingText, string trailingText, MatchMode mode = MatchMode.Regex)
        {
            return mode switch
            {
                MatchMode.Regex => ExtractWithRegex(inputText, leadingText, trailingText),
                MatchMode.StringIndex => ExtractWithStringIndex(inputText, leadingText, trailingText),
                _ => throw new ArgumentException("指定了无效的匹配模式", nameof(mode))
            };
        }

        /// <summary>
        /// 使用正则表达式匹配提取子字符串。
        /// </summary>
        /// <param name="inputText">要提取的输入文本。</param>
        /// <param name="leadingText">要搜索的前导文本。</param>
        /// <param name="trailingText">要搜索的尾随文本。</param>
        /// <returns>提取的子字符串列表。</returns>
        private static List<string> ExtractWithRegex(string inputText, string leadingText, string trailingText)
        {
            var matches = Regex.Matches(inputText, Regex.Escape(leadingText) + "(.*?)" + Regex.Escape(trailingText));
            var result = new List<string>();

            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                {
                    result.Add(match.Groups[1].Value);
                }
            }

            return result;
        }

        /// <summary>
        /// 使用字符串索引匹配提取子字符串。
        /// </summary>
        /// <param name="inputText">要提取的输入文本。</param>
        /// <param name="leadingText">要搜索的前导文本。</param>
        /// <param name="trailingText">要搜索的尾随文本。</param>
        /// <returns>提取的子字符串列表。</returns>
        private static List<string> ExtractWithStringIndex(string inputString, string leadingText, string trailingText)
        {
            var resultList = new List<string>();
            int startPos = 0;

            while (true)
            {
                int leadPos = inputString.IndexOf(leadingText, startPos);
                if (leadPos == -1) break;
                leadPos += leadingText.Length;

                int trailPos = inputString.IndexOf(trailingText, leadPos);
                if (trailPos == -1) break;

                string extractedString = inputString[leadPos..trailPos];

                resultList.Add(extractedString);

                startPos = trailPos + trailingText.Length;
            }

            return resultList;
        }
    }
}