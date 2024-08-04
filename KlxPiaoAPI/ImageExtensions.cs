using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供对 <see cref="Bitmap"/> 和 <see cref="Image"/> 的扩展方法。
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// 替换位图图像中的指定颜色。
        /// </summary>
        /// <param name="originalImage">要处理的原始位图图像。</param>
        /// <param name="oldColor">要被替换的颜色。</param>
        /// <param name="newColor">替换后的新颜色。</param>
        /// <param name="isPreserveAlpha">是否保留原始图像的Alpha通道值。</param>
        /// <returns>返回替换颜色后的新位图图像。</returns>
        public static Bitmap ReplaceColor(this Bitmap originalImage, Color oldColor, Color newColor, bool isPreserveAlpha = true)
        {
            Bitmap newImage = new(originalImage.Width, originalImage.Height, originalImage.PixelFormat);

            BitmapData originalData = originalImage.LockBits(
                new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                ImageLockMode.ReadOnly,
                originalImage.PixelFormat);

            BitmapData newData = newImage.LockBits(
                new Rectangle(0, 0, newImage.Width, newImage.Height),
                ImageLockMode.WriteOnly,
                newImage.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(originalImage.PixelFormat) / 8;
            int byteCount = originalData.Stride * originalImage.Height;
            byte[] pixelBuffer = new byte[byteCount];
            byte[] resultBuffer = new byte[byteCount];

            Marshal.Copy(originalData.Scan0, pixelBuffer, 0, byteCount);
            originalImage.UnlockBits(originalData);

            Parallel.For(0, byteCount / bytesPerPixel, i =>
            {
                int k = i * bytesPerPixel;
                byte blue = pixelBuffer[k];
                byte green = pixelBuffer[k + 1];
                byte red = pixelBuffer[k + 2];
                byte alpha = bytesPerPixel == 4 ? pixelBuffer[k + 3] : (byte)255;

                if (red == oldColor.R && green == oldColor.G && blue == oldColor.B)
                {
                    resultBuffer[k] = newColor.B;
                    resultBuffer[k + 1] = newColor.G;
                    resultBuffer[k + 2] = newColor.R;
                    if (bytesPerPixel == 4)
                    {
                        resultBuffer[k + 3] = isPreserveAlpha ? alpha : newColor.A;
                    }
                }
                else
                {
                    resultBuffer[k] = blue;
                    resultBuffer[k + 1] = green;
                    resultBuffer[k + 2] = red;
                    if (bytesPerPixel == 4)
                    {
                        resultBuffer[k + 3] = alpha;
                    }
                }
            });

            Marshal.Copy(resultBuffer, 0, newData.Scan0, byteCount);
            newImage.UnlockBits(newData);

            return newImage;
        }

        /// <summary>
        /// 添加圆角到指定的 <see cref="Bitmap"/> 图像。
        /// </summary>
        /// <param name="original">原始的 <see cref="Bitmap"/> 图像。</param>
        /// <param name="cornerRadius">角半径，以 <see cref="CornerRadius"/> 结构体表示。</param>
        /// <returns>带有圆角的 <see cref="Bitmap"/> 图像。</returns>
        public static Bitmap AddRounded(this Bitmap original, CornerRadius cornerRadius)
        {
            int width = original.Width;
            int height = original.Height;

            Rectangle rect = new(0, 0, width, height);
            Bitmap roundedImage = new(width, height);

            using (Graphics g = Graphics.FromImage(roundedImage))
            {
                g.Clear(Color.Transparent);
                g.SetClip(rect.ConvertToRoundedPath(cornerRadius));
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