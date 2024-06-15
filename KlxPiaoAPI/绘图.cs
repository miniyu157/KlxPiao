using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供了 GDI+ 的扩展方法，用于快速绘制图形。
    /// </summary>
    public static class 绘图
    {
        /// <summary>
        /// 在指定区域绘制带有圆角的矩形。
        /// </summary>
        /// <param name="g">用于绘制的 Graphics 对象。</param>
        /// <param name="绘制区域">定义绘制的矩形区域</param>
        /// <param name="圆角大小">每个角的圆角大小，自动检测是百分比大小还是像素大小。</param>
        /// <param name="外部颜色">圆角区域外部颜色。</param>
        /// <param name="Pen或Brush">指示填充圆角矩形还是绘制圆角边框，其中包含的Color是边框颜色或内部颜色。</param>
        /// <exception cref="Exception">提供非 Pen 或 SolidBrush 时抛出。</exception>
        public static void 绘制圆角(this Graphics g, Rectangle 绘制区域, CornerRadius 圆角大小, Color 外部颜色, object Pen或Brush)
        {
            if (绘制区域.Width == 0 || 绘制区域.Height == 0)
            {
                return;
            }

            if (Pen或Brush is Pen pen)
            {
                pen = new(pen.Color, pen.Width * 2); //修正画笔大小

                if (pen.Width != 0) //不绘制边框
                {
                    GraphicsPath 圆角路径 = 转为圆角路径(绘制区域, 圆角大小);
                    g.DrawPath(pen, 圆角路径);
                }

                GraphicsPath 外部路径 = 转为圆角路径(绘制区域, 圆角大小, true);
                g.FillPath(new SolidBrush(外部颜色), 外部路径);
            }
            else if (Pen或Brush is SolidBrush brush)
            {
                GraphicsPath 圆角路径 = 转为圆角路径(绘制区域, 圆角大小);
                g.FillPath(brush, 圆角路径);

                GraphicsPath 外部路径 = 转为圆角路径(绘制区域, 圆角大小, true);
                g.FillPath(new SolidBrush(外部颜色), 外部路径);
            }
            else
            {
                throw new Exception("参数 'Pen或Brush' 的类型必须为 Pen 或 SolidBrush ");
            }
        }

        /// <summary>
        /// 将一个矩形转换为圆角路径。
        /// </summary>
        /// <param name="rect">提供的矩形。</param>
        /// <param name="圆角大小">每个角的圆角大小，自动检测是百分比大小还是像素大小。</param>
        /// <param name="返回外部路径">是否返回除圆角区域外的路径</param>
        /// <returns>表示圆角路径的 GraphicsPath。</returns>
        public static GraphicsPath 转为圆角路径(Rectangle rect, CornerRadius 圆角大小, bool 返回外部路径 = false)
        {
            //当做百分比时，使用较短的一侧
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
            float TopLeft = 圆角大小.TopLeft;
            float TopRight = 圆角大小.TopRight;
            float BottomRight = 圆角大小.BottomRight;
            float BottomLeft = 圆角大小.BottomLeft;

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

            try
            {
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            圆角路径.CloseAllFigures();

            if (!返回外部路径)
            {
                return 圆角路径;
            }
            else
            {
                GraphicsPath 外部路径 = new();
                try
                {
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
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                return 外部路径;
            }
        }
    }
}