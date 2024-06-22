using System.ComponentModel;

namespace KlxPiaoControls
{
    /// <summary>
    /// 表示一个按钮控件。
    /// </summary>
    /// <remarks>
    /// <see cref="KlxPiaoButton"/> 继承自 <see cref="Button"/>，是原版 <see cref="Button"/> 的增强版本。
    /// </remarks>
    public partial class KlxPiaoButton : Button
    {
        private bool _可获得焦点;
        private Size _ImageSize;

        [Category("KlxPiaoButton特性")]
        [Description("控件是否可获得焦点")]
        [DefaultValue(true)]
        public bool 可获得焦点
        {
            get { return _可获得焦点; }
            set { _可获得焦点 = value; Invalidate(); }
        }
        [Category("KlxPiaoButton特性")]
        [Description("设置显示在按钮上Image的大小，留空时使用系统默认的大小")]
        [DefaultValue(typeof(Size), "0,0")]
        public Size ImageSize
        {
            get { return _ImageSize; }
            set { _ImageSize = value; Invalidate(); }
        }

        public KlxPiaoButton()
        {
            InitializeComponent();

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 1;
            FlatAppearance.BorderColor = Color.Gainsboro;
            FlatAppearance.MouseDownBackColor = Color.FromArgb(230, 230, 230);
            FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);

            Size = new Size(110, 40);
            DoubleBuffered = true;

            _ImageSize = new Size(0, 0);
            _可获得焦点 = true;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            SetStyle(ControlStyles.Selectable, 可获得焦点);

            base.OnPaint(pevent);

            if (ImageSize != new Size(0, 0) && Image != null && ImageSize != Image.Size)
            {
                Image = new Bitmap(Image, ImageSize);
            }
        }
    }
}