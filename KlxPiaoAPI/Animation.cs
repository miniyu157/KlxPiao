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

        /// <summary>
        /// 重载 == 运算符，比较两个 <see cref="Animation"/> 对象是否相等。
        /// </summary>
        /// <param name="cr1">第一个 <see cref="Animation"/> 对象。</param>
        /// <param name="cr2">第二个 <see cref="Animation"/> 对象。</param>
        /// <returns>如果两个对象相等，则返回 true；否则返回 false。</returns>
        public static bool operator ==(Animation anim1, Animation anim2)
        {
            return anim1.Time == anim2.Time &&
                anim1.FPS == anim2.Time &&
                anim1.Easing == anim2.Easing;
        }

        /// <summary>
        /// 重载 != 运算符，比较两个 <see cref="Animation"/> 对象是否不相等。
        /// </summary>
        /// <param name="cr1">第一个 <see cref="Animation"/> 对象。</param>
        /// <param name="cr2">第二个 <see cref="Animation"/> 对象。</param>
        /// <returns>如果两个对象不相等，则返回 true；否则返回 false。</returns>
        public static bool operator !=(Animation anim1, Animation anim2)
        {
            return !(anim1 == anim2);
        }

        public readonly override int GetHashCode()
        {
            int easingHashCode = Easing == null ? 0 : Easing.GetHashCode();
            return Time.GetHashCode() ^ FPS.GetHashCode() ^ easingHashCode;
        }

        public readonly override bool Equals(object? obj)
        {
            if (obj == null || obj is not Animation)
            {
                return false;
            }
            else
            {
                Animation anim = (Animation)obj;
                return Time == anim.Time && FPS == anim.FPS && Easing == anim.Easing;
            }
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