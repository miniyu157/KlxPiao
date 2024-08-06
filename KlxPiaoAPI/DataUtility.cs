namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供数据处理的实用工具类。
    /// </summary>
    public static class DataUtility
    {
        /// <summary>
        /// 将两个 <see cref="List{T}"/> 合并为一个 <see cref="Dictionary{TKey, TValue}"/>。
        /// </summary>
        /// <typeparam name="TKey">字典的键类型。</typeparam>
        /// <typeparam name="TValue">字典的值类型。</typeparam>
        /// <param name="keys">包含字典键的列表。</param>
        /// <param name="values">包含字典值的列表。</param>
        /// <returns>合并后的字典。</returns>
        /// <exception cref="ArgumentException">当两个列表的长度不相等时引发。</exception>
        public static Dictionary<TKey, TValue> MergeListsToDictionary<TKey, TValue>(List<TKey> keys, List<TValue> values) where TKey : notnull
        {
            if (keys.Count != values.Count)
            {
                throw new ArgumentException("Lists must be of equal length.");
            }

            return keys.Zip(values, (key, value) => new { key, value }).ToDictionary(pair => pair.key, pair => pair.value);
        }

        /// <summary>
        /// 将文本截断以适应指定的宽度，如果文本超出宽度则添加省略号。
        /// </summary>
        /// <param name="text">要截断的文本。</param>
        /// <param name="maxWidth">文本允许的最大宽度。</param>
        /// <param name="font">用于测量文本的字体。</param>
        /// <param name="omitText">表示省略的文本。</param>
        /// <returns>处理后的文本，如果文本超出宽度则添加省略号。</returns>
        public static string TruncateStringToFitWidth(this string text, float maxWidth, Font font, string omitText = "...")
        {
            using Bitmap bitmap = new(1, 1);
            using Graphics g = Graphics.FromImage(bitmap);
            float longTextMaxWidth = maxWidth - g.MeasureString(omitText, font).Width;
            float smallTextMaxWidth = maxWidth;
            string newText = string.Empty;

            if (g.MeasureString(text, font).Width <= smallTextMaxWidth)
            {
                return text;
            }

            for (int i = 1; i <= text.Length; i++)
            {
                if (g.MeasureString(text[..i], font).Width > longTextMaxWidth)
                {
                    newText = text[..i] + omitText;
                    break;
                }
            }
            return newText;
        }

        /// <summary>
        /// 将字符串列表截断以适应指定宽度，超过宽度的部分用省略号替代。
        /// </summary>
        /// <param name="list">要截断的字符串列表。</param>
        /// <param name="maxWidth">允许的最大宽度。</param>
        /// <param name="font">用于测量字符串的字体。</param>
        /// <param name="omitText">省略号文本，默认为 "..."。</param>
        /// <returns>截断后的字符串列表。</returns>
        public static List<string> TruncateToFitWidth(this List<string> list, float maxWidth, Font font, string omitText = "...")
        {
            return list.Select(item => item.TruncateStringToFitWidth(maxWidth, font, omitText)).ToList();
        }

        /// <summary>
        /// 交换列表中两个指定索引位置的元素。
        /// </summary>
        /// <param name="items">要操作的列表。</param>
        /// <param name="index1">第一个索引位置。</param>
        /// <param name="index2">第二个索引位置。</param>
        /// <returns>交换后的列表。</returns>
        public static List<T> SwapListElements<T>(this List<T> items, int index1, int index2)
        {
            if (index1 < 0 || index1 >= items.Count)
                throw new ArgumentOutOfRangeException(nameof(index1), "索引超出了范围。");

            if (index2 < 0 || index2 >= items.Count)
                throw new ArgumentOutOfRangeException(nameof(index2), "索引超出了范围。");

            (items[index2], items[index1]) = (items[index1], items[index2]);
            return items;
        }

        /// <summary>
        /// 交换字典中两个指定索引位置的键值对。
        /// </summary>
        /// <param name="dictionary">要操作的字典。</param>
        /// <param name="index1">第一个索引位置。</param>
        /// <param name="index2">第二个索引位置。</param>
        /// <returns>交换后的字典。</returns>
        /// <typeparam name="TKey">字典中的键的类型。</typeparam>
        /// <typeparam name="TValue">字典中的值的类型。</typeparam>
        public static Dictionary<TKey, TValue> SwapDictionaryElements<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, int index1, int index2) where TKey : notnull
        {
            return DataUtility.MergeListsToDictionary(
                SwapListElements(dictionary.Keys.ToList(), index1, index2),
                SwapListElements(dictionary.Values.ToList(), index1, index2));
        }
    }
}