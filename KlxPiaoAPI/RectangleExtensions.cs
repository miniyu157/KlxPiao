namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供 <see cref="Rectangle"/> 和 <see cref="RectangleF"/> 扩展方法的实用工具类。
    /// </summary>
    public static class RectangleExtensions
    {
        #region (2 个重载) GetInnerFitRectangle
        /// <summary>
        /// 根据给定的圆角半径计算内接矩形。
        /// </summary>
        /// <param name="parentRect">父矩形。</param>
        /// <param name="cornerRadius">圆角的半径，以 <see cref="CornerRadius"/> 结构体表示。 </param>
        /// <returns>紧贴圆角边缘的内接矩形。</returns>
        public static Rectangle GetInnerFitRectangle(this Rectangle parentRect, CornerRadius cornerRadius)
        {
            cornerRadius = cornerRadius.ToPixel(parentRect);
            cornerRadius /= (float)(Math.PI * 2);

            float x = parentRect.Left + cornerRadius.TopLeft;
            float y = parentRect.Top + cornerRadius.TopLeft;
            float width = parentRect.Width - cornerRadius.TopLeft - cornerRadius.TopRight;
            float height = parentRect.Height - cornerRadius.TopLeft - cornerRadius.BottomLeft;

            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }

        /// <summary>
        /// 根据给定的圆角半径计算内接矩形。
        /// </summary>
        /// <param name="parentRect">父矩形。</param>
        /// <param name="cornerRadius">圆角的半径，以 <see cref="CornerRadius"/> 结构体表示。 </param>
        /// <returns>紧贴圆角边缘的内接矩形。</returns>
        public static RectangleF GetInnerFitRectangle(this RectangleF parentRect, CornerRadius cornerRadius)
        {
            cornerRadius = cornerRadius.ToPixel(parentRect);
            cornerRadius /= (float)(Math.PI * 2);

            float x = parentRect.Left + cornerRadius.TopLeft;
            float y = parentRect.Top + cornerRadius.TopLeft;
            float width = parentRect.Width - cornerRadius.TopLeft - cornerRadius.TopRight;
            float height = parentRect.Height - cornerRadius.TopLeft - cornerRadius.BottomLeft;

            return new RectangleF(x, y, width, height);
        }
        #endregion

        #region (2 个重载) ScaleRectangle
        /// <summary>
        /// 缩放矩形的尺寸，通过将指定的缩放因子添加到其宽度和高度。
        /// 矩形的位置会调整以保持其中心点不变。
        /// </summary>
        /// <param name="rectangle">要缩放的矩形。</param>
        /// <param name="scaleFactor">缩放因子，正值表示放大，负值表示缩小。</param>
        /// <returns>尺寸调整和位置调整后的缩放矩形。</returns>
        public static Rectangle ScaleRectangle(this Rectangle rectangle, int scaleFactor)
        {
            return new(
                rectangle.X - scaleFactor / 2,
                rectangle.Y - scaleFactor / 2,
                rectangle.Width + scaleFactor,
                rectangle.Height + scaleFactor);
        }

        /// <summary>
        /// 缩放矩形的尺寸，通过将指定的缩放因子添加到其宽度和高度。
        /// 矩形的位置会调整以保持其中心点不变。
        /// </summary>
        /// <param name="rectangle">要缩放的矩形。</param>
        /// <param name="scaleFactor">缩放因子，正值表示放大，负值表示缩小。</param>
        /// <returns>尺寸调整和位置调整后的缩放矩形。</returns>
        public static RectangleF ScaleRectangle(this RectangleF rectangle, float scaleFactor)
        {
            return new(
                rectangle.X - scaleFactor / 2,
                rectangle.Y - scaleFactor / 2,
                rectangle.Width + scaleFactor,
                rectangle.Height + scaleFactor);
        }
        #endregion
    }
}