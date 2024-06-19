using System.Drawing.Drawing2D;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供了 <see cref="Graphics"/> 的扩展方法，用于快速绘制图形。
    /// </summary>
    public static class GraphicsExtensions
    {
        /// <summary>
        /// 在指定区域绘制带有圆角的矩形。
        /// </summary>
        /// <param name="g">用于绘制的 Graphics 对象。</param>
        /// <param name="rect">定义绘制的矩形区域</param>
        /// <param name="cornerRadius">每个角的圆角大小，自动检测是百分比大小还是像素大小。</param>
        /// <param name="clear">圆角区域外部颜色。</param>
        /// <param name="pen">指定绘制边框的画笔。</param>
        public static void DrawRounded(this Graphics g, Rectangle rect, CornerRadius cornerRadius, Color clear, Pen pen)
        {
            if (rect.Width == 0 || rect.Height == 0)
            {
                return;
            }

            pen = new(pen.Color, pen.Width * 2); //修正画笔大小

            //绘制边框
            if (pen.Width != 0)
            {
                GraphicsPath 圆角路径 = ConvertToRoundedPath(rect, cornerRadius);
                g.DrawPath(pen, 圆角路径);
            }

            //填充外部
            GraphicsPath 外部路径 = ConvertToRoundedPath(rect, cornerRadius, true);
            g.FillPath(new SolidBrush(clear), 外部路径);

        }

        /// <summary>
        /// 在指定区域绘制带有圆角的矩形。
        /// </summary>
        /// <param name="g">用于绘制的 Graphics 对象。</param>
        /// <param name="rect">定义绘制的矩形区域</param>
        /// <param name="cornerRadius">每个角的圆角大小，自动检测是百分比大小还是像素大小。</param>
        /// <param name="clear">圆角区域外部颜色。</param>
        /// <param name="brush">指定填充的 <see cref="SolidBrush"/></param>
        public static void DrawRounded(this Graphics g, Rectangle rect, CornerRadius cornerRadius, Color clear, SolidBrush brush)
        {
            if (rect.Width == 0 || rect.Height == 0)
            {
                return;
            }

            //填充内部
            GraphicsPath 圆角路径 = ConvertToRoundedPath(rect, cornerRadius);
            g.FillPath(brush, 圆角路径);

            //填充外部
            GraphicsPath 外部路径 = ConvertToRoundedPath(rect, cornerRadius, true);
            g.FillPath(new SolidBrush(clear), 外部路径);

        }

        /// <summary>
        /// 将一个矩形转换为圆角路径。
        /// </summary>
        /// <param name="rect">提供的矩形。</param>
        /// <param name="cornerRadius">每个角的圆角大小，自动检测是百分比大小还是像素大小。</param>
        /// <param name="returnOuterPath">是否返回除圆角区域外的路径</param>
        /// <returns>表示圆角路径的 <see cref="GraphicsPath"/> 。</returns>
        public static GraphicsPath ConvertToRoundedPath(Rectangle rect, CornerRadius cornerRadius, bool returnOuterPath = false)
        {
            //作为百分比时，使用较短的一侧
            float 基准尺寸 = Math.Min(rect.Width, rect.Height);

            //获取顶点和中点
            Point TopCenterPoint = rect.GetTopCenterPoint();
            Point BottomCenterPoint = rect.GetBottomCenterPoint();
            Point LeftCenterPoint = rect.GetLeftCenterPoint();
            Point RightCenterPoint = rect.GetRightCenterPoint();
            Point TopLeftPoint = rect.GetTopLeftPoint();
            Point TopRightPoint = rect.GetTopRightPoint();
            Point BottomLeftPoint = rect.GetBottomLeftPoint();
            Point BottomRightPoint = rect.GetBottomRightPoint();

            GraphicsPath 圆角路径 = new();

            //圆角数据
            float TopLeft = cornerRadius.TopLeft;
            float TopRight = cornerRadius.TopRight;
            float BottomRight = cornerRadius.BottomRight;
            float BottomLeft = cornerRadius.BottomLeft;

            //矫正圆角大小使其在合理范围之内
            if (TopLeft > 基准尺寸) TopLeft = 基准尺寸;
            if (TopRight > 基准尺寸) TopRight = 基准尺寸;
            if (BottomRight > 基准尺寸) BottomRight = 基准尺寸;
            if (BottomLeft > 基准尺寸) BottomLeft = 基准尺寸;

            //四个圆角区域的矩形
            Rectangle TopLeftRect = Rectangle.Empty;
            Rectangle TopRightRect = Rectangle.Empty;
            Rectangle BottomRightRect = Rectangle.Empty;
            Rectangle BottomLeftRect = Rectangle.Empty;

            //左上角路径
            if (TopLeft == 0F)
            {
                圆角路径.AddLine(LeftCenterPoint, TopLeftPoint);
            }
            else
            {
                TopLeftRect.Size = TopLeft switch
                {
                    > 0 and <= 1 => new((int)(基准尺寸 * TopLeft), (int)(基准尺寸 * TopLeft)),
                    > 1 => new((int)TopLeft, (int)TopLeft),
                    _ => Size.Empty
                };
                TopLeftRect.Location = new(rect.X, rect.Y);
                圆角路径.AddArc(TopLeftRect, 180, 90);
            }

            //右上角路径
            if (TopRight == 0F)
            {
                圆角路径.AddLine(TopCenterPoint, TopRightPoint);
            }
            else
            {
                TopRightRect.Size = TopRight switch
                {
                    > 0 and <= 1 => new((int)(基准尺寸 * TopRight), (int)(基准尺寸 * TopRight)),
                    > 1 => new((int)TopRight, (int)TopRight),
                    _ => Size.Empty
                };
                TopRightRect.Location = new(rect.Right - TopRightRect.Width, rect.Y);
                圆角路径.AddArc(TopRightRect, 270, 90);
            }

            //右下角路径
            if (BottomRight == 0F)
            {
                圆角路径.AddLine(RightCenterPoint, BottomRightPoint);
            }
            else
            {
                BottomRightRect.Size = BottomRight switch
                {
                    > 0 and <= 1 => new((int)(基准尺寸 * BottomRight), (int)(基准尺寸 * BottomRight)),
                    > 1 => new((int)BottomRight, (int)BottomRight),
                    _ => Size.Empty
                };
                BottomRightRect.Location = new(rect.Right - BottomRightRect.Width, rect.Bottom - BottomRightRect.Height);
                圆角路径.AddArc(BottomRightRect, 0, 90);
            }

            //左下角路径
            if (BottomLeft == 0F)
            {
                圆角路径.AddLine(BottomCenterPoint, BottomLeftPoint);
            }
            else
            {
                BottomLeftRect.Size = BottomLeft switch
                {
                    > 0 and <= 1 => new((int)(基准尺寸 * BottomLeft), (int)(基准尺寸 * BottomLeft)),
                    > 1 => new((int)BottomLeft, (int)BottomLeft),
                    _ => Size.Empty
                };
                BottomLeftRect.Location = new(rect.X, rect.Bottom - BottomLeftRect.Height);
                圆角路径.AddArc(BottomLeftRect, 90, 90);
            }

            圆角路径.CloseAllFigures();

            if (!returnOuterPath)
            {
                return 圆角路径;
            }
            else
            {
                GraphicsPath 外部路径 = new();

                if (TopLeft != 0)
                {
                    外部路径.AddArc(TopLeftRect, 180, 90);
                    外部路径.AddLine(TopLeftPoint, LeftCenterPoint);
                    外部路径.CloseFigure();
                }
                if (TopRight != 0)
                {
                    外部路径.AddArc(TopRightRect, 270, 90);
                    外部路径.AddLine(TopRightPoint, TopCenterPoint);
                    外部路径.CloseFigure();
                }
                if (BottomRight != 0)
                {
                    外部路径.AddArc(BottomRightRect, 0, 90);
                    外部路径.AddLine(BottomRightPoint, RightCenterPoint);
                    外部路径.CloseFigure();
                }
                if (BottomLeft != 0)
                {
                    外部路径.AddArc(BottomLeftRect, 90, 90);
                    外部路径.AddLine(BottomLeftPoint, BottomCenterPoint);
                    外部路径.CloseFigure();
                }

                return 外部路径;
            }
        }
    }
}