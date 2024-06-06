namespace KlxPiaoAPI
{
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
    }
}