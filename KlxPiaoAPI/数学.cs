namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供用于数学计算的实用方法。
    /// </summary>
    public class 数学
    {
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