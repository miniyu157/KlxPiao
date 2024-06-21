namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供对齐和排板计算的帮助方法。
    /// </summary>
    public class LayoutUtilities
    {
        /// <summary>
        /// 根据指定的对齐方式，计算容器矩形内部矩形的位置。
        /// </summary>
        /// <param name="containerRect">包含内部大小的容器矩形。</param>
        /// <param name="internalSize">要在容器内对齐的内部矩形的大小。</param>
        /// <param name="alignment">内部矩形在容器内的对齐方式。</param>
        /// <param name="offset">偏移，用于调整内部矩形在容器中的位置。默认为null。</param>
        /// <returns>经过对齐和偏移调整后的内部矩形的位置。</returns>
        public static Point CalculateAlignedPosition(Rectangle containerRect, Size internalSize, ContentAlignment alignment, Point? offset = null)
        {
            int HLeft = containerRect.X;
            int HCenter = containerRect.X + (containerRect.Width - internalSize.Width) / 2;
            int HRight = containerRect.X + containerRect.Width - internalSize.Width;
            int VTop = containerRect.Y;
            int VCenter = containerRect.Y + (containerRect.Height - internalSize.Height) / 2;
            int VBottom = containerRect.Y + containerRect.Height - internalSize.Height;

            Point point = alignment switch
            {
                ContentAlignment.TopLeft => new Point(HLeft, VTop),
                ContentAlignment.TopCenter => new Point(HCenter, VTop),
                ContentAlignment.TopRight => new Point(HRight, VTop),
                ContentAlignment.MiddleLeft => new Point(HLeft, VCenter),
                ContentAlignment.MiddleCenter => new Point(HCenter, VCenter),
                ContentAlignment.MiddleRight => new Point(HRight, VCenter),
                ContentAlignment.BottomLeft => new Point(HLeft, VBottom),
                ContentAlignment.BottomCenter => new Point(HCenter, VBottom),
                ContentAlignment.BottomRight => new Point(HRight, VBottom),
                _ => Point.Empty
            };

            if (offset != null)
            {
                point.X += ((Point)offset).X;
                point.Y += ((Point)offset).Y;
            }

            return point;
        }

        /// <summary>
        /// 根据指定的对齐方式，计算容器矩形内部矩形的位置。
        /// </summary>
        /// <param name="containerRect">包含内部大小的容器矩形。</param>
        /// <param name="internalSize">要在容器内对齐的内部矩形的大小。</param>
        /// <param name="alignment">内部矩形在容器内的对齐方式。</param>
        /// <param name="offset">偏移，用于调整内部矩形在容器中的位置。默认为null。</param>
        /// <returns>经过对齐和偏移调整后的内部矩形的位置。</returns>
        public static PointF CalculateAlignedPosition(RectangleF containerRect, SizeF internalSize, ContentAlignment alignment, Point? offset = null)
        {
            float HLeft = containerRect.X;
            float HCenter = containerRect.X + (containerRect.Width - internalSize.Width) / 2;
            float HRight = containerRect.X + containerRect.Width - internalSize.Width;
            float VTop = containerRect.Y;
            float VCenter = containerRect.Y + (containerRect.Height - internalSize.Height) / 2;
            float VBottom = containerRect.Y + containerRect.Height - internalSize.Height;

            PointF pointF = alignment switch
            {
                ContentAlignment.TopLeft => new PointF(HLeft, VTop),
                ContentAlignment.TopCenter => new PointF(HCenter, VTop),
                ContentAlignment.TopRight => new PointF(HRight, VTop),
                ContentAlignment.MiddleLeft => new PointF(HLeft, VCenter),
                ContentAlignment.MiddleCenter => new PointF(HCenter, VCenter),
                ContentAlignment.MiddleRight => new PointF(HRight, VCenter),
                ContentAlignment.BottomLeft => new PointF(HLeft, VBottom),
                ContentAlignment.BottomCenter => new PointF(HCenter, VBottom),
                ContentAlignment.BottomRight => new PointF(HRight, VBottom),
                _ => PointF.Empty
            };

            if (offset != null)
            {
                pointF.X += ((PointF)offset).X;
                pointF.Y += ((PointF)offset).Y;
            }

            return pointF;
        }

        /// <summary>
        /// 根据指定的对齐方式，计算容器矩形内部矩形的位置。
        /// </summary>
        /// <param name="containerRect">包含内部大小的容器矩形。</param>
        /// <param name="internalSize">要在容器内对齐的内部矩形的大小。</param>
        /// <param name="alignment">内部矩形在容器内的对齐方式。</param>
        /// <param name="padding">偏移，用于调整内部矩形在容器中的位置。默认为null。</param>
        /// <returns>经过对齐和偏移调整后的内部矩形的位置。</returns>
        public static Point CalculateAlignedPosition(Rectangle containerRect, SizeF internalSize, ContentAlignment alignment, Point? offset = null)
        {
            return CalculateAlignedPosition(containerRect, new Size((int)internalSize.Width, (int)internalSize.Height), alignment, offset);
        }

        /// <summary>
        /// 根据指定的对齐方式，计算容器矩形内部矩形的位置。
        /// </summary>
        /// <param name="containerRect">包含内部大小的容器矩形。</param>
        /// <param name="internalSize">要在容器内对齐的内部矩形的大小。</param>
        /// <param name="alignment">内部矩形在容器内的对齐方式。</param>
        /// <param name="offset">偏移，用于调整内部矩形在容器中的位置。默认为null。</param>
        /// <returns>经过对齐和偏移调整后的内部矩形的位置。</returns>
        public static PointF CalculateAlignedPosition(RectangleF containerRect, Size internalSize, ContentAlignment alignment, Point? offset = null)
        {
            return CalculateAlignedPosition(containerRect, new SizeF(internalSize.Width, internalSize.Height), alignment, offset);
        }

        /// <summary>
        /// 根据指定的对齐方式，计算容器矩形内部矩形的位置。
        /// </summary>
        /// <param name="containerRect">包含内部大小的容器矩形。</param>
        /// <param name="internalSize">要在容器内对齐的内部矩形的大小。</param>
        /// <param name="alignment">内部矩形在容器内的对齐方式。</param>
        /// <param name="offset">偏移，用于调整内部矩形在容器中的位置。默认为null。</param>
        /// <returns>经过对齐和偏移调整后的内部矩形的位置。</returns>
        public static Point CalculateAlignedPosition(Rectangle containerRect, Size internalSize, ContentAlignment alignment, Padding? padding = null)
        {
            Point? offset = padding == null ? null : ConvertToPoint((Padding)padding);
            return CalculateAlignedPosition(containerRect, internalSize, alignment, offset);
        }

        /// <summary>
        /// 根据指定的对齐方式，计算容器矩形内部矩形的位置。
        /// </summary>
        /// <param name="containerRect">包含内部大小的容器矩形。</param>
        /// <param name="internalSize">要在容器内对齐的内部矩形的大小。</param>
        /// <param name="alignment">内部矩形在容器内的对齐方式。</param>
        /// <param name="offset">偏移，用于调整内部矩形在容器中的位置。默认为null。</param>
        /// <returns>经过对齐和偏移调整后的内部矩形的位置。</returns>
        public static PointF CalculateAlignedPosition(RectangleF containerRect, SizeF internalSize, ContentAlignment alignment, Padding? padding = null)
        {
            Point? offset = padding == null ? null : ConvertToPoint((Padding)padding);
            return CalculateAlignedPosition(containerRect, internalSize, alignment, offset);
        }

        /// <summary>
        /// 根据指定的对齐方式，计算容器矩形内部矩形的位置。
        /// </summary>
        /// <param name="containerRect">包含内部大小的容器矩形。</param>
        /// <param name="internalSize">要在容器内对齐的内部矩形的大小。</param>
        /// <param name="alignment">内部矩形在容器内的对齐方式。</param>
        /// <param name="offset">偏移，用于调整内部矩形在容器中的位置。默认为null。</param>
        /// <returns>经过对齐和偏移调整后的内部矩形的位置。</returns>
        public static Point CalculateAlignedPosition(Rectangle containerRect, SizeF internalSize, ContentAlignment alignment, Padding? padding = null)
        {
            Point? offset = padding == null ? null : ConvertToPoint((Padding)padding);
            return CalculateAlignedPosition(containerRect, new Size((int)internalSize.Width, (int)internalSize.Height), alignment, offset);
        }

        /// <summary>
        /// 根据指定的对齐方式，计算容器矩形内部矩形的位置。
        /// </summary>
        /// <param name="containerRect">包含内部大小的容器矩形。</param>
        /// <param name="internalSize">要在容器内对齐的内部矩形的大小。</param>
        /// <param name="alignment">内部矩形在容器内的对齐方式。</param>
        /// <param name="offset">偏移，用于调整内部矩形在容器中的位置。默认为null。</param>
        /// <returns>经过对齐和偏移调整后的内部矩形的位置。</returns>
        public static PointF CalculateAlignedPosition(RectangleF containerRect, Size internalSize, ContentAlignment alignment, Padding? padding = null)
        {
            Point? offset = padding == null ? null : ConvertToPoint((Padding)padding);
            return CalculateAlignedPosition(containerRect, new SizeF(internalSize.Width, internalSize.Height), alignment, offset);
        }

        /// <summary>
        /// 计算网格点，返回在指定容器大小中按给定单元大小和矩阵大小排列的点列表。
        /// </summary>
        /// <param name="容器大小">整个容器的大小。</param>
        /// <param name="单元大小">每个单元的大小。</param>
        /// <param name="矩阵大小">矩阵的行列数。</param>
        /// <param name="边距">容器的边距。</param>
        /// <returns>按矩阵排列的点列表。</returns>
        public static List<PointF> 计算网格点(SizeF 容器大小, SizeF 单元大小, Size 矩阵大小, Padding 边距)
        {
            float x间距 = (容器大小.Width - 边距.Left - 边距.Right - 单元大小.Width * 矩阵大小.Width) / (矩阵大小.Width - 1);
            float y间距 = (容器大小.Height - 边距.Top - 边距.Bottom - 单元大小.Height * 矩阵大小.Height) / (矩阵大小.Height - 1);

            List<PointF> points = [];

            for (int y = 0; y < 矩阵大小.Height; y++)
            {
                for (int x = 0; x < 矩阵大小.Width; x++)
                {
                    points.Add(new PointF(
                        边距.Left + x * (单元大小.Width + x间距),
                        边距.Top + y * (单元大小.Height + y间距)
                    ));
                }
            }
            return points;
        }

        public static Point ConvertToPoint(Padding padding)
        {
            return new(padding.Left - padding.Right, padding.Top - padding.Bottom);
        }
    }
}