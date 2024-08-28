namespace KlxPiaoAPI
{
    /// <summary>
    /// 插值器策略接口，定义了插值计算的方法。
    /// </summary>
    public interface IInterpolatorStrategy
    {
        /// <summary>
        /// 根据起始值和终止值及进度计算插值。
        /// </summary>
        /// <param name="startValue">插值的起始值。</param>
        /// <param name="endValue">插值的终止值。</param>
        /// <param name="progress">插值的进度，范围为0到1。</param>
        /// <returns>计算得到的插值结果。</returns>
        object Interpolate(object startValue, object endValue, double progress);
    }
}