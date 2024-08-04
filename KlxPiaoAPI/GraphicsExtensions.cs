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
        /// <param name="g">用于绘制的 <see cref="Graphics"/> 对象。</param>
        /// <param name="rect">定义绘制的矩形区域。</param>
        /// <param name="cornerRadius">圆角半径，以 <see cref="CornerRadius"/> 结构体表示。</param>
        /// <param name="clear">圆角区域外部颜色（基底颜色）。</param>
        /// <param name="pen">指定绘制边框的画笔。</param>
        public static void DrawRounded(this Graphics g, Rectangle rect, CornerRadius cornerRadius, Color clear, Pen pen)
        {
            if (rect.Width == 0 || rect.Height == 0)
            {
                return;
            }

            //修正画笔大小
            Pen newPen = (Pen)pen.Clone();
            newPen.Width = pen.Width * 2;

            //绘制边框
            if (newPen.Width != 0)
            {
                GraphicsPath roundedPath = rect.ConvertToRoundedPath(cornerRadius);
                g.DrawPath(newPen, roundedPath);
            }

            //填充外部
            GraphicsPath externalPath = rect.ConvertToRoundedPath(cornerRadius, true);
            g.FillPath(new SolidBrush(clear), externalPath);
        }

        /// <summary>
        /// 在指定区域绘制带有圆角的矩形。
        /// </summary>
        /// <param name="g">用于绘制的 <see cref="Graphics"/> 对象。</param>
        /// <param name="rect">定义绘制的矩形区域。</param>
        /// <param name="cornerRadius">圆角半径，以 <see cref="CornerRadius"/> 结构体表示。</param>
        /// <param name="clear">圆角区域外部颜色（基底颜色）。</param>
        /// <param name="brush">指定填充的 <see cref="SolidBrush"/>。</param>
        public static void DrawRounded(this Graphics g, Rectangle rect, CornerRadius cornerRadius, Color clear, SolidBrush brush)
        {
            if (rect.Width == 0 || rect.Height == 0)
            {
                return;
            }

            //填充内部
            GraphicsPath roundedPath = rect.ConvertToRoundedPath(cornerRadius);
            g.FillPath(brush, roundedPath);

            //填充外部
            GraphicsPath externalPath = rect.ConvertToRoundedPath(cornerRadius, true);
            g.FillPath(new SolidBrush(clear), externalPath);
        }

        /// <summary>
        /// 将一个矩形转换为圆角路径。
        /// </summary>
        /// <param name="rect">提供的矩形。</param>
        /// <param name="cornerRadius">角半径，以 <see cref="CornerRadius"/> 结构体表示。</param>
        /// <param name="returnOuterPath">是否返回除圆角区域外的路径。</param>
        /// <returns>表示圆角路径的 <see cref="GraphicsPath"/> 。</returns>
        public static GraphicsPath ConvertToRoundedPath(this Rectangle rect, CornerRadius cornerRadius, bool returnOuterPath = false)
        {
            //作为百分比时，使用较短的一侧
            float referenceSize = Math.Min(rect.Width, rect.Height);

            //获取各个点
            Point topCenterPoint = rect.GetTopCenterPoint();
            Point bottomCenterPoint = rect.GetBottomCenterPoint();
            Point leftCenterPoint = rect.GetLeftCenterPoint();
            Point rightCenterPoint = rect.GetRightCenterPoint();
            Point topLeftPoint = rect.GetTopLeftPoint();
            Point topRightPoint = rect.GetTopRightPoint();
            Point bottomLeftPoint = rect.GetBottomLeftPoint();
            Point bottomRightPoint = rect.GetBottomRightPoint();

            //角半径
            float topLeft = cornerRadius.TopLeft;
            float topRight = cornerRadius.TopRight;
            float bottomRight = cornerRadius.BottomRight;
            float bottomLeft = cornerRadius.BottomLeft;

            //矫正圆角大小使其在合理范围之内
            if (topLeft > referenceSize) topLeft = referenceSize;
            if (topRight > referenceSize) topRight = referenceSize;
            if (bottomRight > referenceSize) bottomRight = referenceSize;
            if (bottomLeft > referenceSize) bottomLeft = referenceSize;

            //四个圆角区域的矩形
            Size GetCornerRectSize(double cR) => cR switch
            {
                > 0 and <= 1 => new Size((int)(referenceSize * cR), (int)(referenceSize * cR)),
                > 1 => new Size((int)cR, (int)cR),
                _ => Size.Empty
            };

            Size topLeftRectSize = GetCornerRectSize(topLeft);
            Size topRightRectSize = GetCornerRectSize(topRight);
            Size bottomRightRectSize = GetCornerRectSize(bottomRight);
            Size bottomLeftRectSize = GetCornerRectSize(bottomLeft);

            Point topLeftRectPos = new(rect.X, rect.Y);
            Point topRightRectPos = new(rect.Right - topRightRectSize.Width, rect.Y);
            Point bottomRightRectPos = new(rect.Right - bottomRightRectSize.Width, rect.Bottom - bottomRightRectSize.Height);
            Point bottomLeftRectPos = new(rect.X, rect.Bottom - bottomLeftRectSize.Height);

            Rectangle topLeftRect = new(topLeftRectPos, topLeftRectSize);
            Rectangle topRightRect = new(topRightRectPos, topRightRectSize);
            Rectangle bottomRightRect = new(bottomRightRectPos, bottomRightRectSize);
            Rectangle bottomLeftRect = new(bottomLeftRectPos, bottomLeftRectSize);

            void AddRoundedPath(GraphicsPath graphicsPath, float cR, Rectangle cRRect, Point cRLine1, Point cRLine2, int arc1, int arc2)
            {
                if (!returnOuterPath)
                {
                    if (cR == 0)
                        graphicsPath.AddLine(cRLine1, cRLine2);
                    else
                        graphicsPath.AddArc(cRRect, arc1, arc2);
                }
                else if (cR != 0)
                {
                    graphicsPath.AddArc(cRRect, arc1, arc2);
                    graphicsPath.AddLine(cRLine1, cRLine2);
                    graphicsPath.CloseFigure();
                }
            }

            if (!returnOuterPath)
            {
                GraphicsPath roundedPath = new();
                AddRoundedPath(roundedPath, topLeft, topLeftRect, leftCenterPoint, topLeftPoint, 180, 90);
                AddRoundedPath(roundedPath, topRight, topRightRect, topCenterPoint, topRightPoint, 270, 90);
                AddRoundedPath(roundedPath, bottomRight, bottomRightRect, rightCenterPoint, bottomRightPoint, 0, 90);
                AddRoundedPath(roundedPath, bottomLeft, bottomLeftRect, bottomCenterPoint, bottomLeftPoint, 90, 90);
                roundedPath.CloseAllFigures();
                return roundedPath;
            }
            else
            {
                GraphicsPath externalPath = new();
                AddRoundedPath(externalPath, topLeft, topLeftRect, topLeftPoint, leftCenterPoint, 180, 90);
                AddRoundedPath(externalPath, topRight, topRightRect, topRightPoint, topCenterPoint, 270, 90);
                AddRoundedPath(externalPath, bottomRight, bottomRightRect, bottomRightPoint, rightCenterPoint, 0, 90);
                AddRoundedPath(externalPath, bottomLeft, bottomLeftRect, bottomLeftPoint, bottomCenterPoint, 90, 90);
                externalPath.CloseAllFigures();
                return externalPath;
            }
        }
    }
}