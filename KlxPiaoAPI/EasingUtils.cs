namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供对于缓动效果处理的使用方法。
    /// </summary>
    public static class EasingUtils
    {
        /// <summary>
        /// 将 <see cref="EasingType"/> 转换为字符串形式的贝塞尔控制点。
        /// </summary>
        /// <param name="easingType">表示缓动类型的枚举。</param>
        /// <returns>表示贝塞尔控制点的字符串</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetControlPoints(EasingType easingType)
        {
            return easingType switch
            {
                EasingType.Linear => "0, 0, 1, 1",
                EasingType.EaseIn => "0.42, 0, 1, 1",
                EasingType.EaseOut => "0, 0, 0.58, 1",
                EasingType.EaseInOut => "0.42, 0, 0.58, 1",
                EasingType.EaseInQuad => "0.55, 0.085, 0.68, 0.53",
                EasingType.EaseOutQuad => "0.25, 0.46, 0.45, 0.94",
                EasingType.EaseInOutQuad => "0.455, 0.03, 0.515, 0.955",
                EasingType.EaseInCubic => "0.55, 0.055, 0.675, 0.19",
                EasingType.EaseOutCubic => "0.215, 0.61, 0.355, 1",
                EasingType.EaseInOutCubic => "0.645, 0.045, 0.355, 1",
                EasingType.EaseInQuart => "0.895, 0.03, 0.685, 0.22",
                EasingType.EaseOutQuart => "0.165, 0.84, 0.44, 1",
                EasingType.EaseInOutQuart => "0.77, 0, 0.175, 1",
                EasingType.EaseInQuint => "0.755, 0.05, 0.855, 0.06",
                EasingType.EaseOutQuint => "0.23, 1, 0.32, 1",
                EasingType.EaseInOutQuint => "0.86, 0, 0.07, 1",
                _ => throw new ArgumentException("Unsupported EasingType."),
            };
        }

        /// <summary>
        /// 将字符串形式的贝塞尔控制点或枚举类型 <see cref="EasingType"/> 的成员转换为点数组。
        /// </summary>
        /// <param name="easing"></param>
        /// <returns></returns>
        public static PointF[] ParseEasing(string easing)
        {
            if (IsValidControlPoint(easing))
            {
                return ParseControlPoints(easing);
            }

            if (Enum.TryParse(easing, true, out EasingType easingType))
            {
                return ParseControlPoints(GetControlPoints(easingType));
            }

            return [new(0, 0), new(1, 1)];
        }

        /// <summary>
        /// 检查字符串形式的贝塞尔控制点是否有效。
        /// </summary>
        /// <param name="controlPoint">字符串形式的贝塞尔控制点。</param>
        /// <returns>若为 true 则有效，否则无效</returns>
        public static bool IsValidControlPoint(string controlPoint)
        {
            var parts = controlPoint.Split(',');
            if (parts.Length % 2 != 0)
            {
                return false;
            }

            return parts.All(part =>
            {
                return float.TryParse(part.Trim(), out _);
            });
        }

        /// <summary>
        /// 将字符串形式的贝塞尔控制点转换为点数组。
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static PointF[] ParseControlPoints(string controlPoint)
        {
            try
            {
                string[] parts = controlPoint.Split(',');

                if (parts.Length % 2 != 0)
                {
                    throw new ArgumentException("The number of control points is not valid. It must be divisible by 2.");
                }

                List<PointF> points = [];
                for (int i = 0; i < parts.Length; i += 2)
                {
                    float x = float.Parse(parts[i].Trim());
                    float y = float.Parse(parts[i + 1].Trim());
                    points.Add(new PointF(x, y));
                }

                return [.. points];
            }
            catch
            {
                throw new ArgumentException("Invalid format for the control point. Please provide comma-separated pairs of x and y coordinates.");
            }
        }
    }
}