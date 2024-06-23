namespace KlxPiaoAPI
{
    /// <summary>
    /// 插值器类型，用于处理各种类型的插值操作。
    /// </summary>
    public class TypeInterpolator
    {
        /// <summary>
        /// 对给定的开始值和结束值进行插值计算。
        /// </summary>
        /// <param name="startValue">插值的开始值。</param>
        /// <param name="endValue">插值的结束值。</param>
        /// <param name="progress">插值的进度，取值范围为0-1。</param>
        /// <returns>返回插值计算的结果。</returns>
        public static object Interpolator(object startValue, object endValue, double progress)
        {
            if (startValue.GetType() != endValue.GetType()) throw new ArgumentException($"参数 '{startValue}' 和 'endValue' 类型必须相同。");

            Type type = startValue.GetType();

            if (startValue.IsTypes(TypeChecker.GetNumberTypeInstance()))
                return Interpolator((double)startValue, (double)endValue, progress);

            if (type == typeof(Size))
                return Interpolator((Size)startValue, (Size)endValue, progress);

            if (type == typeof(SizeF))
                return Interpolator((SizeF)startValue, (SizeF)endValue, progress);

            if (type == typeof(Point))
                return Interpolator((Point)startValue, (Point)endValue, progress);

            if (type == typeof(PointF))
                return Interpolator((PointF)startValue, (PointF)endValue, progress);

            if (type == typeof(Color))
                return Interpolator((Color)startValue, (Color)endValue, progress);

            throw new NotImplementedException($"类型 '{type}' 的插值器尚未实现。");
        }

        /// <summary>
        /// 对给定的开始值和结束值进行插值计算。
        /// </summary>
        /// <param name="startValue">插值的开始值。</param>
        /// <param name="endValue">插值的结束值。</param>
        /// <param name="progress">值的进度，取值范围为0-1。</param>
        /// <returns>返回插值计算的结果。</returns>
        public static double Interpolator(double startValue, double endValue, double progress)
        {
            return (double)startValue + ((double)endValue - (double)startValue) * (double)progress;
        }

        /// <summary>
        /// 对给定的开始颜色和结束颜色进行插值计算。
        /// </summary>
        /// <param name="startColor">插值的开始颜色。</param>
        /// <param name="endColor">插值的结束颜色。</param>
        /// <param name="progress">插值的进度，取值范围为0-1。</param>
        /// <returns>返回插值计算的结果颜色。</returns>
        public static Color Interpolator(Color startColor, Color endColor, double progress)
        {
            int R = startColor.R + (int)((endColor.R - startColor.R) * progress);
            int G = startColor.G + (int)((endColor.G - startColor.G) * progress);
            int B = startColor.B + (int)((endColor.B - startColor.B) * progress);
            return Color.FromArgb(R, G, B);
        }

        /// <summary>
        /// 对给定的开始尺寸和结束尺寸进行插值计算。
        /// </summary>
        /// <param name="startSize">插值的开始尺寸。</param>
        /// <param name="endSize">插值的结束尺寸。</param>
        /// <param name="progress">插值的进度，取值范围为0-1。</param>
        /// <returns>返回插值计算的结果尺寸。</returns>
        public static Size Interpolator(Size startSize, Size endSize, double progress)
        {
            int newWidth = startSize.Width + (int)((endSize.Width - startSize.Width) * progress);
            int newHeight = startSize.Height + (int)((endSize.Height - startSize.Height) * progress);
            return new Size(newWidth, newHeight);
        }

        /// <summary>
        /// 对给定的开始尺寸和结束尺寸进行插值计算。
        /// </summary>
        /// <param name="startSizeF">插值的开始尺寸。</param>
        /// <param name="endSizeF">插值的结束尺寸。</param>
        /// <param name="progress">插值的进度，取值范围为0-1。</param>
        /// <returns>返回插值计算的结果尺寸。</returns>
        public static SizeF Interpolator(SizeF startSizeF, SizeF endSizeF, double progress)
        {
            float newWidthF = startSizeF.Width + (float)((endSizeF.Width - startSizeF.Width) * progress);
            float newHeightF = startSizeF.Height + (float)((endSizeF.Height - startSizeF.Height) * progress);
            return new SizeF(newWidthF, newHeightF);
        }

        /// <summary>
        /// 对给定的开始点和结束点进行插值计算。
        /// </summary>
        /// <param name="startPoint">插值的开始点。</param>
        /// <param name="endPoint">插值的结束点。</param>
        /// <param name="progress">插值的进度，取值范围为0-1。</param>
        /// <returns>返回插值计算的结果点。</returns>
        public static Point Interpolator(Point startPoint, Point endPoint, double progress)
        {
            int newX = startPoint.X + (int)((endPoint.X - startPoint.X) * progress);
            int newY = startPoint.Y + (int)((endPoint.Y - startPoint.Y) * progress);
            return new Point(newX, newY);
        }

        /// <summary>
        /// 对给定的开始点和结束点进行插值计算。
        /// </summary>
        /// <param name="startPointF">插值的开始点。</param>
        /// <param name="endPointF">插值的结束点。</param>
        /// <param name="progress">插值的进度，取值范围为0-1。</param>
        /// <returns>返回插值计算的结果点。</returns>
        public static PointF Interpolator(PointF startPointF, PointF endPointF, double progress)
        {
            float newXF = startPointF.X + (float)((endPointF.X - startPointF.X) * progress);
            float newYF = startPointF.Y + (float)((endPointF.Y - startPointF.Y) * progress);
            return new PointF(newXF, newYF);
        }
    }
}