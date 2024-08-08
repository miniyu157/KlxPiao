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
        /// <summary>
        /// 表示值的格式类型，例如百分比或像素。
        /// </summary>
        public enum FormatType
        {
            /// <summary>
            /// 表示百分比值格式。
            /// </summary>
            Percentage,

            /// <summary>
            /// 表示像素值格式。
            /// </summary>
            Pixel
        }

        private ContentAlignment _textAlign;
        private Image? _image;
        private ContentAlignment _imageAlign;
        private Point _imageOffset;
        private CornerRadius _imageCornerRadius;

        private int _borderSize;
        private Color _borderColor;
        private CornerRadius _borderCornerRadius;
        private Color _baseBackColor;
        private PictureBoxSizeMode _imageSizeMode;
        private SizeF _imageResizing;
        private FormatType _imageResizingFormat;

        private InteractionStyleClass _interactionStyle = new();
        private bool _isEnableAnimation;
        private Animation _colorAnimationConfig;
        private Animation _sizeAnimationConfig;

        public RoundedButton()
        {
            InitializeComponent();

            _textAlign = ContentAlignment.MiddleCenter;
            _image = null;
            _imageAlign = ContentAlignment.MiddleCenter;
            _imageOffset = Point.Empty;
            _imageCornerRadius = new CornerRadius(0);

            _borderSize = 1;
            _borderColor = Color.Gainsboro;
            _BorderPen = new Pen(BorderColor, BorderSize);
            _borderCornerRadius = new CornerRadius(10);
            _baseBackColor = Color.White;
            _imageSizeMode = PictureBoxSizeMode.Zoom;
            _imageResizing = new SizeF(0, 0);
            _imageResizingFormat = FormatType.Pixel;

            _interactionStyle.OverBackColor = Color.FromArgb(245, 245, 245);
            _interactionStyle.DownBackColor = Color.FromArgb(235, 235, 235);

            _isEnableAnimation = true;
            _colorAnimationConfig = new Animation(150, 30, [new(0, 0), new(1, 1)]);
            _sizeAnimationConfig = new Animation(300, 100, [new(0.58F, 1), new(1, 1)]);

            Size = new Size(116, 43);
            DoubleBuffered = true;
        }

        [DefaultValue(typeof(Size), "116,43")]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        /// <summary>
        /// 将在控件上显示的文本的对其方式。
        /// </summary>
        [Category("外观")]
        [Description("将在控件上显示的文本的对其方式。")]
        [DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment TextAlign
        {
            get { return _textAlign; }
            set { _textAlign = value; Invalidate(); }
        }

        #region RoundedButton Appearance
        /// <summary>
        /// 将在控件上显示的图像。
        /// </summary>
        [Category("RoundedButton Appearance")]
        [Description("将在控件上显示的图像。")]
        [DefaultValue(null)]
        public Image? Image
        {
            get { return _image; }
            set { _image = value; Invalidate(); }
        }
        /// <summary>
        /// 将在控件上显示的图像的对其方式。
        /// </summary>
        [Category("RoundedButton Appearance")]
        [Description("将在控件上显示的图像的对其方式。")]
        [DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment ImageAlign
        {
            get { return _imageAlign; }
            set { _imageAlign = value; Invalidate(); }
        }
        /// <summary>
        /// 图像绘制的偏移。
        /// </summary>
        [Category("RoundedButton Appearance")]
        [Description("图像绘制的偏移")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point ImageOffset
        {
            get { return _imageOffset; }
            set { _imageOffset = value; Invalidate(); }
        }
        /// <summary>
        /// 边框的大小。
        /// </summary>
        [Category("RoundedButton Appearance")]
        [Description("边框的大小，为0时隐藏边框")]
        [DefaultValue(1)]
        public int BorderSize
        {
            get => _borderSize;
            set
            {
                _borderSize = value;
                _BorderPen.Width = value;
                Invalidate();
            }
        }
        /// <summary>
        /// 边框的颜色。
        /// </summary>
        [Category("RoundedButton Appearance")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "Gainsboro")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                _BorderPen.Color = value;
                Invalidate();
            }
        }
        /// <summary>
        /// 圆角的大小，以 <see cref="CornerRadius"/> 结构体表示。 
        /// </summary>
        [Category("RoundedButton Appearance")]
        [Description("每个角的圆角大小，自动适应百分比大小或像素大小")]
        [DefaultValue(typeof(CornerRadius), "10,10,10,10")]
        public CornerRadius BorderCornerRadius
        {
            get { return _borderCornerRadius; }
            set { _borderCornerRadius = value; Invalidate(); }
        }
        /// <summary>
        /// 边框外部的颜色，通常与父容器背景色相同。
        /// </summary>
        [Category("RoundedButton Appearance")]
        [Description("边框外部的颜色，通常与父容器背景色相同")]
        [DefaultValue(typeof(Color), "White")]
        public Color BaseBackColor
        {
            get { return _baseBackColor; }
            set { _baseBackColor = value; Invalidate(); }
        }
        /// <summary>
        /// 指定 <see cref="RoundedButton"/> 如何处理图像位置或大小
        /// </summary>
        [Category("RoundedButton Appearance")]
        [Description("如何处理图像位置或大小")]
        [DefaultValue(typeof(PictureBoxSizeMode), "Zoom")]
        public PictureBoxSizeMode ImageSizeMode
        {
            get { return _imageSizeMode; }
            set { _imageSizeMode = value; Invalidate(); }
        }
        /// <summary>
        /// 按比例或像素重置图像大小。
        /// </summary>
        [Category("RoundedButton Appearance")]
        [Description("指定一个新的大小（像素或百分比）缩放图像，新的图像位置会基于原位置居中")]
        [DefaultValue(typeof(SizeF), "0,0")]
        public SizeF ImageResizing
        {
            get { return _imageResizing; }
            set { _imageResizing = value; Invalidate(); }
        }
        /// <summary>
        /// 指定图像大小修正的格式。
        /// </summary>
        [Category("RoundedButton Appearance")]
        [Description("指定图片大小修正的格式为百分比或像素")]
        [DefaultValue(typeof(FormatType), "Pixel")]
        public FormatType ImageResizingFormat
        {
            get { return _imageResizingFormat; }
            set { _imageResizingFormat = value; Invalidate(); }
        }
        /// <summary>
        /// 图像圆角的大小，可以自动检测百分比或像素。
        /// </summary>
        [Category("RoundedButton Appearance")]
        [Description("图像圆角的大小，自动检测百分比或像素")]
        [DefaultValue(typeof(CornerRadius), "0,0,0,0")]
        public CornerRadius ImageCornerRadius
        {
            get { return _imageCornerRadius; }
            set { _imageCornerRadius = value; Invalidate(); }
        }
        #endregion

        #region RoundedButton Interaction
        /// <summary>
        /// 获取或设置交互时是否启用动画。
        /// </summary>
        [Category("RoundedButton Interaction")]
        [Description("交互时是否启用动画")]
        public bool IsEnableAnimation
        {
            get { return _isEnableAnimation; }
            set { _isEnableAnimation = value; }
        }
        /// <summary>
        /// 获取或设置按钮的交互样式。
        /// </summary>
        [Category("RoundedButton Interaction")]
        [Description("定义鼠标交互时按钮的外观")]
        public InteractionStyleClass InteractionStyle
        {
            get { return _interactionStyle; }
            set { _interactionStyle = value; }
        }
        /// <summary>
        /// 定义鼠标交互时按钮的颜色过渡动画配置，以 <see cref="Animation"/> 结构体表示。
        /// </summary>
        [Category("RoundedButton Interaction")]
        [Description("定义鼠标交互时按钮的颜色过渡动画配置")]
        [DefaultValue(typeof(Animation), "150, 30, [0 0;0 0;1 1;1 1]")]
        public Animation ColorAnimationConfig
        {
            get { return _colorAnimationConfig; }
            set { _colorAnimationConfig = value; }
        }
        /// <summary>
        /// 定义鼠标交互时按钮的大小过渡动画配置，以 <see cref="Animation"/> 结构体表示。
        /// </summary>
        [Category("RoundedButton Interaction")]
        [Description("定义鼠标交互时按钮的大小过渡动画配置")]
        [DefaultValue(typeof(Animation), "300, 100, [0 0;0.58 1;1 1;1 1]")]
        public Animation SizeAnimationConfig
        {
            get { return _sizeAnimationConfig; }
            set { _sizeAnimationConfig = value; }
        }
        #endregion

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
            /// 获取或设置鼠标移入时组件的大小。
            /// </summary>
            [Description("鼠标移入时组件的大小。")]
            public Size OverSize { get; set; }

            /// <summary>
            /// 获取或设置鼠标按下时组件的大小。
            /// </summary>
            [Description("鼠标按下时组件的大小。")]
            public Size DownSize { get; set; }

            public override string ToString()
            {
                return "";
            }
        }

        //缓存Pen
        private Pen _BorderPen;

        /// <summary>
        /// 获取或设置边框的画笔。
        /// </summary>
        [Browsable(false)]
        public Pen BorderPen
        {
            get => _BorderPen;
            set => _BorderPen = value;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Rectangle thisRect = new(0, 0, Width, Height);
            using Bitmap bitmap = new(Width, Height);
            {
                Graphics g = Graphics.FromImage(bitmap);
                SizeF textSize = g.MeasureString(Text, Font);
                g.Clear(BackColor);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

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
                            Width = Image.Width;
                            Height = Image.Height;

                            drawPoint = new Point(0, 0);
                            drawSize = Image.Size;
                            break;

                        case PictureBoxSizeMode.CenterImage:
                            int x = (Width - Image.Width) / 2;
                            int y = (Height - Image.Height) / 2;

                            drawPoint = new Point(x, y);
                            drawSize = Image.Size;
                            break;

                        case PictureBoxSizeMode.Zoom:
                            float imageAspect = (float)Image.Width / Image.Height;
                            float controlAspect = (float)Width / Height;

                            int drawWidth, drawHeight;
                            int posX, posY;

                            if (imageAspect > controlAspect)
                            {
                                drawWidth = Width;
                                drawHeight = (int)(Width / imageAspect);
                                posX = 0;
                                posY = (Height - drawHeight) / 2;
                            }
                            else
                            {
                                drawWidth = (int)(Height * imageAspect);
                                drawHeight = Height;
                                posX = (Width - drawWidth) / 2;
                                posY = 0;
                            }

                            drawPoint = new Point(posX, posY);
                            drawSize = new Size(drawWidth, drawHeight);
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
                            FormatType.Percentage => (int)(drawSize.Width * ImageResizing.Width),
                            FormatType.Pixel => (int)ImageResizing.Width,
                            _ => 0
                        };
                        int newHeight = ImageResizingFormat switch
                        {
                            FormatType.Percentage => (int)(drawSize.Height * ImageResizing.Height),
                            FormatType.Pixel => (int)ImageResizing.Height,
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
                g.DrawString(Text, Font, new SolidBrush(ForeColor), LayoutUtilities.CalculateAlignedPosition(thisRect, textSize, TextAlign, LayoutUtilities.PaddingConvertToPoint(Padding)));

                //draw border
                g.DrawRounded(thisRect, BorderCornerRadius, BaseBackColor, BorderPen);
            }

            pe.Graphics.DrawImage(bitmap, 0, 0);

            base.OnPaint(pe);
        }

        #region InteractionStyle
        private enum PropertyAction
        {
            Update,
            Reset
        }

        private enum EventType
        {
            Over,
            Down
        }

        private readonly object?[] oldOverProperties = new object?[4];
        private readonly object?[] oldDownProperties = new object?[4];
        private readonly string[] properties = ["BackColor", "BorderColor", "ForeColor", "Size"];
        private readonly string[] overProperties = ["OverBackColor", "OverBorderColor", "OverForeColor", "OverSize"];
        private readonly string[] downProperties = ["DownBackColor", "DownBorderColor", "DownForeColor", "DownSize"];
        private CancellationTokenSource MouseOverCTS = new();
        private CancellationTokenSource MouseDownCTS = new();

        protected override void OnMouseEnter(EventArgs e)
        {
            UpdateProperties(overProperties, oldOverProperties, PropertyAction.Update, EventType.Over);

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            UpdateProperties(overProperties, oldOverProperties, PropertyAction.Reset, EventType.Over);

            base.OnMouseLeave(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            UpdateProperties(downProperties, oldDownProperties, PropertyAction.Update, EventType.Down);

            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            UpdateProperties(downProperties, oldDownProperties, PropertyAction.Reset, EventType.Down);

            base.OnMouseUp(e);
        }

        private void UpdateProperties(string[] interactionPropArray, object?[] oldPropertiesArray, PropertyAction action, EventType eventType)
        {
            switch (eventType)
            {
                case EventType.Over:
                    MouseOverCTS.Cancel();
                    MouseOverCTS = new();
                    break;

                case EventType.Down:
                    MouseDownCTS.Cancel();
                    MouseDownCTS = new();
                    break;
            }

            for (int i = 0; i < properties.Length; i++)
            {
                string propertyName = properties[i];
                var interactionProp = InteractionStyle.SetOrGetPropertyValue(interactionPropArray[i]);

                if (interactionProp != null &&
                    ((interactionProp is Color color && color != Color.Empty) ||
                        (interactionProp is Size size && size != Size.Empty)))
                {
                    Animation animation = this.GetPropertyType(propertyName).Name switch
                    {
                        "Color" => ColorAnimationConfig,
                        "Size" => SizeAnimationConfig,
                        _ => new(200, 30, "0, 0, 1, 1")
                    };

                    switch (action)
                    {
                        case PropertyAction.Update:
                            oldPropertiesArray[i] = this.SetOrGetPropertyValue(propertyName);
                            SetValue(interactionProp);
                            break;

                        case PropertyAction.Reset:
                            SetValue(oldPropertiesArray[i]);
                            break;
                    }

                    void SetValue(object? newValue)
                    {
                        if (IsEnableAnimation)
                        {
                            switch (eventType)
                            {
                                case EventType.Over:
                                    _ = this.BezierTransition(propertyName, null, newValue, animation, default, true, MouseOverCTS.Token);
                                    break;

                                case EventType.Down:
                                    _ = this.BezierTransition(propertyName, null, newValue, animation, default, true, MouseDownCTS.Token);
                                    break;
                            }
                        }
                        else
                        {
                            this.SetOrGetPropertyValue(propertyName, newValue);
                        }
                    }
                }
            }
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
        /// 引发 OnClick 事件。
        /// </summary>
        public void PerformClick()
        {
            OnClick(EventArgs.Empty);
        }
    }
}