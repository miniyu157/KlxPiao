using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 主要提供对 <see cref="Bitmap"/> 和 <see cref="Image"/> 的扩展方法。
    /// </summary>
    public static class ImageExtensions
    {
        #region (+4 重载) ResetImage
        /// <summary>
        /// 调整原始图像大小并应用基础颜色作为背景颜色。
        /// </summary>
        /// <param name="originalImage">要调整大小的原始图像。</param>
        /// <param name="baseColor">基础背景颜色。</param>
        /// <param name="newSize">新的图像尺寸。</param>
        /// <returns>调整大小后的图像。</returns>
        public static Bitmap ResetImage(this Bitmap originalImage, Size? newSize, Color? baseColor = null)
        {
            int newWidth = newSize == null ? originalImage.Width : newSize.Value.Width;
            int newHeight = newSize == null ? originalImage.Height : newSize.Value.Height;

            Bitmap newBitmap = new(newWidth, newHeight);

            using Graphics g = Graphics.FromImage(newBitmap);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            if (baseColor != null) g.Clear(baseColor.Value);
            g.DrawImage(originalImage, 0, 0, newWidth, newHeight);

            return newBitmap;
        }

        /// <summary>
        /// 调整原始图像大小并应用基础颜色作为背景颜色。
        /// </summary>
        /// <param name="originalImage">要调整大小的原始图像。</param>
        /// <param name="baseColor">基础背景颜色。</param>
        /// <param name="newWidth">新的图像宽度。</param>
        /// <param name="newHeight">新的图像高度寸。</param>
        /// <returns>调整大小后的图像。</returns>
        public static Bitmap ResetImage(this Bitmap originalImage, int newWidth, int newHeight, Color? baseColor = null)
        {
            return originalImage.ResetImage(new Size(newWidth, newHeight), baseColor);
        }

        /// <summary>
        /// 调整原始图像大小并应用基础颜色作为背景颜色。
        /// </summary>
        /// <param name="originalImage">要调整大小的原始图像。</param>
        /// <param name="baseColor">基础背景颜色。</param>
        /// <param name="newWidth">新的图像宽度。</param>
        /// <param name="newHeight">新的图像高度寸。</param>
        /// <returns>调整大小后的图像。</returns>
        public static Image ResetImage(this Image originalImage, int newWidth, int newHeight, Color? baseColor = null)
        {
            return ((Bitmap)originalImage).ResetImage(new Size(newWidth, newHeight), baseColor);
        }

        /// <summary>
        /// 调整原始图像大小并应用基础颜色作为背景颜色。
        /// </summary>
        /// <param name="originalImage">要调整大小的原始图像。</param>
        /// <param name="baseColor">基础背景颜色。</param>
        /// <param name="newSize">新的图像尺寸。</param>
        /// <returns>调整大小后的图像。</returns>
        public static Image ResetImage(this Image originalImage, Size? newSize, Color? baseColor = null)
        {
            return ((Bitmap)originalImage).ResetImage(newSize, baseColor);
        }

        /// <summary>
        /// 调整原始图像大小并应用基础颜色作为背景颜色。
        /// </summary>
        /// <param name="originalImage">要调整大小的原始图像。</param>
        /// <param name="baseColor">基础背景颜色。</param>
        /// <param name="newSize">新的图像尺寸。</param>
        /// <returns>调整大小后的图像。</returns>
        public static Icon ResetImage(this Icon originalImage, Size? newSize, Color? baseColor = null)
        {
            return Icon.FromHandle(originalImage.ToBitmap().ResetImage(newSize, baseColor).GetHicon());
        }
        #endregion

        #region (+1 重载) ReplaceColor
        /// <summary>
        /// 替换位图图像中的指定颜色。
        /// </summary>
        /// <param name="originalImage">要处理的原始位图图像。</param>
        /// <param name="oldColor">要被替换的颜色。</param>
        /// <param name="newColor">替换后的新颜色。</param>
        /// <param name="isPreserveAlpha">是否保留原始图像的 Alpha 通道值。</param>
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
            int byteCount = Math.Abs(originalData.Stride) * originalImage.Height;
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
        /// 替换图像中的指定颜色。
        /// </summary>
        /// <param name="originalImage">要处理的原始图像。</param>
        /// <param name="oldColor">要被替换的颜色。</param>
        /// <param name="newColor">替换后的新颜色。</param>
        /// <param name="isPreserveAlpha">是否保留原始图像的 Alpha 通道值。</param>
        /// <returns>返回替换颜色后的新图像。</returns>
        public static Image ReplaceColor(this Image originalImage, Color oldColor, Color newColor, bool isPreserveAlpha = true)
        {
            return ((Bitmap)originalImage).ReplaceColor(oldColor, newColor, isPreserveAlpha);
        }
        #endregion

        #region (+1 重载) ReplaceNonFullyTransparentPixels
        /// <summary>
        /// 替换图像中所有非完全透明像素的颜色为指定颜色。
        /// </summary>
        /// <param name="originalImage">原始图像。</param>
        /// <param name="replacementColor">用于替换非完全透明像素的颜色。</param>
        /// <returns>替换颜色后的新图像。</returns>
        public static Bitmap ReplaceNonFullyTransparentPixels(this Bitmap originalImage, Color replacementColor)
        {
            Bitmap newImage = new(originalImage.Width, originalImage.Height, PixelFormat.Format32bppArgb);

            BitmapData originalData = originalImage.LockBits(
                new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            BitmapData newData = newImage.LockBits(
                new Rectangle(0, 0, newImage.Width, newImage.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            int bytes = Math.Abs(originalData.Stride) * originalData.Height;
            byte[] pixelBuffer = new byte[bytes];
            Marshal.Copy(originalData.Scan0, pixelBuffer, 0, bytes);

            for (int i = 0; i < pixelBuffer.Length; i += 4)
            {
                if (pixelBuffer[i + 3] != 0)
                {
                    pixelBuffer[i] = replacementColor.B;
                    pixelBuffer[i + 1] = replacementColor.G;
                    pixelBuffer[i + 2] = replacementColor.R;
                }
            }

            Marshal.Copy(pixelBuffer, 0, newData.Scan0, bytes);

            originalImage.UnlockBits(originalData);
            newImage.UnlockBits(newData);

            return newImage;
        }

        /// <summary>
        /// 替换图像中所有非完全透明像素的颜色为指定颜色。
        /// </summary>
        /// <param name="originalImage">原始图像。</param>
        /// <param name="replacementColor">用于替换非完全透明像素的颜色。</param>
        /// <returns>替换颜色后的新图像。</returns>
        public static Image ReplaceNonFullyTransparentPixels(this Image originalImage, Color replacementColor)
        {
            return ((Bitmap)originalImage).ReplaceNonFullyTransparentPixels(replacementColor);
        }
        #endregion

        #region (+3 重载) AddRounded
        /// <summary>
        /// 添加圆角到指定的 <see cref="Bitmap"/> 图像。
        /// </summary>
        /// <param name="original">原始的 <see cref="Bitmap"/> 图像。</param>
        /// <param name="cornerRadius">角半径，以 <see cref="CornerRadius"/> 结构体表示。</param>
        /// <param name="baseColor">添加边框时，边框外部的颜色。</param>
        /// <param name="borderColor">添加边框时，边框的颜色。</param>
        /// <param name="borderSize">添加边框时，边框的大小。</param>
        /// <returns>带有圆角的 <see cref="Bitmap"/> 图像。</returns>
        public static Bitmap AddRounded(this Bitmap original, CornerRadius cornerRadius, Color baseColor, Color? borderColor = null, int borderSize = 0)
        {
            int width = original.Width;
            int height = original.Height;

            Rectangle rect = new(0, 0, width, height);
            Bitmap roundedImage = new(width, height);

            using (Graphics g = Graphics.FromImage(roundedImage))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(original, Point.Empty);
                if (borderColor != Color.Empty && borderColor != null && borderSize != 0)
                {
                    using var roundedPath = rect.ConvertToRoundedPath(cornerRadius);
                    using var borderPen = new Pen(borderColor.Value, borderSize);
                    g.DrawPath(borderPen, roundedPath);
                }
                using var outerPath = rect.ConvertToRoundedPath(cornerRadius, true);
                using var outerBrush = new SolidBrush(baseColor);
                g.FillPath(outerBrush, outerPath);
            }

            return roundedImage;
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
                using var roundedPath = rect.ConvertToRoundedPath(cornerRadius);
                g.SetClip(roundedPath);
                g.DrawImage(original, Point.Empty);
            }

            return roundedImage;
        }

        /// <summary>
        /// 添加圆角到指定的 <see cref="Image"/> 图像。
        /// </summary>
        /// <param name="original">原始的 <see cref="Image"/> 图像。</param>
        /// <param name="cornerRadius">角半径，以 <see cref="CornerRadius"/> 结构体表示。</param>
        /// <param name="baseColor">添加边框时，边框外部的颜色。</param>
        /// <param name="borderColor">添加边框时，边框的颜色。</param>
        /// <param name="borderSize">添加边框时，边框的大小。</param>
        /// <returns>带有圆角的 <see cref="Image"/> 图像。</returns>
        public static Image AddRounded(this Image original, CornerRadius cornerRadius, Color baseColor, Color? borderColor = null, int borderSize = 0)
        {
            return ((Bitmap)original).AddRounded(cornerRadius, baseColor, borderColor, borderSize);
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
        #endregion

        #region (+1 重载) CreateTransparentBackground
        /// <summary>
        /// 为原始图像创建一个具有透明背景的位图。背景为棋盘格模式，通常用于显示透明度。
        /// </summary>
        /// <param name="originalImage">要添加透明背景的原始图像。</param>
        /// <param name="cellSize">棋盘格的单元格大小，默认为 10 像素。</param>
        /// <param name="lightColor">棋盘格中浅色部分的颜色。如果为 null，则使用默认的浅灰色。</param>
        /// <param name="darkColor">棋盘格中深色部分的颜色。如果为 null，则使用默认的灰色。</param>
        /// <returns>带有透明背景的新的位图图像。</returns>
        public static Bitmap CreateTransparentBackground(this Bitmap originalImage, int cellSize = 10, Color? lightColor = null, Color? darkColor = null)
        {
            Bitmap bitmap = new(originalImage.Width, originalImage.Height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);

                using (Brush lightBrush = new SolidBrush(lightColor ?? Color.LightGray))
                using (Brush darkBrush = new SolidBrush(darkColor ?? Color.Gray))
                {
                    for (int y = 0; y < originalImage.Width; y += cellSize)
                    {
                        for (int x = 0; x < originalImage.Height; x += cellSize)
                        {
                            Rectangle cell = new(x, y, cellSize, cellSize);
                            if ((x / cellSize + y / cellSize) % 2 == 0)
                            {
                                g.FillRectangle(lightBrush, cell);
                            }
                            else
                            {
                                g.FillRectangle(darkBrush, cell);
                            }
                        }
                    }
                }
                g.DrawImage(originalImage, 0, 0);
            }

            return bitmap;
        }

        /// <summary>
        /// 为原始图像创建一个具有透明背景的图像。背景为棋盘格模式，通常用于显示透明度。
        /// </summary>
        /// <param name="originalImage">要添加透明背景的原始图像。</param>
        /// <param name="cellSize">棋盘格的单元格大小，默认为 10 像素。</param>
        /// <param name="lightColor">棋盘格中浅色部分的颜色。如果为 null，则使用默认的浅灰色。</param>
        /// <param name="darkColor">棋盘格中深色部分的颜色。如果为 null，则使用默认的灰色。</param>
        /// <returns>带有透明背景的新的图像对象。</returns>
        public static Image CreateTransparentBackground(this Image originalImage, int cellSize = 10, Color? lightColor = null, Color? darkColor = null)
        {
            return ((Bitmap)originalImage).CreateTransparentBackground(cellSize, lightColor, darkColor);
        }
        #endregion

        public static Bitmap AdjustBrightness(this Bitmap image, float brightnessFactor)
        {
            Bitmap adjustedImage = new(image.Width, image.Height);

            Rectangle rect = new(0, 0, image.Width, image.Height);

            BitmapData originalData = image.LockBits(rect, ImageLockMode.ReadOnly, image.PixelFormat);
            BitmapData adjustedData = adjustedImage.LockBits(rect, ImageLockMode.WriteOnly, image.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = originalData.Stride * image.Height;
            byte[] pixelBuffer = new byte[byteCount];
            byte[] resultBuffer = new byte[byteCount];

            Marshal.Copy(originalData.Scan0, pixelBuffer, 0, byteCount);

            for (int i = 0; i < byteCount; i += bytesPerPixel)
            {
                float r = pixelBuffer[i + 2] * brightnessFactor;
                float g = pixelBuffer[i + 1] * brightnessFactor;
                float b = pixelBuffer[i] * brightnessFactor;

                resultBuffer[i + 2] = (byte)Math.Min(255, Math.Max(0, r));
                resultBuffer[i + 1] = (byte)Math.Min(255, Math.Max(0, g));
                resultBuffer[i] = (byte)Math.Min(255, Math.Max(0, b));

                if (bytesPerPixel == 4)
                {
                    resultBuffer[i + 3] = pixelBuffer[i + 3];
                }
            }

            Marshal.Copy(resultBuffer, 0, adjustedData.Scan0, byteCount);

            image.UnlockBits(originalData);
            adjustedImage.UnlockBits(adjustedData);

            return adjustedImage;
        }

        public static Image AdjustBrightness(this Image image, float brightnessFactor)
        {
            return ((Bitmap)image).AdjustBrightness(brightnessFactor);
        }
    }
}