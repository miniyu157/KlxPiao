using System.ComponentModel;

namespace KlxPiaoControls
{
    public partial class TabControlContainer : Control
    {
        private KlxPiaoTabControl? _绑定;
        private Size _选项卡大小;
        private ContentAlignment _文字位置;
        private ContentAlignment _图片位置;
        private Color _边框颜色;
        private Color _投影颜色;
        private int _投影长度;

        public TabControlContainer()
        {
            InitializeComponent();

            SetStyle(ControlStyles.ContainerControl, true);
            DoubleBuffered = true;

            _选项卡大小 = new Size(88, 33);
            _文字位置 = ContentAlignment.MiddleCenter;
            _图片位置 = ContentAlignment.MiddleLeft;
            _边框颜色 = Color.Gainsboro;
            _投影颜色 = Color.Gainsboro;
            _投影长度 = 5;
            _绑定 = null;

            Size = new Size(249, 135);
        }

        #region KlxPiaoTabControl属性
        [Category("KlxPiaoTabControl属性"), Description("选择绑定的KlxPiaoTabPage后，会强制设置绑定选项卡的大小、位置等属性")]
        public KlxPiaoTabControl? 绑定
        {
            get { return _绑定; }
            set { _绑定 = value; Invalidate(); }
        }
        [Category("KlxPiaoTabControl属性"), Description("选项卡的大小")]
        public Size 选项卡大小
        {
            get { return _选项卡大小; }
            set { _选项卡大小 = value; Invalidate(); }
        }
        [Category("KlxPiaoTabControl属性"), Description("文字的位置")]
        public ContentAlignment 文字位置
        {
            get { return _文字位置; }
            set { _文字位置 = value; Invalidate(); }
        }
        [Category("KlxPiaoTabControl属性"), Description("图片的位置")]
        public ContentAlignment 图片位置
        {
            get { return _图片位置; }
            set { _图片位置 = value; Invalidate(); }
        }
        [Category("KlxPiaoTabControl属性"), Description("边框的颜色")]
        public Color 边框颜色
        {
            get { return _边框颜色; }
            set { _边框颜色 = value; Invalidate(); }
        }
        [Category("KlxPiaoTabControl属性"), Description("选项卡菜单边缘的投影颜色")]
        public Color 投影颜色
        {
            get { return _投影颜色; }
            set { _投影颜色 = value; Invalidate(); }
        }
        [Category("KlxPiaoTabControl属性"), Description("选项卡菜单边缘的投影长度，为1时隐藏投影，为0时隐藏边框")]
        public int 投影长度
        {
            get { return _投影长度; }
            set { _投影长度 = value; Invalidate(); }
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

            g.Clear(Color.White);

            //选项卡边缘
            Pen 选项卡Pen = new(边框颜色, 1);
            g.DrawLine(选项卡Pen, 选项卡大小.Width + 1, 0, 选项卡大小.Width + 1, Height);

            //更新选项卡菜单
            foreach (Control control in Controls)
            {
                if (control is 无法获得焦点的按钮)
                {
                    Controls.Remove(control);
                }
            }

            if (绑定 != null)
            {
                for (int i = 0; i < 绑定.TabCount; i++)
                {
                    string 标题 = 绑定.TabPages[i].Text;

                    var 选项卡按钮 = new 无法获得焦点的按钮(绑定)
                    {
                        Size = 选项卡大小,
                        Location = new Point(1, 1 + i * 选项卡大小.Height),
                        Text = 标题,
                        Tag = i,
                        Font = Font,
                        Padding = Padding,
                        TextAlign = 文字位置,
                        ImageAlign = 图片位置
                    };
                    选项卡按钮.FlatAppearance.BorderColor = 边框颜色;

                    if (绑定.ImageList != null)
                    {
                        选项卡按钮.Image = 绑定.ImageList.Images[绑定.TabPages[i].ImageIndex];
                    }

                    Controls.Add(选项卡按钮);
                }

                if (!Controls.Contains(绑定))
                {
                    Controls.Add(绑定);
                }

                绑定.Alignment = TabAlignment.Left;
                绑定.ItemSize = new Size(0, 1);
                绑定.Location = new Point(选项卡大小.Width + 1 + 投影长度, 0);
                绑定.Size = new Size(Width - 选项卡大小.Width - 1 - 投影长度, Height);
                绑定.边框颜色 = 边框颜色;
            }
            else
            {
                g.DrawString($"绑定\nKlxPiaoTabControl\n以使用\n\n请勿重复绑定", new Font("微软雅黑", 9), new SolidBrush(Color.Red), new Point(6, 6));
            }

            //投影
            int 递减值 = 投影长度 == 0 ? 0 : 255 / 投影长度;
            for (int i = 选项卡大小.Width + 1; i <= 选项卡大小.Width + 1 + 投影长度; i++)
            {
                int 透明度 = 255 - (i - (选项卡大小.Width + 1)) * 递减值;
                g.DrawLine(new Pen(Color.FromArgb(透明度, 投影颜色), 1), new Point(i, 0), new Point(i, Height));
            }

            //边框
            Pen 边框Pen = new(边框颜色, 1);
            g.DrawRectangle(边框Pen, 0, 0, Width - 1, Height - 1);
        }

        private class 无法获得焦点的按钮 : Button
        {
            private readonly System.Windows.Forms.Timer 监听Timer = new();
            private readonly KlxPiaoTabControl _传递;

            public 无法获得焦点的按钮(KlxPiaoTabControl 传递变量)
            {
                SetStyle(ControlStyles.Selectable, false);

                FlatStyle = FlatStyle.Flat;
                FlatAppearance.BorderSize = 0;
                FlatAppearance.MouseDownBackColor = Color.FromArgb(245, 245, 245);
                FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);

                Font = new Font("微软雅黑 Light", 9);

                监听Timer.Interval = 100;
                监听Timer.Start();

                _传递 = 传递变量;
                监听Timer.Tick += 监听状态;
                MouseDown += Button_MouseDown;
            }

            private void Button_MouseDown(object? sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _传递.SelectedIndex = Convert.ToInt32(Tag);
                }
            }

            // 启用视觉反馈（目前想不出更好的方法）
            private void 监听状态(object? sender, EventArgs e)
            {
                if (_传递.SelectedIndex == Convert.ToInt32(Tag))
                {
                    BackColor = Color.FromArgb(245, 245, 245);
                }
                else
                {
                    BackColor = Color.White;
                }
            }
        }
    }
}