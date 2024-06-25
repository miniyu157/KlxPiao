using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace KlxPiaoAPI
{
    /// <summary>提供了一种类型转换器，用于将 <see cref="Animation" /> 值与其他各种表示形式进行转换。</summary>
    public class AnimationConverter : TypeConverter
    {
        #region DisableWarning
#pragma warning disable CS8765 // 参数类型的为 Null 性与重写成员不匹配(可能是由于为 Null 性特性)。
#pragma warning disable CS8604 // 引用类型参数可能为 null。
#pragma warning disable CS8603 // 可能返回 null 引用。
#pragma warning disable CS8605 // 取消装箱可能为 null 的值。
        #endregion

        #region ConvertFrom
        /// <summary>确定此转换器是否可以将给定的源类型转换为此转换器的本机类型。</summary>
        /// <param name="context">格式上下文。</param>
        /// <param name="sourceType">要转换的类型。</param>
        /// <returns>如果可以转换，则为 true；否则为 false。</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>将给定的值对象转换为此转换器的本机类型。</summary>
        /// <param name="context">格式上下文。</param>
        /// <param name="culture">区域性信息。</param>
        /// <param name="value">要转换的值。</param>
        /// <returns>转换后的对象。</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string str)
            {
                try
                {
                    char c = culture.TextInfo.ListSeparator[0];
                    var parts = str.Split(c);
                    if (parts.Length < 3) throw new ArgumentException("Invalid format");

                    int time = int.Parse(parts[0].Trim(), culture);
                    int fps = int.Parse(parts[1].Trim(), culture);

                    var easingParts = parts[2].Trim().TrimStart('[').TrimEnd(']').Split(';');
                    PointF[] easing = new PointF[easingParts.Length];
                    for (int i = 0; i < easingParts.Length; i++)
                    {
                        var pointParts = easingParts[i].Trim().Split(' ');
                        if (pointParts.Length != 2) throw new ArgumentException("Invalid point format");
                        float x = float.Parse(pointParts[0], culture);
                        float y = float.Parse(pointParts[1], culture);
                        easing[i] = new PointF(x, y);
                    }

                    return new Animation(time, fps, easing);
                }
                catch
                {
                    throw new ArgumentException($"Cannot convert '{value}' to type Animation");
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
        #endregion

        #region ConvertTo
        /// <summary>确定此转换器是否可以将对象转换为指定的类型。</summary>
        /// <param name="context">格式上下文。</param>
        /// <param name="destinationType">要转换到的类型。</param>
        /// <returns>如果可以转换，则为 true；否则为 false。</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>将 <see cref="Animation"/> 对象转换为字符串。</summary>
        /// <param name="context">格式上下文。</param>
        /// <param name="culture">区域性信息。</param>
        /// <param name="value">要转换的值。</param>
        /// <param name="destinationType">要转换到的类型。</param>
        /// <returns>转换后的对象。</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            // 检查传入的destinationType是否为string类型，以及value是否可以转换为Animation类型
            if (destinationType == typeof(string) && value is Animation animation)
            {
                // 获取当前文化设置的列表分隔符
                char c = culture.TextInfo.ListSeparator[0];

                // 使用LINQ的Select方法，将animation.Easing集合中的每个元素（PointF对象）转换为一个包含X和Y值的字符串，然后将结果赋值给easingParts变量
                var easingParts = animation.Easing.Select(p => $"{p.X} {p.Y}");

                // 使用string.Join方法，将easingParts集合中的每个元素用分号(;)连接起来，然后放入方括号([])中，最后将结果赋值给easingStr变量
                string easingStr = $"[{string.Join(";", easingParts)}]";

                // 返回一个字符串，包含animation的Time属性，FPS属性，以及easingStr变量的值，三者之间用逗号(,)分隔
                return $"{animation.Time}{c} {animation.FPS}{c} {easingStr}";
            }

            // 如果destinationType不是string类型，或者value不能转换为Animation类型，那么调用基类的ConvertTo方法进行处理
            return base.ConvertTo(context, culture, value, destinationType);
        }
        #endregion

        #region Instance
        /// <summary>确定此对象是否支持使用指定的上下文创建实例。</summary>
        /// <param name="context">格式上下文。</param>
        /// <returns>如果支持创建实例，则为 true；否则为 false。</returns>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

        /// <summary>使用指定的属性字典创建对象的实例。</summary>
        /// <param name="context">格式上下文。</param>
        /// <param name="propertyValues">新实例的属性值。</param>
        /// <returns>创建的对象。</returns>
        public override object? CreateInstance(ITypeDescriptorContext? context, IDictionary propertyValues)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(propertyValues);
            return new Animation(
                (int)propertyValues["Time"],
                (int)propertyValues["FPS"],
                (PointF[]?)propertyValues["Easing"]
                );
        }
        #endregion

        #region Properties
        /// <summary>确定此对象是否支持属性。</summary>
        /// <param name="context">格式上下文。</param>
        /// <returns>如果支持属性，则为 true；否则为 false。</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

        /// <summary>返回此对象的属性。</summary>
        /// <param name="context">格式上下文。</param>
        /// <param name="value">要获取属性的对象。</param>
        /// <param name="attributes">要筛选属性的属性数组。</param>
        /// <returns>属性描述符集合。</returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Animation), attributes);
            return properties.Sort(["Time", "FPS", "Easing"]);
        }
        #endregion

        /// <summary>初始化 <see cref="AnimationConverter"/> 类的新实例。</summary>
        public AnimationConverter() { }
    }
}