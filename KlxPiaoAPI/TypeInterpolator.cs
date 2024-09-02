namespace KlxPiaoAPI
{
    /// <summary>
    /// 类型插值器，用于根据已注册的策略对指定类型进行插值计算。
    /// </summary>
    public static class TypeInterpolator
    {
        private static readonly Dictionary<Type, IInterpolatorStrategy> strategies = [];

        /// <summary>
        /// 注册指定类型的插值策略。
        /// </summary>
        /// <typeparam name="T">需要注册插值策略的类型。</typeparam>
        /// <param name="strategy">插值策略实例。</param>
        public static void RegisterStrategy<T>(IInterpolatorStrategy strategy)
        {
            strategies[typeof(T)] = strategy;
        }

        /// <summary>
        /// 对指定类型的起始值和终止值进行插值计算。
        /// </summary>
        /// <typeparam name="T">进行插值的类型。</typeparam>
        /// <param name="startValue">插值的起始值。</param>
        /// <param name="endValue">插值的终止值。</param>
        /// <param name="progress">插值的进度，范围为 0-1。</param>
        /// <returns>计算得到的插值结果。</returns>
        /// <exception cref="ArgumentOutOfRangeException">当 progress 不在 0-1 之间时抛出。</exception>
        /// <exception cref="NotImplementedException">当指定类型的插值策略未注册时抛出。</exception>
        public static T Interpolate<T>(T startValue, T endValue, double progress) where T : notnull
        {
            if (progress < 0 || progress > 1)
                throw new ArgumentOutOfRangeException(nameof(progress), "进度必须在 0-1 之间。");

            Type type = typeof(T);
            if (strategies.TryGetValue(type, out IInterpolatorStrategy? value))
            {
                return (T)value.Interpolate(startValue!, endValue!, progress);
            }

            throw new NotImplementedException($"类型 '{type}' 的插值器尚未实现。");
        }

        // 默认的策略注册
        static TypeInterpolator()
        {
            RegisterStrategy<int>(new IntInterpolator());
            RegisterStrategy<float>(new SingleInterpolator());
            RegisterStrategy<double>(new DoubleInterpolator());
            RegisterStrategy<Color>(new ColorInterpolator());
            RegisterStrategy<Size>(new SizeInterpolator());
            RegisterStrategy<SizeF>(new SizeFInterpolator());
            RegisterStrategy<Point>(new PointInterpolator());
            RegisterStrategy<PointF>(new PointFInterpolator());
        }
    }

    /// <summary>
    /// 整数插值器，提供整数类型的插值计算。
    /// </summary>
    public class IntInterpolator : IInterpolatorStrategy
    {
        /// <summary>
        /// 计算整数类型的插值。
        /// </summary>
        /// <param name="startValue">起始整数值。</param>
        /// <param name="endValue">终止整数值。</param>
        /// <param name="progress">插值的进度。</param>
        /// <returns>插值后的整数结果。</returns>
        public object Interpolate(object startValue, object endValue, double progress)
        {
            int start = (int)startValue;
            int end = (int)endValue;
            return (int)(start + (end - start) * progress);
        }
    }

    /// <summary>
    /// 单精度浮点数插值器，提供单精度浮点数类型的插值计算。
    /// </summary>
    public class SingleInterpolator : IInterpolatorStrategy
    {
        /// <summary>
        /// 计算单精度浮点数类型的插值。
        /// </summary>
        /// <param name="startValue">起始单精度浮点数值。</param>
        /// <param name="endValue">终止单精度浮点数值。</param>
        /// <param name="progress">插值的进度。</param>
        /// <returns>插值后的单精度浮点数结果。</returns>
        public object Interpolate(object startValue, object endValue, double progress)
        {
            float start = (float)startValue;
            float end = (float)endValue;
            return start + (end - start) * (float)progress;
        }
    }

    /// <summary>
    /// 双精度浮点数插值器，提供双精度浮点数类型的插值计算。
    /// </summary>
    public class DoubleInterpolator : IInterpolatorStrategy
    {
        /// <summary>
        /// 计算双精度浮点数类型的插值。
        /// </summary>
        /// <param name="startValue">起始双精度浮点数值。</param>
        /// <param name="endValue">终止双精度浮点数值。</param>
        /// <param name="progress">插值的进度。</param>
        /// <returns>插值后的双精度浮点数结果。</returns>
        public object Interpolate(object startValue, object endValue, double progress)
        {
            double start = (double)startValue;
            double end = (double)endValue;
            return start + (end - start) * progress;
        }
    }

    /// <summary>
    /// 颜色插值器，提供颜色类型的插值计算。
    /// </summary>
    public class ColorInterpolator : IInterpolatorStrategy
    {
        /// <summary>
        /// 计算颜色类型的插值。
        /// </summary>
        /// <param name="startValue">起始颜色值。</param>
        /// <param name="endValue">终止颜色值。</param>
        /// <param name="progress">插值的进度。</param>
        /// <returns>插值后的颜色结果。</returns>
        public object Interpolate(object startValue, object endValue, double progress)
        {
            Color start = (Color)startValue;
            Color end = (Color)endValue;
            int R = start.R + (int)((end.R - start.R) * progress);
            int G = start.G + (int)((end.G - start.G) * progress);
            int B = start.B + (int)((end.B - start.B) * progress);
            return Color.FromArgb(R, G, B);
        }
    }

    /// <summary>
    /// 尺寸插值器，提供尺寸类型的插值计算。
    /// </summary>
    public class SizeInterpolator : IInterpolatorStrategy
    {
        /// <summary>
        /// 计算尺寸类型的插值。
        /// </summary>
        /// <param name="startValue">起始尺寸值。</param>
        /// <param name="endValue">终止尺寸值。</param>
        /// <param name="progress">插值的进度。</param>
        /// <returns>插值后的尺寸结果。</returns>
        public object Interpolate(object startValue, object endValue, double progress)
        {
            Size start = (Size)startValue;
            Size end = (Size)endValue;
            int newWidth = start.Width + (int)((end.Width - start.Width) * progress);
            int newHeight = start.Height + (int)((end.Height - start.Height) * progress);
            return new Size(newWidth, newHeight);
        }
    }

    /// <summary>
    /// 浮点尺寸插值器，提供浮点尺寸类型的插值计算。
    /// </summary>
    public class SizeFInterpolator : IInterpolatorStrategy
    {
        /// <summary>
        /// 计算浮点尺寸类型的插值。
        /// </summary>
        /// <param name="startValue">起始浮点尺寸值。</param>
        /// <param name="endValue">终止浮点尺寸值。</param>
        /// <param name="progress">插值的进度。</param>
        /// <returns>插值后的浮点尺寸结果。</returns>
        public object Interpolate(object startValue, object endValue, double progress)
        {
            SizeF start = (SizeF)startValue;
            SizeF end = (SizeF)endValue;
            float newWidthF = start.Width + (float)((end.Width - start.Width) * progress);
            float newHeightF = start.Height + (float)((end.Height - start.Height) * progress);
            return new SizeF(newWidthF, newHeightF);
        }
    }

    /// <summary>
    /// 点插值器，提供点类型的插值计算。
    /// </summary>
    public class PointInterpolator : IInterpolatorStrategy
    {
        /// <summary>
        /// 计算点类型的插值。
        /// </summary>
        /// <param name="startValue">起始点值。</param>
        /// <param name="endValue">终止点值。</param>
        /// <param name="progress">插值的进度。</param>
        /// <returns>插值后的点结果。</returns>
        public object Interpolate(object startValue, object endValue, double progress)
        {
            Point start = (Point)startValue;
            Point end = (Point)endValue;
            int newX = start.X + (int)((end.X - start.X) * progress);
            int newY = start.Y + (int)((end.Y - start.Y) * progress);
            return new Point(newX, newY);
        }
    }

    /// <summary>
    /// 浮点点插值器，提供浮点点类型的插值计算。
    /// </summary>
    public class PointFInterpolator : IInterpolatorStrategy
    {
        /// <summary>
        /// 计算浮点点类型的插值。
        /// </summary>
        /// <param name="startValue">起始浮点点值。</param>
        /// <param name="endValue">终止浮点点值。</param>
        /// <param name="progress">插值的进度。</param>
        /// <returns>插值后的浮点点结果。</returns>
        public object Interpolate(object startValue, object endValue, double progress)
        {
            PointF start = (PointF)startValue;
            PointF end = (PointF)endValue;
            float newX = start.X + (float)((end.X - start.X) * progress);
            float newY = start.Y + (float)((end.Y - start.Y) * progress);
            return new PointF(newX, newY);
        }
    }
}
