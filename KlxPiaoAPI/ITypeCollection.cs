namespace KlxPiaoAPI
{
    /// <summary>
    /// 类型集合接口，用于判断对象是否属于某种类型集合。
    /// </summary>
    public interface ITypeCollection
    {
        /// <summary>
        /// 判断对象是否属于某种类型集合。
        /// </summary>
        /// <param name="obj">要判断的对象。</param>
        /// <returns>如果对象属于类型集合，则返回 true；否则返回 false。</returns>
        bool IsTypeInCollection(object obj);
    }
}