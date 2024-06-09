namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供用于数学计算的实用方法。
    /// </summary>
    public class 数学
    {
        /// <summary>
        /// 计算网格点，返回在指定容器大小中按给定单元大小和矩阵大小排列的点列表
        /// </summary>
        /// <param name="容器大小">整个容器的大小</param>
        /// <param name="单元大小">每个单元的大小</param>
        /// <param name="矩阵大小">矩阵的行列数</param>
        /// <param name="边距">容器的边距</param>
        /// <returns>按矩阵排列的点列表</returns>
        public static List<PointF> 计算网格点(SizeF 容器大小, SizeF 单元大小, Size 矩阵大小, Padding 边距)
        {
            float x间距 = (容器大小.Width - 边距.Left - 边距.Right - 单元大小.Width * 矩阵大小.Width) / (矩阵大小.Width - 1);
            float y间距 = (容器大小.Height - 边距.Top - 边距.Bottom - 单元大小.Height * 矩阵大小.Height) / (矩阵大小.Height - 1);

            List<PointF> points = [];

            for (int y = 0; y < 矩阵大小.Height; y++)
            {
                for (int x = 0; x < 矩阵大小.Width; x++)
                {
                    points.Add(new PointF(
                        边距.Left + x * (单元大小.Width + x间距),
                        边距.Top + y * (单元大小.Height + y间距)
                    ));
                }
            }
            return points;
        }

        /// <summary>
        /// 计算等差数列的和。
        /// </summary>
        /// <param name="a1">首项。</param>
        /// <param name="n">项数。</param>
        /// <param name="d">公差。</param>
        /// <returns>等差数列的和。</returns>
        public static double 等差数列求和(double a1, int n, double d)
        {
            if (n <= 0)
            {
                throw new ArgumentException("项数 n 必须为正整数。");
            }

            return a1 * n + (n * d * (n - 1)) / 2;
        }

        /// <summary>
        /// 计算等比数列的和。
        /// </summary>
        /// <param name="a1">首项。</param>
        /// <param name="n">项数。</param>
        /// <param name="q">公比。</param>
        /// <returns>等比数列的和。</returns>
        public static double 等比数列求和(double a1, int n, double q)
        {
            if (n <= 0)
            {
                throw new ArgumentException("项数 n 必须为正整数。");
            }
            if (q == 1)
            {
                return a1 * n; // 当公比为1时，等比数列退化为等差数列
            }

            return a1 * (1 - Math.Pow(q, n)) / (1 - q);
        }
    }
}