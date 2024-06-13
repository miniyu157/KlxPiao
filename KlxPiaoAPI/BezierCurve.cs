namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供贝塞尔曲线计算的函数。
    /// </summary>
    public class BezierCurve
    {
        /// <summary>
        /// 根据时间进度计算贝塞尔曲线上的点。
        /// </summary>
        /// <param name="timeProgress">时间进度，范围在0到1之间。</param>
        /// <param name="controlPoints">控制点数组。</param>
        public static PointF CalculateBezierPointByTime(double timeProgress, PointF[] controlPoints)
        {
            double epsilon = 1e-6;
            double lower = 0.0;
            double upper = 1.0;
            double t = (lower + upper) / 2.0;

            while (upper - lower > epsilon)
            {
                t = (lower + upper) / 2.0;
                PointF point = CalculateBezierPoint(t, controlPoints);
                double currentProgress = point.X;

                if (currentProgress < timeProgress)
                {
                    lower = t;
                }
                else
                {
                    upper = t;
                }
            }

            return CalculateBezierPoint(t, controlPoints);
        }
        /// <summary>
        /// 根据曲线进度计算贝塞尔曲线上的点。
        /// </summary>
        /// <param name="t">曲线进度，范围在0到1之间。</param>
        /// <param name="controlPoints">控制点数组。</param>
        public static PointF CalculateBezierPoint(double t, PointF[] controlPoints)
        {
            if (controlPoints == null || controlPoints.Length == 0)
                throw new ArgumentException("控制点数组不能为空");

            int n = controlPoints.Length - 1;
            return CalculateBezierPointRecursive(t, controlPoints, n);
        }
        /// <summary>
        /// 递归计算贝塞尔曲线上的点。
        /// </summary>
        /// <param name="t">曲线进度，范围在0到1之间。</param>
        /// <param name="controlPoints">控制点数组。</param>
        /// <param name="n">控制点的数量减一。</param>
        private static PointF CalculateBezierPointRecursive(double t, PointF[] controlPoints, int n)
        {
            if (n == 0)
                return controlPoints[0];

            PointF[] reducedPoints = new PointF[n];
            for (int i = 0; i < n; i++)
            {
                float x = (float)((1 - t) * controlPoints[i].X + t * controlPoints[i + 1].X);
                float y = (float)((1 - t) * controlPoints[i].Y + t * controlPoints[i + 1].Y);
                reducedPoints[i] = new PointF(x, y);
            }

            return CalculateBezierPointRecursive(t, reducedPoints, n - 1);
        }
    }
}