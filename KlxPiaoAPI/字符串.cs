namespace KlxPiaoAPI
{
    public class 字符串
    {
        /// <summary>
        /// 执行批量替换操作，将字符串中的指定内容替换为新的值。
        /// </summary>
        /// <param name="format">要进行替换操作的原始字符串。</param>
        /// <param name="replacements">用于替换的键值对，键表示要替换的内容，值表示替换后的新值。</param>
        public static string 批量替换(string format, Dictionary<string, string> replacements)
        {
            foreach (var replacement in replacements)
            {
                format = format.Replace(replacement.Key, replacement.Value);
            }
            return format;
        }
    }
}