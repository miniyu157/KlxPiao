using System.ComponentModel;
using System.Text;

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
        /// 初始化 <see cref="Animation"/> 结构的新实例。
        /// </summary>
        /// <param name="time">动画的持续时间。</param>
        /// <param name="fps">动画的帧率（每秒帧数）。</param>
        /// <param name="controlPoint">字符串形式的贝塞尔曲线控制点。</param>
        /// <exception cref="ArgumentException"></exception>
        public Animation(int time, int fps, string controlPoint)
        {
            Time = time;
            FPS = fps;

            try
            {
                string[] parts = controlPoint.Split(',');

                if (parts.Length % 2 == 1)
                {
                    throw new ArgumentException("The number of control points is not valid. It must be divisible by 2.");
                }

                List<PointF> points = [];
                for (int i = 0; i < parts.Length; i += 2)
                {
                    float x = float.Parse(parts[i].Trim());
                    float y = float.Parse(parts[i + 1].Trim());
                    PointF pointF = new(x, y);

                    points.Add(pointF);
                }

                Easing = [.. points];
            }
            catch
            {
                throw new ArgumentException("Invalid format for the control point. Please provide comma-separated pairs of x and y coordinates.");
            }
        }

        public static bool operator ==(Animation anim1, Animation anim2)
        {
            return anim1.Time == anim2.Time &&
                anim1.FPS == anim2.FPS &&
                anim1.Easing == anim2.Easing;
        }

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
            if (Easing != null)
            {
                StringBuilder easingText = new();
                easingText.Append('[');
                for (int i = 0; i < Easing.Length; i++)
                {
                    easingText.Append($"{Easing[i].X}, {Easing[i].Y}");
                    if (i != Easing.Length - 1)
                    {
                        easingText.Append(", ");
                    }
                }
                easingText.Append(']');

                return $"Time: {Time}, FPS: {FPS}, Easing: {easingText}";
            }
            else
            {
                return $"Time: {Time}, FPS: {FPS}, Easing: Null";
            }
        }
    }
}