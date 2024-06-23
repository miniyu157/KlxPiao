using static KlxPiaoAPI.TypeChecker;
using static KlxPiaoAPI.TypeInterpolator;

namespace KlxPiaoAPI
{
    public static class ControlTransitionAnimator
    {
        /// <summary>
        /// 一个委托类型，表示用户定义的曲线函数。
        /// </summary>
        /// <param name="progress">进度，范围在0到1之间。</param>
        public delegate double CustomProgressCurve(double progress);

        /// <summary>
        /// 将贝塞尔曲线应用于控件的过渡动画。
        /// </summary>
        /// <param name="control">要应用动画的控件。</param>
        /// <param name="property">要过渡的属性。</param>
        /// <param name="startValue">动画的起始值。</param>
        /// <param name="endValue">动画的结束值。</param>
        /// <param name="time">动画持续的时间（以毫秒为单位）。</param>
        /// <param name="controlPoints">贝塞尔曲线的控制点数组，留空时缓动效果为Linear。</param>
        /// <param name="FPS">动画的帧率。</param>
        /// <param name="action">每一帧动画完成时执行的操作。</param>
        /// <param name="token">用于取消动画的CancellationToken。</param>
        public static async Task BezierTransition(this Control control, string property, object? startValue, object endValue, int time, PointF[]? controlPoints = null, int FPS = 100, Action<double>? action = default, CancellationToken token = default)
        {
            DateTime startTime = DateTime.Now;
            TimeSpan totalDuration = TimeSpan.FromMilliseconds(time);
            bool isRunning = false; //true表示动画完成

            startValue ??= control.SetOrGetPropertyValue(property);
            controlPoints ??= [new(0, 0), new(1, 1)];

            ITypeCollection numberCollection = NumberType.Instance;
            ITypeCollection pointOrSizeCollection = PointOrSizeType.Instance;

            bool isColor = startValue is Color && endValue is Color;
            bool IsSingleValueType = startValue != null && startValue.GetType() == endValue.GetType() && startValue.IsTypes(numberCollection) && endValue.IsTypes(numberCollection);
            bool IsPointOrSize = startValue != null && startValue.GetType() == endValue.GetType() && startValue.IsTypes(pointOrSizeCollection) && endValue.IsTypes(pointOrSizeCollection);

            var types = (isColor, IsSingleValueType, IsPointOrSize);

            while (!isRunning)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                TimeSpan currentTime = DateTime.Now - startTime;
                double timeProgress = currentTime.TotalMilliseconds / totalDuration.TotalMilliseconds;

                if (timeProgress >= 1.0)
                {
                    isRunning = true;
                    control.Invoke(() => control.SetOrGetPropertyValue(property, endValue));
                }
                else
                {
                    double progress = BezierCurve.CalculateBezierPointByTime(timeProgress, controlPoints).Y;

                    if (startValue != null)
                    {
                        PropertyInterpolator(control, property, startValue, endValue, types, progress);
                    }
                    await Task.Delay(1000 / FPS, token);
                }

                action?.Invoke(timeProgress);
            }
        }

        /// <summary>
        /// 将贝塞尔曲线应用于控件的过渡动画。
        /// </summary>
        /// <param name="control">要应用动画的控件。</param>
        /// <param name="property">要过渡的属性。</param>
        /// <param name="startValue">动画的起始值。</param>
        /// <param name="endValue">动画的结束值。</param>
        /// <param name="animation">动画的基本属性，以 <see cref="Animation"/> 结构体表示。</param>
        /// <param name="action">每一帧动画完成时执行的操作。</param>
        /// <param name="token">用于取消动画的CancellationToken。</param>
        public static async Task BezierTransition(this Control control, string property, object? startValue, object endValue, Animation animation, Action<double>? action = default, CancellationToken token = default)
        {
            await BezierTransition(control, property, startValue, endValue, animation.Time, animation.Easing, animation.FPS, action, token);
        }

        /// <summary>
        /// 将贝塞尔曲线应用于控件的过渡动画。
        /// </summary>
        /// <param name="control">要应用动画的控件。</param>
        /// <param name="property">要过渡的属性。</param>
        /// <param name="startValue">动画的起始值。</param>
        /// <param name="endValue">动画的结束值。</param>
        /// <param name="time">动画持续的时间（以毫秒为单位）。</param>
        /// <param name="customProgressCurve">用户定义的缓动曲线表达式，以 <see cref="CustomProgressCurve"/> 委托类型表示。</param>
        /// <param name="FPS">动画的帧率。</param>
        /// <param name="action">每一帧动画完成时执行的操作。</param>
        /// <param name="token">用于取消动画的CancellationToken。</param>
        public static async Task CustomTransition(this Control control, string property, object? startValue, object endValue, int time, CustomProgressCurve customProgressCurve, int FPS = 100, Action<double>? action = default, CancellationToken token = default)
        {
            DateTime startTime = DateTime.Now;
            TimeSpan totalDuration = TimeSpan.FromMilliseconds(time);
            bool isRunning = false; //true表示动画完成

            startValue ??= control.SetOrGetPropertyValue(property);

            ITypeCollection numberCollection = NumberType.Instance;
            ITypeCollection pointOrSizeCollection = PointOrSizeType.Instance;

            bool isColor = startValue is Color && endValue is Color;
            bool IsSingleValueType = startValue != null && startValue.GetType() == endValue.GetType() && startValue.IsTypes(numberCollection) && endValue.IsTypes(numberCollection);
            bool IsPointOrSize = startValue != null && startValue.GetType() == endValue.GetType() && startValue.IsTypes(pointOrSizeCollection) && endValue.IsTypes(pointOrSizeCollection);

            var types = (isColor, IsSingleValueType, IsPointOrSize);

            while (!isRunning)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                TimeSpan currentTime = DateTime.Now - startTime;
                double timeProgress = currentTime.TotalMilliseconds / totalDuration.TotalMilliseconds;

                if (timeProgress >= 1.0)
                {
                    isRunning = true;
                    control.Invoke(() => control.SetOrGetPropertyValue(property, endValue));
                }
                else
                {
                    double progress = customProgressCurve(timeProgress); //使用用户自定义曲线表达式

                    if (startValue != null)
                    {
                        PropertyInterpolator(control, property, startValue, endValue, types, progress);
                    }
                    await Task.Delay(1000 / FPS, token);
                }

                action?.Invoke(timeProgress);
            }
        }

        //根据要过度的属性进行差值
        private static void PropertyInterpolator(Control control, string property, object startValue, object endValue, (bool isColor, bool IsSingleValueType, bool IsPointOrSize) types, double progress)
        {
            if (startValue == null)
            {
                return;
            }

            if (types.isColor)
            {
                Color newColor = Interpolator((Color)startValue, (Color)endValue, progress);

                control.Invoke(() => control.SetOrGetPropertyValue(property, newColor));
            }
            else if (types.IsSingleValueType)
            {
                double newValue = (double)Interpolator(startValue, endValue, progress);

                control.Invoke(() => control.SetOrGetPropertyValue(property, newValue));
            }
            else if (types.IsPointOrSize)
            {
                object newValue = Interpolator(startValue, endValue, progress);

                control.Invoke(() => control.SetOrGetPropertyValue(property, newValue));
            }
        }
    }
}