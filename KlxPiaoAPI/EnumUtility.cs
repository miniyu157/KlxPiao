namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供与枚举类型操作相关的实用方法。
    /// </summary>
    public class EnumUtility
    {
        /// <summary>
        /// 对枚举类型进行遍历，并执行指定的操作。
        /// </summary>
        /// <typeparam name="T">枚举类型。</typeparam>
        /// <param name="action">要执行的操作。</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ForEachEnum<T>(Action<T> action) where T : Enum
        {
            ArgumentNullException.ThrowIfNull(action);
            Enum.GetValues(typeof(T)).Cast<T>().ToList().ForEach(item => action(item));
        }

        /// <summary>
        /// 重新排序枚举类型，并获取索引处的值。
        /// </summary>
        /// <typeparam name="T">枚举类型。</typeparam>
        /// <param name="index">索引。</param>
        /// <returns>指定索引处的枚举值。</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static T ReorderEnumValues<T>(int index) where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            if (index < 0 || index >= values.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            return values[index];
        }
    }
}