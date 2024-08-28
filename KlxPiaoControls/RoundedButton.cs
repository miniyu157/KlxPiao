using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 一个自定义的带圆角和图像支持的按钮控件。
    /// </summary>
    /// <remarks>
    /// <see cref="RoundedButton"/> 继承自 <see cref="Control"/> 类，是原版 <see cref="Button"/> 的增强版本。
    /// </remarks>
    public partial class RoundedButton : Control
    {
        #region private properties
        //Basic Properties
        private Image? _image;
        private ContentAlignment _imageAlign;
        private Point _imageOffset;
        private CornerRadius _imageCornerRadius;
        private SizeF _imageResizing;
        private ResizeMode _imageResizingFormat;
        private PictureBoxSizeMode _imageSizeMode;
        private bool _isEnableAnimation;
        private Point _textOffset;
        private ContentAlignment _textAlign;

        //Border
        private float _borderSize;
        private Color _borderColor;
        private CornerRadius _borderCornerRadius;
        private Color _baseBackColor;

        //Interaction
        private DisabledStyleClass _disabledStyle = new();
        private InteractionStyleClass _interactionStyle = new();
        private AnimationConfigClass _animationConfig = new();
        private MouseClickModeEnum _mouseClickMode;
        #endregion

        /// <summary>
        /// 定义鼠标点击模式。
        /// </summary>
        public enum MouseClickModeEnum
        {
            /// <summary>
            /// 仅左键点击。
            /// </summary>
            LeftOnly,

            /// <summary>
            /// 仅右键点击。
            /// </summary>
            RightOnly,

            /// <summary>
            /// 左键或右键点击。
            /// </summary>
            LeftOrRight,

            /// <summary>
            /// 任意鼠标按钮点击。
            /// </summary>
            Arbitrarily
        }

        public RoundedButton()
        {
            InitializeComponent();

            _image = null;
            _imageAlign = ContentAlignment.MiddleCenter;
            _imageOffset = Point.Empty;
            _imageCornerRadius = new CornerRadius(0);
            _imageResizing = new SizeF(0, 0);
            _imageResizingFormat = ResizeMode.Pixel;
            _imageSizeMode = PictureBoxSizeMode.Zoom;
            _isEnableAnimation = true;
            _textOffset = Point.Empty;
            _textAlign = ContentAlignment.MiddleCenter;

            _borderCornerRadius = new CornerRadius(10);
            _baseBackColor = Color.White;

            _mouseClickMode = MouseClickModeEnum.LeftOnly;
            _interactionStyle.OverBackColor = Color.FromArgb(245, 245, 245);
            _interactionStyle.DownBackColor = Color.FromArgb(235, 235, 235);
            _animationConfig.ColorAnimation = new AnimationInfo(150, 30, EasingType.Linear);
            _animationConfig.BorderColorAnimation = new AnimationInfo(150, 30, EasingType.Linear);
            _animationConfig.BorderSizeAnimation = new AnimationInfo(100, 30, EasingType.Linear);

            DoubleBuffered = true;

            //animation properties
            BackColor = Color.White;
            ForeColor = Color.Black;
            BorderColor = Color.Gainsboro;
            Size = new Size(116, 43);
            BorderSize = 1;

            SetStyle(ControlStyles.Selectable, true);
        }

        [DefaultValue(typeof(Size), "116, 43")]
        public new Size Size
        {
            get => base.Size;
            set
            {
                base.Size = value;
                Invalidate();
            }
        }

        #region RoundedButton Basic Properties
        /// <summary>
        /// 将在控件上显示的图像。
        /// </summary>
        [Category("RoundedButton Basic Properties")]
        [Description("将在控件上显示的图像。")]
        [DefaultValue(null)]
        public Image? Image
        {
            get => _image;
            set { _image = value; Invalidate(); }
        }
        /// <summary>
        /// 将在控件上显示的图像的对其方式。
        /// </summary>
        [Category("RoundedButton Basic Properties")]
        [Description("将在控件上显示的图像的对其方式。")]
        [DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment ImageAlign
        {
            get => _imageAlign;
            set { _imageAlign = value; Invalidate(); }
        }
        /// <summary>
        /// 图像绘制的偏移。
        /// </summary>
        [Category("RoundedButton Basic Properties")]
        [Description("图像绘制的偏移")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point ImageOffset
        {
            get => _imageOffset;
            set { _imageOffset = value; Invalidate(); }
        }
        /// <summary>
        /// 图像圆角的角半径，以 <see cref="CornerRadius"/> 结构体表示。
        /// </summary>
        [Category("RoundedButton Basic Properties")]
        [Description("图像圆角的大小，自动检测百分比或像素")]
        [DefaultValue(typeof(CornerRadius), "0,0,0,0")]
        public CornerRadius ImageCornerRadius
        {
            get => _imageCornerRadius;
            set { _imageCornerRadius = value; Invalidate(); }
        }
        /// <summary>
        /// 按比例或像素重置图像大小。
        /// </summary>
        [Category("RoundedButton Basic Properties")]
        [Description("指定一个新的大小（像素或百分比）缩放图像，新的图像位置会基于原位置居中")]
        [DefaultValue(typeof(SizeF), "0,0")]
        public SizeF ImageResizing
        {
            get => _imageResizing;
            set { _imageResizing = value; Invalidate(); }
        }
        /// <summary>
        /// 指定图像大小修正的格式。
        /// </summary>
        [Category("RoundedButton Basic Properties")]
        [Description("指定图片大小修正的格式为百分比或像素")]
        [DefaultValue(typeof(ResizeMode), "Pixel")]
        public ResizeMode ImageResizingFormat
        {
            get => _imageResizingFormat;
            set { _imageResizingFormat = value; Invalidate(); }
        }
        /// <summary>
        /// 指定 <see cref="RoundedButton"/> 如何处理图像位置或大小
        /// </summary>
        [Category("RoundedButton Basic Properties")]
        [Description("如何处理图像位置或大小")]
        [DefaultValue(typeof(PictureBoxSizeMode), "Zoom")]
        public PictureBoxSizeMode ImageSizeMode
        {
            get => _imageSizeMode;
            set { _imageSizeMode = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置交互时是否启用动画。
        /// </summary>
        [Category("RoundedButton Basic Properties")]
        [Description("交互时是否启用动画")]
        public bool IsEnableAnimation
        {
            get => _isEnableAnimation;
            set => _isEnableAnimation = value;
        }
        /// <summary>
        /// 文本绘制的偏移。
        /// </summary>
        [Category("RoundedButton Basic Properties")]
        [Description("文本绘制的偏移")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point TextOffset
        {
            get => _textOffset;
            set { _textOffset = value; Invalidate(); }
        }
        /// <summary>
        /// 将在控件上显示的文本的对其方式。
        /// </summary>
        [Category("RoundedButton Basic Properties")]
        [Description("将在控件上显示的文本的对其方式。")]
        [DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment TextAlign
        {
            get => _textAlign;
            set { _textAlign = value; Invalidate(); }
        }
        #endregion

        #region RoundedButton Border
        /// <summary>
        /// 圆角的大小，以 <see cref="CornerRadius"/> 结构体表示。 
        /// </summary>
        [Category("RoundedButton Border")]
        [Description("每个角的圆角大小，自动适应百分比大小或像素大小")]
        [DefaultValue(typeof(CornerRadius), "10,10,10,10")]
        public CornerRadius BorderCornerRadius
        {
            get => _borderCornerRadius;
            set { _borderCornerRadius = value; Invalidate(); }
        }
        /// <summary>
        /// 边框外部的颜色，通常与父容器背景色相同。
        /// </summary>
        [Category("RoundedButton Border")]
        [Description("边框外部的颜色，通常与父容器背景色相同")]
        [DefaultValue(typeof(Color), "White")]
        public Color BaseBackColor
        {
            get => _baseBackColor;
            set { _baseBackColor = value; Invalidate(); }
        }
        #endregion

        #region RoundedButton Interaction
        /// <summary>
        /// 定义 <see cref="RoundedButton"/> 禁用时的样式，以 <see cref="DisabledStyleClass"/> 类表示。
        /// </summary>
        [Category("RoundedButton Interaction")]
        [Description("定义按钮禁用时的样式")]
        [DefaultValue(typeof(Point), "0,0")]
        public DisabledStyleClass DisabledStyle
        {
            get => _disabledStyle;
            set { _disabledStyle = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置按钮的交互样式。
        /// </summary>
        [Category("RoundedButton Interaction")]
        [Description("定义鼠标交互时按钮的外观")]
        public InteractionStyleClass InteractionStyle
        {
            get => _interactionStyle;
            set => _interactionStyle = value;
        }
        /// <summary>
        /// 获取或设置按钮的动画配置。
        /// </summary>
        [Category("RoundedButton Interaction")]
        [Description("定义按钮的动画配置")]
        public AnimationConfigClass AnimationConfig
        {
            get => _animationConfig;
            set => _animationConfig = value;
        }
        /// <summary>
        /// 定义鼠标响应鼠标单击模式，以 <see cref="MouseClickModeEnum"/> 枚举类型表示。
        /// </summary>
        [Category("RoundedButton Interaction")]
        [Description("定义鼠标响应鼠标单击模式")]
        [DefaultValue(typeof(MouseClickModeEnum), "LeftOnly")]
        public MouseClickModeEnum MouseClickMode
        {
            get => _mouseClickMode;
            set => _mouseClickMode = value;
        }
        #endregion

        /// <summary>
        /// 定义 <see cref="RoundedButton"/> 的动画配置。
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class AnimationConfigClass
        {
            /// <summary>
            /// 定义鼠标交互时按钮的颜色过渡动画配置，以 <see cref="AnimationInfo"/> 结构体表示。
            /// </summary>
            [Description("定义鼠标交互时按钮的颜色过渡动画配置")]
            public AnimationInfo ColorAnimation { get; set; }

            /// <summary>
            /// 定义鼠标交互时按钮的边框颜色过渡动画配置，以 <see cref="AnimationInfo"/> 结构体表示。
            /// </summary>
            [Description("定义鼠标交互时按钮的边框颜色过渡动画配置")]
            public AnimationInfo BorderColorAnimation { get; set; }

            /// <summary>
            /// 定义鼠标交互时按钮的边框大小过渡动画配置，以 <see cref="AnimationInfo"/> 结构体表示。
            /// </summary>
            [Description("定义鼠标交互时按钮的边框大小过渡动画配置")]
            public AnimationInfo BorderSizeAnimation { get; set; }

            public override string ToString()
            {
                return "(expandable)";
            }
        }

        /// <summary>
        /// 定义 <see cref="RoundedButton"/> 的交互样式。
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class InteractionStyleClass
        {
            /// <summary>
            /// 获取或设置鼠标移入时组件的背景色。
            /// </summary>
            [Description("鼠标移入时组件的背景色。")]
            public Color OverBackColor { get; set; }

            /// <summary>
            /// 获取或设置鼠标按下时组件的背景色。
            /// </summary>
            [Description("鼠标按下时组件的背景色。")]
            public Color DownBackColor { get; set; }

            /// <summary>
            /// 获取或设置鼠标移入时组件的边框颜色。
            /// </summary>
            [Description("鼠标移入时组件的边框颜色。")]
            public Color OverBorderColor { get; set; }

            /// <summary>
            /// 获取或设置鼠标按下时组件的边框颜色。
            /// </summary>
            [Description("鼠标按下时组件的边框颜色。")]
            public Color DownBorderColor { get; set; }

            /// <summary>
            /// 获取或设置鼠标移入时组件的前景色。
            /// </summary>
            [Description("鼠标移入时组件的前景色。")]
            public Color OverForeColor { get; set; }

            /// <summary>
            /// 获取或设置鼠标按下时组件的前景色。
            /// </summary>
            [Description("鼠标按下时组件的前景色。")]
            public Color DownForeColor { get; set; }

            /// <summary>
            /// 获取或设置鼠标移入时组件的边框大小。
            /// </summary>
            [Description("鼠标移入时组件的边框大小。")]
            public float OverBorderSize { get; set; }

            /// <summary>
            /// 获取或设置鼠标按下时组件的边框大小。
            /// </summary>
            [Description("鼠标按下时组件的边框大小。")]
            public float DownBorderSize { get; set; }

            public override string ToString()
            {
                return "(expandable)";
            }
        }

        /// <summary>
        /// 定义 <see cref="RoundedButton"/> 禁用时的样式。
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class DisabledStyleClass
        {
            /// <summary>
            /// 禁用时的背景色。
            /// </summary>
            public Color BackColor { get; set; }

            /// <summary>
            /// 禁用时的前景色。
            /// </summary>
            public Color ForeColor { get; set; }

            /// <summary>
            /// 禁用时的边框颜色。
            /// </summary>
            public Color BorderColor { get; set; }

            public override string ToString()
            {
                return "(expandable)";
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Rectangle thisRect = new(0, 0, Width, Height);
            using Bitmap bitmap = new(Width, Height);
            {
                using Graphics g = Graphics.FromImage(bitmap);
                SizeF textSize = g.MeasureString(Text, Font);
                g.Clear(DrawBackColor);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //draw image
                if (Image != null)
                {
                    Point drawPoint;
                    Size drawSize;

                    switch (ImageSizeMode)
                    {
                        case PictureBoxSizeMode.Normal:
                            drawPoint = new Point(0, 0);
                            drawSize = Image.Size;
                            break;

                        case PictureBoxSizeMode.StretchImage:
                            drawPoint = new Point(0, 0);
                            drawSize = Size;
                            break;

                        case PictureBoxSizeMode.AutoSize:
                            Size = new Size(Image.Width, Image.Height);

                            drawPoint = new Point(0, 0);
                            drawSize = Image.Size;
                            break;

                        case PictureBoxSizeMode.CenterImage:
                            ImageLayoutUtility.Center(Size, Image.Size, out drawPoint, out drawSize);
                            break;

                        case PictureBoxSizeMode.Zoom:
                            ImageLayoutUtility.Zoom(Size, Image.Size, out drawPoint, out drawSize);
                            break;

                        default:
                            drawPoint = new Point(0, 0);
                            drawSize = Image.Size;
                            break;
                    }

                    //按指定的像素或比例缩放图片
                    if (ImageResizing != new Size(0, 0))
                    {
                        int newWidth = ImageResizingFormat switch
                        {
                            ResizeMode.Percentage => (int)(drawSize.Width * ImageResizing.Width),
                            ResizeMode.Pixel => (int)ImageResizing.Width,
                            _ => 0
                        };
                        int newHeight = ImageResizingFormat switch
                        {
                            ResizeMode.Percentage => (int)(drawSize.Height * ImageResizing.Height),
                            ResizeMode.Pixel => (int)ImageResizing.Height,
                            _ => 0
                        };

                        //调整绘制位置以居中显示调整后的图像
                        int offsetX = (drawSize.Width - newWidth) / 2;
                        int offsetY = (drawSize.Height - newHeight) / 2;

                        drawPoint.Offset(offsetX, offsetY);
                        drawSize = new Size(newWidth, newHeight);
                    }

                    using var roundedImage = Image.AddRounded(ImageCornerRadius);
                    g.DrawImage(roundedImage, new Rectangle(LayoutUtilities.CalculateAlignedPosition(thisRect, drawSize, ImageAlign, ImageOffset), drawSize));
                }

                //draw text
                using SolidBrush textBrush = new(DrawForeColor);
                g.DrawString(Text, Font, textBrush, LayoutUtilities.CalculateAlignedPosition(thisRect, textSize, TextAlign, TextOffset));

                //draw border
                using Pen borderPen = new(DrawBorderColor, DrawBorderSize);
                g.DrawRounded(thisRect, BorderCornerRadius, BaseBackColor, borderPen);
            }

            pe.Graphics.DrawImage(bitmap, 0, 0);

            base.OnPaint(pe);
        }

        #region events
        /// <summary>
        /// 单击事件。
        /// </summary>
        public new event EventHandler? Click;
        /// <summary>
        /// 引发 <see cref="OnClick(EventArgs)"/> 事件。
        /// </summary>
        protected new virtual void OnClick(EventArgs e)
        {
            Click?.Invoke(this, e);
        }

        /// <summary>
        /// 单击事件。
        /// </summary>
        public new event MouseEventHandler? MouseClick;

        /// <summary>
        /// 引发 <see cref="MouseClick"/> 事件。
        /// </summary>
        protected new virtual void OnMouseClick(MouseEventArgs e)
        {
            MouseClick?.Invoke(this, e);
        }
        #endregion

        #region animation properties
        private Color _drawBackColor;
        private Color _drawForeColor;
        private Color _drawBorderColor;
        private float _drawBorderSize;

        /// <summary>
        /// 当前呈现的背景色。
        /// </summary>
        [Browsable(false)]
        public Color DrawBackColor
        {
            get => _drawBackColor;
            set { _drawBackColor = value; Invalidate(); }
        }
        public new Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                DrawBackColor = value;
            }
        }

        /// <summary>
        /// 当前呈现的前景色。
        /// </summary>
        [Browsable(false)]
        public Color DrawForeColor
        {
            get => _drawForeColor;
            set { _drawForeColor = value; Invalidate(); }
        }
        public new Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                DrawForeColor = value;
            }
        }

        /// <summary>
        /// 当前呈现的边框颜色。
        /// </summary>
        [Browsable(false)]
        public Color DrawBorderColor
        {
            get => _drawBorderColor;
            set { _drawBorderColor = value; Invalidate(); }
        }
        /// <summary>
        /// 边框的颜色。
        /// </summary>
        [Category("RoundedButton Border")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "Gainsboro")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                DrawBorderColor = value;
            }
        }

        /// <summary>
        /// 当前呈现的边框大小。
        /// </summary>
        [Browsable(false)]
        public float DrawBorderSize
        {
            get => _drawBorderSize;
            set { _drawBorderSize = value; Invalidate(); }
        }
        /// <summary>
        /// 边框的大小。
        /// </summary>
        [Category("RoundedButton Border")]
        [Description("边框的大小，为 0 时隐藏边框")]
        [DefaultValue(1)]
        public float BorderSize
        {
            get => _borderSize;
            set
            {
                _borderSize = value;
                DrawBorderSize = value;
            }
        }
        #endregion

        #region OnMouseEnter OnMouseLeave OnMouseDown OnMouseUp
        private readonly CancellationTokenSource backColorCTS = new();
        private readonly CancellationTokenSource foreColorCTS = new();
        private readonly CancellationTokenSource borderColorCTS = new();
        private readonly CancellationTokenSource borderSizeCTS = new();

        private void SetColorAnimation(Color newColor, Color startColor, Action<Color> setColor, AnimationInfo animationConfig, CancellationTokenSource cts)
        {
            if (newColor != Color.Empty)
            {
                if (IsEnableAnimation)
                {
                    cts.Cancel();
                    cts = new();
                    _ = ControlAnimator.BezierTransition(startColor, newColor, animationConfig, setColor, true, cts.Token);
                }
                else
                {
                    setColor(newColor);
                }
            }
        }

        private void SetFloatAnimation(float newValue, float startValue, Action<float> setValue, AnimationInfo animationConfig, CancellationTokenSource cts)
        {
            if (newValue != 0)
            {
                if (IsEnableAnimation)
                {
                    cts.Cancel();
                    cts = new();
                    _ = ControlAnimator.BezierTransition(startValue, newValue, animationConfig, setValue, true, cts.Token);
                }
                else
                {
                    setValue(newValue);
                }
            }
        }

        private void ResetColorAnimation(Color newColor, Color startColor, Action<Color> setColor, AnimationInfo animationConfig, CancellationTokenSource cts)
        {
            if (IsEnableAnimation)
            {
                cts.Cancel();
                cts = new();
                _ = ControlAnimator.BezierTransition(startColor, newColor, animationConfig, setColor, true, cts.Token);
            }
            else
            {
                setColor(newColor);
            }
        }

        private void ResetFloatAnimation(float newValue, float startValue, Action<float> setValue, AnimationInfo animationConfig, CancellationTokenSource cts)
        {
            if (IsEnableAnimation)
            {
                cts.Cancel();
                cts = new();
                _ = ControlAnimator.BezierTransition(startValue, newValue, animationConfig, setValue, true, cts.Token);
            }
            else
            {
                setValue(newValue);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            SetColorAnimation(
                InteractionStyle.OverBackColor,
                DrawBackColor,
                value => DrawBackColor = value,
                AnimationConfig.ColorAnimation,
                backColorCTS);

            SetColorAnimation(
                InteractionStyle.OverForeColor,
                DrawForeColor,
                value => DrawForeColor = value,
                AnimationConfig.ColorAnimation,
                foreColorCTS);

            SetColorAnimation(
                InteractionStyle.OverBorderColor,
                DrawBorderColor,
                value => DrawBorderColor = value,
                AnimationConfig.BorderColorAnimation,
                borderColorCTS);

            SetFloatAnimation(
                InteractionStyle.OverBorderSize,
                DrawBorderSize,
                value => DrawBorderSize = value,
                AnimationConfig.BorderSizeAnimation,
                borderSizeCTS);

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            ResetColorAnimation(
                BackColor,
                DrawBackColor,
                value => DrawBackColor = value,
                AnimationConfig.ColorAnimation,
                backColorCTS);

            ResetColorAnimation(
                ForeColor,
                DrawForeColor,
                value => DrawForeColor = value,
                AnimationConfig.ColorAnimation,
                foreColorCTS);

            ResetColorAnimation(
                BorderColor,
                DrawBorderColor,
                value => DrawBorderColor = value,
                AnimationConfig.BorderColorAnimation,
                borderColorCTS);

            ResetFloatAnimation(
                BorderSize,
                DrawBorderSize,
                value => DrawBorderSize = value,
                AnimationConfig.BorderSizeAnimation,
                borderSizeCTS);

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            MouseButtons button = e.Button;
            bool mode = MouseClickMode switch
            {
                MouseClickModeEnum.LeftOnly => button == MouseButtons.Left,
                MouseClickModeEnum.RightOnly => button == MouseButtons.Right,
                MouseClickModeEnum.LeftOrRight => button == MouseButtons.Left && button == MouseButtons.Right,
                MouseClickModeEnum.Arbitrarily => true,
                _ => button == MouseButtons.Left
            };
            if (mode)
            {
                SetColorAnimation(
                    InteractionStyle.DownBackColor,
                    DrawBackColor,
                    value => DrawBackColor = value,
                    AnimationConfig.ColorAnimation,
                    backColorCTS);

                SetColorAnimation(
                    InteractionStyle.DownForeColor,
                    DrawForeColor,
                    value => DrawForeColor = value,
                    AnimationConfig.ColorAnimation,
                    foreColorCTS);

                SetColorAnimation(
                    InteractionStyle.DownBorderColor,
                    DrawBorderColor,
                    value => DrawBorderColor = value,
                    AnimationConfig.BorderColorAnimation,
                    borderColorCTS);

                SetFloatAnimation(
                    InteractionStyle.DownBorderSize,
                    DrawBorderSize,
                    value => DrawBorderSize = value,
                    AnimationConfig.BorderSizeAnimation,
                    borderSizeCTS);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            MouseButtons button = e.Button;
            bool mode = MouseClickMode switch
            {
                MouseClickModeEnum.LeftOnly => button == MouseButtons.Left,
                MouseClickModeEnum.RightOnly => button == MouseButtons.Right,
                MouseClickModeEnum.LeftOrRight => button == MouseButtons.Left && button == MouseButtons.Right,
                MouseClickModeEnum.Arbitrarily => true,
                _ => button == MouseButtons.Left
            };
            if (mode)
            {
                ResetColorAnimation(
                    InteractionStyle.OverBackColor == Color.Empty ? BackColor : InteractionStyle.OverBackColor,
                    DrawBackColor,
                    value => DrawBackColor = value,
                    AnimationConfig.ColorAnimation,
                    backColorCTS);

                ResetColorAnimation(
                    InteractionStyle.OverForeColor == Color.Empty ? ForeColor : InteractionStyle.OverForeColor,
                    DrawForeColor,
                    value => DrawForeColor = value,
                    AnimationConfig.ColorAnimation,
                    foreColorCTS);

                ResetColorAnimation(
                    InteractionStyle.OverBorderColor == Color.Empty ? BorderColor : InteractionStyle.OverBorderColor,
                    DrawBorderColor,
                    value => DrawBorderColor = value,
                    AnimationConfig.BorderColorAnimation,
                    borderColorCTS);

                ResetFloatAnimation(
                    InteractionStyle.OverBorderSize == 0 ? BorderSize : InteractionStyle.OverBorderSize,
                    DrawBorderSize,
                    value => DrawBorderSize = value,
                    AnimationConfig.BorderSizeAnimation,
                    borderSizeCTS);

                //使用重写的 Click 事件，防止用户弹出对话框时，事件触发顺序错误
                OnClick(EventArgs.Empty);
                OnMouseClick(e);
            }

            base.OnMouseUp(e);
        }
        #endregion

        #region OnEnabledChanged
        private void SetDisableColor(Color disableColor, Color startColor, Color baseColor, Action<Color> setColor, CancellationTokenSource cts)
        {
            if (disableColor != Color.Empty)
            {
                Color newColor = Enabled ? baseColor : disableColor;
                if (IsEnableAnimation)
                {
                    cts.Cancel();
                    cts = new();

                    _ = ControlAnimator.BezierTransition(
                        startColor,
                        newColor,
                        new AnimationInfo(150, 30, EasingType.Linear),
                        setColor,
                        true,
                        cts.Token);
                }
                else
                {
                    setColor(newColor);
                }
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            SetDisableColor(
                DisabledStyle.BackColor,
                DrawBackColor,
                BackColor,
                value => DrawBackColor = value,
                backColorCTS);

            SetDisableColor(
                DisabledStyle.ForeColor,
                DrawForeColor,
                ForeColor,
                value => DrawForeColor = value,
                foreColorCTS);

            SetDisableColor(
                DisabledStyle.BorderColor,
                DrawBorderColor,
                BorderColor,
                value => DrawBorderColor = value,
                borderColorCTS);

            base.OnEnabledChanged(e);
        }
        #endregion

        protected override void OnTextChanged(EventArgs e)
        {
            Refresh();

            base.OnTextChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Refresh();

            base.OnSizeChanged(e);
        }

        /// <summary>
        /// 引发 <see cref="OnClick(EventArgs)"/> 事件。
        /// </summary>
        public void PerformClick()
        {
            OnClick(EventArgs.Empty);
        }
    }
}