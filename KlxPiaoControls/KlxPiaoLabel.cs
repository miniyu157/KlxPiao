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
        private bool _isShadowConnectLine;
        private Point _shadowPosition;
        private bool _isEnableColorFading;
        private bool _isNaturalShadowEffectEnabled;

        private bool _isEnableBorder;
        private Color _baseBackColor;
        private CornerRadius _cornerRadius;
        private int _borderSize;
        private Color _borderColor;

        private TextRenderingHint _textRenderingHint;
        private SmoothingMode _smoothingMode;
        private InterpolationMode _interpolationMode;
        private PixelOffsetMode _pixelOffsetMode;
        private Point _drawTextOffset;

        public KlxPiaoLabel()
        {
            InitializeComponent();

            _isEnableShadow = false;
            _shadowColor = Color.DarkGray;
            _isShadowConnectLine = true;
            _shadowPosition = new Point(2, 2);
            _isEnableColorFading = false;
            _isNaturalShadowEffectEnabled = true;

            _isEnableBorder = false;
            _baseBackColor = Color.White;
            _cornerRadius = new CornerRadius(24);
            _borderSize = 1;
            _borderColor = Color.FromArgb(199, 199, 199);

            _textRenderingHint = TextRenderingHint.SystemDefault;
            _smoothingMode = SmoothingMode.Default;
            _interpolationMode = InterpolationMode.Default;
            _pixelOffsetMode = PixelOffsetMode.Default;
            _drawTextOffset = Point.Empty;

            ForeColor = Color.Black;
            BackColor = Color.White;
            AutoSize = true;
        }

        [Browsable(true)]
        [DefaultValue(true)]
        public new bool AutoSize
        {
            get => base.AutoSize;
            set => base.AutoSize = value;
        }

        #region KlxPiaoLabel Shadow
        /// <summary>
        /// 获取或设置是否启用投影。
        /// </summary>
        [Category("KlxPiaoLabel Shadow")]
        [Description("是否启用投影")]
        [DefaultValue(false)]
        public bool IsEnableShadow
        {
            get => _isEnableShadow;
            set { _isEnableShadow = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置投影的颜色。
        /// </summary>
        [Category("KlxPiaoLabel Shadow")]
        [Description("投影的颜色")]
        [DefaultValue(typeof(Color), "DarkGray")]
        public Color ShadowColor
        {
            get => _shadowColor;
            set { _shadowColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置投影的位置。
        /// </summary>
        [Category("KlxPiaoLabel Shadow")]
        [Description("投影的长度和方向")]
        [DefaultValue(typeof(Point), "2, 2")]
        public Point ShadowPosition
        {
            get => _shadowPosition;
            set { _shadowPosition = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否将投影与本体连线。
        /// </summary>
        [Category("KlxPiaoLabel Shadow")]
        [Description("是否将投影与本体连线")]
        [DefaultValue(true)]
        public bool IsShadowConnectLine
        {
            get => _isShadowConnectLine;
            set { _isShadowConnectLine = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否启用颜色减淡。
        /// </summary>
        [Category("KlxPiaoLabel Shadow")]
        [Description("是否启用颜色减淡")]
        [DefaultValue(false)]
        public bool IsEnableColorFading
        {
            get => _isEnableColorFading;
            set { _isEnableColorFading = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否启用自然投影效果。
        /// </summary>
        [Category("KlxPiaoLabel Shadow")]
        [Description("是否启用自然投影效果(需启用 IsEnableColorFading 和 IsShadowConnectLine)")]
        [DefaultValue(true)]
        public bool IsNaturalShadowEffectEnabled
        {
            get => _isNaturalShadowEffectEnabled;
            set { _isNaturalShadowEffectEnabled = value; Invalidate(); }
        }
        #endregion

        #region KlxPiaoLabel Border
        /// <summary>
        /// 获取或设置是否启用边框。
        /// </summary>
        [Category("KlxPiaoLabel Border")]
        [Description("是否启用边框")]
        [DefaultValue(false)]
        public bool IsEnableBorder
        {
            get => _isEnableBorder;
            set { _isEnableBorder = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置圆角外的背景色。
        /// </summary>
        [Category("KlxPiaoLabel Border")]
        [Description("边框外部的颜色，通常与父容器背景色相同")]
        [DefaultValue(typeof(Color), "White")]
        public Color BaseBackColor
        {
            get => _baseBackColor;
            set { _baseBackColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置圆角的大小，以 <see cref="KlxPiaoAPI.CornerRadius"/> 结构体表示。
        /// </summary>
        [Category("KlxPiaoLabel Border")]
        [Description("圆角大小，自动检测是百分比大小还是像素大小。")]
        [DefaultValue(typeof(CornerRadius), "0,0,0,0")]
        public CornerRadius CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的大小.
        /// </summary>
        [Category("KlxPiaoLabel Border")]
        [Description("边框的大小，为0时隐藏边框")]
        [DefaultValue(5)]
        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的颜色。
        /// </summary>
        [Category("KlxPiaoLabel Border")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "LightGray")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }
        #endregion

        #region KlxPiaoLabel Drawing
        /// <summary>
        /// 文本呈现的质量，以 <see cref="System.Drawing.Text.TextRenderingHint"/> 枚举类型表示。
        /// </summary>
        [Category("KlxPiaoLabel Drawing")]
        [Description("指定文本呈现的质量")]
        [DefaultValue(typeof(TextRenderingHint), "SystemDefault")]
        public TextRenderingHint TextRenderingHint
        {
            get => _textRenderingHint;
            set { _textRenderingHint = value; Invalidate(); }
        }
        /// <summary>
        /// 边框抗锯齿的质量，以 <see cref="System.Drawing.Drawing2D.SmoothingMode"/> 枚举类型表示。
        /// </summary>
        [Category("KlxPiaoLabel Drawing")]
        [Description("指定是否将平滑处理（抗锯齿）应用于直线、曲线和已填充区域的边缘")]
        [DefaultValue(typeof(SmoothingMode), "Default")]
        public SmoothingMode SmoothingMode
        {
            get => _smoothingMode;
            set { _smoothingMode = value; Invalidate(); }
        }
        /// <summary>
        /// 缩放或旋转图像时使用的算法，以 <see cref="System.Drawing.Drawing2D.InterpolationMode"/> 枚举类型表示。
        /// </summary>
        [Category("KlxPiaoLabel Drawing")]
        [Description("缩放或旋转图像时使用的算法")]
        [DefaultValue(typeof(InterpolationMode), "Default")]
        public InterpolationMode InterpolationMode
        {
            get => _interpolationMode;
            set { _interpolationMode = value; Invalidate(); }
        }
        /// <summary>
        /// 像素偏移的方式，以 <see cref="System.Drawing.Drawing2D.PixelOffsetMode"/> 枚举类型表示。
        /// </summary>
        [Category("KlxPiaoLabel Drawing")]
        [Description("指定在呈现期间像素偏移的方式")]
        [DefaultValue(typeof(PixelOffsetMode), "Default")]
        public PixelOffsetMode PixelOffsetMode
        {
            get => _pixelOffsetMode;
            set { _pixelOffsetMode = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置文本绘制的偏移。
        /// </summary>
        [Category("KlxPiaoLabel Drawing")]
        [Description("文本绘制的偏移")]
        [DefaultValue(typeof(Point), "0, 0")]
        public Point DrawTextOffset
        {
            get => _drawTextOffset;
            set { _drawTextOffset = value; Invalidate(); }
        }
        #endregion

        #region events
        /// <summary>
        /// 背景绘制事件。
        /// </summary>
        public event PaintEventHandler? BackgroundPaint;

        /// <summary>
        /// 引发 <see cref="OnBackgroundPaint(PaintEventArgs)"/> 事件
        /// </summary>
        /// <param name="g"></param>
        protected virtual void OnBackgroundPaint(PaintEventArgs e)
        {
            BackgroundPaint?.Invoke(this, e);
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

            OnBackgroundPaint(pe);

            PointF drawPosition = PointF.Empty;
            Rectangle thisRect = new(0, 0, Width, Height);
            SizeF textSize = g.MeasureString(Text, Font);

            if (!AutoSize)
            {
                drawPosition = LayoutUtilities.CalculateAlignedPosition(thisRect, textSize, TextAlign, DrawTextOffset);
            }

            if (IsEnableShadow)
            {
                Color startColor = ShadowColor;
                Color endColor = BackColor;
                using SolidBrush shadowBrush = new(startColor);

                if (IsShadowConnectLine)
                {
                    int xAlpha = IsEnableColorFading ? (ShadowPosition.X == 0 ? 0 : 255 / Math.Abs(ShadowPosition.X)) : 255;
                    int yAlpha = IsEnableColorFading ? (ShadowPosition.Y == 0 ? 0 : 255 / Math.Abs(ShadowPosition.Y)) : 255;

                    for (int x = 0; x != ShadowPosition.X; x += (ShadowPosition.X > 0 ? 1 : -1))
                    {
                        double progress = Math.Abs((double)x / ShadowPosition.X);
                        int drawAlpha = IsNaturalShadowEffectEnabled ? xAlpha : (int)(255 * (1 - progress));
                        SolidBrush drawBrush = IsEnableColorFading
                            ? new(Color.FromArgb(drawAlpha, TypeInterpolator.Interpolate(startColor, endColor, progress)))
                            : shadowBrush;
                        float slope = ShadowPosition.Y / (float)ShadowPosition.X;
                        g.DrawString(Text, Font, drawBrush, new PointF(drawPosition.X + x, drawPosition.Y + x * slope));
                    }

                    for (int y = 0; y != ShadowPosition.Y; y += (ShadowPosition.Y > 0 ? 1 : -1))
                    {
                        double progress = Math.Abs((double)y / ShadowPosition.Y);
                        int drawAlpha = IsNaturalShadowEffectEnabled ? yAlpha : (int)(255 * (1 - progress));
                        SolidBrush drawBrush = IsEnableColorFading
                            ? new(Color.FromArgb(drawAlpha, TypeInterpolator.Interpolate(startColor, endColor, progress)))
                            : shadowBrush;
                        float slope = ShadowPosition.X / (float)ShadowPosition.Y;
                        g.DrawString(Text, Font, drawBrush, new PointF(drawPosition.X + y * slope, drawPosition.Y + y));
                    }
                }
                else
                {
                    g.DrawString(Text, Font, shadowBrush, new PointF(drawPosition.X + ShadowPosition.X, drawPosition.Y + ShadowPosition.Y));
                }

                //baseText
                g.DrawString(Text, Font, new SolidBrush(ForeColor), drawPosition);
            }
            else
            {
                //baseText
                g.DrawString(Text, Font, new SolidBrush(ForeColor), drawPosition);
            }

            if (IsEnableBorder)
            {
                g.DrawRounded(thisRect, CornerRadius, BaseBackColor, new Pen(BorderColor, BorderSize));
            }
        }


        /// <summary>
        /// 获取工作区的矩形。
        /// </summary>
        /// <returns>除投影或边框内的矩形。</returns>
        public Rectangle GetClientRectangle()
        {
            return new Rectangle(0, 0, Width, Height);
        }
    }
}