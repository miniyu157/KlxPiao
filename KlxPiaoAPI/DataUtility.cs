namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供数据处理的实用工具类。
    /// </summary>
    public class DataUtility
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
    }
}