namespace KlxPiaoAPI
{
    /// <summary>
    /// 表示常见的缓动曲线类型。
    /// </summary>
    public enum EasingType
    {
        /// <summary>
        /// 线性缓动。动画以恒定速度进行。
        /// </summary>
        Linear,

        /// <summary>
        /// 缓入。动画开始时较慢，然后加速。
        /// </summary>
        EaseIn,

        /// <summary>
        /// 缓出。动画开始时较快，然后减速。
        /// </summary>
        EaseOut,

        /// <summary>
        /// 缓入缓出。动画开始和结束时较慢，中间加速。
        /// </summary>
        EaseInOut,

        /// <summary>
        /// 二次缓入。动画以二次函数的方式缓慢开始，然后加速。
        /// </summary>
        EaseInQuad,

        /// <summary>
        /// 二次缓出。动画以二次函数的方式快速开始，然后减速。
        /// </summary>
        EaseOutQuad,

        /// <summary>
        /// 二次缓入缓出。动画以二次函数的方式缓慢开始和结束，中间加速。
        /// </summary>
        EaseInOutQuad,

        /// <summary>
        /// 三次缓入。动画以三次函数的方式缓慢开始，然后加速。
        /// </summary>
        EaseInCubic,

        /// <summary>
        /// 三次缓出。动画以三次函数的方式快速开始，然后减速。
        /// </summary>
        EaseOutCubic,

        /// <summary>
        /// 三次缓入缓出。动画以三次函数的方式缓慢开始和结束，中间加速。
        /// </summary>
        EaseInOutCubic,

        /// <summary>
        /// 四次缓入。动画以四次函数的方式缓慢开始，然后加速。
        /// </summary>
        EaseInQuart,

        /// <summary>
        /// 四次缓出。动画以四次函数的方式快速开始，然后减速。
        /// </summary>
        EaseOutQuart,

        /// <summary>
        /// 四次缓入缓出。动画以四次函数的方式缓慢开始和结束，中间加速。
        /// </summary>
        EaseInOutQuart,

        /// <summary>
        /// 五次缓入。动画以五次函数的方式缓慢开始，然后加速。
        /// </summary>
        EaseInQuint,

        /// <summary>
        /// 五次缓出。动画以五次函数的方式快速开始，然后减速。
        /// </summary>
        EaseOutQuint,

        /// <summary>
        /// 五次缓入缓出。动画以五次函数的方式缓慢开始和结束，中间加速。
        /// </summary>
        EaseInOutQuint
    }
}