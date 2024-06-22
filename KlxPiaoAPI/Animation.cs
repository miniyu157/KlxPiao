using System.ComponentModel;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 结构体，用于表示一个动画的基本组成元素。
    /// </summary>
    [TypeConverter(typeof(AnimationConverter))]
    public struct Animation
    {
        /// <summary>
        /// 获取或设置动画的持续时间。
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// 获取或设置动画的帧率（每秒帧数）。
        /// </summary>
        public int FPS { get; set; }

        /// <summary>
        /// 获取或设置动画的贝塞尔曲线控制点。
        /// </summary>
        public PointF[]? Easing { get; set; }

        /// <summary>
        /// 初始化 <see cref="Animation"/> 结构的新实例。
        /// </summary>
        /// <param name="time">动画的持续时间。</param>
        /// <param name="fps">动画的帧率（每秒帧数）。</param>
        /// <param name="controlPoint">动画的贝塞尔曲线控制点。</param>
        public Animation(int time, int fps, PointF[]? controlPoint)
        {
            Time = time;
            FPS = fps;
            Easing = controlPoint;
        }

        public Animation() { }

        /// <summary>
        /// 返回表示当前 <see cref="Animation"/> 的字符串。
        /// </summary>
        /// <returns>表示当前 <see cref="Animation"/> 的字符串。</returns>
        public override readonly string ToString()
        {
            return $"Time: {Time}, FPS: {FPS}, Easing: {Easing}";
        }
    }
}