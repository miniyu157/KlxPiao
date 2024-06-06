using System.ComponentModel;

namespace KlxPiaoControls
{
    [DefaultEvent("值Changed")]
    public partial class PointBar : Control
    {
        public enum 坐标系
        {
            /// <summary>
            /// 屏幕坐标系，第一象限在右下角。
            /// </summary>
            计算机图形坐标系,
            /// <summary>
            /// 传统的笛卡尔坐标系，第一象限在右上角。
            /// </summary>
            数学坐标系
        }

        private Color _边框颜色;
        private Color _准星颜色;
        private Color _坐标轴颜色;
        private int _边框大小;
        private int _准星大小;
        private bool _显示坐标轴;
        private bool _显示坐标;

        private string _坐标显示格式;
        private ContentAlignment _坐标显示位置;
        private Point _最小值;
        private Point _最大值;
        private Point _值;
        private bool _响应键盘;
        private int _响应大小;
        private 坐标系 _坐标系类型;

        public PointBar()
        {
            InitializeComponent();

            _边框颜色 = Color.Gray;
            _边框大小 = 1;
            _坐标轴颜色 = Color.LightPink;
            _显示坐标轴 = true;
            _显示坐标 = true;
            _坐标显示位置 = ContentAlignment.TopLeft;
            _准星颜色 = Color.Red;
            _准星大小 = 5;
            _坐标显示格式 = "X:{X},Y:{Y}";

            _最小值 = new Point(-100, -100);
            _最大值 = new Point(100, 100);
            _值 = new Point(0, 0);
            _响应键盘 = true;
            _响应大小 = 1;
            _坐标系类型 = 坐标系.计算机图形坐标系;

            Width = 100;
            Height = 100;
            BackColor = Color.White;

            DoubleBuffered = true;
        }

        #region PointBar外观
        [Category("PointBar外观")]
        [Description("边框的颜色")]
        [DefaultValue("X:{X},Y:{Y}")]
        public string 坐标显示格式
        {
            get { return _坐标显示格式; }
            set { _坐标显示格式 = value; Invalidate(); }
        }
        [Category("PointBar外观")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "Gray")]
        public Color 边框颜色
        {
            get { return _边框颜色; }
            set { _边框颜色 = value; Invalidate(); }
        }
        [Category("PointBar外观")]
        [Description("准星的颜色")]
        [DefaultValue(typeof(Color), "Red")]
        public Color 准星颜色
        {
            get { return _准星颜色; }
            set { _准星颜色 = value; Invalidate(); }
        }
        [Category("PointBar外观")]
        [Description("坐标轴的颜色")]
        [DefaultValue(typeof(Color), "LightPink")]
        public Color 坐标轴颜色
        {
            get { return _坐标轴颜色; }
            set { _坐标轴颜色 = value; Invalidate(); }
        }
        [Category("PointBar外观")]
        [Description("边框的大小")]
        [DefaultValue(1)]
        public int 边框大小
        {
            get { return _边框大小; }
            set { _边框大小 = value; Invalidate(); }
        }
        [Category("PointBar外观")]
        [Description("准星的大小，为0时隐藏准星")]
        [DefaultValue(5)]
        public int 准星大小
        {
            get { return _准星大小; }
            set { _准星大小 = value; Invalidate(); }
        }
        [Category("PointBar外观")]
        [Description("显示以0,0为中心的坐标轴")]
        [DefaultValue(true)]
        public bool 显示坐标轴
        {
            get { return _显示坐标轴; }
            set { _显示坐标轴 = value; Invalidate(); }
        }
        [Category("PointBar外观")]
        [Description("文字颜色由ForeColor属性决定，字体由Font属性决定")]
        [DefaultValue(true)]
        public bool 显示坐标
        {
            get { return _显示坐标; }
            set { _显示坐标 = value; Invalidate(); }
        }
        [Category("PointBar外观")]
        [Description("坐标显示的位置")]
        [DefaultValue(typeof(ContentAlignment), "TopLeft")]
        public ContentAlignment 坐标显示位置
        {
            get { return _坐标显示位置; }
            set { _坐标显示位置 = value; Invalidate(); }
        }
        #endregion

        #region PointBar属性
        [Category("PointBar属性")]
        [Description("坐标的最小值")]
        [DefaultValue(typeof(Point), "-100,-100")]
        public Point 最小值
        {
            get { return _最小值; }
            set { _最小值 = value; Invalidate(); }
        }
        [Category("PointBar属性")]
        [Description("坐标的最大值")]
        [DefaultValue(typeof(Point), "100,100")]
        public Point 最大值
        {
            get { return _最大值; }
            set { _最大值 = value; Invalidate(); }
        }

