using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace KlxPiaoControls
{
    /// <summary>
    /// 一个文本控件，支持投影、边框和高级文本渲染选项。
    /// </summary>
    /// <remarks><see cref="KlxPiaoLabel"/> 继承自 <see cref="Label"/> ，是原版 <see cref="Label"/> 的增强版本。</remarks>
    public partial class KlxPiaoLabel : Label
    {
        private bool _isEnableShadow;
        private Color _shadowColor;
        private bool _isShadowConnectLine; //投影连线
        private Point _shadowPosition;

        private bool _isEnableColorFading; //颜色减淡
        private bool _isEnableBorder;
        private Color _baseBackColor;
        private CornerRadius _cornerRadius;
        private int _borderSize;
        private Color _borderColor;

        private TextRenderingHint _textRenderingHint;
        private SmoothingMode _smoothingMode;
        private InterpolationMode _interpolationMode;
        private PixelOffsetMode _pixelOffsetMode;

        public KlxPiaoLabel()
        {
            InitializeComponent();

            _isEnableShadow = false;
            _shadowColor = Color.DarkGray;
            _isShadowConnectLine = true;
            _shadowPosition = new Point(2, 2);
            _isEnableColorFading = false;

            _isEnableBorder = false;
            _baseBackColor = Color.White;
            _cornerRadius = new CornerRadius(0);
            _borderSize = 5;
            _borderColor = Color.LightGray;

            _textRenderingHint = TextRenderingHint.SystemDefault;
            _smoothingMode = SmoothingMode.Default;
            _interpolationMode = InterpolationMode.Default;
            _pixelOffsetMode = PixelOffsetMode.Default;

            ForeColor = Color.Black;
            BackColor = Color.White;
            AutoSize = true;
        }

        [Browsable(true)]
        [DefaultValue(true)]
        public new bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        #region KlxPiaoLabel投影
        /// <summary>
        /// 获取或设置是否启用投影。
        /// </summary>
        [Category("KlxPiaoLabel投影")]
        [Description("是否启用投影")]
        [DefaultValue(false)]
        public bool IsEnableShadow
        {
            get { return _isEnableShadow; }
            set { _isEnableShadow = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置投影的颜色。
        /// </summary>
        [Category("KlxPiaoLabel投影")]
        [Description("投影的颜色")]
        [DefaultValue(typeof(Color), "DarkGray")]
        public Color ShadowColor
        {
            get { return _shadowColor; }
            set { _shadowColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置投影的位置。
        /// </summary>
        [Category("KlxPiaoLabel投影")]
        [Description("投影的长度和方向")]
        [DefaultValue(typeof(Point), "2,2")]
        public Point ShadowPosition
        {
            get { return _shadowPosition; }
            set { _shadowPosition = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否将投影与本体连线。
        /// </summary>
        [Category("KlxPiaoLabel投影")]
        [Description("是否将投影与本体连线")]
        [DefaultValue(true)]
        public bool IsShadowConnectLine
        {
            get { return _isShadowConnectLine; }
            set { _isShadowConnectLine = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否启用颜色减淡。
        /// </summary>
        [Category("KlxPiaoLabel投影")]
        [Description("是否启用颜色减淡")]
        [DefaultValue(false)]
        public bool IsEnableColorFading
        {
            get { return _isEnableColorFading; }
            set { _isEnableColorFading = value; Invalidate(); }
        }
        #endregion

        #region KlxPiaoLabel边框
        /// <summary>
        /// 获取或设置是否启用边框。
        /// </summary>
        [Category("KlxPiaoLabel边框")]
        [Description("是否启用边框")]
        [DefaultValue(false)]
        public bool IsEnableBorder
        {
            get { return _isEnableBorder; }
            set { _isEnableBorder = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置圆角外的背景色。
        /// </summary>
        [Category("KlxPiaoLabel边框")]
        [Description("边框外部的颜色，通常与父容器背景色相同")]
        [DefaultValue(typeof(Color), "White")]
        public Color BaseBackColor
        {
            get { return _baseBackColor; }
            set { _baseBackColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置圆角的大小，以 <see cref="KlxPiaoAPI.CornerRadius"/> 结构体表示。
        /// </summary>
        [Category("KlxPiaoLabel边框")]
        [Description("圆角大小，自动检测是百分比大小还是像素大小。")]
        [DefaultValue(typeof(CornerRadius), "0,0,0,0")]
        public CornerRadius CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的大小.
        /// </summary>
        [Category("KlxPiaoLabel边框")]
        [Description("边框的大小，为0时隐藏边框")]
        [DefaultValue(5)]
        public int BorderSize
        {
            get { return _borderSize; }
            set { _borderSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的颜色。
        /// </summary>
        [Category("KlxPiaoLabel边框")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "LightGray")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }
        #endregion

        #region KlxPiaoLabel质量
        /// <summary>
        /// 文本呈现的质量，以 <see cref="System.Drawing.Text.TextRenderingHint"/> 枚举类型表示。
        /// </summary>
        [Category("KlxPiaoLabel质量")]
        [Description("指定文本呈现的质量")]
        [DefaultValue(typeof(TextRenderingHint), "SystemDefault")]
        public TextRenderingHint TextRenderingHint
        {
            get { return _textRenderingHint; }
            set { _textRenderingHint = value; Invalidate(); }
        }
        /// <summary>
        /// 边框抗锯齿的质量，以 <see cref="System.Drawing.Drawing2D.SmoothingMode"/> 枚举类型表示。
        /// </summary>
        [Category("KlxPiaoLabel质量")]
        [Description("指定是否将平滑处理（抗锯齿）应用于直线、曲线和已填充区域的边缘")]
        [DefaultValue(typeof(SmoothingMode), "Default")]
        public SmoothingMode SmoothingMode
        {
            get { return _smoothingMode; }
            set { _smoothingMode = value; Invalidate(); }
        }
        /// <summary>
        /// 缩放或旋转图像时使用的算法，以 <see cref="System.Drawing.Drawing2D.InterpolationMode"/> 枚举类型表示。
        /// </summary>
        [Category("KlxPiaoLabel质量")]
        [Description("缩放或旋转图像时使用的算法")]
        [DefaultValue(typeof(InterpolationMode), "Default")]
        public InterpolationMode InterpolationMode
        {
            get { return _interpolationMode; }
            set { _interpolationMode = value; Invalidate(); }
        }
        /// <summary>
        /// 像素偏移的方式，以 <see cref="System.Drawing.Drawing2D.PixelOffsetMode"/> 枚举类型表示。
        /// </summary>
        [Category("KlxPiaoLabel质量")]
        [Description("指定在呈现期间像素偏移的方式")]
        [DefaultValue(typeof(PixelOffsetMode), "Default")]
        public PixelOffsetMode PixelOffsetMode
        {
            get { return _pixelOffsetMode; }
            set { _pixelOffsetMode = value; Invalidate(); }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;

            g.TextRenderingHint = TextRenderingHint;
            g.SmoothingMode = SmoothingMode;
            g.InterpolationMode = InterpolationMode;
            g.PixelOffsetMode = PixelOffsetMode;

            g.Clear(BackColor);

            PointF drawPosition = PointF.Empty;
            Rectangle thisRect = new(0, 0, Width, Height);

            SizeF TextSize = g.MeasureString(Text, Font);

            //适应文字位置
            if (!AutoSize)
            {
                drawPosition = LayoutUtilities.CalculateAlignedPosition(thisRect, TextSize, TextAlign, LayoutUtilities.PaddingConvertToPoint(Padding));
            }

            //绘制投影
            if (IsEnableShadow)
            {
                using SolidBrush brush = new(ShadowColor);
                {
                    if (IsShadowConnectLine)
                    {
                        int xFading = IsEnableColorFading ? (ShadowPosition.X == 0 ? 0 : 255 / Math.Abs(ShadowPosition.X)) : 255;
                        int yFading = IsEnableColorFading ? (ShadowPosition.Y == 0 ? 0 : 255 / Math.Abs(ShadowPosition.Y)) : 255;

                        int xFadingR = (ShadowPosition.X == 0 ? 0 : (255 - ShadowColor.R) / Math.Abs(ShadowPosition.X));
                        int xFadingG = (ShadowPosition.X == 0 ? 0 : (255 - ShadowColor.G) / Math.Abs(ShadowPosition.X));
                        int xFadingB = (ShadowPosition.X == 0 ? 0 : (255 - ShadowColor.B) / Math.Abs(ShadowPosition.X));

                        int yFadingR = (ShadowPosition.Y == 0 ? 0 : (255 - ShadowColor.R) / Math.Abs(ShadowPosition.Y));
                        int yFadingG = (ShadowPosition.Y == 0 ? 0 : (255 - ShadowColor.G) / Math.Abs(ShadowPosition.Y));
                        int yFadingB = (ShadowPosition.Y == 0 ? 0 : (255 - ShadowColor.B) / Math.Abs(ShadowPosition.Y));

                        if (ShadowPosition.X == 0 || ShadowPosition.Y == 0)
                        {
                            //Axis and 0,0

                            //x
                            for (int x = 0; x != ShadowPosition.X; x += (ShadowPosition.X > 0 ? 1 : -1))
                            {
                                int fadingFrequency = Math.Abs(ShadowPosition.X) - Math.Abs(x);
                                Color color = IsEnableColorFading ? Color.FromArgb(255 - fadingFrequency * xFadingR, 255 - fadingFrequency * xFadingG, 255 - fadingFrequency * xFadingB) : ShadowColor;
                                g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(xFading, color)), new PointF(drawPosition.X + x, drawPosition.Y));
                            }
                            //y
                            for (int y = 0; y != ShadowPosition.Y; y += (ShadowPosition.Y > 0 ? 1 : -1))
                            {
                                int fadingFrequency = Math.Abs(ShadowPosition.Y) - Math.Abs(y);
                                Color color = IsEnableColorFading ? Color.FromArgb(255 - fadingFrequency * yFadingR, 255 - fadingFrequency * yFadingG, 255 - fadingFrequency * yFadingB) : ShadowColor;

                                g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(yFading, color)), new PointF(drawPosition.X, drawPosition.Y + y));
                            }
                        }
                        else
                        {
                            bool approashX = Math.Abs(ShadowPosition.X) < Math.Abs(ShadowPosition.Y) / Math.PI;
                            bool approachY = Math.Abs(ShadowPosition.Y) < Math.Abs(ShadowPosition.X) / Math.PI;

                            //Draw on the x-axis
                            if (!approashX)
                            {
                                for (int x = 0; x != ShadowPosition.X; x += (ShadowPosition.X > 0 ? 1 : -1))
                                {
                                    float slope = ShadowPosition.Y / (float)ShadowPosition.X;

                                    //颜色减淡
                                    int fadingFrequency = Math.Abs(ShadowPosition.X) - Math.Abs(x);
                                    Color color = IsEnableColorFading ? Color.FromArgb(255 - fadingFrequency * xFadingR, 255 - fadingFrequency * xFadingG, 255 - fadingFrequency * xFadingB) : ShadowColor;

                                    g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(xFading, color)), new PointF(drawPosition.X + x, drawPosition.Y + x * slope));
                                }
                            }

                            //Draw on the y-axis
                            if (!approachY)
                            {
                                for (int y = 0; y != ShadowPosition.Y; y += (ShadowPosition.Y > 0 ? 1 : -1))
                                {
                                    float slope = ShadowPosition.X / (float)ShadowPosition.Y;

                                    //颜色减淡
                                    int fadingFrequency = Math.Abs(ShadowPosition.Y) - Math.Abs(y);
                                    Color color = IsEnableColorFading ? Color.FromArgb(255 - fadingFrequency * yFadingR, 255 - fadingFrequency * yFadingG, 255 - fadingFrequency * yFadingB) : ShadowColor;

                                    g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(yFading, color)), new PointF(drawPosition.X + y * slope, drawPosition.Y + y));
                                }
                            }
                        }
                    }
                    else
                    {
                        g.DrawString(Text, Font, brush, new PointF(drawPosition.X + ShadowPosition.X, drawPosition.Y + ShadowPosition.Y));
                    }

                    //baseText
                    g.DrawString(Text, Font, new SolidBrush(ForeColor), drawPosition);
                }
            }
            else
            {
                //baseText
                g.DrawString(Text, Font, new SolidBrush(ForeColor), drawPosition);
            }

            if (IsEnableBorder)
            {
                Rectangle 区域 = new(0, 0, Width, Height);
                g.DrawRounded(区域, CornerRadius, BaseBackColor, new Pen(BorderColor, BorderSize));
            }
        }

        /// <summary>
        /// 返回控件绘制的图像。
        /// </summary>
        public Bitmap GetControlImage()
        {
            Bitmap bmp = new(Width, Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                PaintEventArgs e = new(g, new Rectangle(0, 0, Width, Height));
                OnPaint(e);
            }

            return bmp;
        }
    }
}