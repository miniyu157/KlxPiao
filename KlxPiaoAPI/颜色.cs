namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供颜色处理的相关操作。
    /// </summary>
    public class 颜色
    {
        /// <summary>
        /// 细微调整颜色亮度，范围 -1.00 到 +1.00
        /// </summary>
        /// <param name="color">原颜色</param>
        /// <param name="factor">调整值</param>
        /// <returns></returns>
        public static Color 调整亮度(Color color, double factor)
        {
            int red = Math.Min(Math.Max(0, (int)(color.R * (1 + factor))), 255);
            int green = Math.Min(Math.Max(0, (int)(color.G * (1 + factor))), 255);
            int blue = Math.Min(Math.Max(0, (int)(color.B * (1 + factor))), 255);
            return Color.FromArgb(color.A, red, green, blue);
        }

        /// <summary>
        /// 获取指定颜色的亮度
        /// </summary>
        /// <param name="color"></param>
        /// <returns>返回Double 0 到 255 之间</returns>
        public static double 获取亮度(Color color)
        {
            return 0.299 * color.R + 0.587 * color.G + 0.114 * color.B;
        }
    }
}