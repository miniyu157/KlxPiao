namespace KlxPiaoAPI
{
    /// <summary>
    /// 鼠标值修改事件处理选项。
    /// </summary>
    public enum MouseValueChangedEventOption
    {
        /// <summary>
        /// 修改值并触发事件。
        /// </summary>
        OnDefault,

        /// <summary>
        /// 修改值但不触发事件。
        /// </summary>
        OnNoEvent,

        /// <summary>
        /// 不修改值且不触发事件。
        /// </summary>
        OnNoChangeValueNoEvent,
    }

    /// <summary>
    /// 表示调整大小的模式
    /// </summary>
    public enum ResizeMode
    {
        /// <summary>
        /// 表示百分比值缩放模式。
        /// </summary>
        Percentage,
        /// <summary>
        /// 表示像素缩放模式。
        /// </summary>
        Pixel
    }

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