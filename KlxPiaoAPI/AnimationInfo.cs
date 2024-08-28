using System.ComponentModel;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 结构体，用于表示一个动画的基本组成元素。
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(AnimationInfoConverter))]
    public struct AnimationInfo
    {
        /// <summary>
        /// 获取或设置动画的持续时间。
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// 获取或设置动画的帧率。
        /// </summary>
        public int FPS { get; set; }

        /// <summary>
        /// 获取或设置动画的贝塞尔曲线控制点。
        /// </summary>
        public string Easing { get; set; }

        /// <summary>
        /// 初始化 <see cref="AnimationInfo"/> 结构的新实例。
        /// </summary>
        /// <param name="time">动画的持续时间。</param>
        /// <param name="fps">动画的帧率（每秒帧数）。</param>
        /// <param name="controlPoint">字符串形式的贝塞尔曲线控制点。</param>
        public AnimationInfo(int time, int fps, string controlPoint)
        {
            Time = time;
            FPS = fps;
            Easing = controlPoint;
        }

        /// <summary>
        /// 初始化 <see cref="AnimationInfo"/> 结构的新实例。
        /// </summary>
        /// <param name="time">动画的持续时间。</param>
        /// <param name="fps">动画的帧率（每秒帧数）。</param>
        /// <param name="easingType">表示缓动类型的枚举值。</param>
        public AnimationInfo(int time, int fps, EasingType easingType)
        {
            Time = time;
            FPS = fps;
            Easing = EasingUtils.GetControlPoints(easingType);
        }

        public static bool operator ==(AnimationInfo anim1, AnimationInfo anim2)
        {
            return anim1.Time == anim2.Time &&
                anim1.FPS == anim2.FPS &&
                anim1.Easing == anim2.Easing;
        }

        public static bool operator !=(AnimationInfo anim1, AnimationInfo anim2)
        {
            return !(anim1 == anim2);
        }

        public override readonly int GetHashCode()
        {
            return Time.GetHashCode() ^ FPS.GetHashCode() ^ Easing.GetHashCode();
        }

        public override readonly bool Equals(object? obj)
        {
            if (obj == null || obj is not AnimationInfo)
            {
                return false;
            }
            else
            {
                AnimationInfo anim = (AnimationInfo)obj;
                return Time == anim.Time && FPS == anim.FPS && Easing == anim.Easing;
            }
        }

        public AnimationInfo() : this(200, 30, EasingUtils.GetControlPoints(EasingType.Linear)) { }

        /// <summary>
        /// 返回表示当前 <see cref="AnimationInfo"/> 的字符串。
        /// </summary>
        /// <returns>表示当前 <see cref="AnimationInfo"/> 的字符串。</returns>
        public override readonly string ToString()
        {
            return $"Time: {Time}, FPS: {FPS}, Easing: [{Easing}])]";
        }
    }
}