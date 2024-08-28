using System.ComponentModel;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 结构体，用于表示每个角的角半径（像素或百分比）
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(CornerRadiusConverter))]
    public struct CornerRadius
    {
        /// <summary>
        /// 获取或设置左上角的角半径。
        /// </summary>
        public float TopLeft { get; set; }

        /// <summary>
        /// 获取或设置右上角的角半径。
        /// </summary>
        public float TopRight { get; set; }

        /// <summary>
        /// 获取或设置右下角的角半径。
        /// </summary>
        public float BottomRight { get; set; }

        /// <summary>
        /// 获取或设置左下角的角半径。
        /// </summary>
        public float BottomLeft { get; set; }

        /// <summary>
        /// 初始化 <see cref="CornerRadius"/> 结构的新实例，指定每个角的角半径。
        /// </summary>
        /// <param name="topLeft">左上角的角半径。</param>
        /// <param name="topRight">右上角的角半径。</param>
        /// <param name="bottomRight">右下角的角半径。</param>
        /// <param name="bottomLeft">左下角的角半径。</param>
        public CornerRadius(float topLeft, float topRight, float bottomRight, float bottomLeft)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
            BottomLeft = bottomLeft;
        }

        public CornerRadius() { }

        /// <summary>
        /// 指定一个角半径，初始化 <see cref="CornerRadius"/> 结构的新实例。
        /// </summary>
        /// <param name="all">所有角的半径。</param>
        public CornerRadius(float all)
        {
            TopLeft = TopRight = BottomRight = BottomLeft = all;
        }

        #region operator
        public static CornerRadius operator +(CornerRadius cr1, CornerRadius cr2)
        {
            return new CornerRadius(
                cr1.TopLeft + cr2.TopLeft,
                cr1.TopRight + cr2.TopRight,
                cr1.BottomRight + cr2.BottomRight,
                cr1.BottomLeft + cr2.BottomLeft
            );
        }

        public static CornerRadius operator *(CornerRadius cr, float multiplier)
        {
            return new CornerRadius(
                cr.TopLeft * multiplier,
                cr.TopRight * multiplier,
                cr.BottomRight * multiplier,
                cr.BottomLeft * multiplier
            );
        }

        public static CornerRadius operator /(CornerRadius cr, float multiplier)
        {
            return new CornerRadius(
                cr.TopLeft / multiplier,
                cr.TopRight / multiplier,
                cr.BottomRight / multiplier,
                cr.BottomLeft / multiplier
            );
        }

        public static bool operator ==(CornerRadius cr1, CornerRadius cr2)
        {
            return cr1.TopLeft == cr2.TopLeft &&
                   cr1.TopRight == cr2.TopRight &&
                   cr1.BottomRight == cr2.BottomRight &&
                   cr1.BottomLeft == cr2.BottomLeft;
        }

        public static bool operator !=(CornerRadius cr1, CornerRadius cr2)
        {
            return !(cr1 == cr2);
        }
        #endregion

        public readonly override int GetHashCode()
        {
            return TopLeft.GetHashCode() ^ TopRight.GetHashCode() ^ BottomRight.GetHashCode() ^ BottomLeft.GetHashCode();
        }

        public readonly override bool Equals(object? obj)
        {
            if (obj == null || obj is not CornerRadius)
            {
                return false;
            }
            else
            {
                CornerRadius cr = (CornerRadius)obj;
                return TopLeft == cr.TopLeft && TopRight == cr.TopRight && BottomRight == cr.BottomRight && BottomLeft == cr.BottomLeft;
            }
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