namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供枚举类型的扩展方法。
    /// </summary>
    public static class EnumUtility
    {
        /// <summary>
        /// 对枚举类型进行遍历操作。
        /// </summary>
        /// <typeparam name="T">枚举类型。</typeparam>
        /// <param name="action">要执行的操作。</param>
        public static void ForEachEnum<T>(Action<T> action) where T : Enum
        {
            Enum.GetValues(typeof(T)).Cast<T>().ToList().ForEach(item => action(item));
        }

        /// <summary>
        /// 重新排序指定枚举类型的值。
        /// </summary>
        /// <typeparam name = "T" > 枚举类型。</typeparam>
        /// <param name = "index" > 要返回的值的索引。</param>
        /// <returns>指定索引处的枚举值。</returns>
        public static T ReorderEnumValues<T>(int index) where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList()[index];
        }
    }
}