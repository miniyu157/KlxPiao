namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供颜色处理的实用工具类。
    /// </summary>
    public class ColorProcessor
    {
        /// <summary>
        /// 调整颜色的亮度。
        /// </summary>
        /// <param name="color">要调整的颜色。</param>
        /// <param name="factor">
        /// 亮度调整因子。正值增加亮度，负值降低亮度。
        /// <br/>例如：0.2 表示增加20%的亮度，-0.2 表示减少 20% 的亮度。
        /// </param>
        /// <returns>调整亮度后的颜色。</returns>
        public static Color AdjustBrightness(Color color, double factor)
        {
            int red = Math.Min(Math.Max(0, (int)(color.R * (1 + factor))), 255);
            int green = Math.Min(Math.Max(0, (int)(color.G * (1 + factor))), 255);
            int blue = Math.Min(Math.Max(0, (int)(color.B * (1 + factor))), 255);
            return Color.FromArgb(color.A, red, green, blue);
        }

        /// <summary>
        /// 获取颜色的亮度。
        /// </summary>
        /// <param name="color">要计算亮度的颜色。</param>
        /// <returns>颜色的亮度值，范围从0到255。</returns>
        public static double GetBrightness(Color color)
        {
            return 0.299 * color.R + 0.587 * color.G + 0.114 * color.B;
        }

        /// <summary>
        /// 设置颜色的亮度。
        /// </summary>
        /// <param name="color">要设置亮度的颜色。</param>
        /// <param name="Brightness">目标亮度值，范围从0到255。</param>
        /// <param name="precision">调整亮度的精度，默认为0.08。</param>
        /// <returns>调整后的颜色，其亮度接近于指定的目标亮度值。</returns>
        public static Color SetBrightness(Color color, double Brightness, double precision = 0.008)
        {
            double 当前亮度 = GetBrightness(color);
            double newBrightness = 当前亮度;

            Color newColor = color;
            Color oldColor=Color.Empty;

            if (当前亮度 > Brightness) //变暗
            {
                while (newBrightness > Brightness)
                {
                    oldColor = newColor; //记录调整前颜色
                    newColor = AdjustBrightness(newColor, -precision);
                    newBrightness = GetBrightness(newColor);
                }
                return oldColor;
            }
            else if (当前亮度 < Brightness) //变亮
            {
                while (newBrightness < Brightness)
                {
                    oldColor = newColor; //记录调整前颜色
                    newColor = AdjustBrightness(newColor, +precision);
                    newBrightness = GetBrightness(newColor);
                }
                return oldColor;
            }
            else
            {
                return color;
            }
        }
    }
}