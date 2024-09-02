using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 表示一个可以自定义外观的列表控件。
    /// </summary>
    /// <remarks><see cref="KlxPiaoListBox"/> 继承自 <see cref="ListBox"/>，是原版 <see cref="ListBox"/> 的增强版本。</remarks>
    public partial class KlxPiaoListBox : ListBox
    {
        private Color _selectedBackColor;
        private Color _selectedForeColor;
        private ContentAlignment _textAlign;
        private Point _textOffset;

        /// <summary>
        /// 获取或设置选中项的背景色。
        /// </summary>
        [Category("KlxPiaoListBox")]
        [DefaultValue(typeof(Color), "Empty")]
        [Description("选中项的背景色")]
        public Color SelectedBackColor
        {
            get => _selectedBackColor;
            set { _selectedBackColor = value; Invalidate(); }
        }

        /// <summary>
        /// 获取或设置选中项的前景色。
        /// </summary>
        [Category("KlxPiaoListBox")]
        [DefaultValue(typeof(Color), "Empty")]
        [Description("选中项的前景色")]
        public Color SelectedForeColor
        {
            get => _selectedForeColor;
            set { _selectedForeColor = value; Invalidate(); }
        }

        /// <summary>
        /// 获取或设置文本的布局。
        /// </summary>
        [Category("KlxPiaoListBox")]
        [DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        [Description("文本的布局")]
        public ContentAlignment TextAlign
        {
            get => _textAlign;
            set { _textAlign = value; Invalidate(); }
        }

        /// <summary>
        /// 获取或设置文本的偏移。
        /// </summary>
        [Category("KlxPiaoListBox")]
        [DefaultValue(typeof(Point), "0, 0")]
        [Description("文本的偏移")]
        public Point TextOffset
        {
            get => _textOffset;
            set { _textOffset = value; Invalidate(); }
        }

        public KlxPiaoListBox()
        {
            InitializeComponent();

            _textAlign = ContentAlignment.MiddleCenter;
            _textOffset = Point.Empty;

            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 30;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (e.Index < 0 || e.Index >= Items.Count)
                return;

            e.DrawBackground();

            var itemText = Items[e.Index].ToString();
            Font? font = e.Font;
            if (font != null)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                var textSize = g.MeasureString(itemText, font);
                var textPos = LayoutUtilities.CalculateAlignedPosition(e.Bounds, textSize, TextAlign, TextOffset);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    using SolidBrush backgroundBrush = new(SelectedBackColor);
                    using SolidBrush foregroundBrush = new(SelectedForeColor == Color.Empty ? ForeColor : SelectedForeColor);
                    g.FillRectangle(backgroundBrush, e.Bounds);
                    g.DrawString(itemText, font, foregroundBrush, textPos.X, textPos.Y);
                }
                else
                {
                    using SolidBrush defForegroundBrush = new(ForeColor);
                    g.DrawString(itemText, font, defForegroundBrush, textPos.X, textPos.Y);
                }
                e.DrawFocusRectangle();
            }
        }
    }
}