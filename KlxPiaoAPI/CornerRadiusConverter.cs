using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace KlxPiaoAPI
{
    /// <summary>提供了一种类型转换器，用于将 <see cref="CornerRadius" /> 值与其他各种表示形式进行转换。</summary>
    public class CornerRadiusConverter : TypeConverter
    {
#pragma warning disable CS8765 // 参数类型的为 Null 性与重写成员不匹配(可能是由于为 Null 性特性)。
#pragma warning disable CA2208 // 正确实例化参数异常
#pragma warning disable CS8604 // 引用类型参数可能为 null。
#pragma warning disable CS8601 // 引用类型赋值可能为 null。
#pragma warning disable CS8605 // 取消装箱可能为 null 的值。

        /// <summary>返回此转换器是否可以将一个类型的对象转换为此转换器的类型。</summary>
        /// <param name="context">提供格式上下文的 <see cref="T:System.ComponentModel.ITypeDescriptorContext" />。</param>
        /// <param name="sourceType">表示要从中转换的类型的 <see cref="T:System.Type" />。</param>
        /// <returns>如果此对象可以执行转换，则为 <see langword="true" />；否则为 <see langword="false" />。</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>返回此转换器是否可以使用指定的上下文将对象转换为指定的类型。</summary>
        /// <param name="context">格式上下文。</param>
        /// <param name="destinationType">要转换为的类型。</param>
        /// <returns>如果此转换器可以执行转换，则为 <see langword="true" />；否则为 <see langword="false" />。</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>使用指定的上下文和区域性信息将给定对象转换为此转换器的类型。</summary>
        /// <param name="context">提供格式上下文的 <see cref="T:System.ComponentModel.ITypeDescriptorContext" />。</param>
        /// <param name="culture">要用作当前区域性的 <see cref="T:System.Globalization.CultureInfo" />。</param>
        /// <param name="value">要转换的 <see cref="T:System.Object" />。</param>
        /// <returns>表示转换后的值的 <see cref="T:System.Object" />。</returns>
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object? value)
        {
            if (value is string text)
            {
                string text2 = text.Trim();
                if (text2.Length == 0)
                {
                    return null;
                }
                culture ??= CultureInfo.CurrentCulture;
                char c = culture.TextInfo.ListSeparator[0];
                string[] array = text2.Split(c);
                float[] array2 = new float[array.Length];
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(float));
                for (int i = 0; i < array2.Length; i++)
                {
                    array2[i] = (float)converter.ConvertFromString(context, culture, array[i])!;
                }
                if (array2.Length == 4)
                {
                    return new CornerRadius(array2[0], array2[1], array2[2], array2[3]);
                }
                //指定一个值初始化新实例
                if (array2.Length == 1)
                {
                    return new CornerRadius(array2[0]);
                }

                throw new ArgumentException();
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>将给定的值对象转换为指定的类型，使用指定的上下文和区域性信息。</summary>
        /// <param name="context">提供格式上下文的 <see cref="T:System.ComponentModel.ITypeDescriptorContext" />。</param>
        /// <param name="culture">一个 <see cref="T:System.Globalization.CultureInfo" />。如果传递了 null，则假定当前区域性。</param>
        /// <param name="value">要转换的 <see cref="T:System.Object" />。</param>
        /// <param name="destinationType">要将 value 参数转换为的 <see cref="T:System.Type" />。</param>
        /// <returns>表示转换后的值的 <see cref="T:System.Object" />。</returns>
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            ArgumentNullException.ThrowIfNull(destinationType);

            if (value is CornerRadius CornerRadiusValue)
            {
                if (destinationType == typeof(string))
                {
                    CornerRadius CornerRadius1 = CornerRadiusValue;
                    culture ??= CultureInfo.CurrentCulture;
                    string separator = culture.TextInfo.ListSeparator + " ";
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(float));
                    string[] array = new string[4];
                    int num = 0;

                    array[num++] = converter.ConvertToString(context, culture, CornerRadius1.TopLeft);
                    array[num++] = converter.ConvertToString(context, culture, CornerRadius1.TopRight);
                    array[num++] = converter.ConvertToString(context, culture, CornerRadius1.BottomRight);
                    array[num++] = converter.ConvertToString(context, culture, CornerRadius1.BottomLeft);

                    return string.Join(separator, array);
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    CornerRadius CornerRadius2 = CornerRadiusValue;
                    return new InstanceDescriptor(typeof(CornerRadius).GetConstructor(
                    [
                        typeof(float),
                        typeof(float),
                        typeof(float),
                        typeof(float)
                    ]), new object[4] { CornerRadius2.TopLeft, CornerRadius2.TopRight, CornerRadius2.BottomRight, CornerRadius2.BottomLeft });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>使用指定的上下文和对象属性值的一组属性值，创建与此 <see cref="T:System.ComponentModel.TypeConverter" /> 关联的类型的实例。</summary>
        /// <param name="context">提供格式上下文的 <see cref="T:System.ComponentModel.ITypeDescriptorContext" />。</param>
        /// <param name="propertyValues">一个新的属性值的 <see cref="T:System.Collections.IDictionary" />。</param>
        /// <returns>表示给定的 <see cref="T:System.Collections.IDictionary" /> 的 <see cref="T:System.Object" />，或者如果无法创建对象，则为 <see langword="null" />。此方法始终返回 <see langword="null" />。</returns>
        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(propertyValues);
            return new CornerRadius(
                (float)propertyValues["TopLeft"],
                (float)propertyValues["TopRight"],
                (float)propertyValues["BottomRight"],
                (float)propertyValues["BottomLeft"]
                );
        }

        /// <summary>返回更改此对象上的值是否需要调用 <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> 来使用指定的上下文创建新值。</summary>
        /// <param name="context">提供格式上下文的 <see cref="T:System.ComponentModel.ITypeDescriptorContext" />。</param>
        /// <returns>
        ///   如果更改此对象上的属性需要调用 <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> 来创建新值，则为 <see langword="true" />；否则为 <see langword="false" />。</returns>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

        /// <summary>使用指定的上下文和属性，返回由值参数指定的数组类型的属性集合。</summary>
        /// <param name="context">提供格式上下文的 <see cref="T:System.ComponentModel.ITypeDescriptorContext" />。</param>
        /// <param name="value">指定要获取属性的数组类型的 <see cref="T:System.Object" />。</param>
        /// <param name="attributes">用作过滤器的 <see cref="T:System.Attribute" /> 类型的数组。</param>
        /// <returns>一个 <see cref="T:System.ComponentModel.PropertyDescriptorCollection" />，其中包含为此数据类型公开的属性，如果没有属性，则为 null。</returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(CornerRadius), attributes);
            return properties.Sort(["TopLeft", "TopRight", "BottomRight", "BottomLeft"]);
        }

        /// <summary>返回此对象是否支持属性，使用指定的上下文。</summary>
        /// <param name="context">提供格式上下文的 <see cref="T:System.ComponentModel.ITypeDescriptorContext" />。</param>
        /// <returns>
        ///   如果应调用 <see cref="Overload:System.ComponentModel.TypeConverter.GetProperties" /> 来查找此对象的属性，则为 <see langword="true" />；否则为 <see langword="false" />。</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

        /// <summary>初始化 <see cref="T:System.Windows.Forms.CornerRadiusConverter" /> 类的新实例。</summary>
        public CornerRadiusConverter() { }
    }
}