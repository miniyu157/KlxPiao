namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供用于判断对象类型的辅助方法和类型集合。
    /// </summary>
    public static class TypeChecker
    {
        /// <summary>
        /// 判断对象是否属于指定的类型集合。
        /// </summary>
        /// <param name="types">类型集合的实例。</param>
        /// <param name="obj">要判断的对象。</param>
        /// <returns>如果对象属于类型集合，则返回 true；否则返回 false。</returns>
        public static bool IsTypes(this object obj, ITypeCollection types)
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
        /// 获取 <see cref="NumberType"/> 的唯一实例。
        /// </summary>
        public static ITypeCollection GetNumberTypeInstance() => NumberType.Instance;

        /// <summary>
        /// 获取 <see cref="PointOrSizeType"/> 的唯一实例。
        /// </summary>
        public static ITypeCollection GetPointOrSizeTypeInstance() => PointOrSizeType.Instance;

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
                    TypeCode.Byte or
                    TypeCode.SByte or
                    TypeCode.UInt16 or
                    TypeCode.UInt32 or
                    TypeCode.UInt64 or
                    TypeCode.Int16 or
                    TypeCode.Int32 or
                    TypeCode.Int64 or
                    TypeCode.Single or
                    TypeCode.Double or
                    TypeCode.Decimal => true,
                    _ => false,
                };
            }
            public NumberType() { }

            /// <summary>
            /// 获取 <see cref="NumberType"/> 的唯一实例。
            /// </summary>
            public static NumberType Instance { get; } = new();
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

            public PointOrSizeType() { }

            /// <summary>
            /// 获取 <see cref="PointOrSizeType"/> 的唯一实例。
            /// </summary>
            public static PointOrSizeType Instance { get; } = new();
        }
    }
}