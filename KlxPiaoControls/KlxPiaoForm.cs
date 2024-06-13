using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 一个美化的 WinForms 窗体控件，提供多种自定义风格和功能选项。
    /// </summary>
    public partial class KlxPiaoForm : Form
    {
        public enum 两端
        {
            左,
            右
        }
        public enum 位置
        {
            左,
            中,
            右
        }
        public enum 标题按钮样式
        {
            全部显示,
            关闭和最小化,
            仅关闭,
            不显示
        }
        public enum 窗体位置
        {
            仅标题框,
            整个窗体,
            不启用
        }
        public enum 风格
        {
            Windows,
            Mac
        }
        public enum 关闭按钮功能
        {
            关闭窗体,
            隐藏窗体,
            退出应用程序
        }
        public enum StartupSequence
        {
            /// <summary>
            /// 等待Load事件完成再执行启动动画
            /// </summary>
            WaitOnLoadThenAnimate,
            /// <summary>
            /// 先执行启动动画，再执行Load事件
            /// </summary>
            AnimateThenOnLoad
        }

        private Color _边框颜色;
        private 风格 _主题;

        private int _标题框高度;
        private Color _标题框背景色;
        private Color _标题框前景色;
        private Font _标题字体;
        private 位置 _标题位置;
        private int _标题左右边距;
        private 标题按钮样式 _标题按钮显示;
        private int _标题按钮宽度;
        private 两端 _标题按钮位置;
        private SizeF _标题按钮图标大小;
        private float _标题按钮颜色反馈;
        private bool _启用关闭按钮;
        private bool _启用缩放按钮;
        private bool _启用最小化按钮;
        private Color _标题按钮未启用前景色;

        private 窗体位置 _拖动方式;
        private 窗体位置 _快捷缩放方式;
        private bool _启用启动动画;
        private bool _启用关闭动画;
        private bool _启用缩放动画;
        private bool _自动隐藏窗体边框;
        private bool _可调整大小;
        private 关闭按钮功能 _关闭按钮的功能;
        private StartupSequence _启动顺序;

        private Color _未激活标题框背景色;
        private Color _未激活标题框前景色;
        private Color _未激活边框颜色;

        private void 初始化标题按钮()
        {
            关闭按钮.Name = "C";
            缩放按钮.Name = "R";
            最小化按钮.Name = "M";

            关闭按钮.Click += 关闭按钮_Click;
            缩放按钮.Click += 缩放按钮_Click;
            最小化按钮.Click += 最小化按钮_Click;
            关闭按钮.Paint += 标题按钮_Paint;
            缩放按钮.Paint += 标题按钮_Paint;
            最小化按钮.Paint += 标题按钮_Paint;
            //调整窗体大小有关
            关闭按钮.MouseDown += 标题按钮_MouseDown;
            缩放按钮.MouseDown += 标题按钮_MouseDown;
            最小化按钮.MouseDown += 标题按钮_MouseDown;
            关闭按钮.MouseMove += 标题按钮_MouseMove;
            缩放按钮.MouseMove += 标题按钮_MouseMove;
            最小化按钮.MouseMove += 标题按钮_MouseMove;
            关闭按钮.MouseUp += 标题按钮_MouseUp;
            缩放按钮.MouseUp += 标题按钮_MouseUp;
            最小化按钮.MouseUp += 标题按钮_MouseUp;

            关闭按钮.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular);
            缩放按钮.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular);
            最小化按钮.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular);
            关闭按钮.Top = 1;
            缩放按钮.Top = 1;
            最小化按钮.Top = 1;
            关闭按钮.FlatAppearance.BorderSize = 0;
            缩放按钮.FlatAppearance.BorderSize = 0;
            最小化按钮.FlatAppearance.BorderSize = 0;
            关闭按钮.可获得焦点 = false;
            缩放按钮.可获得焦点 = false;
            最小化按钮.可获得焦点 = false;
        }
        private void 标题按钮_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            PointF 图标位置 = new((关闭按钮.Width - 标题按钮图标大小.Width) / 2, (关闭按钮.Height - 标题按钮图标大小.Height) / 2);
            RectangleF 图标矩形 = new(图标位置, 标题按钮图标大小);

            Pen pen = new(标题框前景色, 1);
            Pen disablePen = new(标题按钮未启用前景色, 1); //Mac风格失效

            Pen p1 = 启用关闭按钮 ? pen : disablePen;
            Pen p2 = 启用缩放按钮 ? pen : disablePen;
            Pen p3 = 启用最小化按钮 ? pen : disablePen;

            if (sender is Control but)
            {
                switch (but.Name)
                {
                    case "C":
                        switch (主题)
                        {
                            case 风格.Windows:
                                g.SmoothingMode = SmoothingMode.HighQuality;
                                g.DrawLine(p1, 图标矩形.X, 图标矩形.Y, 图标矩形.Right - 1, 图标矩形.Bottom - 1);
                                g.DrawLine(p1, 图标矩形.Right - 1, 图标矩形.Y, 图标矩形.X, 图标矩形.Bottom - 1);
                                break;
                            case 风格.Mac:
                                g.SmoothingMode = SmoothingMode.HighQuality;
                                g.FillEllipse(new SolidBrush(Color.Red), 图标矩形);
                                break;
                        }
                        break;
                    case "R":
                        switch (主题)
                        {
                            case 风格.Windows:
                                g.SmoothingMode = SmoothingMode.Default;
                                switch (WindowState)
                                {
                                    case FormWindowState.Normal:
                                        g.DrawRectangle(p2, new RectangleF(图标矩形.X, 图标矩形.Y, 图标矩形.Width - 1, 图标矩形.Height - 1));
                                        break;
                                    case FormWindowState.Maximized:
                                        GraphicsPath path = new();
                                        path.AddLine(图标矩形.X + 图标矩形.Width * 0.2F, 图标矩形.Y + 图标矩形.Width * 0.2F, 图标矩形.X, 图标矩形.Y + 图标矩形.Width * 0.2F);
                                        path.AddLine(图标矩形.X, 图标矩形.Bottom - 1, 图标矩形.Right - 图标矩形.Width * 0.2F - 1, 图标矩形.Bottom - 1);
                                        path.AddLine(图标矩形.Right - 图标矩形.Width * 0.2F - 1, 图标矩形.Y + 图标矩形.Width * 0.2F, 图标矩形.X + 图标矩形.Width * 0.2F, 图标矩形.Y + 图标矩形.Width * 0.2F);
                                        path.AddLine(图标矩形.X + 图标矩形.Width * 0.2F, 图标矩形.Y, 图标矩形.Right - 1, 图标矩形.Y);
                                        path.AddLine(图标矩形.Right - 1, 图标矩形.Bottom - 图标矩形.Width * 0.2F - 1, 图标矩形.Right - 图标矩形.Width * 0.2F, 图标矩形.Bottom - 图标矩形.Width * 0.2F - 1);
                                        g.DrawPath(p2, path);
                                        break;
                                }
                                break;
                            case 风格.Mac:
                                g.SmoothingMode = SmoothingMode.HighQuality;
                                g.FillEllipse(new SolidBrush(Color.Orange), 图标矩形);
                                break;
                        }
                        break;
                    case "M":
                        switch (主题)
                        {
                            case 风格.Windows:
                                g.SmoothingMode = SmoothingMode.Default;
                                g.DrawLine(p3, 图标矩形.X, 图标位置.Y + 图标矩形.Height / 2 - 1, 图标矩形.Right - 1, 图标矩形.Y + 图标矩形.Height / 2 - 1);
                                break;
                            case 风格.Mac:
                                g.SmoothingMode = SmoothingMode.HighQuality;
                                g.FillEllipse(new SolidBrush(Color.DarkGray), 图标矩形);
                                break;
                        }
                        break;
                }
            }
            else
            {   
                //这里不知道怎么写
                //标题按钮_Paint(关闭按钮, e);
                //标题按钮_Paint(缩放按钮, e);
                //标题按钮_Paint(最小化按钮, e);
            }
        }
        public KlxPiaoForm()
        {
            InitializeComponent();
            初始化标题按钮();

            _边框颜色 = SystemColors.WindowFrame;
            _主题 = 风格.Windows;

            _标题框高度 = 31;
            _标题框背景色 = Color.FromArgb(224, 224, 224);
            _标题框前景色 = Color.Black;
            _标题字体 = new Font("Microsoft YaHei UI", 9);
            _标题位置 = 位置.左;
            _标题左右边距 = 11;
            _标题按钮显示 = 标题按钮样式.全部显示;
            _标题按钮宽度 = 40;
            _标题按钮位置 = 两端.右;
            _标题按钮图标大小 = new SizeF(10F, 10F);
            _标题按钮颜色反馈 = -0.04F;
            _启用关闭按钮 = true;
            _启用缩放按钮 = true;
            _启用最小化按钮 = true;
            _标题按钮未启用前景色 = Color.DarkGray;

            _拖动方式 = 窗体位置.仅标题框;
            _快捷缩放方式 = 窗体位置.仅标题框;
            _启用启动动画 = true;
            _启用关闭动画 = true;
            _启用缩放动画 = true;
            _自动隐藏窗体边框 = true;
            _可调整大小 = true;
            _关闭按钮的功能 = 关闭按钮功能.关闭窗体;
            _启动顺序 = StartupSequence.WaitOnLoadThenAnimate;

            _未激活标题框背景色 = Color.Transparent;
            _未激活标题框前景色 = SystemColors.WindowFrame;
            _未激活边框颜色 = Color.Silver;

            BackColor = Color.White;
            Text = Name;
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(700, 450);
            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
        }

        #region KlxPiaoForm外观
        [Category("KlxPiaoForm外观")]
        [Description("窗体边框的颜色")]
        public Color 边框颜色
        {
            get { return _边框颜色; }
            set { _边框颜色 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm外观")]
        [Description("窗体的视觉风格")]
        public 风格 主题
        {
            get { return _主题; }
            set { _主题 = value; Refresh(); }   //立即重绘，防止延迟
        }
        #endregion

        #region KlxPiaoForm标题框
        [Category("KlxPiaoForm标题框")]
        [Description("标题框的高度")]
        public int 标题框高度
        {
            get { return _标题框高度; }
            set { _标题框高度 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm标题框")]
        [Description("标题框的背景色")]
        public Color 标题框背景色
        {
            get { return _标题框背景色; }
            set { _标题框背景色 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm标题框")]
        [Description("标题文字的前景色和窗体按钮的前景色")]
        public Color 标题框前景色
        {
            get { return _标题框前景色; }
            set { _标题框前景色 = value; Refresh(); }   //立即重绘，防止延迟
        }
        [Category("KlxPiaoForm标题框")]
        [Description("标题的字体")]
        public Font 标题字体
        {
            get { return _标题字体; }
            set { _标题字体 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm标题框")]
        [Description("标题文字位于标题框的位置")]
        public 位置 标题位置
        {
            get { return _标题位置; }
            set { _标题位置 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm标题框")]
        [Description("标题文字位于标题框的左右边距")]
        public int 标题左右边距
        {
            get { return _标题左右边距; }
            set { _标题左右边距 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm标题框")]
        [Description("显示在标题框的按钮")]
        public 标题按钮样式 标题按钮显示
        {
            get { return _标题按钮显示; }
            set { _标题按钮显示 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm标题框")]
        [Description("标题按钮的宽度")]
        public int 标题按钮宽度
        {
            get { return _标题按钮宽度; }
            set { _标题按钮宽度 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm标题框")]
        [Description("标题按钮的位置")]
        public 两端 标题按钮位置
        {
            get { return _标题按钮位置; }
            set { _标题按钮位置 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm标题框")]
        [Description("标题按钮图标绘制大小")]
        public SizeF 标题按钮图标大小
        {
            get { return _标题按钮图标大小; }
            set { _标题按钮图标大小 = value; Refresh(); }   //立即重绘，防止延迟
        }
        [Category("KlxPiaoForm标题框")]
        [Description("决定标题按钮移入和按下的背景色，范围 -1.00 到 +1.00")]
        public float 标题按钮颜色反馈
        {
            get { return _标题按钮颜色反馈; }
            set { _标题按钮颜色反馈 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm标题框")]
        [Description("是否启用关闭按钮")]
        public bool 启用关闭按钮
        {
            get { return _启用关闭按钮; }
            set { _启用关闭按钮 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm标题框")]
        [Description("是否启用缩放按钮")]
        public bool 启用缩放按钮
        {
            get { return _启用缩放按钮; }
            set { _启用缩放按钮 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm标题框")]
        [Description("是否启用最小化按钮")]
        public bool 启用最小化按钮
        {
            get { return _启用最小化按钮; }
            set { _启用最小化按钮 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm标题框")]
        [Description("标题按钮未启用时的前景色")]
        public Color 标题按钮未启用前景色
        {
            get { return _标题按钮未启用前景色; }
            set { _标题按钮未启用前景色 = value; Refresh(); }   //立即重绘，防止延迟
        }
        #endregion

        #region KlxPiaoForm特性
        [Category("KlxPiaoForm特性")]
        [Description("窗体拖动的方式")]
        public 窗体位置 拖动方式
        {
            get { return _拖动方式; }
            set { _拖动方式 = value; }
        }
        [Category("KlxPiaoForm特性")]
        [Description("最大化或还原的快捷方式，仅缩放按钮显示并启用时生效")]
        public 窗体位置 快捷缩放方式
        {
            get { return _快捷缩放方式; }
            set { _快捷缩放方式 = value; }
        }
        [Category("KlxPiaoForm特性")]
        [Description("是否启用启动动画")]
        public bool 启用启动动画
        {
            get { return _启用启动动画; }
            set { _启用启动动画 = value; }
        }
        [Category("KlxPiaoForm特性")]
        [Description("是否启用关闭动画")]
        public bool 启用关闭动画
        {
            get { return _启用关闭动画; }
            set { _启用关闭动画 = value; }
        }
        [Category("KlxPiaoForm特性")]
        [Description("最大化或还原的动画")]
        public bool 启用缩放动画
        {
            get { return _启用缩放动画; }
            set { _启用缩放动画 = value; }
        }
        [Category("KlxPiaoForm特性")]
        [Description("设置为True后，全屏时自动隐藏窗体边框")]
        public bool 自动隐藏窗体边框
        {
            get { return _自动隐藏窗体边框; }
            set { _自动隐藏窗体边框 = value; }
        }
        [Category("KlxPiaoForm特性")]
        [Description("窗体是否可调整大小")]
        public bool 可调整大小
        {
            get { return _可调整大小; }
            set { _可调整大小 = value; }
        }
        [Category("KlxPiaoForm特性")]
        [Description("用户单击关闭按钮时执行的操作")]
        public 关闭按钮功能 关闭按钮的功能
        {
            get { return _关闭按钮的功能; }
            set { _关闭按钮的功能 = value; }
        }
        [Category("KlxPiaoForm特性")]
        [Description("启动动画和Load事件的顺序")]
        public StartupSequence 启动顺序
        {
            get { return _启动顺序; }
            set { _启动顺序 = value; Invalidate(); }
        }
        #endregion

        #region KlxPiaoForm焦点
        [Category("KlxPiaoForm焦点")]
        [Description("窗体未激活时标题框的背景色，Transparent：未激活时不改变背景色")]
        public Color 未激活标题框背景色
        {
            get { return _未激活标题框背景色; }
            set { _未激活标题框背景色 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm焦点")]
        [Description("窗体未激活时标题文字前景色和窗体按钮的前景色，Transparent：未激活时不改变前景色")]
        public Color 未激活标题框前景色
        {
            get { return _未激活标题框前景色; }
            set { _未激活标题框前景色 = value; Invalidate(); }
        }
        [Category("KlxPiaoForm焦点")]
        [Description("窗体未激活时边框的颜色，Transparent：未激活时不改变边框颜色")]
        public Color 未激活边框颜色
        {
            get { return _未激活边框颜色; }
            set { _未激活边框颜色 = value; Invalidate(); }
        }
        #endregion

        [Browsable(false)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = value; }
        }

        private readonly KlxPiaoButton 关闭按钮 = new();
        private readonly KlxPiaoButton 缩放按钮 = new();
        private readonly KlxPiaoButton 最小化按钮 = new();
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            Rectangle 窗体矩形 = new(0, 0, Width, Height);
            g.Clear(BackColor);

            //绘制标题框
            using (Brush brush = new SolidBrush(标题框背景色))
            {
                g.FillRectangle(brush, new Rectangle(窗体矩形.X, 窗体矩形.Y, 窗体矩形.Width, 标题框高度));
            }
            //绘制边框
            if (!(自动隐藏窗体边框 && WindowState == FormWindowState.Maximized))
            {
                Pen borderPen = new(边框颜色, 1);
                g.DrawRectangle(borderPen, new Rectangle(窗体矩形.X, 窗体矩形.Y, 窗体矩形.Width - 1, 窗体矩形.Height - 1));
            }
            //更新标题按钮属性
            更新标题按钮属性([关闭按钮, 缩放按钮, 最小化按钮]);
            switch (标题按钮位置)
            {
                case 两端.右:
                    关闭按钮.Left = Width - 标题按钮宽度 - 1;
                    缩放按钮.Left = Width - 标题按钮宽度 * 2 - 1;
                    break;
                case 两端.左:
                    关闭按钮.Left = 1;
                    缩放按钮.Left = 标题按钮宽度 + 1;
                    break;
            }
            //更新标题按钮显示
            switch (标题按钮显示)
            {
                case 标题按钮样式.全部显示:
                    最小化按钮.Left = 标题按钮位置 switch
                    {
                        两端.右 => Width - 标题按钮宽度 * 3 - 1,
                        两端.左 => 最小化按钮.Left = 标题按钮宽度 * 2 + 1,
                        _ => 0
                    };
                    Controls.Add(关闭按钮);
                    Controls.Add(缩放按钮);
                    Controls.Add(最小化按钮);
                    break;
                case 标题按钮样式.关闭和最小化:
                    最小化按钮.Left = 标题按钮位置 switch
                    {
                        两端.右 => Width - 标题按钮宽度 * 2 - 1,
                        两端.左 => 标题按钮宽度 + 1,
                        _ => 0
                    };
                    Controls.Add(关闭按钮);
                    Controls.Remove(缩放按钮);
                    Controls.Add(最小化按钮);
                    break;
                case 标题按钮样式.仅关闭:
                    最小化按钮.Left = 标题按钮位置 switch
                    {
                        两端.右 => 最小化按钮.Left = Width - 标题按钮宽度 - 1,
                        两端.左 => 最小化按钮.Left = 1,
                        _ => 0
                    };
                    Controls.Add(关闭按钮);
                    Controls.Remove(缩放按钮);
                    Controls.Remove(最小化按钮);
                    break;
                case 标题按钮样式.不显示:
                    Controls.Remove(关闭按钮);
                    Controls.Remove(缩放按钮);
                    Controls.Remove(最小化按钮);
                    break;
            }
            //绘制标题文字
            using (Brush brush = new SolidBrush(标题框前景色))
            {
                SizeF 文字大小 = g.MeasureString(Text, 标题字体);
                int 横向 = 0;
                switch (标题位置)
                {
                    case 位置.左:
                        if (标题按钮位置 == 两端.左)
                        {
                            横向 = 标题按钮显示 switch
                            {
                                标题按钮样式.不显示 => 标题左右边距,
                                标题按钮样式.仅关闭 => 标题左右边距 + 标题按钮宽度,
                                标题按钮样式.关闭和最小化 => 标题左右边距 + 标题按钮宽度 * 2,
                                标题按钮样式.全部显示 => 标题左右边距 + 标题按钮宽度 * 3,
                                _ => 0
                            };
                        }
                        else
                        {
                            横向 = 标题左右边距;
                        }
                        break;
                    case 位置.中:
                        横向 = (int)((Width - 文字大小.Width) / 2); break;
                    case 位置.右:
                        if (标题按钮位置 == 两端.右)
                        {
                            横向 = 标题按钮显示 switch
                            {
                                标题按钮样式.不显示 => (int)(Width - 文字大小.Width - 标题左右边距),
                                标题按钮样式.仅关闭 => (int)(Width - 文字大小.Width - 标题左右边距 - 标题按钮宽度),
                                标题按钮样式.关闭和最小化 => (int)(Width - 文字大小.Width - 标题左右边距 - 标题按钮宽度 * 2),
                                标题按钮样式.全部显示 => (int)(Width - 文字大小.Width - 标题左右边距 - 标题按钮宽度 * 3),
                                _ => 0
                            };
                        }
                        else
                        {
                            横向 = (int)(Width - 文字大小.Width - 标题左右边距);
                        }
                        break;
                }
                int 纵向 = (int)((标题框高度 - 文字大小.Height) / 2);

                //根据图标位置进行偏移
                if (Icon != null && ShowIcon && 标题位置 == 位置.左)
                {
                    g.DrawString(Text, 标题字体, brush, new Point(横向 + (标题框高度 - Icon.ToBitmap().Height) + 1, 纵向));
                }
                else
                {
                    g.DrawString(Text, 标题字体, brush, new Point(横向, 纵向));
                }
            }
            //绘制图标
            if (Icon != null && ShowIcon)
            {
                Bitmap icon = Icon.ToBitmap();
                g.DrawImage(icon, new Point((标题框高度 - icon.Height) / 2 + 1, (标题框高度 - icon.Height) / 2));
            }

            base.OnPaint(pe);
        }

        private void 更新标题按钮属性(KlxPiaoButton[] buttons)
        {
            foreach (KlxPiaoButton b in buttons)
            {
                b.Size = new Size(标题按钮宽度, 标题框高度 - 1);
                b.BackColor = 标题框背景色;
                b.FlatAppearance.MouseOverBackColor = 颜色.调整亮度(标题框背景色, 标题按钮颜色反馈);
                b.FlatAppearance.MouseDownBackColor = 颜色.调整亮度(b.FlatAppearance.MouseOverBackColor, 标题按钮颜色反馈);

                关闭按钮.Enabled = 启用关闭按钮;
                缩放按钮.Enabled = 启用缩放按钮;
                最小化按钮.Enabled = 启用最小化按钮;
            }
        }

        //关闭动画
        bool isClosing = false;
        private void CloseForm()
        {
            if (!isClosing)
            {
                isClosing = true;

                if (启用关闭动画)
                {
                    //定格窗体
                    Bitmap newbit = new(Width, Height);
                    PictureBox picbox = new()
                    {
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = newbit,
                        Location = new Point(0, 0)
                    };
                    using (Graphics g = Graphics.FromImage(newbit))
                    {
                        g.CopyFromScreen(Left, Top, 0, 0, Size);
                    }
                    if (关闭按钮的功能 != 关闭按钮功能.隐藏窗体) Controls.Clear();
                    Controls.Add(picbox);

                    Size 原大小 = Size;
                    Point 原位置 = Location;
                    float 长度 = 7F;
                    var fadeOutTask = Task.Run(async () =>
                    {
                        for (float i = 1; i >= 0; i -= (1 / 长度))
                        {
                            Invoke(() => { Opacity = i; });
                            await Task.Delay(10);
                        }
                    });
                    var shrinkTask = Task.Run(async () =>
                    {
                        for (int i = 0; i <= 长度; i++)
                        {
                            Invoke(() =>
                            {
                                Size -= new Size(i, i);
                                Location += new Size(i / 2, i / 2);
                                picbox.Size = Size; //缩放窗体
                            });
                            await Task.Delay(10);
                        }
                    });

                    Action action = 关闭按钮的功能 switch
                    {
                        关闭按钮功能.关闭窗体 => new Action(Close),
                        关闭按钮功能.隐藏窗体 => new Action(() =>
                        {
                            Hide();
                            Controls.Remove(picbox);
                            Opacity = 1;
                            Size = 原大小;
                            Location = 原位置;
                            isClosing = false;
                        }),
                        关闭按钮功能.退出应用程序 => new Action(Application.Exit),
                        _ => new Action(() => { }),
                    };
                    Task.WhenAll(fadeOutTask, shrinkTask).ContinueWith(t => Invoke(action));
                }
                else
                {
                    Action action = 关闭按钮的功能 switch
                    {
                        关闭按钮功能.关闭窗体 => new Action(Close),
                        关闭按钮功能.隐藏窗体 => new Action(Hide),
                        关闭按钮功能.退出应用程序 => new Action(Application.Exit),
                        _ => new Action(() => { }),
                    };
                    action();
                }
            }
        }

        #region "标题按钮事件"
        private void 关闭按钮_Click(object? sender, EventArgs e)
        {
            CloseForm();
        }
        private void 缩放按钮_Click(object? sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            switch (WindowState)
            {
                case FormWindowState.Normal:
                    if (启用缩放动画)
                    {
                        Opacity = 0;
                        var 最大化动画 = Task.Run(async () =>
                        {
                            for (float i = 0; i <= 1; i += 0.1F)
                            {
                                Invoke(() => { Opacity = i; });
                                await Task.Delay(10);
                            }
                            Invoke(() => { Opacity = 1; });
                        });
                    }
                    WindowState = FormWindowState.Maximized;
                    break;
                case FormWindowState.Maximized:
                    if (启用缩放动画)
                    {
                        Opacity = 0;
                        var 最大化动画 = Task.Run(async () =>
                        {
                            for (float i = 0; i <= 1; i += 0.1F)
                            {
                                Invoke(() => { Opacity = i; });
                                await Task.Delay(10);
                            }
                            Invoke(() => { Opacity = 1; });
                        });
                    }
                    WindowState = FormWindowState.Normal;
                    break;
            }
            base.OnSizeChanged(e);
        }
        private void 最小化按钮_Click(object? sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        #endregion

        //启动动画
        protected override void OnLoad(EventArgs e)
        {
            if (!启用启动动画)
            {
                base.OnLoad(e);
                return;
            }

            switch (启动顺序)
            {
                case StartupSequence.WaitOnLoadThenAnimate:
                    base.OnLoad(e);
                    FormBorderStyle = FormBorderStyle.FixedDialog;
                    FormBorderStyle = FormBorderStyle.None;
                    Refresh();
                    break;
                case StartupSequence.AnimateThenOnLoad:
                    FormBorderStyle = FormBorderStyle.FixedDialog;
                    FormBorderStyle = FormBorderStyle.None;
                    Refresh();
                    base.OnLoad(e);
                    break;
            }

        }

        //关闭事件，引发关闭动画
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            CloseForm();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            CloseForm();
        }

        //标题文字改变时重绘
        protected override void OnTextChanged(EventArgs e)
        {
            Refresh();
            base.OnTextChanged(e);
        }

        //大小改变时重绘
        protected override void OnSizeChanged(EventArgs e)
        {
            Refresh();
            base.OnSizeChanged(e);
        }

        //缩放快捷方式
        protected override void OnDoubleClick(EventArgs e)
        {
            if (标题按钮显示 == 标题按钮样式.全部显示 && 启用缩放按钮)
            {
                if (快捷缩放方式 == 窗体位置.整个窗体)
                {
                    缩放按钮_Click(缩放按钮, e);
                }
                else if (快捷缩放方式 == 窗体位置.仅标题框 && 按下位置.Y <= 标题框高度)
                {
                    缩放按钮_Click(缩放按钮, e);
                }
            }
            base.OnDoubleClick(e);
        }

        #region "窗体拖动和调整大小"
        private enum 调整位置
        {
            西, 北, 东, 南, 调完了, 西北, 东北, 西南, 东南
        }

        private Point 按下位置 = Point.Empty;
        private readonly int 判定 = 7;
        private 调整位置 正在调整位置 = 调整位置.调完了;
        private Point 原来的位置 = Point.Empty;
        private Size 原来的大小 = Size.Empty;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            按下位置 = e.Location;

            //传递正在调整的位置
            if (可调整大小 && WindowState != FormWindowState.Maximized)
            {
                原来的位置 = Location;
                原来的大小 = Size;

                bool 西 = e.X <= 判定;
                bool 东 = e.X >= Width - 判定;
                bool 北 = e.Y <= 判定;
                bool 南 = e.Y >= Height - 判定;
                if (西)
                {
                    if (北) { 正在调整位置 = 调整位置.西北; }
                    else if (南) { 正在调整位置 = 调整位置.西南; }
                    else { 正在调整位置 = 调整位置.西; }
                }
                else if (东)
                {
                    if (北) { 正在调整位置 = 调整位置.东北; }
                    else if (南) { 正在调整位置 = 调整位置.东南; }
                    else { 正在调整位置 = 调整位置.东; }
                }
                else if (北)
                {
                    正在调整位置 = 调整位置.北;
                }
                else if (南)
                {
                    正在调整位置 = 调整位置.南;
                }
                else
                {
                    正在调整位置 = 调整位置.调完了;
                }
            }

            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //拖动
            if (e.Button == MouseButtons.Left && 按下位置 != Point.Empty && 正在调整位置 == 调整位置.调完了 && WindowState != FormWindowState.Maximized)
            {
                if (拖动方式 == 窗体位置.整个窗体 || (拖动方式 == 窗体位置.仅标题框 && 按下位置.Y <= 标题框高度))
                {
                    Location = new Point(Location.X + e.X - 按下位置.X, Location.Y + e.Y - 按下位置.Y);
                }
            }

            //适应光标
            if (可调整大小 && WindowState != FormWindowState.Maximized)
            {
                bool 西 = e.X <= 判定;
                bool 东 = e.X >= Width - 判定;
                bool 北 = e.Y <= 判定;
                bool 南 = e.Y >= Height - 判定;
                if (西)
                {
                    if (北) { Cursor = Cursors.SizeNWSE; }
                    else if (南) { Cursor = Cursors.SizeNESW; }
                    else { Cursor = Cursors.SizeWE; }
                }
                else if (东)
                {
                    if (北) { Cursor = Cursors.SizeNESW; }
                    else if (南) { Cursor = Cursors.SizeNWSE; }
                    else { Cursor = Cursors.SizeWE; }
                }
                else if (北 || 南)
                {
                    Cursor = Cursors.SizeNS;
                }
                else
                {
                    Cursor = Cursors.Default;
                }

                //调整大小
                if (正在调整位置 != 调整位置.调完了)
                {
                    switch (正在调整位置)
                    {
                        case 调整位置.东:
                            Width = Cursor.Position.X - Left; break;
                        case 调整位置.南:
                            Height = Cursor.Position.Y - Top; break;
                        case 调整位置.西:
                            Left = Cursor.Position.X;
                            Width = 原来的大小.Width + 原来的位置.X - Cursor.Position.X;
                            break;
                        case 调整位置.北:
                            Top = Cursor.Position.Y;
                            Height = 原来的大小.Height + 原来的位置.Y - Cursor.Position.Y;
                            break;
                        case 调整位置.西北:
                            Location = Cursor.Position;
                            Width = 原来的大小.Width + 原来的位置.X - Cursor.Position.X;
                            Height = 原来的大小.Height + 原来的位置.Y - Cursor.Position.Y;
                            break;
                        case 调整位置.西南:
                            Left = Cursor.Position.X;
                            Width = 原来的大小.Width + 原来的位置.X - Cursor.Position.X;
                            Height = Cursor.Position.Y - Top;
                            break;
                        case 调整位置.东北:
                            Top = Cursor.Position.Y;
                            Width = Cursor.Position.X - Left;
                            Height = 原来的大小.Height + 原来的位置.Y - Cursor.Position.Y;
                            break;
                        case 调整位置.东南:
                            Width = Cursor.Position.X - Left;
                            Height = Cursor.Position.Y - Top;
                            break;
                    }
                }
            }

            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            按下位置 = Point.Empty;
            正在调整位置 = 调整位置.调完了;
            base.OnMouseUp(e);
        }

        //对于标题按钮特殊处理，只响应北和东
        private void 标题按钮_MouseDown(object? sender, MouseEventArgs e)
        {
            //传递正在调整的位置
            if (可调整大小 && WindowState != FormWindowState.Maximized)
            {
                原来的位置 = Location;
                原来的大小 = Size;

                bool 东 = e.X >= Width - 判定;
                bool 北 = e.Y <= 判定;

                if (东 && 北)
                {
                    正在调整位置 = 调整位置.东北;
                }
                else if (东)
                {
                    正在调整位置 = 调整位置.东;
                }
                else if (北)
                {
                    正在调整位置 = 调整位置.北;
                }

            }
        }
        private void 标题按钮_MouseMove(object? sender, MouseEventArgs e)
        {
            //适应光标
            if (可调整大小 && WindowState != FormWindowState.Maximized)
            {
                bool 东 = e.X >= Width - 判定;
                bool 北 = e.Y <= 判定;

                if (东 && 北)
                {
                    Cursor = Cursors.SizeNESW;
                }
                else if (东)
                {
                    Cursor = Cursors.SizeWE;
                }
                else if (北)
                {
                    Cursor = Cursors.SizeNS;
                }
                else
                {
                    Cursor = Cursors.Default;
                }

                //调整大小
                if (正在调整位置 != 调整位置.调完了)
                {
                    switch (正在调整位置)
                    {
                        case 调整位置.东:
                            Width = Cursor.Position.X - Left; break;
                        case 调整位置.北:
                            Top = Cursor.Position.Y;
                            Height = 原来的大小.Height + 原来的位置.Y - Cursor.Position.Y;
                            break;
                        case 调整位置.东北:
                            Top = Cursor.Position.Y;
                            Width = Cursor.Position.X - Left;
                            Height = 原来的大小.Height + 原来的位置.Y - Cursor.Position.Y;
                            break;
                    }
                }
            }
        }
        private void 标题按钮_MouseUp(object? sender, MouseEventArgs e)
        {
            按下位置 = Point.Empty;
            正在调整位置 = 调整位置.调完了;
        }
        #endregion

        #region "焦点反馈"
        private Color 原来的背景色 = Color.Empty;
        private Color 原来的前景色 = Color.Empty;
        private Color 原来的边框颜色 = Color.Empty;
        protected override void OnActivated(EventArgs e)
        {
            if (原来的背景色 == Color.Empty)
            {
                原来的背景色 = 标题框背景色;
            }
            if (原来的前景色 == Color.Empty)
            {
                原来的前景色 = 标题框前景色;
            }
            if (原来的边框颜色 == Color.Empty)
            {
                原来的边框颜色 = 边框颜色;
            }
            标题框背景色 = 原来的背景色;
            标题框前景色 = 原来的前景色;
            边框颜色 = 原来的边框颜色;

            Refresh();
            base.OnActivated(e);
        }
        protected override void OnDeactivate(EventArgs e)
        {
            原来的背景色 = 标题框背景色;
            原来的前景色 = 标题框前景色;
            原来的边框颜色 = 边框颜色;

            if (未激活标题框背景色 != Color.Transparent)
            {
                标题框背景色 = 未激活标题框背景色;
            }
            if (未激活标题框前景色 != Color.Transparent)
            {
                标题框前景色 = 未激活标题框前景色;
            }
            if (未激活边框颜色 != Color.Transparent)
            {
                边框颜色 = 未激活边框颜色;
            }

            Refresh();
            base.OnDeactivate(e);
        }
        #endregion

        /// <summary>
        /// 获取工作区的大小
        /// </summary>
        /// <returns></returns>
        public Size 获取工作区大小()
        {
            return new Size(Width - 2, Height - 标题框高度 - 1);
        }
        /// <summary>
        /// 获取工作区的矩形
        /// </summary>
        /// <returns></returns>
        public Rectangle 获取工作区矩形()
        {
            return new Rectangle(new Point(1, 标题框高度), 获取工作区大小());
        }
        /// <summary>
        /// 将指定的字体应用与窗体的每个控件，不改变字体样式和字体大小（可选）
        /// </summary>
        /// <param name="字体"></param>
        /// <param name="字体大小修正">要应用到每个控件的字体大小的偏移量，默认为0</param>
        public void 设置全局字体(FontFamily 字体, float 字体大小修正 = 0)
        {
            控件.遍历<Control>(this, control =>
            {
                control.Font = new Font(字体, control.Font.Size + 字体大小修正, control.Font.Style);
            });

            标题字体 = new Font(字体, 标题字体.Size, 标题字体.Style);
        }
    }
}