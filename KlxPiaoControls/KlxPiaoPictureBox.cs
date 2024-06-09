using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    public partial class KlxPiaoPictureBox : PictureBox
    {
        private bool _启用边框;
        private Color _边框外部颜色;
        private float _圆角百分比;
        private int _边框大小;
        private Color _边框颜色;

        public KlxPiaoPictureBox()
        {
            InitializeComponent();

            _启用边框 = false;
            _边框外部颜色 = Color.White;
            _圆角百分比 = 0;
            _边框大小 = 10;
            _边框颜色 = Color.LightGray;

            SizeMode = PictureBoxSizeMode.Zoom;
            Size = new Size(155, 155);
        }

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
        #region KlxPiaoPictureBox外观
        [Category("KlxPiaoPictureBox外观")]
        [Description("是否启用边框")]
        [DefaultValue(false)]
        public bool 启用边框
        {
            get { return _启用边框; }
            set { _启用边框 = value; Invalidate(); }
        }
        [Category("KlxPiaoPictureBox外观")]
        [Description("边框外部的颜色")]
        [DefaultValue(typeof(Color), "White")]
        public Color 边框外部颜色
        {
            get { return _边框外部颜色; }
            set { _边框外部颜色 = value; Invalidate(); }
        }
        [Category("KlxPiaoPictureBox外观")]
        [Description("范围：0.00-1.00，等于0时取消圆角")]
        [DefaultValue(0)]
        public float 圆角百分比
        {
            get { return _圆角百分比; }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(圆角百分比), "圆角的百分比必须在0和1之间");
                }
                _圆角百分比 = value; Invalidate();
            }
        }
        [Category("KlxPiaoPictureBox外观")]
        [Description("边框的大小，为0时隐藏边框")]
        [DefaultValue(10)]
        public int 边框大小
        {
            get { return _边框大小; }
            set { _边框大小 = value; Invalidate(); }
        }
        [Category("KlxPiaoPictureBox外观")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "LightGray")]
        public Color 边框颜色
        {
            get { return _边框颜色; }
            set { _边框颜色 = value; Invalidate(); }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            {
                Graphics g = pe.Graphics;

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                if (启用边框)
                {
                    Rectangle 区域 = new(0, 0, Width, Height);
                    g.绘制圆角(区域, new CornerRadius(圆角百分比), 边框外部颜色, new Pen(边框颜色, 边框大小));
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
