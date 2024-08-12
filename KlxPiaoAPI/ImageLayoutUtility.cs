namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供用于计算图像布局的实用方法。
    /// </summary>
    public class ImageLayoutUtility
    {
        /// <summary>
        /// 将图像在指定的基础大小区域内居中显示。
        /// </summary>
        /// <param name="baseSize">要将图像居中显示的区域大小。</param>
        /// <param name="imageSize">要居中的图像的原始大小。</param>
        /// <param name="point">输出参数，表示图像居中后的位置。</param>
        /// <param name="size">输出参数，表示图像的大小。</param>
        public static void Center(Size baseSize, Size imageSize, out Point point, out Size size)
        {
            int x = (baseSize.Width - imageSize.Width) / 2;
            int y = (baseSize.Height - imageSize.Height) / 2;
            point = new Point(x, y);
            size = imageSize;
        }

        /// <summary>
        /// 在指定的基础大小区域内按比例缩放图像。
        /// </summary>
        /// <param name="baseSize">要将图像缩放显示的区域大小。</param>
        /// <param name="imageSize">要缩放的图像的原始大小。</param>
        /// <param name="point">输出参数，表示图像缩放后的位置。</param>
        /// <param name="size">输出参数，表示图像缩放后的大小。</param>
        public static void Zoom(Size baseSize, Size imageSize, out Point point, out Size size)
        {
            int baseWidth = baseSize.Width;
            int baseHeight = baseSize.Height;

            int imageWidth = imageSize.Width;
            int imageHeight = imageSize.Height;

            float imageAspect = (float)imageWidth / imageHeight;
            float controlAspect = (float)baseWidth / baseHeight;

            int drawWidth, drawHeight;
            int posX, posY;

            if (imageAspect > controlAspect)
            {
                drawWidth = baseWidth;
                drawHeight = (int)(baseWidth / imageAspect);
                posX = 0;
                posY = (baseHeight - drawHeight) / 2;
            }
            else
            {
                drawWidth = (int)(baseHeight * imageAspect);
                drawHeight = baseHeight;
                posX = (baseWidth - drawWidth) / 2;
                posY = 0;
            }

            point = new Point(posX, posY);
            size = new Size(drawWidth, drawHeight);
        }
    }
}