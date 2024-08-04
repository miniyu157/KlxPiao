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
        private Color _crosshairColor; //准星
        private Color _axisColor; //坐标轴
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


        #region PointBar外观
        /// <summary>
        /// 获取或设置圆角的大小，以 <see cref="KlxPiaoAPI.CornerRadius"/> 结构体表示。
        /// </summary>
        [Category("PointBar外观")]
        [Description("边框每个角的角半径，自动检测百分比或像素大小")]
        [DefaultValue(typeof(CornerRadius), "0,0,0,0")]
        public CornerRadius CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置坐标显示的格式。
        /// </summary>
        /// <remarks>占位符：<br/>- {x}：X<br/>- {y}：Y。</remarks>
        [Category("PointBar外观")]
        [Description("边框的颜色")]
        [DefaultValue("X:{X},Y:{Y}")]
        public string CoordinateDisplayFormat
        {
            get { return _coordinateDisplayFormat; }
            set { _coordinateDisplayFormat = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置绘制坐标时产生的偏移。
        /// </summary>
        [Category("PointBar外观")]
        [Description("绘制坐标时产生的偏移")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point CoordinateTextOffset
        {
            get { return _coordinateTextOffset; }
            set { _coordinateTextOffset = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的颜色。
        /// </summary>
        [Category("PointBar外观")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "Gray")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置圆角外部的颜色。
        /// </summary>
        [Category("PointBar外观")]
        [Description("边框外部的颜色")]
        [DefaultValue(typeof(Color), "White")]
        public Color BaseBackColor
        {
            get { return _baseBackColor; }
            set { _baseBackColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置准星的颜色。
        /// </summary>
        [Category("PointBar外观")]
        [Description("准星的颜色")]
        [DefaultValue(typeof(Color), "Red")]
        public Color CrosshairColor
        {
            get { return _crosshairColor; }
            set { _crosshairColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置准星绘制的优先级。
        /// </summary>
        [Category("PointBar外观")]
        [Description("准星绘制的优先级")]
        [DefaultValue(typeof(PriorityLevel), "Low")]
        public PriorityLevel CrosshairDrawingPriority
        {
            get { return _crosshairDrawingPriority; }
            set { _crosshairDrawingPriority = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置坐标轴的颜色。
        /// </summary>
        [Category("PointBar外观")]
        [Description("坐标轴的颜色")]
        [DefaultValue(typeof(Color), "LightPink")]
        public Color AxisColor
        {
            get { return _axisColor; }
            set { _axisColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的大小。
        /// </summary>
        [Category("PointBar外观")]
        [Description("边框的大小")]
        [DefaultValue(1)]
        public int BorderSize
        {
            get { return _borderSize; }
            set { _borderSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置准星的大小。
        /// </summary>
        [Category("PointBar外观")]
        [Description("准星的大小，为0时隐藏准星")]
        [DefaultValue(5)]
        public int CrosshairSize
        {
            get { return _crosshairSize; }
            set { _crosshairSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否显示坐标轴。
        /// </summary>
        [Category("PointBar外观")]
        [Description("显示以0,0为中心的坐标轴")]
        [DefaultValue(true)]
        public bool DisplayAxis
        {
            get { return _displayAxis; }
            set { _displayAxis = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否显示坐标信息。
        /// </summary>
        [Category("PointBar外观")]
        [Description("是否显示坐标信息")]
        [DefaultValue(true)]
        public bool DisplayCoordinates
        {
            get { return _displayCoordinates; }
            set { _displayCoordinates = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置坐标显示的位置。
        /// </summary>
        [Category("PointBar外观")]
        [Description("坐标显示的位置")]
        [DefaultValue(typeof(ContentAlignment), "TopLeft")]
        public ContentAlignment CoordinateTextAlign
        {
            get { return _coordinateTextAlign; }
            set { _coordinateTextAlign = value; Invalidate(); }
        }
        #endregion

        #region PointBar属性
        /// <summary>
        /// 获取或设置最小值。
        /// </summary>
        [Category("PointBar属性")]
        [Description("坐标的最小值")]
        [DefaultValue(typeof(Point), "-100,-100")]
        public Point MinValue
        {
            get { return _minValue; }
            set { _minValue = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置最大值。
        /// </summary>
        [Category("PointBar属性")]
        [Description("坐标的最大值")]
        [DefaultValue(typeof(Point), "100,100")]
        public Point MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置当前的值。
        /// </summary>
        [Category("PointBar属性")]
        [Description("坐标的值")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point Value
        {
            get { return _value; }
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
        [Category("PointBar属性")]
        [Description("是否可以通过方向键调整")]
        [DefaultValue(true)]
        public bool RespondToKeyboard
        {
            get { return _respondToKeyboard; }
            set { _respondToKeyboard = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置键盘一次响应的大小。
        /// </summary>
        [Category("PointBar属性")]
        [Description("通过方向键一次移动的大小")]
        [DefaultValue(1)]
        public int ResponseSize
        {
            get { return _responseSize; }
            set { _responseSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置坐标系的类型。
        /// </summary>
        [Category("PointBar属性")]
        [Description("ComputerGraphicsCoordinateSystem（第一象限在右下角）和CartesianCoordinateSystem（第一象限在右上角）")]
        [DefaultValue(typeof(CoordinateSystem), "ComputerGraphicsCoordinateSystem")]
        public CoordinateSystem CoordinateSystemType
        {
            get { return _coordinateSystemType; }
            set { _coordinateSystemType = value; Invalidate(); }
        }
        #endregion

        [Browsable(false)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; Invalidate(); }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;
            Rectangle thisRect = new(0, 0, Width, Height);

            int x总长 = MaxValue.X - MinValue.X;
            int y总长 = MaxValue.Y - MinValue.Y;

            //绘制坐标轴
            if (DisplayAxis)
            {
                using Pen 坐标系Pen = new(AxisColor, 1);

                float y轴 = (0 - MinValue.X) / (float)x总长;
                float x轴 = (0 - MinValue.Y) / (float)y总长;

                if (CoordinateSystemType == CoordinateSystem.CartesianCoordinateSystem) { x轴 = 1 - x轴; }

                g.DrawLine(坐标系Pen, Width * y轴, 0, Width * y轴, Height);
                g.DrawLine(坐标系Pen, 0, Height * x轴, Width, Height * x轴);
            }

            var 绘制边框 = new Action(() =>
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pen pen = new(BorderColor, BorderSize * 2); //修正画笔大小
                if (pen.Width != 0)
                {
                    GraphicsPath 圆角路径 = thisRect.ConvertToRoundedPath(CornerRadius);
                    g.DrawPath(pen, 圆角路径);
                }
                g.SmoothingMode = SmoothingMode.Default;
                g.PixelOffsetMode = PixelOffsetMode.Default;
            });

            var 绘制准星 = new Action(() =>
            {
                Pen 准星Pen = new(CrosshairColor, 1);
                void 画出十字(Point 坐标, int 长度)
                {
                    double x比例 = (坐标.X - MinValue.X) / (double)x总长;
                    double y比例 = (坐标.Y - MinValue.Y) / (double)y总长;

                    if (CoordinateSystemType == CoordinateSystem.CartesianCoordinateSystem) { y比例 = 1 - y比例; }

                    int newx = (int)(Width * x比例);
                    int newy = (int)(Height * y比例);

                    g.DrawLine(准星Pen, newx - 长度, newy, newx + 长度, newy);
                    g.DrawLine(准星Pen, newx, newy - 长度, newx, newy + 长度);
                }
                画出十字(Value + new Size(1, 1), CrosshairSize);
            });

            var 填充外部 = new Action(() =>
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                GraphicsPath 外部路径 = thisRect.ConvertToRoundedPath(CornerRadius, true);
                g.FillPath(new SolidBrush(BaseBackColor), 外部路径);
                g.SmoothingMode = SmoothingMode.Default;
                g.PixelOffsetMode = PixelOffsetMode.Default;
            });

            switch (CrosshairDrawingPriority)
            {
                case PriorityLevel.Low:     //文本覆盖图像
                    绘制准星();
                    绘制边框();
                    填充外部();
                    break;

                case PriorityLevel.Medium:  //文本覆盖边框，不得超出圆角
                    绘制边框();
                    绘制准星();
                    填充外部();
                    break;

                case PriorityLevel.High:    //覆盖全部元素
                    绘制边框();
                    填充外部();
                    绘制准星();
                    break;
            }

            //绘制值
            if (DisplayCoordinates)
            {
                var replacements = new Dictionary<string, string> {
                    {"{X}", Value.X.ToString()},
                    {"{Y}", Value.Y.ToString()}
                };

                string 文字 = CoordinateDisplayFormat.ReplaceMultiple(replacements);
                using SolidBrush Textbrush = new(ForeColor);

                SizeF 文字大小 = g.MeasureString(文字, Font);
                Point 文字位置 = LayoutUtilities.CalculateAlignedPosition(thisRect, 文字大小, CoordinateTextAlign, CoordinateTextOffset);

                g.DrawString(文字, Font, Textbrush, 文字位置);
            }
        }

        bool 正在拖动 = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                int x轴总长度 = MaxValue.X - MinValue.X;
                int y轴总长度 = MaxValue.Y - MinValue.Y;

                Value = new Point(
                    MinValue.X + (int)(x轴总长度 * (e.Location.X / (float)Width)),
                    MinValue.Y + (int)(y轴总长度 * (CoordinateSystemType == CoordinateSystem.CartesianCoordinateSystem ? 1 - e.Location.Y / (float)Height : e.Location.Y / (float)Height))
                );

                OnValueChanged(new ValueChangedEvent(Value));

                正在拖动 = true;
            }

            Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (正在拖动)
            {
                int x轴总长度 = MaxValue.X - MinValue.X;
                int y轴总长度 = MaxValue.Y - MinValue.Y;

                Value = new Point(
                    MinValue.X + (int)(x轴总长度 * (e.Location.X / (float)Width)),
                    MinValue.Y + (int)(y轴总长度 * (CoordinateSystemType == CoordinateSystem.CartesianCoordinateSystem ? 1 - e.Location.Y / (float)Height : e.Location.Y / (float)Height))
                );

                if (Value.X < MinValue.X) { Value = new Point(MinValue.X, Value.Y); }
                if (Value.X > MaxValue.X) { Value = new Point(MaxValue.X, Value.Y); }
                if (Value.Y < MinValue.Y) { Value = new Point(Value.X, MinValue.Y); }
                if (Value.Y > MaxValue.Y) { Value = new Point(Value.X, MaxValue.Y); }

                OnValueChanged(new ValueChangedEvent(Value));

                Refresh();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            正在拖动 = false;
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (!RespondToKeyboard) return;

            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    Point 移动后的值 = Value;

                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            移动后的值.Y -= ResponseSize * (CoordinateSystemType == CoordinateSystem.ComputerGraphicsCoordinateSystem ? 1 : -1);
                            break;
                        case Keys.Down:
                            移动后的值.Y += ResponseSize * (CoordinateSystemType == CoordinateSystem.ComputerGraphicsCoordinateSystem ? 1 : -1);
                            break;
                        case Keys.Left:
                            移动后的值.X -= ResponseSize;
                            break;
                        case Keys.Right:
                            移动后的值.X += ResponseSize;
                            break;
                    }

                    移动后的值.X = Math.Max(MinValue.X, Math.Min(MaxValue.X, 移动后的值.X));
                    移动后的值.Y = Math.Max(MinValue.Y, Math.Min(MaxValue.Y, 移动后的值.Y));

                    Value = 移动后的值;

                    OnValueChanged(new ValueChangedEvent(Value));
                    break;

                default:
                    e.IsInputKey = false;
                    break;
            }
        }
    }
}