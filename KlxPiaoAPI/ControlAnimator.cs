namespace KlxPiaoAPI
{
    /// <summary>
    /// 控件过渡动画类，提供使用贝塞尔曲线或自定义缓动函数进行过渡动画的方法。
    /// </summary>
    public static class ControlAnimator
    {
        /// <summary>
        /// 使用贝塞尔曲线进行过渡动画。
        /// </summary>
        /// <typeparam name="T">动画值的类型。</typeparam>
        /// <param name="startValue">动画的起始值。</param>
        /// <param name="endValue">动画的终止值。</param>
        /// <param name="animationInfo">动画信息，包括时长和缓动曲线等。</param>
        /// <param name="setValue">用于设置动画中间值的委托。</param>
        /// <param name="isCheckControlPoint">是否检查控制点，如果为 true，保证控制点的起始和终止分别为 (0,0) 和 (1,1)。</param>
        /// <param name="token">用于取消动画的 <see cref="CancellationToken"/>。</param>
        /// <returns>表示异步动画操作的 <see cref="Task"/>。</returns>
        public static async Task BezierTransition<T>(T startValue, T endValue, AnimationInfo animationInfo, Action<T> setValue, bool isCheckControlPoint = true, CancellationToken token = default) where T : notnull
        {
            PointF[] controlPoints = EasingUtils.ParseEasing(animationInfo.Easing);

            DateTime startTime = DateTime.Now;
            TimeSpan totalDuration = TimeSpan.FromMilliseconds(animationInfo.Time);
            bool isRunning = false;

            if (endValue.Equals(startValue))
            {
                return;
            }

            if (isCheckControlPoint)
            {
                var newControlPoints = controlPoints.ToList();
                if (controlPoints[0] != new PointF(0, 0)) newControlPoints.Insert(0, new PointF(0, 0));
                if (controlPoints.First() != new PointF(1, 1)) newControlPoints.Add(new PointF(1, 1));
                controlPoints = [.. newControlPoints];
            }

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
                    setValue(endValue);
                }
                else
                {
                    double progress = BezierCurve.CalculateBezierPointByTime(timeProgress, controlPoints).Y;

                    if (startValue != null)
                    {
                        setValue(TypeInterpolator.Interpolate(startValue, endValue, progress));
                    }
                    await Task.Delay(1000 / animationInfo.FPS, token);
                }
            }
        }

        /// <summary>
        /// 一个委托类型，表示用户定义的缓动函数。
        /// </summary>
        /// <param name="progress">进度，范围在 0-1 之间。</param>
        public delegate double CustomEasingDelegate(double progress);

        /// <summary>
        /// 使用自定义缓动函数进行过渡动画。
        /// </summary>
        /// <typeparam name="T">动画值的类型。</typeparam>
        /// <param name="startValue">动画的起始值。</param>
        /// <param name="endValue">动画的终止值。</param>
        /// <param name="time">动画持续时间，单位为毫秒。</param>
        /// <param name="fps">动画的帧率，单位为每秒帧数。</param>
        /// <param name="customEasing">自定义缓动函数，用于计算动画进度。</param>
        /// <param name="setValue">用于设置动画中间值的委托。</param>
        /// <param name="token">用于取消动画的 <see cref="CancellationToken"/>。</param>
        /// <returns>表示异步动画操作的 <see cref="Task"/>。</returns>
        public static async Task CustomTransition<T>(T startValue, T endValue, int time, int fps, CustomEasingDelegate customEasing, Action<T> setValue, CancellationToken token = default) where T : notnull
        {
            DateTime startTime = DateTime.Now;
            TimeSpan totalDuration = TimeSpan.FromMilliseconds(time);
            bool isRunning = false;

            if (endValue.Equals(startValue))
            {
                return;
            }

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
                    setValue(endValue);
                }
                else
                {
                    double progress = customEasing(timeProgress);

                    if (startValue != null)
                    {
                        setValue(TypeInterpolator.Interpolate(startValue, endValue, progress));
                    }
                    await Task.Delay(1000 / fps, token);
                }
            }
        }
    }
}