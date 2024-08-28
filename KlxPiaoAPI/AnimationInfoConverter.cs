using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 将 <see cref="AnimationInfo"/> 类型的对象与其他类型之间进行转换的转换器。
    /// </summary>
    public class AnimationInfoConverter : TypeConverter
    {
        #region DisableWarning
#pragma warning disable CS8765 // 参数类型的为 Null 性与重写成员不匹配(可能是由于为 Null 性特性)。
#pragma warning disable CS8602 // 解引用可能出现空引用。
#pragma warning disable CS8603 // 可能返回 null 引用。
#pragma warning disable CS8605 // 取消装箱可能为 null 的值。
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
#pragma warning disable CS8604 // 引用类型参数可能为 null。
        #endregion

        #region ConvertFrom
        /// <summary>
        /// 确定是否可以从指定的源类型转换。
        /// </summary>
        /// <param name="context">有关转换的上下文信息。</param>
        /// <param name="sourceType">要转换的源类型。</param>
        /// <returns>如果可以转换，则为 <c>true</c>；否则为 <c>false</c>。</returns>

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// 从指定的值创建一个 <see cref="AnimationInfo"/> 对象。
        /// </summary>
        /// <param name="context">有关转换的上下文信息。</param>
        /// <param name="culture">用于转换的文化信息。</param>
        /// <param name="value">要转换的值。</param>
        /// <returns>转换后的 <see cref="AnimationInfo"/> 对象。</returns>
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string str)
            {
                char c = culture == null ? ',' : culture.TextInfo.ListSeparator[0];
                var parts = str.Split(c);

                int time = int.Parse(parts[0].Trim(), culture);
                int fps = int.Parse(parts[1].Trim(), culture);

                //用户输入了控制点，例如 300, 120, [0.77, 0, 0.175, 1]
                if (parts.Length > 3)
                {
                    string easingText = str.Split('[')[1].Trim().TrimEnd(']');
                    if (EasingUtils.IsValidControlPoint(easingText))
                    {
                        return new AnimationInfo(time, fps, easingText);
                    }
                }

                //用户输入了枚举类型的成员，例如 300, 120, EaseIn
                if (parts.Length == 3)
                {
                    string easingText = parts[2].Trim();
                    if (Enum.TryParse(easingText, true, out EasingType easingType))
                    {
                        return new AnimationInfo(time, fps, EasingUtils.GetControlPoints(easingType));
                    }
                }

                throw new ArgumentException($"Cannot convert '{value}' to type Animation.");
            }
            return base.ConvertFrom(context, culture, value);
        }
        #endregion

        #region ConvertTo
        /// <summary>
        /// 确定是否可以将对象转换为指定的目标类型。
        /// </summary>
        /// <param name="context">有关转换的上下文信息。</param>
        /// <param name="destinationType">目标类型。</param>
        /// <returns>如果可以转换，则为 <c>true</c>；否则为 <c>false</c>。</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// 将 <see cref="AnimationInfo"/> 对象转换为指定的目标类型。
        /// </summary>
        /// <param name="context">有关转换的上下文信息。</param>
        /// <param name="culture">用于转换的文化信息。</param>
        /// <param name="value">要转换的值。</param>
        /// <param name="destinationType">目标类型。</param>
        /// <returns>转换后的值。</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is AnimationInfo animation)
            {
                char c = culture.TextInfo.ListSeparator[0];
                return $"{animation.Time}{c} {animation.FPS}{c} [{animation.Easing}]";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
        #endregion

        #region Instance
        /// <summary>
        /// 获取是否支持创建实例。
        /// </summary>
        /// <param name="context">有关上下文的描述信息。</param>
        /// <returns>如果支持创建实例，则为 <c>true</c>；否则为 <c>false</c>。</returns>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

        /// <summary>
        /// 使用指定的属性值创建一个新的 <see cref="AnimationInfo"/> 实例。
        /// </summary>
        /// <param name="context">有关上下文的描述信息。</param>
        /// <param name="propertyValues">用于创建实例的属性值集合。</param>
        /// <returns>新创建的 <see cref="AnimationInfo"/> 实例。</returns>
        public override object? CreateInstance(ITypeDescriptorContext? context, IDictionary propertyValues)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(propertyValues);

            int time = (int)propertyValues["Time"];
            int fps = (int)propertyValues["FPS"];
            string easing = (string)propertyValues["Easing"];

            if (EasingUtils.IsValidControlPoint(easing))
            {
                return new AnimationInfo(time, fps, easing);
            }

            if (Enum.TryParse(easing, true, out EasingType easingType))
            {
                return new AnimationInfo(time, fps, EasingUtils.GetControlPoints(easingType));
            }

            throw new ArgumentException($"The specified easing '{easing}' is not recognized.");
        }
        #endregion

        #region Properties
        /// <summary>
        /// 获取是否支持属性。
        /// </summary>
        /// <param name="context">有关上下文的描述信息。</param>
        /// <returns>如果支持属性，则为 <c>true</c>；否则为 <c>false</c>。</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

        /// <summary>
        /// 获取 <see cref="AnimationInfo"/> 类型的属性集合，并按指定顺序排序。
        /// </summary>
        /// <param name="context">有关上下文的描述信息。</param>
        /// <param name="value">要获取属性的值。</param>
        /// <param name="attributes">要应用于属性的特性。</param>
        /// <returns>按指定顺序排序的属性集合。</returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(AnimationInfo), attributes);
            return properties.Sort(["Time", "FPS", "Easing"]);
        }
        #endregion

        public AnimationInfoConverter() { }
    }
}