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
    /// 表示渲染或处理程序优先级的枚举。
    /// </summary>
    public enum PriorityLevel
    {
        /// <summary>
        /// 低优先级。
        /// </summary>
        Low,

        /// <summary>
        /// 中优先级。
        /// </summary>
        Medium,

        /// <summary>
        /// 高优先级。
        /// </summary>
        High
    }
}