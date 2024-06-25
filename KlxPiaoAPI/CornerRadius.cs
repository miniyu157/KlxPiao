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

        #region 重载运算符
        /// <summary>
        /// 重载 + 运算符，实现两个 <see cref="CornerRadius"/> 对象相加。
        /// </summary>
        /// <param name="cr1">第一个 <see cref="CornerRadius"/> 对象。</param>
        /// <param name="cr2">第二个 <see cref="CornerRadius"/> 对象。</param>
        /// <returns>相加后的 <see cref="CornerRadius"/> 对象。</returns>
        public static CornerRadius operator +(CornerRadius cr1, CornerRadius cr2)
        {
            return new CornerRadius(
                cr1.TopLeft + cr2.TopLeft,
                cr1.TopRight + cr2.TopRight,
                cr1.BottomRight + cr2.BottomRight,
                cr1.BottomLeft + cr2.BottomLeft
            );
        }

        /// <summary>
        /// 重载 * 运算符，实现 <see cref="CornerRadius"/> 对象与 <see cref="int"/> 相乘。
        /// </summary>
        /// <param name="cr"><see cref="CornerRadius"/> 对象。</param>
        /// <param name="multiplier">乘数。</param>
        /// <returns>相乘后的 <see cref="CornerRadius"/> 对象。</returns>
        public static CornerRadius operator *(CornerRadius cr, float multiplier)
        {
            return new CornerRadius(
                cr.TopLeft * multiplier,
                cr.TopRight * multiplier,
                cr.BottomRight * multiplier,
                cr.BottomLeft * multiplier
            );
        }

        /// <summary>
        /// 重载 / 运算符，实现 <see cref="CornerRadius"/> 对象与 <see cref="int"/> 相除。
        /// </summary>
        /// <param name="cr"><see cref="CornerRadius"/> 对象。</param>
        /// <param name="multiplier">除数。</param>
        /// <returns>相除后的 <see cref="CornerRadius"/> 对象。</returns>
        public static CornerRadius operator /(CornerRadius cr, float multiplier)
        {
            return new CornerRadius(
                cr.TopLeft / multiplier,
                cr.TopRight / multiplier,
                cr.BottomRight / multiplier,
                cr.BottomLeft / multiplier
            );
        }

        /// <summary>
        /// 重载 == 运算符，比较两个 <see cref="CornerRadius"/> 对象是否相等。
        /// </summary>
        /// <param name="cr1">第一个 <see cref="CornerRadius"/> 对象。</param>
        /// <param name="cr2">第二个 <see cref="CornerRadius"/> 对象。</param>
        /// <returns>如果两个对象相等，则返回 true；否则返回 false。</returns>
        public static bool operator ==(CornerRadius cr1, CornerRadius cr2)
        {
            return cr1.TopLeft == cr2.TopLeft &&
                   cr1.TopRight == cr2.TopRight &&
                   cr1.BottomRight == cr2.BottomRight &&
                   cr1.BottomLeft == cr2.BottomLeft;
        }

        /// <summary>
        /// 重载 != 运算符，比较两个 <see cref="CornerRadius"/> 对象是否不相等。
        /// </summary>
        /// <param name="cr1">第一个 <see cref="CornerRadius"/> 对象。</param>
        /// <param name="cr2">第二个 <see cref="CornerRadius"/> 对象。</param>
        /// <returns>如果两个对象不相等，则返回 true；否则返回 false。</returns>
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