        public event PropertyChangedEventHandler? 值Changed;
        protected virtual void OnValueChanged(string propertyName)
        {
            值Changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        [Category("PointBar属性")]
        [Description("坐标的值")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point 值
        {
            get { return _值; }
            set
            {
                _值 = value;
                Invalidate();

                OnValueChanged(nameof(值));
            }
        }
        [Category("PointBar属性")]
        [Description("是否可以通过方向键调整")]
        [DefaultValue(true)]
        public bool 响应键盘
        {
            get { return _响应键盘; }
            set { _响应键盘 = value; Invalidate(); }
        }
        [Category("PointBar属性")]
        [Description("通过方向键一次移动的大小")]
        [DefaultValue(1)]
        public int 响应大小
        {
            get { return _响应大小; }
            set { _响应大小 = value; Invalidate(); }
        }
        [Category("PointBar属性")]
        [Description("计算机图形坐标系（第一象限在右下角）和数学坐标系（第一象限在右上角）")]
        [DefaultValue(typeof(坐标系), "计算机图形坐标系")]
        public 坐标系 坐标系类型
        {
            get { return _坐标系类型; }
            set { _坐标系类型 = value; Invalidate(); }
        }
        #endregion

        [Browsable(false)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; Invalidate(); }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;

            int x总长 = 最大值.X - 最小值.X;
            int y总长 = 最大值.Y - 最小值.Y;

            //绘制坐标轴
            if (显示坐标轴)
            {
                Pen 坐标系Pen = new(坐标轴颜色, 1);
                float y轴 = (0 - 最小值.X) / (float)x总长;
                float x轴 = (0 - 最小值.Y) / (float)y总长;

                if (坐标系类型 == 坐标系.数学坐标系) { x轴 = 1 - x轴; }

                g.DrawLine(坐标系Pen, Width * y轴, 0, Width * y轴, Height);
                g.DrawLine(坐标系Pen, 0, Height * x轴, Width, Height * x轴);
            }
            //绘制边框
            if (边框大小 != 0)
            {
                Pen 边框Pen = new(边框颜色, 边框大小);
                g.DrawRectangle(边框Pen, new Rectangle(0, 0, Width - 1, Height - 1));
            }
            //绘制准星
            Pen 准星Pen = new(准星颜色, 1);
            void 画出十字(Point 坐标, int 长度)
            {
                double x比例 = (坐标.X - 最小值.X) / (double)x总长;
                double y比例 = (坐标.Y - 最小值.Y) / (double)y总长;

                if (坐标系类型 == 坐标系.数学坐标系) { y比例 = 1 - y比例; }

                int newx = (int)(Width * x比例);
                int newy = (int)(Height * y比例);

                g.DrawLine(准星Pen, newx - 长度, newy, newx + 长度, newy);
                g.DrawLine(准星Pen, newx, newy - 长度, newx, newy + 长度);
            }
            画出十字(值 + new Size(1, 1), 准星大小);
            // 绘制值
            if (显示坐标)
            {
                SolidBrush brush = new(ForeColor);

                string 文字 = 坐标显示格式.Replace("{X}", 值.X.ToString()).Replace("{Y}", 值.Y.ToString());
                SizeF 文字Size = g.MeasureString(文字, Font);
                float 文字Width = 文字Size.Width;
                float 文字Height = 文字Size.Height;

                var 绘制位置 = 坐标显示位置 switch
                {
                    ContentAlignment.TopLeft => new Point(0, 0),
                    ContentAlignment.TopCenter => new Point((int)((Width - 文字Width) / 2), 0),
                    ContentAlignment.TopRight => new Point((int)(Width - 文字Width), 0),
                    ContentAlignment.MiddleLeft => new Point(0, (int)((Height - 文字Height) / 2)),
                    ContentAlignment.MiddleCenter => new Point((int)((Width - 文字Width) / 2), (int)((Height - 文字Height) / 2)),
                    ContentAlignment.MiddleRight => new Point((int)(Width - 文字Width), (int)((Height - 文字Height) / 2)),
                    ContentAlignment.BottomLeft => new Point(0, (int)(Height - 文字Height)),
                    ContentAlignment.BottomCenter => new Point((int)((Width - 文字Width) / 2), (int)(Height - 文字Height)),
                    ContentAlignment.BottomRight => new Point((int)(Width - 文字Width), (int)(Height - 文字Height)),
                    _ => new Point(0, 0),
                };
                g.DrawString(文字, Font, brush, 绘制位置);
            }
        }

        bool 正在拖动 = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                int x轴总长度 = 最大值.X - 最小值.X;
                int y轴总长度 = 最大值.Y - 最小值.Y;

                值 = new Point(
                    最小值.X + (int)(x轴总长度 * (e.Location.X / (float)Width)),
                    最小值.Y + (int)(y轴总长度 * (坐标系类型 == 坐标系.数学坐标系 ? 1 - e.Location.Y / (float)Height : e.Location.Y / (float)Height))
                );

                正在拖动 = true;
            }

            Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (正在拖动)
            {
                int x轴总长度 = 最大值.X - 最小值.X;
                int y轴总长度 = 最大值.Y - 最小值.Y;

                值 = new Point(
                    最小值.X + (int)(x轴总长度 * (e.Location.X / (float)Width)),
                    最小值.Y + (int)(y轴总长度 * (坐标系类型 == 坐标系.数学坐标系 ? 1 - e.Location.Y / (float)Height : e.Location.Y / (float)Height))
                );

                if (值.X < 最小值.X) { 值 = new Point(最小值.X, 值.Y); }
                if (值.X > 最大值.X) { 值 = new Point(最大值.X, 值.Y); }
                if (值.Y < 最小值.Y) { 值 = new Point(值.X, 最小值.Y); }
                if (值.Y > 最大值.Y) { 值 = new Point(值.X, 最大值.Y); }

                Refresh();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            正在拖动 = false;
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (!响应键盘) return;

            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    Point 移动后的值 = 值;

                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            移动后的值.Y -= 响应大小 * (坐标系类型 == 坐标系.计算机图形坐标系 ? 1 : -1);
                            break;
                        case Keys.Down:
                            移动后的值.Y += 响应大小 * (坐标系类型 == 坐标系.计算机图形坐标系 ? 1 : -1);
                            break;
                        case Keys.Left:
                            移动后的值.X -= 响应大小;
                            break;
                        case Keys.Right:
                            移动后的值.X += 响应大小;
                            break;
                    }

                    移动后的值.X = Math.Max(最小值.X, Math.Min(最大值.X, 移动后的值.X));
                    移动后的值.Y = Math.Max(最小值.Y, Math.Min(最大值.Y, 移动后的值.Y));

                    值 = 移动后的值;
                    break;

                default:
                    e.IsInputKey = false;
                    break;
            }
        }
    }
}