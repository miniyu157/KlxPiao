using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 表示一个增强的拖动条控件，可以设置边框样式、圆角大小等外观属性。
    /// </summary>
    /// <remarks>
    /// <see cref="KlxPiaoTrackBar"/> 继承自 <see cref="Control"/> 类，是原版 <see cref="TrackBar"/> 的增强版本。
    /// </remarks>
    [DefaultEvent("ValueChanged")]
    public partial class KlxPiaoTrackBar : Control
    {
        /// <summary>
        /// 表示鼠标滚轮响应的方向。
        /// </summary>
        public enum 鼠标滚轮响应
        {
            /// <summary>
            /// 向上增加，向下减少
            /// </summary>
            正向 = 1,
            /// <summary>
            /// 向上减少，向下增加
            /// </summary>
            逆向 = -1,
            /// <summary>
            /// 不启用鼠标滚轮响应
            /// </summary>
            不启用 = 0
        }

        /// <summary>
        /// 表示键盘响应的方向。
        /// </summary>
        public enum 键盘响应
        {
            /// <summary>
            /// 仅响应Up和Down键
            /// </summary>
            仅上下键,
            /// <summary>
            /// 仅响应Left和Right键
            /// </summary>
            仅左右键,
            /// <summary>
            /// 响应Up,Down,Left,Right键
            /// </summary>
            全方向键,
            /// <summary>
            /// 不启用键盘响应
            /// </summary>
            不启用
        }

        /// <summary>
        /// 表示文字绘制的位置。
        /// </summary>
        public enum 文字位置
        {
            不显示,
            左,
            居中,
            右,
            跟随
        }

        /// <summary>
        /// 表示触发事件的类型。
        /// </summary>
        public enum EventTriggerType
        {
            /// <summary>
            /// 非用户交互触发。
            /// </summary>
            Code,
            /// <summary>
            /// 鼠标调整时触发。
            /// </summary>
            Mouse,
            /// <summary>
            /// 键盘调整时触发。
            /// </summary>
            Keyboard,
            /// <summary>
            /// 鼠标滚轮调整时触发
            /// </summary>
            MouseWheel
        }

        private Color _背景色;
        private Color _前景色;
        private int _边框大小;
        private Color _边框颜色;
        private 文字位置 _值显示方式;
        private int _跟随时偏移;
        private float _圆角大小;
        private bool _反向绘制;

        private int _值显示边距;
        private Color _焦点边框颜色;
        private int _焦点边框大小;
        private Color _焦点前景色;

        private Color _焦点背景色;
        private Color _移入边框颜色;
        private int _移入边框大小;
        private Color _移入前景色;

        private Color _移入背景色;
        private float _值;
        private float _最大值;
        private float _最小值;
        private 鼠标滚轮响应 _鼠标滚轮响应方式;
        private 键盘响应 _键盘响应方式;
        private float _增减大小;
        private int _保留小数位数;
        private string _值显示格式;

        public KlxPiaoTrackBar()
        {
            InitializeComponent();

            _背景色 = Color.Gainsboro;
            _前景色 = Color.Gray;
            _边框大小 = 0;
            _边框颜色 = Color.FromArgb(0, 210, 212);
            _值显示方式 = 文字位置.不显示;
            _跟随时偏移 = -9001;
            _值显示边距 = 0;
            _圆角大小 = 1;
            _反向绘制 = false;

            _焦点边框颜色 = Color.Transparent;
            _焦点边框大小 = -1;
            _焦点前景色 = Color.Transparent;
            _焦点背景色 = Color.Transparent;

            _移入边框颜色 = Color.Transparent;
            _移入边框大小 = -1;
            _移入前景色 = Color.Transparent;
            _移入背景色 = Color.Transparent;

            _值 = 0;
            _最大值 = 100;
            _最小值 = 0;
            _鼠标滚轮响应方式 = 鼠标滚轮响应.正向;
            _键盘响应方式 = 键盘响应.全方向键;
            _增减大小 = 1;
            _保留小数位数 = 0;
            _值显示格式 = "{value}";

            Size = new Size(286, 10);
            BackColor = Color.White;

            //防止闪烁
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, true);
        }

        #region KlxPiaoTrackBar外观
        [Category("KlxPiaoTrackBar外观")]
        [Description("组件的背景色")]
        [DefaultValue(typeof(Color), "Gainsboro")]
        public Color 背景色
        {
            get { return _背景色; }
            set { _背景色 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar外观")]
        [Description("组件的前景色")]
        [DefaultValue(typeof(Color), "Gray")]
        public Color 前景色
        {
            get { return _前景色; }
            set { _前景色 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar外观")]
        [Description("边框的大小，为0时隐藏边框")]
        [DefaultValue(0)]
        public int 边框大小
        {
            get { return _边框大小; }
            set { _边框大小 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar外观")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "0,210,212")]
        public Color 边框颜色
        {
            get { return _边框颜色; }
            set { _边框颜色 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar外观")]
        [Description("显示值到拖动条上，字体由Font属性决定，字体颜色由ForeColor属性决定")]
        [DefaultValue(typeof(文字位置), "不显示")]
        public 文字位置 值显示方式
        {
            get { return _值显示方式; }
            set { _值显示方式 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar外观")]
        [Description("仅当值显示方式为跟随时有效。特殊值：0 -> 居中，-9001 -> 一侧，-9002 -> 另一侧")]
        [DefaultValue(-9001)]
        public int 跟随时偏移
        {
            get { return _跟随时偏移; }
            set { _跟随时偏移 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar外观")]
        [Description("显示值的左右边距")]
        [DefaultValue(0)]
        public int 值显示边距
        {
            get { return _值显示边距; }
            set { _值显示边距 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar外观")]
        [Description("圆角的大小，自动检测是百分比大小还是像素大小")]
        [DefaultValue(1F)]
        public float 圆角大小
        {
            get { return _圆角大小; }
            set
            {
                //防止超出最大范围
                float 校准大小 = Math.Min(Width, Height);
                if (value > 校准大小)
                {
                    value = 校准大小;
                }

                _圆角大小 = value;


                Invalidate();
            }
        }
        [Category("KlxPiaoTrackBar外观")]
        [Description("是否进行反向绘制")]
        [DefaultValue(false)]
        public bool 反向绘制
        {
            get { return _反向绘制; }
            set { _反向绘制 = value; Invalidate(); }
        }
        #endregion

        #region KlxPiaoTrackBar焦点
        [Category("KlxPiaoTrackBar焦点")]
        [Description("控件激活时的边框颜色，Transparent：控件激活时不改变边框颜色")]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color 焦点边框颜色
        {
            get { return _焦点边框颜色; }
            set { _焦点边框颜色 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar焦点")]
        [Description("控件激活时的边框大小，-1：控件激活时不改变边框大小")]
        [DefaultValue(-1)]
        public int 焦点边框大小
        {
            get { return _焦点边框大小; }
            set { _焦点边框大小 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar焦点")]
        [Description("控件激活时的前景色，Transparent：控件激活时不改变前景色")]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color 焦点前景色
        {
            get { return _焦点前景色; }
            set { _焦点前景色 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar焦点")]
        [Description("控件激活时的背景色，Transparent：控件激活时不改变背景色")]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color 焦点背景色
        {
            get { return _焦点背景色; }
            set { _焦点背景色 = value; Invalidate(); }
        }
        #endregion

        #region KlxPiaoTrackBar移入
        [Category("KlxPiaoTrackBar移入")]
        [Description("鼠标移入时的边框颜色，Transparent：鼠标移入时不改变边框颜色")]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color 移入边框颜色
        {
            get { return _移入边框颜色; }
            set { _移入边框颜色 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar移入")]
        [Description("鼠标移入时的边框大小，-1：鼠标移入时不改变边框大小")]
        [DefaultValue(-1)]
        public int 移入边框大小
        {
            get { return _移入边框大小; }
            set { _移入边框大小 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar移入")]
        [Description("鼠标移入时的前景色，Transparent：鼠标移入时不改变前景色")]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color 移入前景色
        {
            get { return _移入前景色; }
            set { _移入前景色 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar移入")]
        [Description("鼠标移入时的背景色，Transparent：鼠标移入时不改变背景色")]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color 移入背景色
        {
            get { return _移入背景色; }
            set { _移入背景色 = value; Invalidate(); }
        }
        #endregion

        /// <summary>
        /// 值改变事件
        /// </summary>
        /// <param name="value">值大小。</param>
        /// <param name="drawPercentage">绘制的百分比。</param>
        public class ValueChangedEventArgs(float value, EventTriggerType eventTriggerType, float drawPercentage) : EventArgs
        {
            public float Value { get; } = value;
            public EventTriggerType EventTriggerType { get; } = eventTriggerType;
            public float DrawPercentage { get; } = drawPercentage;
        }

        //用户交互触发值改变事件
        public event EventHandler<ValueChangedEventArgs>? ValueChanged;
        protected virtual void OnValueChanged(ValueChangedEventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        //代码设置触发值改变事件
        public event EventHandler<ValueChangedEventArgs>? ValueChangedByCode;
        protected virtual void OnValueChangedByCode(ValueChangedEventArgs e)
        {
            ValueChangedByCode?.Invoke(this, e);
        }

        [Category("KlxPiaoTrackBar属性")]
        [Description("当前的值")]
        [DefaultValue(0F)]
        public float 值
        {
            get { return _值; }
            set
            {
                _值 = value;

                OnValueChangedByCode(new ValueChangedEventArgs(value, EventTriggerType.Code, 绘制百分比));

                Invalidate();
            }
        }
        #region "KlxPiaoTrackBar属性"
        [Category("KlxPiaoTrackBar属性")]
        [Description("最大的值")]
        [DefaultValue(100F)]
        public float 最大值
        {
            get { return _最大值; }
            set { _最大值 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar属性")]
        [Description("最小的值")]
        [DefaultValue(0F)]
        public float 最小值
        {
            get { return _最小值; }
            set { _最小值 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar属性")]
        [Description("鼠标滚轮响应的方式，正向：向上增加，向下减少；逆向：向上减少，向下增加")]
        [DefaultValue(typeof(鼠标滚轮响应), "正向")]
        public 鼠标滚轮响应 鼠标滚轮响应方式
        {
            get { return _鼠标滚轮响应方式; }
            set { _鼠标滚轮响应方式 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar属性")]
        [Description("键盘响应的方式")]
        [DefaultValue(typeof(键盘响应), "全方向键")]
        public 键盘响应 键盘响应方式
        {
            get { return _键盘响应方式; }
            set { _键盘响应方式 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar属性")]
        [Description("鼠标滚轮或键盘按下一次增减的大小")]
        [DefaultValue(1F)]
        public float 增减大小
        {
            get { return _增减大小; }
            set { _增减大小 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar属性")]
        [Description("保留的小数位数，决定值的精度")]
        [DefaultValue(0)]
        public int 保留小数位数
        {
            get { return _保留小数位数; }
            set { _保留小数位数 = value; Invalidate(); }
        }
        [Category("KlxPiaoTrackBar属性")]
        [Description("值显示的格式")]
        [DefaultValue("{value}")]
        public string 值显示格式
        {
            get { return _值显示格式; }
            set { _值显示格式 = value; Invalidate(); }
        }
        #endregion

        [DefaultValue(typeof(Size), "286,10")]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; Invalidate(); }
        }

        [Browsable(false)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; Invalidate(); }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            pe.Graphics.Clear(BackColor);

            Bitmap bit = new(Width, Height);
            Graphics g = Graphics.FromImage(bit);

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.Clear(!反向绘制 ? 背景色 : 前景色);

            Rectangle 工作矩形 = new(0, 0, Width, Height);

            //如果代码设置值非用户设置，则根据值的大小计算绘制百分比
            if (!(正在拖动 || 正在滚动 || 正在按键))
                绘制百分比 = !反向绘制 ? (值 - 最小值) / (最大值 - 最小值) : (最大值 - 值) / (最大值 - 最小值);

            //绘制前景
            Rectangle 前景区域;
            float 百分比位置;
            CornerRadius cornerRadius;

            if (工作矩形.Width >= 工作矩形.Height) //横向
            {
                百分比位置 = 工作矩形.Width * 绘制百分比;
                前景区域 = new(工作矩形.X, 工作矩形.Y, (int)百分比位置, Height);
                cornerRadius = new CornerRadius(圆角大小, 0, 0, 圆角大小);
            }
            else //纵向
            {
                百分比位置 = 工作矩形.Height * 绘制百分比;
                前景区域 = new(工作矩形.X, 工作矩形.Y, Width, (int)百分比位置);
                cornerRadius = new CornerRadius(圆角大小, 圆角大小, 0, 0);
            }
            g.DrawRounded(前景区域, cornerRadius, Color.Empty, new SolidBrush(!反向绘制 ? 前景色 : 背景色));

            //绘制边框
            g.DrawRounded(new Rectangle(0, 0, Width, Height), new CornerRadius(圆角大小), BackColor, new Pen(边框颜色, 边框大小));

            // 绘制值
            if (值显示方式 != 文字位置.不显示)
            {
                SizeF 文字大小 = g.MeasureString(值显示格式.Replace("{value}", 值.ToString()), Font);
                float 竖直居中 = (Height - 文字大小.Height) / 2;
                float 横向居中 = (Width - 文字大小.Width) / 2;

                PointF 绘制位置 = 值显示方式 switch
                {
                    文字位置.左 => new PointF(边框大小 + 值显示边距, 竖直居中),
                    文字位置.右 => new PointF(Width - 边框大小 - 文字大小.Width - 值显示边距, 竖直居中),
                    文字位置.居中 => new PointF((Width - 文字大小.Width) / 2, 竖直居中),
                    _ => PointF.Empty,
                };

                if (值显示方式 == 文字位置.跟随)
                {
                    float 偏移量 = 跟随时偏移 switch
                    {
                        -9001 => 工作矩形.Width >= 工作矩形.Height ? -文字大小.Width : -文字大小.Height,
                        -9002 => 工作矩形.Width >= 工作矩形.Height ? +文字大小.Width / 5 : +文字大小.Height / 5,
                        _ => 工作矩形.Width >= 工作矩形.Height ? -文字大小.Width / 2 + 跟随时偏移 : -文字大小.Height / 2 + 跟随时偏移
                    };

                    if (工作矩形.Width >= 工作矩形.Height) //横向
                    {
                        绘制位置 = new(工作矩形.Width * 绘制百分比 + 偏移量, 竖直居中);
                    }
                    else //纵向
                    {
                        绘制位置 = new(横向居中, 工作矩形.Height * 绘制百分比 + 偏移量);
                    }
                }

                g.DrawString(值显示格式.Replace("{value}", 值.ToString()), Font, new SolidBrush(ForeColor), 绘制位置);
            }

            pe.Graphics.DrawImage(bit, 0, 0);
        }

        private float 绘制百分比 = 0;

        #region 鼠标调整值
        private bool 正在拖动 = false;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                正在拖动 = true;

                绘制百分比 = (Width >= Height) ? (float)e.X / Width : (float)e.Y / Height;
                值 = (float)Math.Round(最小值 + 绘制百分比 * (最大值 - 最小值), 保留小数位数);

                if (反向绘制) { 值 = (float)Math.Round(最大值 - (值 - 最小值), 保留小数位数); }

                OnValueChanged(new ValueChangedEventArgs(值, EventTriggerType.Mouse, 绘制百分比));
            }

            Focus();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (正在拖动)
            {
                绘制百分比 = (Width >= Height) ? (float)e.X / Width : (float)e.Y / Height;

                if (绘制百分比 > 1) { 绘制百分比 = 1; }
                if (绘制百分比 < 0) { 绘制百分比 = 0; }

                值 = (float)Math.Round(最小值 + 绘制百分比 * (最大值 - 最小值), 保留小数位数);

                if (反向绘制) { 值 = (float)Math.Round(最大值 - (值 - 最小值), 保留小数位数); }

                OnValueChanged(new ValueChangedEventArgs(值, EventTriggerType.Mouse, 绘制百分比));
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            正在拖动 = false;
        }
        #endregion

        #region 滚轮调整值
        private bool 正在滚动 = false;

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            正在滚动 = true;

            float 一次移动大小 = (1 / (最大值 - 最小值)) * 增减大小;

            switch (e.Delta)
            {
                case > 0:
                    绘制百分比 += 一次移动大小 * (int)鼠标滚轮响应方式;
                    break;
                default:
                    绘制百分比 -= 一次移动大小 * (int)鼠标滚轮响应方式;
                    break;
            }

            if (绘制百分比 > 1) 绘制百分比 = 1;
            if (绘制百分比 < 0) 绘制百分比 = 0;

            值 = (float)Math.Round(最小值 + 绘制百分比 * (最大值 - 最小值), 保留小数位数);

            OnValueChanged(new ValueChangedEventArgs(值, EventTriggerType.MouseWheel, 绘制百分比));

            Focus();
        }
        #endregion

        #region 键盘调整值
        private bool 正在按键 = false;

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            正在按键 = true;
            e.IsInputKey = true;

            float 一次移动大小 = (1 / (最大值 - 最小值)) * 增减大小;

            switch (键盘响应方式)
            {
                case 键盘响应.全方向键:
                    switch (e.KeyCode)
                    {
                        case Keys.Left:
                        case Keys.Up:
                            绘制百分比 -= 一次移动大小;
                            break;
                        case Keys.Right:
                        case Keys.Down:
                            绘制百分比 += 一次移动大小;
                            break;
                        default:
                            e.IsInputKey = false;
                            break;
                    }
                    break;
                case 键盘响应.仅上下键:
                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            绘制百分比 -= 一次移动大小;
                            break;
                        case Keys.Down:
                            绘制百分比 += 一次移动大小;
                            break;
                        default:
                            e.IsInputKey = false;
                            break;
                    }
                    break;
                case 键盘响应.仅左右键:
                    switch (e.KeyCode)
                    {
                        case Keys.Left:
                            绘制百分比 -= 一次移动大小;
                            break;
                        case Keys.Right:
                            绘制百分比 += 一次移动大小;
                            break;
                        default:
                            e.IsInputKey = false;
                            break;
                    }
                    break;
            }

            if (绘制百分比 > 1) 绘制百分比 = 1;
            if (绘制百分比 < 0) 绘制百分比 = 0;

            值 = (float)Math.Round(最小值 + 绘制百分比 * (最大值 - 最小值), 保留小数位数);

            OnValueChanged(new ValueChangedEventArgs(值, EventTriggerType.Keyboard, 绘制百分比));
        }
        #endregion

        #region 应用焦点反馈、移入反馈
        private int 原来的边框大小1;
        private Color 原来的边框颜色1;
        private Color 原来的前景色1;
        private Color 原来的背景色1;

        private int 原来的边框大小2;
        private Color 原来的边框颜色2;
        private Color 原来的前景色2;
        private Color 原来的背景色2;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            原来的边框大小1 = 边框大小;
            原来的边框颜色1 = 边框颜色;
            原来的前景色1 = 前景色;
            原来的背景色1 = 背景色;

            if (移入边框大小 != -1) 边框大小 = 移入边框大小;
            if (移入边框颜色 != Color.Transparent) 边框颜色 = 移入边框颜色;
            if (移入前景色 != Color.Transparent) 前景色 = 移入前景色;
            if (移入背景色 != Color.Transparent) 背景色 = 移入背景色;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (移入边框大小 != -1) 边框大小 = 原来的边框大小1;
            if (移入边框颜色 != Color.Transparent) 边框颜色 = 原来的边框颜色1;
            if (移入前景色 != Color.Transparent) 前景色 = 原来的前景色1;
            if (移入背景色 != Color.Transparent) 背景色 = 原来的背景色1;
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            原来的边框大小2 = 边框大小;
            原来的边框颜色2 = 边框颜色;
            原来的前景色2 = 前景色;
            原来的背景色2 = 背景色;

            if (焦点边框大小 != -1) 边框大小 = 焦点边框大小;
            if (焦点边框颜色 != Color.Transparent) 边框颜色 = 焦点边框颜色;
            if (焦点前景色 != Color.Transparent) 前景色 = 焦点前景色;
            if (焦点背景色 != Color.Transparent) 背景色 = 焦点背景色;
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (焦点边框大小 != -1) 边框大小 = 原来的边框大小2;
            if (焦点边框颜色 != Color.Transparent) 边框颜色 = 原来的边框颜色2;
            if (焦点前景色 != Color.Transparent) 前景色 = 原来的前景色2;
            if (焦点背景色 != Color.Transparent) 背景色 = 原来的背景色2;
        }
        #endregion
    }
}