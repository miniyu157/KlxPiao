namespace KlxPiaoControls
{
    /// <summary>
    /// 初始化一些属性值的原版LinkLabel。
    /// </summary>
    public partial class KlxPiaoLinkLabel : LinkLabel
    {
        public KlxPiaoLinkLabel()
        {
            InitializeComponent();

            LinkBehavior = LinkBehavior.HoverUnderline;
            BackColor = Color.White;
            LinkColor = Color.Black;
            ForeColor = Color.Black;
            ActiveLinkColor = Color.Black;
            DisabledLinkColor = Color.FromArgb(210, 210, 210);
        }
    }
}