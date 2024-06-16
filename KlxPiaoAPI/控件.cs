using System.Reflection;
using static KlxPiaoAPI.类型;

namespace KlxPiaoAPI
{
    public static class 控件
    {
        /// <summary>
        /// 遍历指定容器中的控件，并对匹配的控件执行指定的操作。
        /// </summary>
        /// <typeparam name="T">要匹配的控件类型。</typeparam>
        /// <param name="container">要遍历的控件容器。</param>
        /// <param name="action">匹配成功时要执行的操作。</param>
        /// <param name="traverseSubControls">是否递归遍历子控件，默认为 false。</param>
        public static void 遍历<T>(this Control container, Action<T> action, bool traverseSubControls = false) where T : Control
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
                    遍历(c, action, traverseSubControls); //隐示传递true
                }
            }
        }

        /// <summary>
        /// 设置或获取对象的属性值。
        /// </summary>
        /// <param name="obj">要操作的对象。</param>
        /// <param name="propertyName">属性的名称。</param>
        /// <param name="newValue">新的属性值。如果为 null，则表示获取属性值。</param>
        /// <returns>如果 newValue 为 null，则返回属性的当前值；否则返回 null。</returns>
        /// <exception cref="ArgumentNullException">当 obj 为 null 时抛出。</exception>
        /// <exception cref="ArgumentException">当属性名称为空或属性不存在时抛出。</exception>
        public static object? SetOrGetPropertyValue(this object obj, string propertyName, object? newValue = null)
        {
            ArgumentNullException.ThrowIfNull(obj);

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("属性名不能为空", nameof(propertyName));

            Type type = obj.GetType();

            PropertyInfo property = type.GetProperty(propertyName) ?? throw new ArgumentException($"属性 {propertyName} 在类型 {type.Name} 中不存在", nameof(propertyName));

            if (newValue != null)
            {
                if (!property.PropertyType.IsAssignableFrom(newValue.GetType()))
                    throw new ArgumentException($"值类型 {newValue.GetType().Name} 与属性类型 {property.PropertyType.Name} 不匹配", nameof(newValue));

                property.SetValue(obj, newValue);
                return null;
            }
            else
            {
                return property.GetValue(obj);
            }
        }

        /// <summary>
        /// 一个委托类型，表示用户定义的曲线函数。
        /// </summary>
        /// <param name="progress">进度，范围在0到1之间。</param>
        public delegate double 自定义函数(double progress);

        /// <summary>
        /// 将贝塞尔曲线应用于控件的过渡动画。
        /// </summary>
        /// <param name="控件">要应用动画的控件。</param>
        /// <param name="属性">要过渡的属性。</param>
        /// <param name="开始值">动画的起始值。</param>
        /// <param name="结束值">动画的结束值。</param>
        /// <param name="持续时间">动画持续的时间（以毫秒为单位）。</param>
        /// <param name="控制点">贝塞尔曲线的控制点数组，留空时缓动效果为Linear。</param>
        /// <param name="帧率">动画的帧率。</param>
        /// <param name="action">每一帧动画完成时执行的操作。</param>
        /// <param name="token">用于取消动画的CancellationToken。</param>
        public static async Task 贝塞尔过渡动画(this Control 控件, string 属性, object? 开始值, object 结束值, int 持续时间, PointF[]? 控制点 = null, int 帧率 = 100, Action<double>? action = default, CancellationToken token = default)
        {
            DateTime 启动时间 = DateTime.Now;
            TimeSpan 总时长 = TimeSpan.FromMilliseconds(持续时间);
            bool 状态 = false; //true表示动画完成

            开始值 ??= SetOrGetPropertyValue(控件, 属性);
            控制点 ??= [new(0, 0), new(1, 1)];

            ITypeCollection 数字类型集合 = new NumberType();
            ITypeCollection 点和大小类型集合 = new PointOrSizeType();

            bool 颜色 = 开始值 is Color && 结束值 is Color;
            bool 单个数值类型 = 开始值 != null && 开始值.GetType() == 结束值.GetType() && 判断(数字类型集合, 开始值) && 判断(数字类型集合, 结束值);
            bool 点或大小 = 开始值 != null && 开始值.GetType() == 结束值.GetType() && 判断(点和大小类型集合, 开始值) && 判断(点和大小类型集合, 结束值);

            bool[] types = [颜色, 单个数值类型, 点或大小];

            while (!状态)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                TimeSpan 当前时间 = DateTime.Now - 启动时间;
                double 时间进度 = 当前时间.TotalMilliseconds / 总时长.TotalMilliseconds;

                if (时间进度 >= 1.0)
                {
                    状态 = true;
                    控件.Invoke(() => 控件.SetOrGetPropertyValue(属性, 结束值));
                }
                else
                {
                    double 进度 = BezierCurve.CalculateBezierPointByTime(时间进度, 控制点).Y;

                    if (开始值 != null)
                    {
                        动画逻辑(控件, 属性, 开始值, 结束值, types, 进度);
                    }
                    await Task.Delay(1000 / 帧率, token);
                }

                action?.Invoke(时间进度);
            }
        }

        /// <summary>
        /// 将用户自定义函数曲线应用于控件的过渡动画。
        /// </summary>
        /// <param name="控件">要应用动画的控件。</param>
        /// <param name="属性">要过渡的属性。</param>
        /// <param name="开始值">动画的起始值。</param>
        /// <param name="结束值">动画的结束值。</param>
        /// <param name="持续时间">动画持续的时间（以毫秒为单位）。</param>
        /// <param name="自定义曲线表达式">用户自定义的动画曲线表达式。</param>
        /// <param name="帧率">动画的帧率。</param>
        /// <param name="token">用于取消动画的CancellationToken。</param>
        /// <returns></returns>
        public static async Task 自定义过渡动画(this Control 控件, string 属性, object? 开始值, object 结束值, int 持续时间, 自定义函数 自定义曲线表达式, int 帧率 = 100, Action<double>? action = default, CancellationToken token = default)
        {
            DateTime 启动时间 = DateTime.Now;
            TimeSpan 总时长 = TimeSpan.FromMilliseconds(持续时间);
            bool 状态 = false; //true表示动画完成

            开始值 ??= 控件.SetOrGetPropertyValue(属性);

            ITypeCollection 数字类型集合 = new NumberType();
            ITypeCollection 点和大小类型集合 = new PointOrSizeType();

            bool 颜色 = 开始值 is Color && 结束值 is Color;
            bool 单个数值类型 = 开始值 != null && 开始值.GetType() == 结束值.GetType() && 判断(数字类型集合, 开始值) && 判断(数字类型集合, 结束值);
            bool 点或大小 = 开始值 != null && 开始值.GetType() == 结束值.GetType() && 判断(点和大小类型集合, 开始值) && 判断(点和大小类型集合, 结束值);

            bool[] types = [颜色, 单个数值类型, 点或大小];

            while (!状态)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                TimeSpan 当前时间 = DateTime.Now - 启动时间;
                double 时间进度 = 当前时间.TotalMilliseconds / 总时长.TotalMilliseconds;

                if (时间进度 >= 1.0)
                {
                    状态 = true;
                    控件.Invoke(() => 控件.SetOrGetPropertyValue(属性, 结束值));
                }
                else
                {
                    double 进度 = 自定义曲线表达式(时间进度); //使用用户自定义曲线表达式

                    if (开始值 != null)
                    {
                        动画逻辑(控件, 属性, 开始值, 结束值, types, 进度);
                    }
                    await Task.Delay(1000 / 帧率, token);
                }

                action?.Invoke(时间进度);
            }
        }

        //通用过渡动画逻辑
        private static void 动画逻辑(Control 控件, string 属性, object 开始值, object 结束值, bool[] types, double 进度)
        {
            if (开始值 != null && types[0])
            {
                Color startColor = (Color)开始值;
                Color endColor = (Color)结束值;

                int R = startColor.R + (int)((endColor.R - startColor.R) * 进度);
                int G = startColor.G + (int)((endColor.G - startColor.G) * 进度);
                int B = startColor.B + (int)((endColor.B - startColor.B) * 进度);
                Color newColor = Color.FromArgb(R, G, B);

                控件.Invoke(() => 控件.SetOrGetPropertyValue(属性, newColor));
            }
            else if (开始值 != null && types[1])
            {
                double startValue = Convert.ToDouble(开始值);
                double endValue = Convert.ToDouble(结束值);
                double newValue = startValue + (endValue - startValue) * 进度;

                控件.Invoke(() => 控件.SetOrGetPropertyValue(属性, newValue));
            }
            else if (开始值 != null && types[2])
            {
                object newValue = InterpolateValues(开始值, 结束值, 进度);

                控件.Invoke(() => 控件.SetOrGetPropertyValue(属性, newValue));
            }
        }

        //在两个值之间进行插值，根据指定的进度返回插值后的新值
        private static object InterpolateValues(object startValue, object endValue, double progress)
        {
            if (startValue is Size startSize && endValue is Size endSize)
            {
                int newWidth = startSize.Width + (int)((endSize.Width - startSize.Width) * progress);
                int newHeight = startSize.Height + (int)((endSize.Height - startSize.Height) * progress);
                return new Size(newWidth, newHeight);
            }
            else if (startValue is SizeF startSizeF && endValue is SizeF endSizeF)
            {
                float newWidthF = startSizeF.Width + (float)((endSizeF.Width - startSizeF.Width) * progress);
                float newHeightF = startSizeF.Height + (float)((endSizeF.Height - startSizeF.Height) * progress);
                return new SizeF(newWidthF, newHeightF);
            }
            else if (startValue is Point startPoint && endValue is Point endPoint)
            {
                int newX = startPoint.X + (int)((endPoint.X - startPoint.X) * progress);
                int newY = startPoint.Y + (int)((endPoint.Y - startPoint.Y) * progress);
                return new Point(newX, newY);
            }
            else if (startValue is PointF startPointF && endValue is PointF endPointF)
            {
                float newXF = startPointF.X + (float)((endPointF.X - startPointF.X) * progress);
                float newYF = startPointF.Y + (float)((endPointF.Y - startPointF.Y) * progress);
                return new PointF(newXF, newYF);
            }

            throw new ArgumentException("Unsupported value types for interpolation");
        }
    }
}