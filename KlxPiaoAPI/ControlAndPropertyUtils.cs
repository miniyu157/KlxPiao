using System.Reflection;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供操作控件和处理对象属性的静态工具方法。
    /// </summary>
    public static class ControlAndPropertyUtils
    {
        /// <summary>
        /// 遍历指定容器中的控件，并对匹配的控件执行指定的操作。
        /// </summary>
        /// <typeparam name="T">要匹配的控件类型。</typeparam>
        /// <param name="container">要遍历的控件容器。</param>
        /// <param name="action">匹配成功时要执行的操作。</param>
        /// <param name="traverseSubControls">是否递归遍历子控件，默认为 false。</param>
        public static void ForEachControl<T>(this Control container, Action<T> action, bool traverseSubControls = false) where T : Control
        {
            ArgumentNullException.ThrowIfNull(container);
            ArgumentNullException.ThrowIfNull(action);

            foreach (Control c in container.Controls)
            {
                if (c is T matchingControl)
                {
                    action(matchingControl);
                }

                if (traverseSubControls && c.Controls.Count > 0)
                {
                    ForEachControl(c, action, traverseSubControls); //隐示传递true
                }
            }
        }

        /// <summary>
        /// 设置或获取对象的属性值。
        /// </summary>
        /// <param name="obj">要操作的对象。</param>
        /// <param name="propertyName">属性的名称。</param>
        /// <param name="newValue">新的属性值。如果为 null，则表示获取属性值。</param>
        /// <returns>如果 newValue 为 null，则返回属性的当前值。</returns>
        /// <exception cref="ArgumentNullException">当 obj 为 null 时抛出。</exception>
        /// <exception cref="ArgumentException">当属性名称为空或属性不存在时抛出。</exception>
        public static object? SetOrGetPropertyValue(this object obj, string propertyName, object? newValue = null)
        {
            PropertyInfo property = obj.GetPropertyInfo(propertyName);

            if (newValue != null)
            {
                if (!property.PropertyType.IsAssignableFrom(newValue.GetType()))
                    throw new ArgumentException($"值类型 {newValue.GetType().Name} 与属性类型 {property.PropertyType.Name} 不匹配。", nameof(newValue));

                property.SetValue(obj, newValue);

                return null;
            }
            else
            {
                return property.GetValue(obj);
            }
        }

        /// <summary>
        /// 获取一个对象中属性的类型。
        /// </summary>
        /// <param name="obj">要操作的对象。</param>
        /// <param name="propertyName">属性的名称。</param>
        /// <returns><see cref="Type"/> 属性的类型。</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Type GetPropertyType(this object obj, string propertyName)
        {
            return obj.GetPropertyInfo(propertyName).PropertyType;
        }

        /// <summary>
        /// 获取一个对象中的属性。
        /// </summary>
        /// <param name="obj">要操作的对象。</param>
        /// <param name="propertyName">属性的名称。</param>
        /// <returns><see cref="PropertyInfo"/> 属性的信息。</returns>
        /// <exception cref="ArgumentException"></exception>
        public static PropertyInfo GetPropertyInfo(this object obj, string propertyName)
        {
            ArgumentNullException.ThrowIfNull(obj);

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("属性名不能为空", nameof(propertyName));

            Type type = obj.GetType();

            return type.GetProperty(propertyName) ?? throw new ArgumentException($"属性 {propertyName} 在类型 {type.Name} 中不存在。", nameof(propertyName));
        }
    }
}