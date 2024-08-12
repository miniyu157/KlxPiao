using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 表示一个拖动条控件，可以设置边框样式、圆角大小等外观属性。
    /// </summary>
    /// <remarks>
    /// <see cref="KlxPiaoTrackBar"/> 继承自 <see cref="Control"/> 类，是原版 <see cref="TrackBar"/> 的增强版本。
    /// </remarks>
    [DefaultEvent("ValueChanged")]
    public partial class KlxPiaoTrackBar : Control
    {
        #region enums
        /// <summary>
        /// 表示鼠标滚轮响应的方向。
        /// </summary>
        public enum MouseWheelResponseMode
        {
            /// <summary>
            /// 向上增加，向下减少。
            /// </summary>
            Forward = 1, //正向
            /// <summary>
            /// 向上减少，向下增加。
            /// </summary>
            Reverse = -1, //逆向
            /// <summary>
            /// 不启用鼠标滚轮响应。
            /// </summary>
            Disabled = 0
        }

        /// <summary>
        /// 表示键盘响应的方向。
        /// </summary>
        public enum KeyboardResponseMode
        {
            /// <summary>
            /// 仅响应 Up 和 Down 键。
            /// </summary>
            UpDownOnly,
            /// <summary>
            /// 仅响应 Left 和 Right 键。
            /// </summary>
            LeftRightOnly,
            /// <summary>
            /// 响应 Up, Down, Left, Right 键。
            /// </summary>
            AllDirections,
            /// <summary>
            /// 不启用键盘响应。
            /// </summary>
            Disabled
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
        #endregion

        #region private properties
        //基本外观
        private Color _trackBackColor;
        private Color _trackForeColor;
        private int _borderSize;
        private Color _borderColor;
        private float _cornerRadius;
        private bool _isReverseDrawing;

        //值属性
        private float _value;
        private float _maxValue;
        private float _minValue;
        private int _decimalPlaces; //保留小数位数

        //交互方式
        private InteractionStyleClass _interactionStyle = new();
        private MouseWheelResponseMode _mouseWheelResponse;
        private KeyboardResponseMode _keyboardResponse;
        private MouseValueChangedEventOption _mouseDownEventOption;
        private MouseValueChangedEventOption _mouseMoveEventOption;
        private float _responseSize;

        //值文本绘制
        private ContentAlignment _valueTextDrawAlign;
        private string _valueTextDisplayFormat;
        private Point _valueTextDrawOffset;
        private bool _isDrawValueText;
        private bool _isAutoComplete;
        #endregion

        public KlxPiaoTrackBar()
        {
            InitializeComponent();

            _trackBackColor = Color.Gainsboro;
            _trackForeColor = Color.Gray;
            _borderSize = 0;
            _borderColor = Color.FromArgb(0, 210, 212);
            _cornerRadius = 1;
            _isReverseDrawing = false;

            _value = 0;
            _maxValue = 100;
            _minValue = 0;
            _decimalPlaces = 0;

            _mouseWheelResponse = MouseWheelResponseMode.Forward;
            _keyboardResponse = KeyboardResponseMode.AllDirections;
            _mouseDownEventOption = MouseValueChangedEventOption.OnDefault;
            _mouseMoveEventOption = MouseValueChangedEventOption.OnDefault;
            _responseSize = 1;

            _valueTextDrawAlign = ContentAlignment.MiddleCenter;
            _valueTextDisplayFormat = "{value}";
            _valueTextDrawOffset = Point.Empty;
            _isDrawValueText = false;

            Size = new Size(286, 10);
            BackColor = Color.White;

            //防止闪烁
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, true);
        }

        #region KlxPiaoTrackBar Basic Appearance
        /// <summary>
        /// 获取或设置绘制区域的背景色。
        /// </summary>
        [Category("KlxPiaoTrackBar Basic Appearance")]
        [Description("绘制区域的背景色")]
        [DefaultValue(typeof(Color), "Gainsboro")]
        public Color TrackBackColor
        {
            get => _trackBackColor;
            set { _trackBackColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置绘制区域的前景色。
        /// </summary>
        [Category("KlxPiaoTrackBar Basic Appearance")]
        [Description("绘制区域的前景色")]
        [DefaultValue(typeof(Color), "Gray")]
        public Color TrackForeColor
        {
            get => _trackForeColor;
            set { _trackForeColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的大小。
        /// </summary>
        [Category("KlxPiaoTrackBar Basic Appearance")]
        [Description("边框的大小")]
        [DefaultValue(0)]
        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的颜色。
        /// </summary>
        [Category("KlxPiaoTrackBar Basic Appearance")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "0,210,212")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置圆角的大小，以 <see cref="KlxPiaoAPI.CornerRadius"/> 结构体表示。
        /// </summary>
        [Category("KlxPiaoTrackBar Basic Appearance")]
        [Description("圆角的大小，支持百分比大小或像素大小")]
        [DefaultValue(1F)]
        public float CornerRadius
        {
            get => _cornerRadius;
            set
            {
                //防止超出最大范围
                float calibrationSize = Math.Min(Width, Height);
                if (value > calibrationSize)
                {
                    value = calibrationSize;
                }

                _cornerRadius = value;

                Invalidate();
            }
        }
        /// <summary>
        /// 获取或设置是否反向绘制拖动条。
        /// </summary>
        [Category("KlxPiaoTrackBar Basic Appearance")]
        [Description("是否进行反向绘制")]
        [DefaultValue(false)]
        public bool IsReverseDrawing
        {
            get => _isReverseDrawing;
            set { _isReverseDrawing = value; Invalidate(); }
        }
        #endregion

        #region KlxPiaoTrackBar Basic Properties
        /// <summary>
        /// 获取或设置当前的值。
        /// </summary>
        [Category("KlxPiaoTrackBar Basic Properties")]
        [Description("当前的值")]
        [DefaultValue(0F)]
        public float Value
        {
            get => _value;
            set
            {
                _value = value;

                OnValueChangedByCode(new ValueChangedEventArgs(value, EventTriggerType.Code, drawPercentage));

                Invalidate();
            }
        }
        /// <summary>
        /// 获取或设置最大值。
        /// </summary>
        [Category("KlxPiaoTrackBar Basic Properties")]
        [Description("最大的值")]
        [DefaultValue(100F)]
        public float MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }
        /// <summary>
        /// 获取或设置最小值。
        /// </summary>
        [Category("KlxPiaoTrackBar Basic Properties")]
        [Description("最小的值")]
        [DefaultValue(0F)]
        public float MinValue
        {
            get => _minValue;
            set => _minValue = value;
        }
        /// <summary>
        /// 获取或设置小数点后保留的位数。
        /// </summary>
        [Category("KlxPiaoTrackBar Basic Properties")]
        [Description("保留的小数位数，决定值的精度")]
        [DefaultValue(0)]
        public int DecimalPlaces
        {
            get => _decimalPlaces;
            set { _decimalPlaces = value; Invalidate(); }
        }
        #endregion

        #region KlxPiaoTrackBar Interaction
        /// <summary>
        /// 获取或设置 <see cref="KlxPiaoTrackBar"/> 的交互样式。
        /// </summary>
        [Category("KlxPiaoTrackBar Interaction")]
        [Description("用户交互时的外观")]
        public InteractionStyleClass InteractionStyle
        {
            get => _interactionStyle;
            set => _interactionStyle = value;
        }
        /// <summary>
        /// 获取或设置鼠标滚轮响应的方式。
        /// </summary>
        [Category("KlxPiaoTrackBar Interaction")]
        [Description("鼠标滚轮响应的方式，正向：向上增加，向下减少；逆向：向上减少，向下增加")]
        [DefaultValue(typeof(MouseWheelResponseMode), "Forward")]
        public MouseWheelResponseMode MouseWheelResponse
        {
            get => _mouseWheelResponse;
            set => _mouseWheelResponse = value;
        }
        /// <summary>
        /// 获取或设置键盘响应的方式。
        /// </summary>
        [Category("KlxPiaoTrackBar Interaction")]
        [Description("键盘响应的方式")]
        [DefaultValue(typeof(KeyboardResponseMode), "AllDirections")]
        public KeyboardResponseMode KeyboardResponse
        {
            get => _keyboardResponse;
            set => _keyboardResponse = value;
        }
        /// <summary>
        /// 获取或设置如何处理鼠标按下事件。
        /// </summary>
        [Category("KlxPiaoTrackBar Interaction")]
        [Description("如何处理鼠标按下事件")]
        [DefaultValue(typeof(MouseValueChangedEventOption), "OnDefault")]
        public MouseValueChangedEventOption MouseDownEventOption
        {
            get => _mouseDownEventOption;
            set => _mouseDownEventOption = value;
        }
        /// <summary>
        /// 获取或设置如何处理鼠标移动事件。
        /// </summary>
        [Category("KlxPiaoTrackBar Interaction")]
        [Description("如何处理鼠标移动事件")]
        [DefaultValue(typeof(MouseValueChangedEventOption), "OnDefault")]
        public MouseValueChangedEventOption MouseMoveEventOption
        {
            get => _mouseMoveEventOption;
            set => _mouseMoveEventOption = value;
        }
        /// <summary>
        /// 获取或设置通过鼠标滚轮或键盘交互，一次调整的大小。
        /// </summary>
        [Category("KlxPiaoTrackBar Interaction")]
        [Description("鼠标滚轮或键盘交互，一次调整的大小")]
        [DefaultValue(1F)]
        public float ResponseSize
        {
            get => _responseSize;
            set => _responseSize = value;
        }
        #endregion

        #region KlxPiaoTrackBar TextDrawing
        /// <summary>
        /// 获取或设置值文本的对齐方式。
        /// </summary>
        [Category("KlxPiaoTrackBar TextDrawing")]
        [Description("值文本的对齐方式")]
        [DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment ValueTextDrawAlign
        {
            get => _valueTextDrawAlign;
            set { _valueTextDrawAlign = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置值文本绘制的格式。
        /// </summary>
        [Category("KlxPiaoTrackBar TextDrawing")]
        [Description("值显示的格式")]
        [DefaultValue("{value}")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ValueTextDisplayFormat
        {
            get => _valueTextDisplayFormat;
            set { _valueTextDisplayFormat = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置值文本绘制的偏移。
        /// </summary>
        [Category("KlxPiaoTrackBar TextDrawing")]
        [Description("值文本的偏移")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point ValueTextDrawOffset
        {
            get => _valueTextDrawOffset;
            set { _valueTextDrawOffset = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置值是否绘制值文本。
        /// </summary>
        [Category("KlxPiaoTrackBar TextDrawing")]
        [Description("是否绘制值文本")]
        [DefaultValue(false)]
        public bool IsDrawValueText
        {
            get => _isDrawValueText;
            set { _isDrawValueText = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否自动补全至小数精度位数。
        /// </summary>
        [Category("KlxPiaoTrackBar TextDrawing")]
        [Description("是否自动补全至小数精度位数")]
        [DefaultValue(false)]
        public bool IsAutoComplete
        {
            get => _isAutoComplete;
            set { _isAutoComplete = value; Invalidate(); }
        }
        #endregion

        #region events
        /// <summary>
        /// 值改变事件处理器。
        /// </summary>
        /// <param name="value">值大小。</param>
        /// <param name="drawPercentage">绘制的百分比。</param>
        public class ValueChangedEventArgs(float value, EventTriggerType eventTriggerType, float drawPercentage) : EventArgs
        {
            public float Value { get; } = value;
            public EventTriggerType EventTriggerType { get; } = eventTriggerType;
            public float DrawPercentage { get; } = drawPercentage;
        }

        /// <summary>
        /// 用户交互触发值改变事件。
        /// </summary>
        public event EventHandler<ValueChangedEventArgs>? ValueChanged;
        /// <summary>
        /// 触发非用户交互的值改变事件。
        /// </summary>
        protected virtual void OnValueChanged(ValueChangedEventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        /// <summary>
        /// 非用户交互触发值改变事件。
        /// </summary>
        public event EventHandler<ValueChangedEventArgs>? ValueChangedByCode;
        /// <summary>
        /// 触发用户交互的值改变事件。
        /// </summary>
        protected virtual void OnValueChangedByCode(ValueChangedEventArgs e)
        {
            ValueChangedByCode?.Invoke(this, e);
        }
        #endregion

        /// <summary>
        /// 定义 <see cref="KlxPiaoTrackBar"/> 的交互样式。
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class InteractionStyleClass
        {
            /// <summary>
            /// 获取或设置控件激活时的边框颜色。
            /// </summary>
            [Category("KlxPiaoTrackBar焦点外观")]
            [Description("控件激活时的边框颜色")]
            public Color? FocusBorderColor { get; set; }
            /// <summary>
            /// 获取或设置控件激活时的边框大小。
            /// </summary>
            [Category("KlxPiaoTrackBar焦点外观")]
            [Description("控件激活时的边框大小")]
            public int? FocusBorderSize { get; set; }
            /// <summary>
            /// 获取或设置控件激活时绘制区域的前景色。
            /// </summary>
            [Category("KlxPiaoTrackBar焦点外观")]
            [Description("控件激活时绘制区域的前景色")]
            public Color? FocusTrackForeColor { get; set; }
            /// <summary>
            /// 获取或设置控件激活时绘制区域的背景色。
            /// </summary>
            [Category("KlxPiaoTrackBar焦点外观")]
            [Description("控件激活时绘制区域的背景色")]
            public Color? FocusTrackBackColor { get; set; }
            /// <summary>
            /// 获取或设置鼠标移入时的边框颜色。
            /// </summary>
            [Category("KlxPiaoTrackBar鼠标交互")]
            [Description("鼠标移入时的边框颜色")]
            public Color? MouseOverBorderColor { get; set; }
            /// <summary>
            /// 获取或设置鼠标移入时的边框大小。
            /// </summary>
            [Category("KlxPiaoTrackBar鼠标交互")]
            [Description("鼠标移入时的边框大小")]
            public int? MouseOverBorderSize { get; set; }
            /// <summary>
            /// 获取或设置鼠标移入时绘制区域的前景色。
            /// </summary>
            [Category("KlxPiaoTrackBar鼠标交互")]
            [Description("鼠标移入时绘制区域的前景色")]
            public Color? MouseOverTrackForeColor { get; set; }
            /// <summary>
            /// 获取或设置鼠标移入时绘制区域的背景色。
            /// </summary>
            [Category("KlxPiaoTrackBar鼠标交互")]
            [Description("鼠标移入时绘制区域的背景色")]
            public Color? MouseOverTrackBackColor { get; set; }

            public override string ToString()
            {
                return "";
            }
        }

        [DefaultValue(typeof(Size), "286,10")]
        public new Size Size
        {
            get => base.Size;
            set { base.Size = value; Invalidate(); }
        }

        [Browsable(false)]
        public new string Text
        {
            get => base.Text;
            set { base.Text = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.Clear(BackColor);

            using Bitmap bit = new(Width, Height);
            using Graphics g = Graphics.FromImage(bit);

            Rectangle thisRect = new(0, 0, Width, Height);

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            g.Clear(!IsReverseDrawing ? TrackBackColor : TrackForeColor);

            //如果非用户设置 Value，则根据值的大小计算绘制百分比
            if (!(draging || wheeling || keyboarding))
                drawPercentage = !IsReverseDrawing
                    ? (Value - MinValue) / (MaxValue - MinValue)
                    : (MaxValue - Value) / (MaxValue - MinValue);

            //draw progress
            float drawLength;
            Rectangle TrackForeRect;
            CornerRadius cornerRadius;

            if (thisRect.Width >= thisRect.Height) //横向
            {
                drawLength = thisRect.Width * drawPercentage;
                TrackForeRect = new(thisRect.X, thisRect.Y, (int)drawLength, Height);
                cornerRadius = new CornerRadius(CornerRadius, 0, 0, CornerRadius);
            }
            else //纵向
            {
                drawLength = thisRect.Height * drawPercentage;
                TrackForeRect = new(thisRect.X, thisRect.Y, Width, (int)drawLength);
                cornerRadius = new CornerRadius(CornerRadius, CornerRadius, 0, 0);
            }

            //draw interior
            g.DrawRounded(TrackForeRect, cornerRadius, Color.Empty, new SolidBrush(!IsReverseDrawing ? TrackForeColor : TrackBackColor));

            //draw border
            g.DrawRounded(new Rectangle(0, 0, Width, Height), new CornerRadius(CornerRadius), BackColor, new Pen(BorderColor, BorderSize));

            //draw text
            if (IsDrawValueText)
            {
                string drawText = ValueTextDisplayFormat.Replace("{value}", Value.ToString(IsAutoComplete ? $"F{DecimalPlaces}" : null));

                SizeF textSize = g.MeasureString(drawText, Font);
                PointF drawPosition = LayoutUtilities.CalculateAlignedPosition(thisRect, textSize, ValueTextDrawAlign, ValueTextDrawOffset);

                g.DrawString(drawText, Font, new SolidBrush(ForeColor), drawPosition);
            }

            pe.Graphics.DrawImage(bit, 0, 0);

            base.OnPaint(pe);
        }

        //根据鼠标拖动产生的绘制的百分比
        private float drawPercentage = 0;

        private bool draging = false;
        private bool wheeling = false;
        private bool keyboarding = false;

        #region OnMouseDown OnMouseMove OnMouseUp
        private void TriggerMouseEvent()
        {
            OnValueChanged(new ValueChangedEventArgs(Value, EventTriggerType.Mouse, drawPercentage));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                draging = true;

                void UpdateValue()
                {
                    drawPercentage = (Width >= Height) ? (float)e.X / Width : (float)e.Y / Height;
                    Value = (float)Math.Round(MinValue + drawPercentage * (MaxValue - MinValue), DecimalPlaces);
                    if (IsReverseDrawing) { Value = (float)Math.Round(MaxValue - (Value - MinValue), DecimalPlaces); }
                }

                switch (MouseDownEventOption)
                {
                    case MouseValueChangedEventOption.OnDefault:
                        UpdateValue();
                        TriggerMouseEvent();
                        break;

                    case MouseValueChangedEventOption.OnNoEvent:
                        UpdateValue();
                        break;

                    case MouseValueChangedEventOption.OnNoChangeValueNoEvent:
                        break;
                }
            }
            Focus();

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (draging)
            {
                void UpdateValue()
                {
                    drawPercentage = (Width >= Height) ? (float)e.X / Width : (float)e.Y / Height;

                    if (drawPercentage > 1) { drawPercentage = 1; }
                    if (drawPercentage < 0) { drawPercentage = 0; }

                    Value = (float)Math.Round(MinValue + drawPercentage * (MaxValue - MinValue), DecimalPlaces);

                    if (IsReverseDrawing) { Value = (float)Math.Round(MaxValue - (Value - MinValue), DecimalPlaces); }
                }

                switch (MouseMoveEventOption)
                {
                    case MouseValueChangedEventOption.OnDefault:
                        UpdateValue();
                        TriggerMouseEvent();
                        break;

                    case MouseValueChangedEventOption.OnNoEvent:
                        UpdateValue();
                        break;

                    case MouseValueChangedEventOption.OnNoChangeValueNoEvent:
                        break;
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            draging = false;
            TriggerMouseEvent();
            base.OnMouseUp(e);
        }
        #endregion

        #region OnMouseWheel OnPreviewKeyDown
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            wheeling = true;

            float adjustSize = (1 / (MaxValue - MinValue)) * ResponseSize;

            switch (e.Delta)
            {
                case > 0:
                    drawPercentage += adjustSize * (int)MouseWheelResponse;
                    break;
                default:
                    drawPercentage -= adjustSize * (int)MouseWheelResponse;
                    break;
            }

            if (drawPercentage > 1) drawPercentage = 1;
            if (drawPercentage < 0) drawPercentage = 0;

            Value = (float)Math.Round(MinValue + drawPercentage * (MaxValue - MinValue), DecimalPlaces);

            OnValueChanged(new ValueChangedEventArgs(Value, EventTriggerType.MouseWheel, drawPercentage));

            Focus();

            base.OnMouseWheel(e);
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            keyboarding = true;
            e.IsInputKey = true;

            float adjustSize = (1 / (MaxValue - MinValue)) * ResponseSize;

            switch (KeyboardResponse)
            {
                case KeyboardResponseMode.AllDirections:
                    switch (e.KeyCode)
                    {
                        case Keys.Left:
                        case Keys.Up:
                            drawPercentage -= adjustSize;
                            break;

                        case Keys.Right:
                        case Keys.Down:
                            drawPercentage += adjustSize;
                            break;

                        default:
                            e.IsInputKey = false;
                            break;
                    }
                    break;

                case KeyboardResponseMode.UpDownOnly:
                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            drawPercentage -= adjustSize;
                            break;

                        case Keys.Down:
                            drawPercentage += adjustSize;
                            break;

                        default:
                            e.IsInputKey = false;
                            break;
                    }
                    break;

                case KeyboardResponseMode.LeftRightOnly:
                    switch (e.KeyCode)
                    {
                        case Keys.Left:
                            drawPercentage -= adjustSize;
                            break;

                        case Keys.Right:
                            drawPercentage += adjustSize;
                            break;

                        default:
                            e.IsInputKey = false;
                            break;
                    }
                    break;
            }

            if (drawPercentage > 1) drawPercentage = 1;
            if (drawPercentage < 0) drawPercentage = 0;

            Value = (float)Math.Round(MinValue + drawPercentage * (MaxValue - MinValue), DecimalPlaces);

            OnValueChanged(new ValueChangedEventArgs(Value, EventTriggerType.Keyboard, drawPercentage));

            base.OnPreviewKeyDown(e);
        }
        #endregion

        #region OnMouseEnter OnMouseLeave OnGotFocus OnLostFocus
        private enum PropertyAction
        {
            Update,
            Reset
        }

        private readonly string[] properties = ["BorderSize", "BorderColor", "TrackForeColor", "TrackBackColor"];
        private readonly object?[] oldOverProperties = new object?[4];
        private readonly object?[] oldFocusProperties = new object?[4];
        private readonly string[] overProperties = ["MouseOverBorderSize", "MouseOverBorderColor", "MouseOverTrackForeColor", "MouseOverTrackBackColor"];
        private readonly string[] focusProperties = ["FocusBorderSize", "FocusBorderColor", "FocusTrackForeColor", "FocusTrackBackColor"];

        protected override void OnMouseEnter(EventArgs e)
        {
            UpdateProperties(overProperties, oldOverProperties, PropertyAction.Update);

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            UpdateProperties(overProperties, oldOverProperties, PropertyAction.Reset);

            base.OnMouseLeave(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            UpdateProperties(focusProperties, oldFocusProperties, PropertyAction.Update);

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            UpdateProperties(focusProperties, oldFocusProperties, PropertyAction.Reset);

            base.OnLostFocus(e);
        }

        private void UpdateProperties(string[] interactionPropArray, object?[] oldPropertiesArray, PropertyAction action)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                var interactionProp = InteractionStyle.SetOrGetPropertyValue(interactionPropArray[i]);

                if (interactionProp != null && (
                    (interactionProp is int) ||
                    (interactionProp is Color c && c != Color.Empty)
                    ))
                {
                    switch (action)
                    {
                        case PropertyAction.Update:
                            oldPropertiesArray[i] = this.SetOrGetPropertyValue(properties[i]);
                            this.SetOrGetPropertyValue(properties[i], interactionProp);
                            break;

                        case PropertyAction.Reset:
                            this.SetOrGetPropertyValue(properties[i], oldPropertiesArray[i]);
                            break;
                    }
                }
            }
        }
        #endregion
    }
}