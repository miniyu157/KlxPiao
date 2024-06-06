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
            set { _圆角百分比 = value; Invalidate(); }
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

                Rectangle 区域 = new(0, 0, Width, Height);
                SizeF 圆角区域;
                switch (区域.Width - 区域.Height)
                {
                    case 0:
                        圆角区域 = new SizeF(区域.Width * 圆角百分比, 区域.Height * 圆角百分比);
                        break;
                    case < 0:
                        圆角区域 = new SizeF(区域.Width * 圆角百分比, 区域.Width * 圆角百分比);
                        break;
                    case > 0:
                        圆角区域 = new SizeF(区域.Height * 圆角百分比, 区域.Height * 圆角百分比);
                        break;
                }
                PointF 圆角区域1 = new(区域.X, 区域.Y);
                PointF 圆角区域2 = new(区域.Width - 圆角区域.Width, 区域.Y);
                PointF 圆角区域3 = new(区域.Width - 圆角区域.Width, 区域.Height - 圆角区域.Height);
                PointF 圆角区域4 = new(区域.X, 区域.Height - 圆角区域.Height);
                if (边框大小 != 0 && 启用边框)
                {
                    GraphicsPath 边框 = new();
                    if (圆角百分比 != 0)
                    {
                        边框.AddArc(new RectangleF(圆角区域1, 圆角区域), 180, 90);
                        边框.AddArc(new RectangleF(圆角区域2, 圆角区域), 270, 90);
                        边框.AddArc(new RectangleF(圆角区域3, 圆角区域), 0, 90);
                        边框.AddArc(new RectangleF(圆角区域4, 圆角区域), 90, 90);
                    }
                    else
                    {
                        边框.AddRectangle(区域);
                    }
                    边框.CloseFigure();

                    g.DrawPath(new Pen(边框颜色, 边框大小), 边框);
                }
                if (圆角百分比 != 0)
                {
                    GraphicsPath 圆角 = new();
                    // 左上角
                    圆角.AddArc(new RectangleF(圆角区域1, 圆角区域), 180, 90);
                    圆角.AddLine(圆角区域1, new PointF(圆角区域1.X, 圆角区域1.Y + 圆角区域.Height / 2));
                    圆角.CloseFigure();
                    // 右上角
                    圆角.AddArc(new RectangleF(圆角区域2, 圆角区域), 270, 90);
                    圆角.AddLine(new PointF(圆角区域2.X + 圆角区域.Width, 圆角区域2.Y), 圆角区域2);
                    圆角.CloseFigure();
                    // 右下角
                    圆角.AddArc(new RectangleF(圆角区域3, 圆角区域), 0, 90);
                    圆角.AddLine(new PointF(圆角区域3.X + 圆角区域.Width, 圆角区域3.Y + 圆角区域.Height), new PointF(圆角区域3.X + 圆角区域.Width, 圆角区域3.Y));
                    圆角.CloseFigure();
                    // 左下角
                    圆角.AddArc(new RectangleF(圆角区域4, 圆角区域), 90, 90);
                    圆角.AddLine(new PointF(圆角区域4.X, 圆角区域4.Y + 圆角区域.Height), new PointF(圆角区域4.X + 圆角区域.Width, 圆角区域4.Y + 圆角区域.Height));
                    圆角.CloseFigure();

                    g.FillPath(new SolidBrush(边框外部颜色), 圆角);
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
