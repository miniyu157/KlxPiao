using System.ComponentModel;

namespace KlxPiaoControls
{
    /// <summary>
    /// 表示一个与外壳 <see cref="TabControlContainer"/> 绑定的自定义选项卡控件。
    /// </summary>
    public partial class KlxPiaoTabControl : TabControl
    {
        public Color 边框颜色;
        private Color 当前页背景色 = Color.Empty;

        public KlxPiaoTabControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            Size = new Size(299, 127);
        }

        [DefaultValue(typeof(Size), "299,127")]
        public new Size Size
        {
            get => base.Size;
            set { base.Size = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;

            if (当前页背景色 == Color.Empty)
            {
                if (SelectedIndex > -1)
                {
                    g.Clear(TabPages[SelectedIndex].BackColor);
                }
            }
            else
            {
                g.Clear(当前页背景色);
            }

            //边框
            Pen BorderPen = new(边框颜色, 1);
            g.DrawLine(BorderPen, 0, 0, Width - 1, 0);                   //上
            g.DrawLine(BorderPen, Width - 1, 0, Width - 1, Height - 1);  //右
            g.DrawLine(BorderPen, Width - 1, Height - 1, 0, Height - 1); //下

            //未绑定时显示提示文本
            if (ItemSize.Height != 1)
            {
                g.DrawString($"{Name}:请绑定TabControlContainer", new Font("微软雅黑", 9), new SolidBrush(Color.Red), new Point(6, 6));
            }
        }
        protected override void OnSelected(TabControlEventArgs e)
        {
            base.OnSelected(e);
            if (e.TabPage != null)
            {
                当前页背景色 = e.TabPage.BackColor;
                Refresh();
            }
        }
    }
}