using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 表示一个带有自定义外观的图片框控件，支持边框和圆角设置。
    /// </summary>
    /// <remarks>
    /// KlxPiaoPictureBox 继承自 <see cref="PictureBox"/> 类，允许设置边框样式、圆角大小、以及返回绘制的图像。
    /// </remarks>
    public partial class KlxPiaoPictureBox : PictureBox
    {
        private PriorityLevel _TextDrawPriority;
        private ContentAlignment _TextAlign;
        private Point _TextOffset;
        private bool _ShowText;

        private bool _启用边框;
        private Color _边框外部颜色;
        private CornerRadius _圆角大小;
        private int _边框大小;
        private Color _边框颜色;

        public KlxPiaoPictureBox()
        {
            InitializeComponent();

            _TextAlign = ContentAlignment.MiddleCenter;
            _ShowText = false;
            _TextDrawPriority = PriorityLevel.Low;
            _TextOffset = new Point(0, 0);

            _启用边框 = false;
            _边框外部颜色 = Color.White;
            _圆角大小 = new CornerRadius(0);
            _边框大小 = 10;
            _边框颜色 = Color.LightGray;

            SizeMode = PictureBoxSizeMode.Zoom;
            Size = new Size(155, 155);
        }

        #region KlxPiaoPictureBox文本
        /// <summary>
        /// 文本绘制的优先级。
        /// </summary>
        [Category("KlxPiaoPictureBox文本")]
        [Description("文本绘制的优先级。")]
        [DefaultValue(typeof(PriorityLevel), "Low")]
        public PriorityLevel TextDrawPriority
        {
            get { return _TextDrawPriority; }
            set { _TextDrawPriority = value; Invalidate(); }
        }
        /// <summary>
        /// 将在控件上显示的文本的对其方式。
        /// </summary>
        [Category("KlxPiaoPictureBox文本")]
        [Description("将在控件上显示的文本的对其方式")]
        [DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment TextAlign
        {
            get { return _TextAlign; }
            set { _TextAlign = value; Invalidate(); }
        }
        /// <summary>
        /// 文本绘制时的偏移。
        /// </summary>
        [Category("KlxPiaoPictureBox文本")]
        [Description("文本绘制时的偏移")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point TextOffset
        {
            get { return _TextOffset; }
            set { _TextOffset = value; Invalidate(); }
        }
        /// <summary>
        /// 是否显示在控件中的文本。
        /// </summary>
        [Category("KlxPiaoPictureBox文本")]
        [Description("是否在控件中显示文本")]
        [DefaultValue(false)]
        public bool ShowText
        {
            get { return _ShowText; }
            set { _ShowText = value; Invalidate(); }
        }
        [Browsable(true)]
        [Category("KlxPiaoPictureBox文本")]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; Invalidate(); }
        }
        #endregion

        #region KlxPiaoPictureBox外观
        /// <summary>
        /// 获取或设置是否启用边框。
        /// </summary>
        [Category("KlxPiaoPictureBox外观")]
        [Description("是否启用边框")]
        [DefaultValue(false)]
        public bool 启用边框
        {
            get { return _启用边框; }
            set { _启用边框 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框外部的颜色
        /// </summary>
        [Category("KlxPiaoPictureBox外观")]
        [Description("边框外部的颜色")]
        [DefaultValue(typeof(Color), "White")]
        public Color 边框外部颜色
        {
            get { return _边框外部颜色; }
            set { _边框外部颜色 = value; Invalidate(); }
        }
        /// <summary>
        /// 圆角的大小，以 <see cref="CornerRadius"/> 结构体表示。 
        /// </summary>
        [Category("KlxPiaoPictureBox外观")]
        [Description("每个角的圆角大小，自动适应百分比大小或像素大小")]
        [DefaultValue(typeof(CornerRadius), "0,0,0,0")]
        public CornerRadius 圆角大小
        {
            get { return _圆角大小; }
            set { _圆角大小 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的大小。
        /// </summary>
        [Category("KlxPiaoPictureBox外观")]
        [Description("边框的大小，为0时隐藏边框")]
        [DefaultValue(10)]
        public int 边框大小
        {
            get { return _边框大小; }
            set { _边框大小 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的颜色.
        /// </summary>
        [Category("KlxPiaoPictureBox外观")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "LightGray")]
        public Color 边框颜色
        {
            get { return _边框颜色; }
            set { _边框颜色 = value; Invalidate(); }
        }
        #endregion

        [DefaultValue(typeof(Size), "155,155")]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; Invalidate(); }
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
            {
                Rectangle thisRect = new(0, 0, Width, Height);
                Graphics g = pe.Graphics;

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                SizeF textSize = g.MeasureString(Text, Font);
                PointF textLocation = LayoutUtilities.CalculateAlignedPosition(thisRect, textSize, TextAlign, TextOffset);

                //绘制边框
                var drawBorder = new Action(() =>
                {
                    if (启用边框)
                    {
                        Pen pen = new(边框颜色, 边框大小 * 2); //修正画笔大小
                        if (pen.Width != 0)
                        {
                            GraphicsPath 圆角路径 = GraphicsExtensions.ConvertToRoundedPath(thisRect, 圆角大小);
                            g.DrawPath(pen, 圆角路径);
                        }
                    }
                });

                //绘制文本
                var drawText = new Action(() =>
                {
                    if (ShowText)
                    {
                        using SolidBrush foreBrush = new(ForeColor);
                        g.DrawString(Text, Font, foreBrush, textLocation);
                    }
                });

                //填充外部
                var drawOuter = new Action(() =>
                {
                    GraphicsPath 外部路径 = GraphicsExtensions.ConvertToRoundedPath(thisRect, 圆角大小, true);
                    g.FillPath(new SolidBrush(边框外部颜色), 外部路径);
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
        /// <summary>
        /// 返回控件绘制的图像
        /// </summary>
        /// <returns></returns>
        public Bitmap 返回图像()
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
