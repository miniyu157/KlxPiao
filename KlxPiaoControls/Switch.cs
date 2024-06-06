using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    [DefaultEvent("CheckedChanged")]
    public partial class Switch : Control
    {
        private bool _Checked;

        private Color _背景色;
        private Color _前景色;
        private Color _边框颜色;
        private int _边框大小;

        private int _文字与开关间距;
        private Color _激活时背景色;
        private Color _激活时前景色;
        private Color _激活时边框颜色;
        private int _激活时边框大小;
        private string _激活时文字;

        public Switch()
        {
            InitializeComponent();

            _Checked = false;

            _背景色 = Color.FromArgb(204, 204, 204);
            _前景色 = Color.White;
            _边框颜色 = Color.FromArgb(204, 204, 204);
            _边框大小 = 1;
            _文字与开关间距 = 5;

            _激活时背景色 = Color.FromArgb(120, 214, 144);
            _激活时前景色 = Color.Transparent;
            _激活时边框颜色 = Color.Transparent;
            _激活时边框大小 = -1;
            _激活时文字 = string.Empty;

            Size = new Size(94, 17);
            DoubleBuffered = true;
            SetStyle(ControlStyles.Selectable, true);

        }
        public event PropertyChangedEventHandler? CheckedChanged;

        protected virtual void OnValueChanged(string propertyName)
        {
            CheckedChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Category("Switch属性")]
        [Description("获取或设置开关的状态")]
        [DefaultValue(false)]
        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                Invalidate();
                OnValueChanged(nameof(Checked));
            }
        }

        #region Switch外观
        [Category("Switch外观")]
        [Description("开关背景色，并不是BackColor属性")]
        [DefaultValue(typeof(Color), "204,204,204")]
        public Color 背景色
        {
            get { return _背景色; }
            set { _背景色 = value; Invalidate(); }
        }
        [Category("Switch外观")]
        [Description("开关的颜色")]
        [DefaultValue(typeof(Color), "White")]
        public Color 前景色
        {
            get { return _前景色; }
            set { _前景色 = value; Invalidate(); }
        }
        [Category("Switch外观")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "204,204,204")]
        public Color 边框颜色
        {
            get { return _边框颜色; }
            set { _边框颜色 = value; Invalidate(); }
        }
        [Category("Switch外观")]
        [Description("边框的大小，为0时隐藏边框")]
        [DefaultValue(1)]
        public int 边框大小
        {
            get { return _边框大小; }
            set { _边框大小 = value; Invalidate(); }
        }
        [Category("Switch外观")]
        [Description("文字与开关的间距")]
        [DefaultValue(5)]
        public int 文字与开关间距
        {
            get { return _文字与开关间距; }
            set { _文字与开关间距 = value; Invalidate(); }
        }
        #endregion

        #region Switch激活
        [Category("Switch激活")]
        [Description("Checked=True时呈现的背景色，Transparent：不改变前景色")]
        [DefaultValue(typeof(Color), "120,214,144")]
        public Color 激活时背景色
        {
            get { return _激活时背景色; }
            set { _激活时背景色 = value; Invalidate(); }
        }
        [Category("Switch激活")]
        [Description("Checked=True时呈现的前景色，Transparent：不改变前景色")]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color 激活时前景色
        {
            get { return _激活时前景色; }
            set { _激活时前景色 = value; Invalidate(); }
        }
        [Category("Switch激活")]
        [Description("Checked=True时呈现的边框颜色，Transparent：不改变边框颜色")]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color 激活时边框颜色
        {
            get { return _激活时边框颜色; }
            set { _激活时边框颜色 = value; Invalidate(); }
        }
        [Category("Switch激活")]
        [Description("Checked=True时呈现的边框大小，-1：不改变边框大小")]
        [DefaultValue(-1)]
        public int 激活时边框大小
        {
            get { return _激活时边框大小; }
            set { _激活时边框大小 = value; Invalidate(); }
        }
        [Category("Switch激活")]
        [Description("Checked=True时呈现的文字，留空时不改变文字")]
        public string 激活时文字
        {
            get { return _激活时文字; }
            set { _激活时文字 = value; Invalidate(); }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Color 背景色修正 = 激活时背景色 == Color.Transparent ? 背景色 : 激活时背景色;
            Color 前景色修正 = 激活时前景色 == Color.Transparent ? 前景色 : 激活时前景色;
            Color 边框颜色修正 = 激活时边框颜色 == Color.Transparent ? 边框颜色 : 激活时边框颜色;
            int 边框大小修正 = 激活时边框大小 == -1 ? 边框大小 : 激活时边框大小;
            string 文字修正 = 激活时文字 == string.Empty ? Text : 激活时文字;

            int 默认文字宽度 = (int)g.MeasureString(Text, Font).Width;
            int 激活文字宽度 = (int)g.MeasureString(激活时文字, Font).Width;
            int 较长的文字宽度 = Math.Max(默认文字宽度, 激活文字宽度);
            int 当前显示的文字宽度 = (int)g.MeasureString(Checked ? 文字修正 : Text, Font).Width;
            int 文字高度 = (int)g.MeasureString(Checked ? 文字修正 : Text, Font).Height;

            Rectangle 区域 = new(较长的文字宽度 + 文字与开关间距, 0, Width, Height);

            if (!Checked)
            {
                Rectangle 绘制区域 = new(区域.X + 边框大小 / 2, 区域.Y + 边框大小 / 2, 区域.Height - 边框大小, 区域.Height - 边框大小);
                g.Clear(背景色);
                g.FillEllipse(new SolidBrush(前景色), 绘制区域);
            }
            else
            {
                Rectangle 绘制区域 = new(区域.Width - 边框大小修正 / 2 - (区域.Height - 边框大小修正), 区域.Y + 边框大小修正 / 2, 区域.Height - 边框大小修正, 区域.Height - 边框大小修正);
                g.Clear(背景色修正);
                g.FillEllipse(new SolidBrush(前景色修正), 绘制区域);
            }

            #region 边框&圆角
            Color 边框外部颜色 = BackColor;
            float 圆角百分比 = 1;
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
            if (边框大小 != 0)
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

                g.DrawPath(new Pen(Checked ? 边框颜色修正 : 边框颜色, Checked ? 边框大小修正 : 边框大小), 边框);
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
            #endregion

            //覆盖多余的部分
            g.FillRectangle(new SolidBrush(边框外部颜色), new Rectangle(0, 0, 较长的文字宽度 + 文字与开关间距, Height));
            g.DrawString(Checked ? 文字修正 : Text, Font, new SolidBrush(ForeColor), new PointF((较长的文字宽度 + 文字与开关间距 - 当前显示的文字宽度) / 2, (Height - 文字高度) / 2));
        }

        //点击事件
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (e.Button == MouseButtons.Left)
            {
                Checked = !Checked;
                Focus();
            }
        }
        //文字改变时及时刷新
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Refresh();
        }

        //键盘响应
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Space:
                    Checked = !Checked;
                    break;
                case Keys.Left:
                    if (Checked)
                    {
                        Checked = false;
                    }
                    e.IsInputKey = true;
                    break;
                case Keys.Right:
                    if (!Checked)
                    {
                        Checked = true;
                    }
                    e.IsInputKey = true;
                    break;
            }
        }
    }
}