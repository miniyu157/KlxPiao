using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 一个自定义的带圆角和图像支持的按钮控件。
    /// </summary>
    /// <remarks>
    /// RoundedButton 继承自 <see cref="Control"/> 类，是原版 <see cref="Button"/> 的强化版本。
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

        private ContentAlignment _TextAlign;
        private Image? _Image;
        private ContentAlignment _ImageAlign;
        private Padding _ImagePadding;
        private CornerRadius _ImageCornerRadius;

        private int _边框大小;
        private Color _边框颜色;
        private CornerRadius _圆角大小;
        private Color _外部颜色;
        private PictureBoxSizeMode _图像大小模式;
        private SizeF _图像大小修正;
        private FormatType _图像大小修正格式;

        private bool _启用动画;
        private 交互样式类 _交互样式 = new();
        private Animation _颜色过渡配置;
        private Animation _大小过渡配置;

        public RoundedButton()
        {
            InitializeComponent();

            _TextAlign = ContentAlignment.MiddleCenter;
            _Image = null;
            _ImageAlign = ContentAlignment.MiddleCenter;
            _ImagePadding = new Padding(0);
            _ImageCornerRadius = new CornerRadius(0);

            _边框大小 = 1;
            _边框颜色 = Color.Gainsboro;
            _BorderPen = new Pen(边框颜色, 边框大小);
            _圆角大小 = new CornerRadius(10);
            _外部颜色 = Color.White;
            _图像大小模式 = PictureBoxSizeMode.Zoom;
            _图像大小修正 = new SizeF(0, 0);
            _图像大小修正格式 = FormatType.Pixel;

            _交互样式.移入背景色 = Color.FromArgb(245, 245, 245);
            _交互样式.按下背景色 = Color.FromArgb(235, 235, 235);

            _启用动画 = true;
            _颜色过渡配置 = new Animation(150, 30, [new(0, 0), new(0, 0), new(1, 1), new(1, 1)]);
            _大小过渡配置 = new Animation(300, 100, [new(0, 0), new(0.58F, 1), new(1, 1), new(1, 1)]);

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
            get { return _TextAlign; }
            set { _TextAlign = value; Invalidate(); }
        }

        #region RoundedButton外观
        /// <summary>
        /// 将在控件上显示的图像。
        /// </summary>
        [Category("RoundedButton外观")]
        [Description("将在控件上显示的图像。")]
        [DefaultValue(null)]
        public Image? Image
        {
            get { return _Image; }
            set { _Image = value; Invalidate(); }
        }
        /// <summary>
        /// 将在控件上显示的图像的对其方式。
        /// </summary>
        [Category("RoundedButton外观")]
        [Description("将在控件上显示的图像的对其方式。")]
        [DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment ImageAlign
        {
            get { return _ImageAlign; }
            set { _ImageAlign = value; Invalidate(); }
        }
        /// <summary>
        /// 图像相对于组件的内边距。
        /// </summary>
        [Category("RoundedButton外观")]
        [Description("图像相对于组件的内边距")]
        [DefaultValue(typeof(Padding), "0,0,0,0")]
        public Padding ImagePadding
        {
            get { return _ImagePadding; }
            set { _ImagePadding = value; Invalidate(); }
        }
        /// <summary>
        /// 边框的大小。
        /// </summary>
        [Category("RoundedButton外观")]
        [Description("边框的大小，为0时隐藏边框")]
        [DefaultValue(1)]
        public int 边框大小
        {
            get => _边框大小;
            set
            {
                _边框大小 = value;
                _BorderPen.Width = value;
                Invalidate();
            }
        }
        /// <summary>
        /// 边框的颜色。
        /// </summary>
        [Category("RoundedButton外观")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "Gainsboro")]
        public Color 边框颜色
        {
            get => _边框颜色;
            set
            {
                _边框颜色 = value;
                _BorderPen.Color = value;
                Invalidate();
            }
        }
        /// <summary>
        /// 每个角的圆角大小。
        /// </summary>
        [Category("RoundedButton外观")]
        [Description("每个角的圆角大小，自动适应百分比大小或像素大小")]
        [DefaultValue(typeof(CornerRadius), "10,10,10,10")]
        public CornerRadius 圆角大小
        {
            get { return _圆角大小; }
            set { _圆角大小 = value; Invalidate(); }
        }
        /// <summary>
        /// 边框外部的颜色，通常与父容器背景色相同。
        /// </summary>
        [Category("RoundedButton外观")]
        [Description("边框外部的颜色，通常与父容器背景色相同")]
        [DefaultValue(typeof(Color), "White")]
        public Color 外部颜色
        {
            get { return _外部颜色; }
            set { _外部颜色 = value; Invalidate(); }
        }
        /// <summary>
        /// 指定 RoundedButton 如何处理图像位置或大小
        /// </summary>
        [Category("RoundedButton外观")]
        [Description("如何处理图像位置或大小")]
        [DefaultValue(typeof(PictureBoxSizeMode), "Zoom")]
        public PictureBoxSizeMode 图像大小模式
        {
            get { return _图像大小模式; }
            set { _图像大小模式 = value; Invalidate(); }
        }
        /// <summary>
        /// 按比例或像素重置图像大小。
        /// </summary>
        [Category("RoundedButton外观")]
        [Description("指定一个新的大小（像素或百分比）缩放图像，新的图像位置会基于原位置居中")]
        [DefaultValue(typeof(SizeF), "0,0")]
        public SizeF 图像大小修正
        {
            get { return _图像大小修正; }
            set { _图像大小修正 = value; Invalidate(); }
        }
        /// <summary>
        /// 指定图像大小修正的格式。
        /// </summary>
        [Category("RoundedButton外观")]
        [Description("指定图片大小修正的格式为百分比或像素")]
        [DefaultValue(typeof(FormatType), "Pixel")]
        public FormatType 图像大小修正格式
        {
            get { return _图像大小修正格式; }
            set { _图像大小修正格式 = value; Invalidate(); }
        }
        /// <summary>
        /// 图像圆角的大小，可以自动检测百分比或像素。
        /// </summary>
        [Category("RoundedButton外观")]
        [Description("图像圆角的大小，自动检测百分比或像素")]
        [DefaultValue(typeof(CornerRadius), "0,0,0,0")]
        public CornerRadius ImageCornerRadius
        {
            get { return _ImageCornerRadius; }
            set { _ImageCornerRadius = value; Invalidate(); }
        }
        #endregion

        #region RoundedButton交互样式
        /// <summary>
        /// 获取或设置交互时是否启用动画。
        /// </summary>
        [Category("RoundedButton交互样式")]
        [Description("交互时是否启用动画")]
        public bool 启用动画
        {
            get { return _启用动画; }
            set { _启用动画 = value; }
        }
        /// <summary>
        /// 获取或设置按钮的交互样式。
        /// </summary>
        [Category("RoundedButton交互样式")]
        [Description("定义鼠标交互时按钮的外观")]
        public 交互样式类 交互样式
        {
            get { return _交互样式; }
            set { _交互样式 = value; }
        }
        /// <summary>
        /// 定义鼠标交互时按钮的颜色过渡动画配置。
        /// </summary>
        [Category("RoundedButton交互样式")]
        [Description("定义鼠标交互时按钮的颜色过渡动画配置")]
        [DefaultValue(typeof(Animation), "150, 30, [0 0;0 0;1 1;1 1]")]
        public Animation 颜色过渡配置
        {
            get { return _颜色过渡配置; }
            set { _颜色过渡配置 = value; }
        }
        /// <summary>
        /// 定义鼠标交互时按钮的大小过渡动画配置。
        /// </summary>
        [Category("RoundedButton交互样式")]
        [Description("定义鼠标交互时按钮的大小过渡动画配置")]
        [DefaultValue(typeof(Animation), "300, 100, [0 0;0.58 1;1 1;1 1]")]
        public Animation 大小过渡配置
        {
            get { return _大小过渡配置; }
            set { _大小过渡配置 = value; }
        }
        #endregion

        /// <summary>
        /// 定义 <see cref="RoundedButton"/> 的交互样式。
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class 交互样式类
        {
            /// <summary>
            /// 获取或设置鼠标移入时组件的背景色。
            /// </summary>
            [Description("鼠标移入时组件的背景色。")]
            public Color 移入背景色 { get; set; }

            /// <summary>
            /// 获取或设置鼠标按下时组件的背景色。
            /// </summary>
            [Description("鼠标按下时组件的背景色。")]
            public Color 按下背景色 { get; set; }

            /// <summary>
            /// 获取或设置鼠标移入时组件的边框颜色。
            /// </summary>
            [Description("鼠标移入时组件的边框颜色。")]
            public Color 移入边框颜色 { get; set; }

            /// <summary>
            /// 获取或设置鼠标按下时组件的边框颜色。
            /// </summary>
            [Description("鼠标按下时组件的边框颜色。")]
            public Color 按下边框颜色 { get; set; }

            /// <summary>
            /// 获取或设置鼠标移入时组件的前景色。
            /// </summary>
            [Description("鼠标移入时组件的前景色。")]
            public Color 移入前景色 { get; set; }

            /// <summary>
            /// 获取或设置鼠标按下时组件的前景色。
            /// </summary>
            [Description("鼠标按下时组件的前景色。")]
            public Color 按下前景色 { get; set; }

            /// <summary>
            /// 获取或设置鼠标移入时组件的大小。
            /// </summary>
            [Description("鼠标移入时组件的大小。")]
            public Size 移入大小 { get; set; }

            /// <summary>
            /// 获取或设置鼠标按下时组件的大小。
            /// </summary>
            [Description("鼠标按下时组件的大小。")]
            public Size 按下大小 { get; set; }

            public override string ToString()
            {
                return "";
            }
        }

        //缓存Pen
        private Pen _BorderPen;
        [Browsable(false)]
        public Pen BorderPen
        {
            get => _BorderPen;
            set => _BorderPen = value;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            using Bitmap bitmap = new(Width, Height);
            {
                Graphics g = Graphics.FromImage(bitmap);
                g.Clear(BackColor);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Rectangle 工作区 = new(0, 0, Width, Height);
                SizeF 文本大小 = g.MeasureString(Text, Font);

                //绘制图像
                if (Image != null)
                {
                    Point drawPoint;
                    Size drawSize;

                    switch (图像大小模式)
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
                    if (图像大小修正 != new Size(0, 0))
                    {
                        int newWidth = 图像大小修正格式 switch
                        {
                            FormatType.Percentage => (int)(drawSize.Width * 图像大小修正.Width),
                            FormatType.Pixel => (int)图像大小修正.Width,
                            _ => 0
                        };
                        int newHeight = 图像大小修正格式 switch
                        {
                            FormatType.Percentage => (int)(drawSize.Height * 图像大小修正.Height),
                            FormatType.Pixel => (int)图像大小修正.Height,
                            _ => 0
                        };

                        //调整绘制位置以居中显示调整后的图像
                        int offsetX = (drawSize.Width - newWidth) / 2;
                        int offsetY = (drawSize.Height - newHeight) / 2;

                        drawPoint.Offset(offsetX, offsetY);
                        drawSize = new Size(newWidth, newHeight);
                    }

                    using var roundedImage = Image.AddRounded(ImageCornerRadius);
                    g.DrawImage(roundedImage, new Rectangle(LayoutUtilities.CalculateAlignedPosition(工作区, drawSize, ImageAlign, ImagePadding), drawSize));
                }

                //绘制文本
                g.DrawString(Text, Font, new SolidBrush(ForeColor), LayoutUtilities.CalculateAlignedPosition(工作区, 文本大小, TextAlign, Padding));

                //边框
                g.DrawRounded(工作区, 圆角大小, 外部颜色, BorderPen);

                base.OnPaint(pe);
            }

            pe.Graphics.DrawImage(bitmap, 0, 0);
        }

        #region 交互样式
        //存储数据
        private readonly List<object?> oldMouseOverData = [];
        private readonly List<object?> oldMouseDownData = [];

        //目标属性
        private readonly string[] mouseOverProperties = ["移入背景色", "移入边框颜色", "移入前景色", "移入大小"];
        private readonly string[] mouseDownProperties = ["按下背景色", "按下边框颜色", "按下前景色", "按下大小"];

        private readonly string[] propertyNames = ["BackColor", "边框颜色", "ForeColor", "Size"];

        private CancellationTokenSource MouseOverCTS = new();
        private CancellationTokenSource MouseDownCTS = new();

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseOverCTS.Cancel();
            MouseOverCTS = new CancellationTokenSource();

            oldMouseOverData.Clear();

            for (int i = 0; i < propertyNames.Length; i++)
            {
                string property = propertyNames[i];
                object? currentValue = this.SetOrGetPropertyValue(property);
                object? targetValue = 交互样式.SetOrGetPropertyValue(mouseOverProperties[i]);

                oldMouseOverData.Add(currentValue);

                if (targetValue != null &&
                    ((targetValue is Color color && color != Color.Empty) ||
                        (targetValue is Size size && size != Size.Empty)))
                {
                    if (启用动画)
                    {
                        int time = property switch
                        {
                            "BackColor" or "边框颜色" or "ForeColor" => 颜色过渡配置.Time,
                            "Size" => 大小过渡配置.Time,
                            _ => 200
                        };

                        int FPS = property switch
                        {
                            "BackColor" or "边框颜色" or "ForeColor" => 颜色过渡配置.FPS,
                            "Size" => 大小过渡配置.FPS,
                            _ => 30
                        };

                        PointF[]? easing = property switch
                        {
                            "BackColor" or "边框颜色" or "ForeColor" => 颜色过渡配置.Easing,
                            "Size" => 大小过渡配置.Easing,
                            _ => [new(0, 0), new(1, 1)]
                        };

                        _ = this.贝塞尔过渡动画(property, null, targetValue, new Animation(time, FPS, easing), default, MouseOverCTS.Token);
                    }
                    else
                    {
                        this.SetOrGetPropertyValue(property, targetValue);
                    }
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseOverCTS.Cancel();
            MouseOverCTS = new CancellationTokenSource();

            for (int i = 0; i < propertyNames.Length; i++)
            {
                string property = propertyNames[i];
                object? targetValue = 交互样式.SetOrGetPropertyValue(mouseOverProperties[i]);
                object? oldPropertyValue = oldMouseOverData[i];

                if (oldPropertyValue != null &&
                    ((oldPropertyValue is Color color && color != Color.Empty) ||
                        (oldPropertyValue is Size size && size != Size.Empty)))
                {
                    if (启用动画)
                    {
                        int time = property switch
                        {
                            "BackColor" or "边框颜色" or "ForeColor" => 颜色过渡配置.Time,
                            "Size" => 大小过渡配置.Time,
                            _ => 200
                        };

                        int FPS = property switch
                        {
                            "BackColor" or "边框颜色" or "ForeColor" => 颜色过渡配置.FPS,
                            "Size" => 大小过渡配置.FPS,
                            _ => 30
                        };

                        PointF[]? easing = property switch
                        {
                            "BackColor" or "边框颜色" or "ForeColor" => 颜色过渡配置.Easing,
                            "Size" => 大小过渡配置.Easing,
                            _ => [new(0, 0), new(1, 1)]
                        };

                        _ = this.贝塞尔过渡动画(property, null, oldPropertyValue, new Animation(time, FPS, easing), default, MouseOverCTS.Token);
                    }
                    else
                    {
                        this.SetOrGetPropertyValue(property, oldPropertyValue);
                    }
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MouseDownCTS.Cancel();
            MouseDownCTS = new CancellationTokenSource();

            oldMouseDownData.Clear();

            for (int i = 0; i < propertyNames.Length; i++)
            {
                string property = propertyNames[i];
                object? currentValue = this.SetOrGetPropertyValue(property);
                object? targetValue = 交互样式.SetOrGetPropertyValue(mouseDownProperties[i]);

                oldMouseDownData.Add(currentValue);

                if (targetValue != null &&
                    ((targetValue is Color color && color != Color.Empty) ||
                        (targetValue is Size size && size != Size.Empty)))
                {
                    if (启用动画)
                    {
                        int time = property switch
                        {
                            "BackColor" or "边框颜色" or "ForeColor" => 颜色过渡配置.Time,
                            "Size" => 大小过渡配置.Time,
                            _ => 200
                        };

                        int FPS = property switch
                        {
                            "BackColor" or "边框颜色" or "ForeColor" => 颜色过渡配置.FPS,
                            "Size" => 大小过渡配置.FPS,
                            _ => 30
                        };

                        PointF[]? easing = property switch
                        {
                            "BackColor" or "边框颜色" or "ForeColor" => 颜色过渡配置.Easing,
                            "Size" => 大小过渡配置.Easing,
                            _ => [new(0, 0), new(1, 1)]
                        };

                        _ = this.贝塞尔过渡动画(property, null, targetValue, new Animation(time, FPS, easing), default, MouseDownCTS.Token);
                    }
                    else
                    {
                        this.SetOrGetPropertyValue(property, targetValue);
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MouseDownCTS.Cancel();
            MouseDownCTS = new CancellationTokenSource();

            for (int i = 0; i < propertyNames.Length; i++)
            {
                string property = propertyNames[i];
                object? targetValue = 交互样式.SetOrGetPropertyValue(mouseDownProperties[i]);
                object? oldPropertyValue = oldMouseDownData[i];

                if (oldPropertyValue != null &&
                    ((oldPropertyValue is Color color && color != Color.Empty) ||
                        (oldPropertyValue is Size size && size != Size.Empty)))
                {
                    if (启用动画)
                    {
                        int time = property switch
                        {
                            "BackColor" or "边框颜色" or "ForeColor" => 颜色过渡配置.Time,
                            "Size" => 大小过渡配置.Time,
                            _ => 200
                        };

                        int FPS = property switch
                        {
                            "BackColor" or "边框颜色" or "ForeColor" => 颜色过渡配置.FPS,
                            "Size" => 大小过渡配置.FPS,
                            _ => 30
                        };

                        PointF[]? easing = property switch
                        {
                            "BackColor" or "边框颜色" or "ForeColor" => 颜色过渡配置.Easing,
                            "Size" => 大小过渡配置.Easing,
                            _ => [new(0, 0), new(1, 1)]
                        };

                        _ = this.贝塞尔过渡动画(property, null, oldPropertyValue, new Animation(time, FPS, easing), default, MouseDownCTS.Token);
                    }
                    else
                    {
                        this.SetOrGetPropertyValue(property, oldPropertyValue);
                    }
                }
            }
        }
        #endregion

        //修改布局时立即重绘
        protected override void OnPaddingChanged(EventArgs e)
        {
            Refresh();

            base.OnPaddingChanged(e);
        }

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
    }
}