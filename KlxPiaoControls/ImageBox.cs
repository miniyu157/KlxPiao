using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 表示一个图片框控件，支持边框和圆角等自定义外观设置。
    /// </summary>
    /// <remarks>
    /// <see cref="ImageBox"/> 继承自 <see cref="Control"/>。
    /// <br/>相较于 <see cref="KlxPiaoPictureBox"/>，<see cref="ImageBox"/> 提供了更加自由的图片绘制选项。
    /// </remarks>
    public partial class ImageBox : Control
    {
        private Image? _image;
        private ContentAlignment _imageAlign;
        private Point _imageOffset;
        private CornerRadius _imageCornerRadius;
        private PictureBoxSizeMode _imageSizeMode;
        private SizeF _imageResizing;
        private ResizeMode _imageResizingFormat;
        private int _imageBorderSize;
        private Color _imageBorderColor;
        private bool _enableNewZoomMode;

        private bool _isEnableBorder;
        private Color _baseBackColor;
        private CornerRadius _borderCornerRadius;
        private int _borderSize;
        private Color _borderColor;

        public ImageBox()
        {
            _image = null;
            _imageAlign = ContentAlignment.MiddleCenter;
            _imageOffset = Point.Empty;
            _imageCornerRadius = new CornerRadius(0);
            _imageSizeMode = PictureBoxSizeMode.Zoom;
            _imageResizing = new SizeF(0, 0);
            _imageResizingFormat = ResizeMode.Pixel;
            _imageBorderSize = 0;
            _imageBorderColor = Color.Pink;
            _enableNewZoomMode = false;

            _isEnableBorder = false;
            _baseBackColor = Color.White;
            _borderColor = Color.LightGray;
            _borderCornerRadius = new CornerRadius(0);
            _borderSize = 10;

            ImageSizeMode = PictureBoxSizeMode.Zoom;
            Size = new Size(155, 155);
            DoubleBuffered = true;
        }

        #region ImageBox Appearance
        /// <summary>
        /// 获取或设置显示图像。
        /// </summary>
        [Category("ImageBox Appearance")]
        [Description("将在控件上显示的图像。")]
        [DefaultValue(null)]
        public Image? Image
        {
            get => _image;
            set { _image = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置图像显示的布局。
        /// </summary>
        [Category("ImageBox Appearance")]
        [Description("将在控件上显示的图像的对齐方式。")]
        [DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment ImageAlign
        {
            get => _imageAlign;
            set { _imageAlign = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置图片显示的偏移。
        /// </summary>
        [Category("ImageBox Appearance")]
        [Description("图像绘制的偏移")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point ImageOffset
        {
            get => _imageOffset;
            set { _imageOffset = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置缩放模式。
        /// </summary>
        [Category("ImageBox Appearance")]
        [Description("如何处理图像位置或大小")]
        [DefaultValue(typeof(PictureBoxSizeMode), "Zoom")]
        public PictureBoxSizeMode ImageSizeMode
        {
            get => _imageSizeMode;
            set { _imageSizeMode = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置图像修正大小。
        /// </summary>
        [Category("ImageBox Appearance")]
        [Description("指定一个新的大小（像素或百分比）缩放图像，新的图像位置会基于原位置居中")]
        [DefaultValue(typeof(SizeF), "0,0")]
        public SizeF ImageResizing
        {
            get => _imageResizing;
            set { _imageResizing = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置图像修正模式。
        /// </summary>
        [Category("ImageBox Appearance")]
        [Description("指定图像大小修正的格式为百分比或像素")]
        [DefaultValue(typeof(ResizeMode), "Pixel")]
        public ResizeMode ImageResizingFormat
        {
            get => _imageResizingFormat;
            set { _imageResizingFormat = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置图像圆角大小，以 <see cref="CornerRadius"/> 结构体表示。
        /// </summary>
        [Category("ImageBox Appearance")]
        [Description("图像圆角的大小，自动检测百分比或像素")]
        [DefaultValue(typeof(CornerRadius), "0,0,0,0")]
        public CornerRadius ImageCornerRadius
        {
            get => _imageCornerRadius;
            set { _imageCornerRadius = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置图像的边框大小。
        /// </summary>
        [Category("ImageBox Appearance")]
        [Description("图像边框的大小。")]
        [DefaultValue(0)]
        public int ImageBorderSize
        {
            get => _imageBorderSize;
            set { _imageBorderSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置图像的边框颜色。
        /// </summary>
        [Category("ImageBox Appearance")]
        [Description("图像边框的颜色。")]
        [DefaultValue(typeof(Color), "Pink")]
        public Color ImageBorderColor
        {
            get => _imageBorderColor;
            set { _imageBorderColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否启用新的缩放模式。
        /// </summary>
        [Category("ImageBox Appearance")]
        [Description("是否启用新的缩放模式。")]
        [DefaultValue(false)]
        public bool EnableNewZoomMode
        {
            get => _enableNewZoomMode;
            set { _enableNewZoomMode = value; Invalidate(); }
        }
        #endregion

        #region ImageBox Border
        /// <summary>
        /// 获取或设置是否启用边框。
        /// </summary>
        [Category("ImageBox Border")]
        [Description("是否启用边框")]
        [DefaultValue(false)]
        public bool IsEnableBorder
        {
            get => _isEnableBorder;
            set { _isEnableBorder = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框外部的颜色
        /// </summary>
        [Category("ImageBox Border")]
        [Description("边框外部的颜色")]
        [DefaultValue(typeof(Color), "White")]
        public Color BaseBackColor
        {
            get => _baseBackColor;
            set { _baseBackColor = value; Invalidate(); }
        }
        /// <summary>
        /// 圆角的大小，以 <see cref="CornerRadius"/> 结构体表示。 
        /// </summary>
        [Category("ImageBox Border")]
        [Description("每个角的圆角大小，自动适应百分比大小或像素大小")]
        [DefaultValue(typeof(CornerRadius), "0,0,0,0")]
        public CornerRadius BorderCornerRadius
        {
            get => _borderCornerRadius;
            set { _borderCornerRadius = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的大小。
        /// </summary>
        [Category("ImageBox Border")]
        [Description("边框的大小，为0时隐藏边框")]
        [DefaultValue(10)]
        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的颜色.
        /// </summary>
        [Category("ImageBox Border")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "LightGray")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pe)
        {
            Rectangle thisRect = new(0, 0, Width, Height);
            Graphics g = pe.Graphics;
            g.Clear(BackColor);

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

                        Width = Image.Width;
                        Height = Image.Height;
                        drawPoint = new Point(0, 0);
                        drawSize = Image.Size;
                        break;

                    case PictureBoxSizeMode.CenterImage:
                        ImageLayoutUtility.Center(Size, Image.Size, out drawPoint, out drawSize);
                        break;

                    case PictureBoxSizeMode.Zoom:
                        if (Image.Width < Width && Image.Height < Height && EnableNewZoomMode)
                        {
                            ImageLayoutUtility.Center(Size, Image.Size, out drawPoint, out drawSize);
                        }
                        else
                        {
                            ImageLayoutUtility.Zoom(Size, Image.Size, out drawPoint, out drawSize);
                        }
                        break;

                    default:
                        drawPoint = new Point(0, 0);
                        drawSize = Image.Size;
                        break;
                }

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

                    int offsetX = (drawSize.Width - newWidth) / 2;
                    int offsetY = (drawSize.Height - newHeight) / 2;

                    drawPoint.Offset(offsetX, offsetY);
                    drawSize = new Size(newWidth, newHeight);
                }

                if (drawSize.Width != 0 && drawSize.Height != 0)
                {
                    using Image roundedImage = Image.ResetImage(drawSize) //重置为新的大小再添加圆角，使其和控件比例相同
                        .AddRounded(ImageCornerRadius, BackColor, ImageBorderColor, ImageBorderSize);
                    g.DrawImage(roundedImage, LayoutUtilities.CalculateAlignedPosition(thisRect, drawSize, ImageAlign, ImageOffset));
                }
            }

            //draw border
            if (IsEnableBorder)
            {
                using Pen borderPen = new(BorderColor, BorderSize);
                g.DrawRounded(thisRect, BorderCornerRadius, BaseBackColor, borderPen);
            }

            base.OnPaint(pe);
        }
    }
}