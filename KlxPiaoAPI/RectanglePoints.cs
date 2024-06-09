namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供获取矩形各个位置点的方法。
    /// </summary>
    public static class RectanglePoints
    {
        #region RectangleF扩展方法
        /// <summary>
        /// 获取矩形顶部中心点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回顶部中心点的坐标。</returns>
        public static PointF GetTopCenterPoint(this RectangleF rect)
        {
            return new PointF(rect.X + rect.Width / 2, rect.Y);
        }

        /// <summary>
        /// 获取矩形底部中心点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回底部中心点的坐标。</returns>
        public static PointF GetBottomCenterPoint(this RectangleF rect)
        {
            return new PointF(rect.X + rect.Width / 2, rect.Bottom);
        }

        /// <summary>
        /// 获取矩形左侧中心点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回左侧中心点的坐标。</returns>
        public static PointF GetLeftCenterPoint(this RectangleF rect)
        {
            return new PointF(rect.X, rect.Y + rect.Height / 2);
        }

        /// <summary>
        /// 获取矩形右侧中心点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回右侧中心点的坐标。</returns>
        public static PointF GetRightCenterPoint(this RectangleF rect)
        {
            return new PointF(rect.Right, rect.Y + rect.Height / 2);
        }

        /// <summary>
        /// 获取矩形左上角点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回左上角点的坐标。</returns>
        public static PointF GetTopLeftPoint(this RectangleF rect)
        {
            return new PointF(rect.X, rect.Y);
        }

        /// <summary>
        /// 获取矩形右上角点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回右上角点的坐标。</returns>
        public static PointF GetTopRightPoint(this RectangleF rect)
        {
            return new PointF(rect.Right, rect.Y);
        }

        /// <summary>
        /// 获取矩形右下角点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回右下角点的坐标。</returns>
        public static PointF GetBottomRightPoint(this RectangleF rect)
        {
            return new PointF(rect.Right, rect.Bottom);
        }

        /// <summary>
        /// 获取矩形左下角点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回左下角点的坐标。</returns>
        public static PointF GetBottomLeftPoint(this RectangleF rect)
        {
            return new PointF(rect.X, rect.Bottom);
        }
        #endregion

        #region Rectangle扩展方法
        /// <summary>
        /// 获取矩形顶部中心点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回顶部中心点的坐标。</returns>
        public static Point GetTopCenterPoint(this Rectangle rect)
        {
            return new Point(rect.X + rect.Width / 2, rect.Y);
        }

        /// <summary>
        /// 获取矩形底部中心点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回底部中心点的坐标。</returns>
        public static Point GetBottomCenterPoint(this Rectangle rect)
        {
            return new Point(rect.X + rect.Width / 2, rect.Bottom);
        }

        /// <summary>
        /// 获取矩形左侧中心点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回左侧中心点的坐标。</returns>
        public static Point GetLeftCenterPoint(this Rectangle rect)
        {
            return new Point(rect.X, rect.Y + rect.Height / 2);
        }

        /// <summary>
        /// 获取矩形右侧中心点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回右侧中心点的坐标。</returns>
        public static Point GetRightCenterPoint(this Rectangle rect)
        {
            return new Point(rect.Right, rect.Y + rect.Height / 2);
        }

        /// <summary>
        /// 获取矩形左上角点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回左上角点的坐标。</returns>
        public static Point GetTopLeftPoint(this Rectangle rect)
        {
            return new Point(rect.X, rect.Y);
        }

        /// <summary>
        /// 获取矩形右上角点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回右上角点的坐标。</returns>
        public static Point GetTopRightPoint(this Rectangle rect)
        {
            return new Point(rect.Right, rect.Y);
        }

        /// <summary>
        /// 获取矩形右下角点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回右下角点的坐标。</returns>
        public static Point GetBottomRightPoint(this Rectangle rect)
        {
            return new Point(rect.Right, rect.Bottom);
        }

        /// <summary>
        /// 获取矩形左下角点。
        /// </summary>
        /// <param name="rect">矩形对象。</param>
        /// <returns>返回左下角点的坐标。</returns>
        public static Point GetBottomLeftPoint(this Rectangle rect)
        {
            return new Point(rect.X, rect.Bottom);
        }
        #endregion
    }
}