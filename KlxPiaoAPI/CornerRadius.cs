using System.ComponentModel;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 结构体，用于表示每个圆角的像素大小（或百分比大小）
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(CornerRadiusConverter))]
    public struct CornerRadius
    {
        /// <summary>
        /// 获取或设置左上角的大小。
        /// </summary>
        public float TopLeft { get; set; }

        /// <summary>
        /// 获取或设置右上角的大小。
        /// </summary>
        public float TopRight { get; set; }

        /// <summary>
        /// 获取或设置右下角的大小。
        /// </summary>
        public float BottomRight { get; set; }

        /// <summary>
        /// 获取或设置左下角的大小。
        /// </summary>
        public float BottomLeft { get; set; }

        /// <summary>
        /// 初始化 CornerRadius 结构的新实例，指定每个角的大小。
        /// </summary>
        /// <param name="topLeft">左上角的大小。</param>
        /// <param name="topRight">右上角的大小。</param>
        /// <param name="bottomRight">右下角的大小。</param>
        /// <param name="bottomLeft">左下角的大小。</param>
        public CornerRadius(float topLeft, float topRight, float bottomRight, float bottomLeft)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
            BottomLeft = bottomLeft;
        }

        /// <summary>
        /// 初始化 CornerRadius 结构的新实例，所有角的大小相同。
        /// </summary>
        /// <param name="all">所有角的半径。</param>
        public CornerRadius(float all)
        {
            TopLeft = TopRight = BottomRight = BottomLeft = all;
        }

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override readonly string ToString()
        {
            return $"TopLeft: {TopLeft}, TopRight: {TopRight}, BottomRight: {BottomRight}, BottomLeft: {BottomLeft}";
        }
    }
}