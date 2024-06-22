namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供对 <see cref="Bitmap"/> 和 <see cref="Image"/> 的扩展方法。
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// 添加圆角到指定的 <see cref="Bitmap"/> 图像。
        /// </summary>
        /// <param name="original">原始的 <see cref="Bitmap"/> 图像。</param>
        /// <param name="cornerRadius">角半径，以 <see cref="CornerRadius"/> 结构体表示。</param>
        /// <returns>带有圆角的 <see cref="Bitmap"/> 图像。</returns>
        public static Bitmap AddRounded(this Bitmap original, CornerRadius cornerRadius)
        {
            Rectangle rect = new(0, 0, original.Width, original.Height);

            int width = original.Width;
            int height = original.Height;
            Bitmap roundedImage = new(width, height);
            using (Graphics g = Graphics.FromImage(roundedImage))
            {
                g.Clear(Color.Transparent);
                g.SetClip(GraphicsExtensions.ConvertToRoundedPath(rect, cornerRadius));
                g.DrawImage(original, Point.Empty);
            }

            return roundedImage;
        }

        /// <summary>
        /// 添加圆角到指定的 <see cref="Image"/> 图像。
        /// </summary>
        /// <param name="original">原始的 <see cref="Image"/> 图像。</param>
        /// <param name="cornerRadius">角半径，以 <see cref="CornerRadius"/> 结构体表示。</param>
        /// <returns>带有圆角的 <see cref="Image"/> 图像。</returns>
        public static Image AddRounded(this Image original, CornerRadius cornerRadius)
        {
            return ((Bitmap)original).AddRounded(cornerRadius);
        }
    }
}