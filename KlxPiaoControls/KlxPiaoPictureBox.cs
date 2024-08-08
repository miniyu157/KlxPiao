using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 表示一个带有自定义外观的图片框控件，支持边框和圆角设置。
    /// </summary>
    /// <remarks>
    /// <see cref="KlxPiaoPictureBox"/> 继承自 <see cref="PictureBox"/>，是原版 <see cref="PictureBox"/> 的增强版本。
    /// </remarks>
    public partial class KlxPiaoPictureBox : PictureBox
    {
        private PriorityLevel _textDrawPriority;
        private ContentAlignment _textAlign;
        private Point _textOffset;
        private bool _showText;

        private bool _isEnableBorder;
        private Color _baseBackColor;
        private CornerRadius _borderCornerRadius;
        private int _borderSize;
        private Color _borderColor;

        public KlxPiaoPictureBox()
        {
            InitializeComponent();

            _textAlign = ContentAlignment.MiddleCenter;
            _showText = false;
            _textDrawPriority = PriorityLevel.Low;
            _textOffset = new Point(0, 0);

            _isEnableBorder = false;
            _baseBackColor = Color.White;
            _borderCornerRadius = new CornerRadius(0);
            _borderSize = 10;
            _borderColor = Color.LightGray;

            SizeMode = PictureBoxSizeMode.Zoom;
            Size = new Size(155, 155);
        }

        #region KlxPiaoPictureBox Text
        /// <summary>
        /// 文本绘制的优先级。
        /// </summary>
        [Category("KlxPiaoPictureBox Text")]
        [Description("文本绘制的优先级。")]
        [DefaultValue(typeof(PriorityLevel), "Low")]
        public PriorityLevel TextDrawPriority
        {
            get { return _textDrawPriority; }
            set { _textDrawPriority = value; Invalidate(); }
        }
        /// <summary>
        /// 将在控件上显示的文本的对其方式。
        /// </summary>
        [Category("KlxPiaoPictureBox Text")]
        [Description("将在控件上显示的文本的对其方式")]
        [DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment TextAlign
        {
            get { return _textAlign; }
            set { _textAlign = value; Invalidate(); }
        }
        /// <summary>
        /// 文本绘制时的偏移。
        /// </summary>
        [Category("KlxPiaoPictureBox Text")]
        [Description("文本绘制时的偏移")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point TextOffset
        {
            get { return _textOffset; }
            set { _textOffset = value; Invalidate(); }
        }
        /// <summary>
        /// 是否显示在控件中的文本。
        /// </summary>
        [Category("KlxPiaoPictureBox Text")]
        [Description("是否在控件中显示文本")]
        [DefaultValue(false)]
        public bool ShowText
        {
            get { return _showText; }
            set { _showText = value; Invalidate(); }
        }
        [Browsable(true)]
        [Category("KlxPiaoPictureBox Text")]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; Invalidate(); }
        }
        #endregion

        #region KlxPiaoPictureBox Appearance
        /// <summary>
        /// 获取或设置是否启用边框。
        /// </summary>
        [Category("KlxPiaoPictureBox Appearance")]
        [Description("是否启用边框")]
        [DefaultValue(false)]
        public bool IsEnableBorder
        {
            get { return _isEnableBorder; }
            set { _isEnableBorder = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框外部的颜色
        /// </summary>
        [Category("KlxPiaoPictureBox Appearance")]
        [Description("边框外部的颜色")]
        [DefaultValue(typeof(Color), "White")]
        public Color BaseBackColor
        {
            get { return _baseBackColor; }
            set { _baseBackColor = value; Invalidate(); }
        }
        /// <summary>
        /// 圆角的大小，以 <see cref="CornerRadius"/> 结构体表示。 
        /// </summary>
        [Category("KlxPiaoPictureBox Appearance")]
        [Description("每个角的圆角大小，自动适应百分比大小或像素大小")]
        [DefaultValue(typeof(CornerRadius), "0,0,0,0")]
        public CornerRadius BorderCornerRadius
        {
            get { return _borderCornerRadius; }
            set { _borderCornerRadius = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的大小。
        /// </summary>
        [Category("KlxPiaoPictureBox Appearance")]
        [Description("边框的大小，为0时隐藏边框")]
        [DefaultValue(10)]
        public int BorderSize
        {
            get { return _borderSize; }
            set { _borderSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的颜色.
        /// </summary>
        [Category("KlxPiaoPictureBox Appearance")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "LightGray")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }
        #endregion

        [DefaultValue(typeof(Size), "155,155")]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; Invalidate(); }
        }

        [Browsable(true)]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; Invalidate(); }
        }

        [DefaultValue(typeof(PictureBoxSizeMode), "Zoom")]
        public new PictureBoxSizeMode SizeMode
        {
            get { return base.SizeMode; }
            set { base.SizeMode = value; Invalidate(); }
        }

        [Browsable(true)]
        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Rectangle thisRect = new(0, 0, Width, Height);
            Graphics g = pe.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            SizeF textSize = g.MeasureString(Text, Font);
            PointF textLocation = LayoutUtilities.CalculateAlignedPosition(thisRect, textSize, TextAlign, TextOffset);

            var drawBorder = new Action(() =>
            {
                if (IsEnableBorder)
                {
                    using Pen borderPen = new(BorderColor, BorderSize * 2); //修正画笔大小
                    if (borderPen.Width != 0)
                    {
                        using GraphicsPath roundedPath = thisRect.ConvertToRoundedPath(BorderCornerRadius);
                        g.DrawPath(borderPen, roundedPath);
                    }
                }
            });

            var drawText = new Action(() =>
            {
                if (ShowText)
                {
                    using SolidBrush foreBrush = new(ForeColor);
                    g.DrawString(Text, Font, foreBrush, textLocation);
                }
            });

            var drawOuter = new Action(() =>
            {
                using GraphicsPath outerPath = thisRect.ConvertToRoundedPath(BorderCornerRadius, true);
                g.FillPath(new SolidBrush(BaseBackColor), outerPath);
            });

            switch (TextDrawPriority)
            {
                case PriorityLevel.Low:     //文本覆盖图像
                    drawText();
                    drawBorder();
                    drawOuter();
                    break;

                case PriorityLevel.Medium:  //文本覆盖边框，不得超出圆角
                    drawBorder();
                    drawText();
                    drawOuter();
                    break;

                case PriorityLevel.High:    //覆盖全部元素
                    drawBorder();
                    drawOuter();
                    drawText();
                    break;
            }
        }
    }
}