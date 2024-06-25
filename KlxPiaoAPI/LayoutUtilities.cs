namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供对齐和排板计算的帮助方法。
    /// </summary>
    public class LayoutUtilities
    {
        #region (8 个重载) CalculateAlignedPosition
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
        #endregion

        #region (2 个重载) AdjustContentAlignment
        /// <summary>
        /// 根据指定的水平对齐方式调整 <see cref="ContentAlignment"/>。
        /// </summary>
        /// <param name="baseAlignment">要根据其进行调整的基本水平对齐方式。</param>
        /// <param name="currentContentAlignment">要调整的当前 <see cref="ContentAlignment"/>。</param>
        /// <returns>根据基本水平对齐方式调整后的 <see cref="ContentAlignment"/>。</returns>
        public static ContentAlignment AdjustContentAlignment(HorizontalAlignment baseAlignment, ContentAlignment currentContentAlignment)
        {
            return (baseAlignment, currentContentAlignment) switch
            {
                (HorizontalAlignment.Left, ContentAlignment.TopCenter or ContentAlignment.TopRight) => ContentAlignment.TopLeft,
                (HorizontalAlignment.Left, ContentAlignment.MiddleCenter or ContentAlignment.MiddleRight) => ContentAlignment.MiddleLeft,
                (HorizontalAlignment.Left, ContentAlignment.BottomCenter or ContentAlignment.BottomRight) => ContentAlignment.BottomLeft,

                (HorizontalAlignment.Center, ContentAlignment.TopLeft or ContentAlignment.TopRight) => ContentAlignment.TopCenter,
                (HorizontalAlignment.Center, ContentAlignment.MiddleLeft or ContentAlignment.MiddleRight) => ContentAlignment.MiddleCenter,
                (HorizontalAlignment.Center, ContentAlignment.BottomLeft or ContentAlignment.BottomRight) => ContentAlignment.BottomCenter,

                (HorizontalAlignment.Right, ContentAlignment.TopLeft or ContentAlignment.TopCenter) => ContentAlignment.TopRight,
                (HorizontalAlignment.Right, ContentAlignment.MiddleLeft or ContentAlignment.MiddleCenter) => ContentAlignment.MiddleRight,
                (HorizontalAlignment.Right, ContentAlignment.BottomLeft or ContentAlignment.BottomCenter) => ContentAlignment.BottomRight,

                _ => ContentAlignment.MiddleCenter
            };
        }

        /// <summary>
        /// 根据指定的竖直对齐方式调整 <see cref="ContentAlignment"/>。
        /// </summary>
        /// <param name="baseAlignment">要根据其进行调整的基本竖直对齐方式。</param>
        /// <param name="currentContentAlignment">要调整的当前ContentAlignment。</param>
        /// <returns>根据基本竖直对齐方式调整后的 <see cref="ContentAlignment"/>。</returns>
        public static ContentAlignment AdjustContentAlignment(System.Windows.Forms.VisualStyles.VerticalAlignment baseAlignment, ContentAlignment currentContentAlignment)
        {
            return (baseAlignment, currentContentAlignment) switch
            {
                (System.Windows.Forms.VisualStyles.VerticalAlignment.Top, ContentAlignment.MiddleLeft or ContentAlignment.BottomLeft) => ContentAlignment.TopLeft,
                (System.Windows.Forms.VisualStyles.VerticalAlignment.Top, ContentAlignment.MiddleCenter or ContentAlignment.MiddleRight) => ContentAlignment.TopCenter,
                (System.Windows.Forms.VisualStyles.VerticalAlignment.Top, ContentAlignment.TopRight or ContentAlignment.BottomRight) => ContentAlignment.TopRight,

                (System.Windows.Forms.VisualStyles.VerticalAlignment.Center, ContentAlignment.TopLeft or ContentAlignment.BottomLeft) => ContentAlignment.MiddleCenter,
                (System.Windows.Forms.VisualStyles.VerticalAlignment.Center, ContentAlignment.MiddleLeft or ContentAlignment.MiddleRight) => ContentAlignment.MiddleCenter,
                (System.Windows.Forms.VisualStyles.VerticalAlignment.Center, ContentAlignment.TopRight or ContentAlignment.BottomRight) => ContentAlignment.MiddleCenter,

                (System.Windows.Forms.VisualStyles.VerticalAlignment.Bottom, ContentAlignment.TopLeft or ContentAlignment.MiddleLeft) => ContentAlignment.BottomLeft,
                (System.Windows.Forms.VisualStyles.VerticalAlignment.Bottom, ContentAlignment.TopCenter or ContentAlignment.MiddleCenter) => ContentAlignment.BottomCenter,
                (System.Windows.Forms.VisualStyles.VerticalAlignment.Bottom, ContentAlignment.TopRight or ContentAlignment.MiddleRight) => ContentAlignment.BottomRight,
                _ => ContentAlignment.MiddleCenter
            };
        }
        #endregion

        #region GetHorizontalAlignment & GetVerticalAlignment
        /// <summary>
        /// 根据给定的 <see cref="ContentAlignment"/> 获取水平对齐方式。
        /// </summary>
        /// <param name="contentAlignment">内容对齐方式</param>
        /// <returns>水平对齐方式</returns>
        public static HorizontalAlignment GetHorizontalAlignment(ContentAlignment contentAlignment)
        {
            return contentAlignment switch
            {
                ContentAlignment.TopLeft or ContentAlignment.MiddleLeft or ContentAlignment.BottomLeft => HorizontalAlignment.Left,
                ContentAlignment.TopCenter or ContentAlignment.MiddleCenter or ContentAlignment.BottomCenter => HorizontalAlignment.Center,
                ContentAlignment.TopRight or ContentAlignment.MiddleRight or ContentAlignment.BottomRight => HorizontalAlignment.Right,
                _ => HorizontalAlignment.Center
            };
        }

        /// <summary>
        /// 根据给定的 <see cref="ContentAlignment"/> 获取垂直对齐方式。
        /// </summary>
        /// <param name="contentAlignment">内容对齐方式</param>
        /// <returns>垂直对齐方式</returns>
        public static System.Windows.Forms.VisualStyles.VerticalAlignment GetVerticalAlignment(ContentAlignment contentAlignment)
        {
            return contentAlignment switch
            {
                ContentAlignment.TopLeft or ContentAlignment.TopCenter or ContentAlignment.TopRight => System.Windows.Forms.VisualStyles.VerticalAlignment.Top,
                ContentAlignment.MiddleLeft or ContentAlignment.MiddleCenter or ContentAlignment.MiddleRight => System.Windows.Forms.VisualStyles.VerticalAlignment.Center,
                ContentAlignment.BottomLeft or ContentAlignment.BottomCenter or ContentAlignment.BottomRight => System.Windows.Forms.VisualStyles.VerticalAlignment.Bottom,
                _ => System.Windows.Forms.VisualStyles.VerticalAlignment.Center
            };
        }
        #endregion

        /// <summary>
        /// 计算网格点，返回在指定容器大小中按给定单元大小和矩阵大小排列的点列表。
        /// </summary>
        /// <param name="containerSize">整个容器的大小。</param>
        /// <param name="cellSize">每个单元的大小。</param>
        /// <param name="matrixSize">矩阵的行列数。</param>
        /// <param name="padding">内边距。</param>
        /// <returns>按矩阵排列的点列表。</returns>
        public static List<PointF> CalculateGridPoints(SizeF containerSize, SizeF cellSize, Size matrixSize, Padding padding)
        {
            float x间距 = (containerSize.Width - padding.Left - padding.Right - cellSize.Width * matrixSize.Width) / (matrixSize.Width - 1);
            float y间距 = (containerSize.Height - padding.Top - padding.Bottom - cellSize.Height * matrixSize.Height) / (matrixSize.Height - 1);

            List<PointF> points = [];

            for (int y = 0; y < matrixSize.Height; y++)
            {
                for (int x = 0; x < matrixSize.Width; x++)
                {
                    points.Add(new PointF(
                        padding.Left + x * (cellSize.Width + x间距),
                        padding.Top + y * (cellSize.Height + y间距)
                    ));
                }
            }
            return points;
        }

        /// <summary>
        /// 将一个 <see cref="Padding"/> 对象转换为一个 <see cref="Point"/> 对象。
        /// </summary>
        /// <param name="padding">要转换的 <see cref="Padding"/> 对象。</param>
        /// <returns>
        /// 一个 <see cref="Point"/> 对象，其中 X 坐标计算为 padding.Left 减去 padding.Right，
        /// Y 坐标计算为 padding.Top 减去 padding.Bottom。
        /// </returns>
        public static Point ConvertToPoint(Padding padding)
        {
            return new(padding.Left - padding.Right, padding.Top - padding.Bottom);
        }
    }
}