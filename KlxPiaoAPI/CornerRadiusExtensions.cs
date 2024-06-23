namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供 <see cref="CornerRadius"/> 的扩展方法的实用工具类。
    /// </summary>
    public static class CornerRadiusExtensions
    {
        #region (6 个重载) ToPixel
        /// <summary>
        /// 将 <see cref="CornerRadius"/> 的百分比转换为像素单位。
        /// </summary>
        /// <param name="cornerRadius">要转换的 <see cref="CornerRadius"/>。</param>
        /// <param name="size">用于计算新尺寸的大小。</param>
        /// <returns>转换后的 <see cref="CornerRadius"/>。</returns>
        public static CornerRadius ToPixel(this CornerRadius cornerRadius, Size size)
        {
            int newDimensions = Math.Min(size.Width, size.Height);

            float ConvertToPixels(float value)
            {
                if (value > 0 && value <= 1)
                    return value * newDimensions;
                else
                    return value;
            }

            float newTopLeft = ConvertToPixels(cornerRadius.TopLeft);
            float newTopRight = ConvertToPixels(cornerRadius.TopRight);
            float newBottomRight = ConvertToPixels(cornerRadius.BottomRight);
            float newBottomLeft = ConvertToPixels(cornerRadius.BottomLeft);

            return new CornerRadius(newTopLeft, newTopRight, newBottomRight, newBottomLeft);
        }

        /// <summary>
        /// 将 <see cref="CornerRadius"/> 的百分比转换为像素单位。
        /// </summary>
        /// <param name="cornerRadius">要转换的 <see cref="CornerRadius"/>。</param>
        /// <param name="size">用于计算新尺寸的大小。</param>
        /// <returns>转换后的 <see cref="CornerRadius"/>。</returns>
        public static CornerRadius ToPixel(this CornerRadius cornerRadius, SizeF size)
        {
            return cornerRadius.ToPixel(new Size((int)size.Width, (int)size.Height));
        }

        /// <summary>
        /// 将 <see cref="CornerRadius"/> 的百分比转换为像素单位。
        /// </summary>
        /// <param name="cornerRadius">要转换的 <see cref="CornerRadius"/>。</param>
        /// <param name="rectangle">用于计算新尺寸的矩形区域。</param>
        /// <returns>转换后的 <see cref="CornerRadius"/>。</returns>
        public static CornerRadius ToPixel(this CornerRadius cornerRadius, Rectangle rectangle)
        {
            return cornerRadius.ToPixel(rectangle.Size);
        }

        /// <summary>
        /// 将 <see cref="CornerRadius"/> 的百分比转换为像素单位。
        /// </summary>
        /// <param name="cornerRadius">要转换的 <see cref="CornerRadius"/>。</param>
        /// <param name="rectangle">用于计算新尺寸的矩形区域。</param>
        /// <returns>转换后的 <see cref="CornerRadius"/>。</returns>
        public static CornerRadius ToPixel(this CornerRadius cornerRadius, RectangleF rectangle)
        {
            return cornerRadius.ToPixel(new Size((int)rectangle.Width, (int)rectangle.Height));
        }

        /// <summary>
        /// 将 <see cref="CornerRadius"/> 的百分比转换为像素单位。
        /// </summary>
        /// <param name="cornerRadius">要转换的 <see cref="CornerRadius"/>。</param>
        /// <param name="Width">用于计算新尺寸的宽度。</param>
        /// <param name="Height">用于计算新尺寸的高度。</param>
        /// <returns>转换后的 <see cref="CornerRadius"/>。</returns>
        public static CornerRadius ToPixel(this CornerRadius cornerRadius, int Width, int Height)
        {
            return cornerRadius.ToPixel(new Size(Width, Height));
        }

        /// <summary>
        /// 将 <see cref="CornerRadius"/> 的百分比转换为像素单位。
        /// </summary>
        /// <param name="cornerRadius">要转换的 <see cref="CornerRadius"/>。</param>
        /// <param name="Width">用于计算新尺寸的宽度。</param>
        /// <param name="Height">用于计算新尺寸的高度。</param>
        /// <returns>转换后的 <see cref="CornerRadius"/>。</returns>
        public static CornerRadius ToPixel(this CornerRadius cornerRadius, float Width, float Height)
        {
            return cornerRadius.ToPixel(new Size((int)Width, (int)Height));
        }
        #endregion
    }
}