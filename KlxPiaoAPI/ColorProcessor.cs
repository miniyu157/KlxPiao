namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供颜色处理的实用工具类。
    /// </summary>
    public static class ColorProcessor
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
        public static Color AdjustBrightness(this Color color, double factor)
        {
            int red = Math.Min(Math.Max(0, (int)(color.R * (1 + factor))), 255);
            int green = Math.Min(Math.Max(0, (int)(color.G * (1 + factor))), 255);
            int blue = Math.Min(Math.Max(0, (int)(color.B * (1 + factor))), 255);
            return Color.FromArgb(color.A, red, green, blue);
        }

        /// <summary>
        /// 获取颜色的亮度（根据YUV颜色模型）。
        /// </summary>
        /// <param name="color">要计算亮度的颜色。</param>
        /// <returns>颜色的亮度值，范围从 0 到 255。</returns>
        public static double GetBrightnessForYUV(this Color color)
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
        public static Color SetBrightness(this Color color, double Brightness, double precision = 0.008)
        {
            double oldBrightness = color.GetBrightnessForYUV();
            double newBrightness = oldBrightness;

            Color newColor = color;
            Color oldColor = Color.Empty;

            if (oldBrightness > Brightness) //变暗
            {
                while (newBrightness > Brightness)
                {
                    oldColor = newColor; //记录调整前颜色
                    newColor = AdjustBrightness(newColor, -precision);
                    newBrightness = newColor.GetBrightnessForYUV();
                }
                return oldColor;
            }
            else if (oldBrightness < Brightness) //变亮
            {
                while (newBrightness < Brightness)
                {
                    oldColor = newColor; //记录调整前颜色
                    newColor = AdjustBrightness(newColor, +precision);
                    newBrightness = newColor.GetBrightnessForYUV();
                }
                return oldColor;
            }
            else
            {
                return color;
            }
        }

        /// <summary>
        /// 将一个 <see cref="Color"/> 对象转换为 Hex 的字符串形式。
        /// </summary>
        /// <param name="color">指定的 <see cref="Color"/> 对象。</param>
        /// <returns>表示颜色的 Hex 字符串。</returns>
        public static string ToHex(this Color color) => $"#{color.R:X2}{color.G:X2}{color.B:X2}";

        /// <summary>
        /// 将一个 <see cref="Color"/> 对象转换为包含 Alpha 通道的 Hex 字符串形式。
        /// </summary>
        /// <param name="color">指定的 <see cref="Color"/> 对象。</param>
        /// <returns>表示颜色的 Hex 字符串，包括 Alpha 通道。</returns>
        public static string ToHexAlpha(this Color color) => $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";

        /// <summary>
        /// 将一个 Hex 颜色字符串转换为 <see cref="Color"/> 对象。
        /// </summary>
        /// <param name="hex">Hex 颜色字符串，可以是 #RRGGBB 或 #RRGGBBAA 格式。</param>
        /// <returns>对应的 <see cref="Color"/> 对象。</returns>
        /// <exception cref="ArgumentException">当传入的字符串格式不正确时抛出。</exception>
        public static Color FromHex(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                throw new ArgumentException("Hex string cannot be null or empty.", nameof(hex));

            hex = hex.TrimStart('#');

            if (hex.Length != 6 && hex.Length != 8)
                throw new ArgumentException("Hex string must be 7 or 9 characters long (including #).", nameof(hex));

            int r, g, b, a = 255;

            if (hex.Length == 8)
            {
                a = Convert.ToInt32(hex[..2], 16);
                r = Convert.ToInt32(hex[2..4], 16);
                g = Convert.ToInt32(hex[4..6], 16);
                b = Convert.ToInt32(hex[6..8], 16);
            }
            else
            {
                r = Convert.ToInt32(hex[..2], 16);
                g = Convert.ToInt32(hex[2..4], 16);
                b = Convert.ToInt32(hex[4..6], 16);
            }

            return Color.FromArgb(a, r, g, b);
        }
    }
}