using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 表示一个点坐标控件，用于显示和选择一个二维点坐标。
    /// </summary>
    /// <remarks>
    /// 这个控件支持在指定的坐标系中显示和编辑一个二维点坐标。它可以显示坐标轴、定制准星样式和颜色，并提供键盘控制功能以便精确调整点的位置。
    /// </remarks>
    [DefaultEvent("ValueChanged")]
    public partial class PointBar : Control
    {
        public enum CoordinateSystem
        {
            /// <summary>
            /// 屏幕坐标系，第一象限在右下角。
            /// </summary>
            ComputerGraphicsCoordinateSystem,
            /// <summary>
            /// 传统的笛卡尔坐标系，第一象限在右上角。
            /// </summary>
            CartesianCoordinateSystem
        }

        /*
          Crosshair    准星
          Coordinate   坐标
          Axis         坐标轴
          Respond      响应
        */

        private CornerRadius _cornerRadius;
        private Color _borderColor;
        private Color _baseBackColor;
        private Color _crosshairColor;
        private Color _axisColor;
        private int _borderSize;
        private int _crosshairSize;
        private PriorityLevel _crosshairDrawingPriority;
        private bool _displayAxis;
        private bool _displayCoordinates;

        private string _coordinateDisplayFormat;
        private ContentAlignment _coordinateTextAlign;
        private Point _coordinateTextOffset;
        private Point _minValue;
        private Point _maxValue;
        private Point _value;
        private bool _respondToKeyboard;
        private int _responseSize;
        private CoordinateSystem _coordinateSystemType;
        private MouseValueChangedEventOption _mouseDownEventOption;
        private MouseValueChangedEventOption _mouseMoveEventOption;

        public PointBar()
        {
            InitializeComponent();

            _cornerRadius = new CornerRadius(0);
            _borderColor = Color.Gray;
            _baseBackColor = Color.White;
            _borderSize = 1;
            _axisColor = Color.LightPink;
            _displayAxis = true;
            _displayCoordinates = true;
            _coordinateTextAlign = ContentAlignment.TopLeft;
            _coordinateTextOffset = Point.Empty;
            _crosshairColor = Color.Red;
            _crosshairSize = 5;
            _crosshairDrawingPriority = PriorityLevel.Low;
            _coordinateDisplayFormat = "X:{X}, Y:{Y}";
            _mouseDownEventOption = MouseValueChangedEventOption.OnDefault;
            _mouseMoveEventOption = MouseValueChangedEventOption.OnDefault;

            _minValue = new Point(-100, -100);
            _maxValue = new Point(100, 100);
            _value = new Point(0, 0);
            _respondToKeyboard = true;
            _responseSize = 1;
            _coordinateSystemType = CoordinateSystem.ComputerGraphicsCoordinateSystem;

            Width = 100;
            Height = 100;
            BackColor = Color.White;

            DoubleBuffered = true;
        }

        #region events
        /// <summary>
        /// 值改变事件。
        /// </summary>
        public class ValueChangedEvent(Point point) : EventArgs
        {
            /// <summary>
            /// 改变的点
            /// </summary>
            public Point Point { get; } = point;
        }

        /// <summary>
        /// 值改变事件处理器。
        /// </summary>
        public event EventHandler<ValueChangedEvent>? ValueChanged;

        /// <summary>
        /// 当值改变时触发。
        /// </summary>
        /// <param name="e">值改变事件参数。</param>
        protected virtual void OnValueChanged(ValueChangedEvent e)
        {
            ValueChanged?.Invoke(this, e);
        }

        /// <summary>
        /// 由代码改变值的事件处理器。
        /// </summary>
        public event EventHandler<ValueChangedEvent>? ValueChangedByCode;

        /// <summary>
        /// 当由代码改变值时触发。
        /// </summary>
        /// <param name="e">值改变事件参数。</param>
        protected virtual void OnValueChangedByCode(ValueChangedEvent e)
        {
            ValueChangedByCode?.Invoke(this, e);
        }
        #endregion

        #region PointBar Appearance
        /// <summary>
        /// 获取或设置圆角的大小，以 <see cref="KlxPiaoAPI.CornerRadius"/> 结构体表示。
        /// </summary>
        [Category("PointBar Appearance")]
        [Description("边框每个角的角半径，自动检测百分比或像素大小")]
        [DefaultValue(typeof(CornerRadius), "0,0,0,0")]
        public CornerRadius CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置坐标显示的格式。
        /// </summary>
        /// <remarks>占位符：<br/>- {x}：X<br/>- {y}：Y。</remarks>
        [Category("PointBar Appearance")]
        [Description("边框的颜色")]
        [DefaultValue("X:{X},Y:{Y}")]
        public string CoordinateDisplayFormat
        {
            get => _coordinateDisplayFormat;
            set { _coordinateDisplayFormat = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置绘制坐标时产生的偏移。
        /// </summary>
        [Category("PointBar Appearance")]
        [Description("绘制坐标时产生的偏移")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point CoordinateTextOffset
        {
            get => _coordinateTextOffset;
            set { _coordinateTextOffset = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的颜色。
        /// </summary>
        [Category("PointBar Appearance")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "Gray")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置圆角外部的颜色。
        /// </summary>
        [Category("PointBar Appearance")]
        [Description("边框外部的颜色")]
        [DefaultValue(typeof(Color), "White")]
        public Color BaseBackColor
        {
            get => _baseBackColor;
            set { _baseBackColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置准星的颜色。
        /// </summary>
        [Category("PointBar Appearance")]
        [Description("准星的颜色")]
        [DefaultValue(typeof(Color), "Red")]
        public Color CrosshairColor
        {
            get => _crosshairColor;
            set { _crosshairColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置准星绘制的优先级。
        /// </summary>
        [Category("PointBar Appearance")]
        [Description("准星绘制的优先级")]
        [DefaultValue(typeof(PriorityLevel), "Low")]
        public PriorityLevel CrosshairDrawingPriority
        {
            get => _crosshairDrawingPriority;
            set { _crosshairDrawingPriority = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置坐标轴的颜色。
        /// </summary>
        [Category("PointBar Appearance")]
        [Description("坐标轴的颜色")]
        [DefaultValue(typeof(Color), "LightPink")]
        public Color AxisColor
        {
            get => _axisColor;
            set { _axisColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的大小。
        /// </summary>
        [Category("PointBar Appearance")]
        [Description("边框的大小")]
        [DefaultValue(1)]
        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置准星的大小。
        /// </summary>
        [Category("PointBar Appearance")]
        [Description("准星的大小，为0时隐藏准星")]
        [DefaultValue(5)]
        public int CrosshairSize
        {
            get => _crosshairSize;
            set { _crosshairSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否显示坐标轴。
        /// </summary>
        [Category("PointBar Appearance")]
        [Description("显示以0,0为中心的坐标轴")]
        [DefaultValue(true)]
        public bool DisplayAxis
        {
            get => _displayAxis;
            set { _displayAxis = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否显示坐标信息。
        /// </summary>
        [Category("PointBar Appearance")]
        [Description("是否显示坐标信息")]
        [DefaultValue(true)]
        public bool DisplayCoordinates
        {
            get => _displayCoordinates;
            set { _displayCoordinates = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置坐标显示的位置。
        /// </summary>
        [Category("PointBar Appearance")]
        [Description("坐标显示的位置")]
        [DefaultValue(typeof(ContentAlignment), "TopLeft")]
        public ContentAlignment CoordinateTextAlign
        {
            get => _coordinateTextAlign;
            set { _coordinateTextAlign = value; Invalidate(); }
        }
        #endregion

        #region PointBar Properties
        /// <summary>
        /// 获取或设置最小值。
        /// </summary>
        [Category("PointBar Properties")]
        [Description("坐标的最小值")]
        [DefaultValue(typeof(Point), "-100,-100")]
        public Point MinValue
        {
            get => _minValue;
            set { _minValue = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置最大值。
        /// </summary>
        [Category("PointBar Properties")]
        [Description("坐标的最大值")]
        [DefaultValue(typeof(Point), "100,100")]
        public Point MaxValue
        {
            get => _maxValue;
            set { _maxValue = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置当前的值。
        /// </summary>
        [Category("PointBar Properties")]
        [Description("坐标的值")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point Value
        {
            get => _value;
            set
            {
                _value = value;
                Invalidate();

                OnValueChangedByCode(new ValueChangedEvent(value));
            }
        }
        /// <summary>
        /// 获取或设置是否响应键盘。
        /// </summary>
        [Category("PointBar Properties")]
        [Description("是否可以通过方向键调整")]
        [DefaultValue(true)]
        public bool RespondToKeyboard
        {
            get => _respondToKeyboard;
            set { _respondToKeyboard = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置键盘一次响应的大小。
        /// </summary>
        [Category("PointBar Properties")]
        [Description("通过方向键一次移动的大小")]
        [DefaultValue(1)]
        public int ResponseSize
        {
            get => _responseSize;
            set { _responseSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置坐标系的类型。
        /// </summary>
        [Category("PointBar Properties")]
        [Description("ComputerGraphicsCoordinateSystem（第一象限在右下角）和CartesianCoordinateSystem（第一象限在右上角）")]
        [DefaultValue(typeof(CoordinateSystem), "ComputerGraphicsCoordinateSystem")]
        public CoordinateSystem CoordinateSystemType
        {
            get => _coordinateSystemType;
            set { _coordinateSystemType = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置如何处理鼠标按下事件。
        /// </summary>
        [Category("PointBar Properties")]
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
        [Category("PointBar Properties")]
        [Description("如何处理鼠标移动事件")]
        [DefaultValue(typeof(MouseValueChangedEventOption), "OnDefault")]
        public MouseValueChangedEventOption MouseMoveEventOption
        {
            get => _mouseMoveEventOption;
            set => _mouseMoveEventOption = value;
        }
        #endregion

        [Browsable(false)]
        public new string Text
        {
            get => base.Text;
            set { base.Text = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            Rectangle thisRect = new(0, 0, Width, Height);

            int xRange = MaxValue.X - MinValue.X;
            int yRange = MaxValue.Y - MinValue.Y;

            //draw axis
            if (DisplayAxis)
            {
                using Pen axisPen = new(AxisColor, 1);

                float yAxis = (0 - MinValue.X) / (float)xRange;
                float xAxis = (0 - MinValue.Y) / (float)yRange;

                if (CoordinateSystemType == CoordinateSystem.CartesianCoordinateSystem) { xAxis = 1 - xAxis; }

                g.DrawLine(axisPen, Width * yAxis, 0, Width * yAxis, Height);
                g.DrawLine(axisPen, 0, Height * xAxis, Width, Height * xAxis);
            }

            var drawBorder = new Action(() =>
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pen pen = new(BorderColor, BorderSize * 2); //修正画笔大小
                if (pen.Width != 0)
                {
                    using GraphicsPath roundedPath = thisRect.ConvertToRoundedPath(CornerRadius);
                    g.DrawPath(pen, roundedPath);
                }
                g.SmoothingMode = SmoothingMode.Default;
                g.PixelOffsetMode = PixelOffsetMode.Default;
            });

            var drawCrosshair = new Action(() =>
            {
                Pen crosshairPen = new(CrosshairColor, 1);
                void drawCrosshair(Point corr, int px)
                {
                    double xPercentage = (corr.X - MinValue.X) / (double)xRange;
                    double yPercentage = (corr.Y - MinValue.Y) / (double)yRange;

                    if (CoordinateSystemType == CoordinateSystem.CartesianCoordinateSystem) yPercentage = 1 - yPercentage;

                    int newx = (int)(Width * xPercentage);
                    int newy = (int)(Height * yPercentage);

                    g.DrawLine(crosshairPen, newx - px, newy, newx + px, newy);
                    g.DrawLine(crosshairPen, newx, newy - px, newx, newy + px);
                }
                drawCrosshair(Value, CrosshairSize);
            });

            var drawOuter = new Action(() =>
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using GraphicsPath outerPath = thisRect.ConvertToRoundedPath(CornerRadius, true);
                g.FillPath(new SolidBrush(BaseBackColor), outerPath);
                g.SmoothingMode = SmoothingMode.Default;
                g.PixelOffsetMode = PixelOffsetMode.Default;
            });

            switch (CrosshairDrawingPriority)
            {
                case PriorityLevel.Low:     //文本覆盖图像
                    drawCrosshair();
                    drawBorder();
                    drawOuter();
                    break;

                case PriorityLevel.Medium:  //文本覆盖边框，不得超出圆角
                    drawBorder();
                    drawCrosshair();
                    drawOuter();
                    break;

                case PriorityLevel.High:    //覆盖全部元素
                    drawBorder();
                    drawOuter();
                    drawCrosshair();
                    break;
            }

            //draw text
            if (DisplayCoordinates)
            {
                var replacements = new Dictionary<string, string> {
                    {"{X}", Value.X.ToString()},
                    {"{Y}", Value.Y.ToString()}
                };

                string text = CoordinateDisplayFormat.ReplaceMultiple(replacements);
                using SolidBrush Textbrush = new(ForeColor);

                SizeF textSize = g.MeasureString(text, Font);
                PointF textPos = LayoutUtilities.CalculateAlignedPosition(thisRect, textSize, CoordinateTextAlign, CoordinateTextOffset);

                g.DrawString(text, Font, Textbrush, textPos);
            }

            base.OnPaint(pe);
        }

        bool draging = false;
        private void TriggerMouseEvent()
        {
            OnValueChanged(new ValueChangedEvent(Value));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                draging = true;

                void UpdateValue()
                {
                    int xRange = MaxValue.X - MinValue.X;
                    int yRange = MaxValue.Y - MinValue.Y;

                    Value = new Point(
                        MinValue.X + (int)Math.Round(xRange * (e.Location.X / (float)Width)),
                        MinValue.Y + (int)Math.Round(yRange * (CoordinateSystemType == CoordinateSystem.CartesianCoordinateSystem
                        ? 1 - e.Location.Y / (float)Height
                        : e.Location.Y / (float)Height))
                    );
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
                    int xRange = MaxValue.X - MinValue.X;
                    int yRange = MaxValue.Y - MinValue.Y;

                    Value = new Point(
                        MinValue.X + (int)Math.Round(xRange * (e.Location.X / (float)Width)),
                        MinValue.Y + (int)Math.Round(yRange * (CoordinateSystemType == CoordinateSystem.CartesianCoordinateSystem
                        ? 1 - e.Location.Y / (float)Height
                        : e.Location.Y / (float)Height))
                    );

                    if (Value.X < MinValue.X) Value = new Point(MinValue.X, Value.Y);
                    if (Value.X > MaxValue.X) Value = new Point(MaxValue.X, Value.Y);
                    if (Value.Y < MinValue.Y) Value = new Point(Value.X, MinValue.Y);
                    if (Value.Y > MaxValue.Y) Value = new Point(Value.X, MaxValue.Y);
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

                Refresh();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            draging = false;
            TriggerMouseEvent();
            base.OnMouseUp(e);
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (!RespondToKeyboard) return;

            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    Point newValue = Value;

                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            newValue.Y -= ResponseSize * (CoordinateSystemType == CoordinateSystem.ComputerGraphicsCoordinateSystem ? 1 : -1);
                            break;

                        case Keys.Down:
                            newValue.Y += ResponseSize * (CoordinateSystemType == CoordinateSystem.ComputerGraphicsCoordinateSystem ? 1 : -1);
                            break;

                        case Keys.Left:
                            newValue.X -= ResponseSize;
                            break;

                        case Keys.Right:
                            newValue.X += ResponseSize;
                            break;
                    }

                    newValue.X = Math.Max(MinValue.X, Math.Min(MaxValue.X, newValue.X));
                    newValue.Y = Math.Max(MinValue.Y, Math.Min(MaxValue.Y, newValue.Y));

                    Value = newValue;

                    OnValueChanged(new ValueChangedEvent(Value));
                    break;

                default:
                    e.IsInputKey = false;
                    break;
            }

            base.OnPreviewKeyDown(e);
        }
    }
}