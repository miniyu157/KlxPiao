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
    }
}