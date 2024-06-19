using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace KlxPiaoControls
{
    /// <summary>
    /// 一个自定义的 Label 控件，支持投影、边框和高级文本渲染选项。
    /// </summary>
    public partial class KlxPiaoLabel : Label
    {
        private bool _启用投影;
        private Color _投影颜色;
        private bool _投影连线;
        private Point _偏移量;

        private bool _颜色减淡;
        private bool _启用边框;
        private Color _边框外部颜色;
        private float _圆角大小;
        private int _边框大小;

        private Color _边框颜色;
        private TextRenderingHint _文本呈现质量;
        private SmoothingMode _抗锯齿;
        private InterpolationMode _算法;
        private PixelOffsetMode _偏移方式;

        public KlxPiaoLabel()
        {
            InitializeComponent();

            _启用投影 = false;
            _投影颜色 = Color.DarkGray;
            _投影连线 = true;
            _偏移量 = new Point(2, 2);
            _颜色减淡 = false;

            _启用边框 = false;
            _边框外部颜色 = Color.White;
            _圆角大小 = 0;
            _边框大小 = 5;
            _边框颜色 = Color.LightGray;

            _文本呈现质量 = TextRenderingHint.SystemDefault;
            _抗锯齿 = SmoothingMode.Default;
            _算法 = InterpolationMode.Default;
            _偏移方式 = PixelOffsetMode.Default;

            ForeColor = Color.Black;
            BackColor = Color.White;
            AutoSize = true;
        }

        #region KlxPiaoLabel投影
        [Category("KlxPiaoLabel投影")]
        [Description("是否启用投影")]
        [DefaultValue(false)]
        public bool 启用投影
        {
            get { return _启用投影; }
            set { _启用投影 = value; Invalidate(); }
        }
        [Category("KlxPiaoLabel投影")]
        [Description("投影的颜色")]
        [DefaultValue(typeof(Color), "DarkGray")]
        public Color 投影颜色
        {
            get { return _投影颜色; }
            set { _投影颜色 = value; Invalidate(); }
        }
        [Category("KlxPiaoLabel投影")]
        [Description("决定了投影的长度和方向")]
        [DefaultValue(typeof(Point), "2,2")]
        public Point 偏移量
        {
            get { return _偏移量; }
            set { _偏移量 = value; Invalidate(); }
        }
        [Category("KlxPiaoLabel投影")]
        [Description("设置为True时，相当于物体的投影；设置为False时，相当于复制了一份")]
        [DefaultValue(true)]
        public bool 投影连线
        {
            get { return _投影连线; }
            set { _投影连线 = value; Invalidate(); }
        }
        [Category("KlxPiaoLabel投影")]
        [Description("设置为True时，建议把投影颜色设置为为更深的颜色，例如：Black")]
        [DefaultValue(false)]
        public bool 颜色减淡
        {
            get { return _颜色减淡; }
            set { _颜色减淡 = value; Invalidate(); }
        }
        [Category("KlxPiaoLabel投影"), Description("是否固定大小（优化原版）")]
        [Browsable(true)]
        [DefaultValue(true)]
        public new bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }
        #endregion

        #region KlxPiaoLabel边框
        [Category("KlxPiaoLabel边框")]
        [Description("是否启用边框")]
        [DefaultValue(false)]
        public bool 启用边框
        {
            get { return _启用边框; }
            set { _启用边框 = value; Invalidate(); }
        }
        [Category("KlxPiaoLabel边框")]
        [Description("边框外部的颜色，通常与父容器背景色相同")]
        [DefaultValue(typeof(Color), "White")]
        public Color 边框外部颜色
        {
            get { return _边框外部颜色; }
            set { _边框外部颜色 = value; Invalidate(); }
        }
        [Category("KlxPiaoLabel边框")]
        [Description("圆角大小，自动检测是百分比大小还是像素大小。")]
        [DefaultValue(0F)]
        public float 圆角大小
        {
            get { return _圆角大小; }
            set { _圆角大小 = value; Invalidate(); }
        }
        [Category("KlxPiaoLabel边框")]
        [Description("边框的大小，为0时隐藏边框")]
        [DefaultValue(5)]
        public int 边框大小
        {
            get { return _边框大小; }
            set { _边框大小 = value; Invalidate(); }
        }
        [Category("KlxPiaoLabel边框")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "LightGray")]
        public Color 边框颜色
        {
            get { return _边框颜色; }
            set { _边框颜色 = value; Invalidate(); }
        }
        #endregion

        #region KlxPiaoLabel质量
        [Category("KlxPiaoLabel质量")]
        [Description("指定文本呈现的质量")]
        [DefaultValue(typeof(TextRenderingHint), "SystemDefault")]
        public TextRenderingHint 文本呈现质量
        {
            get { return _文本呈现质量; }
            set { _文本呈现质量 = value; Invalidate(); }
        }
        [Category("KlxPiaoLabel质量")]
        [Description("指定是否将平滑处理（抗锯齿）应用于直线、曲线和已填充区域的边缘")]
        [DefaultValue(typeof(SmoothingMode), "Default")]
        public SmoothingMode 抗锯齿
        {
            get { return _抗锯齿; }
            set { _抗锯齿 = value; Invalidate(); }
        }
        [Category("KlxPiaoLabel质量")]
        [Description("InterpolationMode 枚举指定在缩放或旋转图像时使用的算法")]
        [DefaultValue(typeof(InterpolationMode), "Default")]
        public InterpolationMode 算法
        {
            get { return _算法; }
            set { _算法 = value; Invalidate(); }
        }
        [Category("KlxPiaoLabel质量")]
        [Description("指定在呈现期间像素偏移的方式")]
        [DefaultValue(typeof(PixelOffsetMode), "Default")]
        public PixelOffsetMode 偏移方式
        {
            get { return _偏移方式; }
            set { _偏移方式 = value; Invalidate(); }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;

            g.TextRenderingHint = 文本呈现质量;
            g.SmoothingMode = 抗锯齿;
            g.InterpolationMode = 算法;
            g.PixelOffsetMode = 偏移方式;

            g.Clear(BackColor);

            PointF 绘制位置 = PointF.Empty;

            float 文字Width = g.MeasureString(Text, Font).Width;
            float 文字Height = g.MeasureString(Text, Font).Height;

            //适应文字位置
            if (!AutoSize)
            {
                绘制位置 = TextAlign switch
                {
                    ContentAlignment.TopLeft => new PointF(0, 0),
                    ContentAlignment.TopCenter => new PointF((Width - 文字Width) / 2, 0),
                    ContentAlignment.TopRight => new PointF(Width - 文字Width, 0),
                    ContentAlignment.MiddleLeft => new PointF(0, (Height - 文字Height) / 2),
                    ContentAlignment.MiddleCenter => new PointF((Width - 文字Width) / 2, (Height - 文字Height) / 2),
                    ContentAlignment.MiddleRight => new PointF(Width - 文字Width, (Height - 文字Height) / 2),
                    ContentAlignment.BottomLeft => new PointF(0, Height - 文字Height),
                    ContentAlignment.BottomCenter => new PointF((Width - 文字Width) / 2, Height - 文字Height),
                    ContentAlignment.BottomRight => new PointF(Width - 文字Width, Height - 文字Height),
                    _ => PointF.Empty
                };
                绘制位置.Y -= Padding.Bottom;
                绘制位置.Y += Padding.Top;
                绘制位置.X -= Padding.Right;
                绘制位置.X += Padding.Left;
            }
            //绘制投影
            if (启用投影)
            {
                using SolidBrush brush = new(投影颜色);
                {
                    if (投影连线)
                    {
                        int 横向递减值 = 颜色减淡 ? (偏移量.X == 0 ? 0 : 255 / Math.Abs(偏移量.X)) : 255;
                        int 纵向递减值 = 颜色减淡 ? (偏移量.Y == 0 ? 0 : 255 / Math.Abs(偏移量.Y)) : 255;

                        int 横向递减颜色R = (偏移量.X == 0 ? 0 : (255 - 投影颜色.R) / Math.Abs(偏移量.X));
                        int 横向递减颜色G = (偏移量.X == 0 ? 0 : (255 - 投影颜色.G) / Math.Abs(偏移量.X));
                        int 横向递减颜色B = (偏移量.X == 0 ? 0 : (255 - 投影颜色.B) / Math.Abs(偏移量.X));

                        int 纵向递减颜色R = (偏移量.Y == 0 ? 0 : (255 - 投影颜色.R) / Math.Abs(偏移量.Y));
                        int 纵向递减颜色G = (偏移量.Y == 0 ? 0 : (255 - 投影颜色.G) / Math.Abs(偏移量.Y));
                        int 纵向递减颜色B = (偏移量.Y == 0 ? 0 : (255 - 投影颜色.B) / Math.Abs(偏移量.Y));

                        if (偏移量.X == 0 || 偏移量.Y == 0)
                        {
                            //坐标轴和原点
                            //x轴
                            for (int x = 0; x != 偏移量.X; x += (偏移量.X > 0 ? 1 : -1))
                            {
                                int 递减次数 = Math.Abs(偏移量.X) - Math.Abs(x);
                                Color 颜色 = 颜色减淡 ? Color.FromArgb(255 - 递减次数 * 横向递减颜色R, 255 - 递减次数 * 横向递减颜色G, 255 - 递减次数 * 横向递减颜色B) : 投影颜色;
                                g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(横向递减值, 颜色)), new PointF(绘制位置.X + x, 绘制位置.Y));
                            }
                            //y轴
                            for (int y = 0; y != 偏移量.Y; y += (偏移量.Y > 0 ? 1 : -1))
                            {
                                int 递减次数 = Math.Abs(偏移量.Y) - Math.Abs(y);
                                Color 颜色 = 颜色减淡 ? Color.FromArgb(255 - 递减次数 * 纵向递减颜色R, 255 - 递减次数 * 纵向递减颜色G, 255 - 递减次数 * 纵向递减颜色B) : 投影颜色;

                                g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(纵向递减值, 颜色)), new PointF(绘制位置.X, 绘制位置.Y + y));
                            }
                        }
                        else
                        {
                            //四个象限，不包括坐标轴和原点
                            //防止接近xy轴时会出现问题，同时减少内存占用
                            bool 接近x轴 = false;
                            bool 接近y轴 = false;
                            if (Math.Abs(偏移量.X) < Math.Abs(偏移量.Y) / Math.PI)
                            {
                                接近x轴 = true;
                            }
                            else if (Math.Abs(偏移量.Y) < Math.Abs(偏移量.X) / Math.PI)
                            {
                                接近y轴 = true;
                            }

                            //横向绘制
                            if (!接近x轴)
                            {
                                for (int x = 0; x != 偏移量.X; x += (偏移量.X > 0 ? 1 : -1))
                                {
                                    float 斜率 = 偏移量.Y / (float)偏移量.X;
                                    Console.WriteLine(斜率);
                                    //颜色减淡
                                    int 递减次数 = Math.Abs(偏移量.X) - Math.Abs(x);
                                    Color 颜色 = 颜色减淡 ? Color.FromArgb(255 - 递减次数 * 横向递减颜色R, 255 - 递减次数 * 横向递减颜色G, 255 - 递减次数 * 横向递减颜色B) : 投影颜色;

                                    g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(横向递减值, 颜色)), new PointF(绘制位置.X + x, 绘制位置.Y + x * 斜率));
                                }
                            }
                            //纵向绘制
                            if (!接近y轴)
                            {
                                for (int y = 0; y != 偏移量.Y; y += (偏移量.Y > 0 ? 1 : -1))
                                {
                                    float 斜率 = 偏移量.X / (float)偏移量.Y;

                                    //颜色减淡
                                    int 递减次数 = Math.Abs(偏移量.Y) - Math.Abs(y);
                                    Color 颜色 = 颜色减淡 ? Color.FromArgb(255 - 递减次数 * 纵向递减颜色R, 255 - 递减次数 * 纵向递减颜色G, 255 - 递减次数 * 纵向递减颜色B) : 投影颜色;

                                    g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(纵向递减值, 颜色)), new PointF(绘制位置.X + y * 斜率, 绘制位置.Y + y));
                                }
                            }
                        }
                    }
                    else
                    {
                        g.DrawString(Text, Font, brush, new PointF(绘制位置.X + 偏移量.X, 绘制位置.Y + 偏移量.Y));
                    }
                    //文字本体
                    g.DrawString(Text, Font, new SolidBrush(ForeColor), 绘制位置);
                }
            }
            else
            {
                //文字本体
                g.DrawString(Text, Font, new SolidBrush(ForeColor), 绘制位置);
            }

            if (启用边框)
            {
                Rectangle 区域 = new(0, 0, Width, Height);
                g.DrawRounded(区域, new CornerRadius(圆角大小), 边框外部颜色, new Pen(边框颜色, 边框大小));
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