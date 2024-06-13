namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供用于判断对象类型的辅助方法和类型集合。
    /// </summary>
    public class 类型
    {
        /// <summary>
        /// 判断对象是否属于指定的类型集合。
        /// </summary>
        /// <param name="types">类型集合的实例。</param>
        /// <param name="obj">要判断的对象。</param>
        /// <returns>如果对象属于类型集合，则返回 true；否则返回 false。</returns>
        public static bool 判断(ITypeCollection types, object obj)
        {
            return types.IsTypeInCollection(obj);
        }

        /// <summary>
        /// 类型集合接口，用于判断对象是否属于某种类型集合。
        /// </summary>
        public interface ITypeCollection
        {
            /// <summary>
            /// 判断对象是否属于某种类型集合。
            /// </summary>
            /// <param name="obj">要判断的对象。</param>
            /// <returns>如果对象属于类型集合，则返回 true；否则返回 false。</returns>
            bool IsTypeInCollection(object obj);
        }

        /// <summary>
        /// 数字类型集合，判断对象是否是数字类型。
        /// </summary>
        public class NumberType : ITypeCollection
        {
            /// <summary>
            /// 判断对象是否是数字类型。
            /// </summary>
            /// <param name="obj">要判断的对象。</param>
            /// <returns>如果对象是数字类型，则返回 true；否则返回 false。</returns>
            public bool IsTypeInCollection(object obj)
            {
                return Type.GetTypeCode(obj.GetType()) switch
                {
                    TypeCode.Byte or TypeCode.SByte or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64 or TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64 or TypeCode.Single or TypeCode.Double or TypeCode.Decimal => true,
                    _ => false,
                };
            }
        }

        /// <summary>
        /// 点和大小类型集合，判断对象是否是 Point(F) 或 Size(F) 类型。
        /// </summary>
        public class PointOrSizeType : ITypeCollection
        {
            /// <summary>
            /// 判断对象是否是 Point 或 Size 类型。
            /// </summary>
            /// <param name="obj">要判断的对象。</param>
            /// <returns>如果对象是 Point(F) 或 Size(F) 类型，则返回 true；否则返回 false。</returns>
            public bool IsTypeInCollection(object obj)
            {
                return obj.GetType().Name switch
                {
                    "Size" or "SizeF" or "Point" or "PointF" => true,
                    _ => false,
                };
            }
        }
    }
}