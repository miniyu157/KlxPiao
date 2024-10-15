using KlxPiaoAPI;

namespace KlxPiaoControls
{
    public partial class SlideSwitch : Control
    {
        private string[] _items;
        private int _selectIndex;
        private Size _itemSize;
        private Size _selectItemSize;

        public SlideSwitch()
        {
            InitializeComponent();

            _items = ["item1", "item2"];
            _selectIndex = 0;
            _itemSize = new(45, 35);
            _selectItemSize = new(45, 35);

            //
            // containersPanel
            //
            containersPanel.BorderSize = 3;

            selectLabel.BorderSize = 3;

        }

        public string[] Items
        {
            get => _items;
            set
            {
                if (value.Length < 1)
                {
                    throw new ArgumentException("至少保留一项", nameof(value));
                }

                _items = value;
                RefreshSize();
            }
        }
        public Size ItemSize
        {
            get => _itemSize;
            set
            {
                _itemSize = value;
                RefreshSize();
            }
        }
        public Size SelectItemSize
        {
            get => _selectItemSize;
            set
            {
                _selectItemSize = value;
                RefreshSize();
            }
        }

        private void RefreshSize()
        {
            Rectangle thisRect = new(0, 0, Width, Height);

            Width = Math.Max(SelectItemSize.Width, ItemSize.Width) * Items.Length;
            Height = Math.Max(SelectItemSize.Height, ItemSize.Height);

            containersPanel.Size = new Size(ItemSize.Width * Items.Length, Height);
            selectLabel.Size = SelectItemSize;

            containersPanel.Location = LayoutUtilities.CalculateAlignedPosition(thisRect, containersPanel.Size, ContentAlignment.MiddleCenter);
            //selectLabel.Location = LayoutUtilities.CalculateAlignedPosition(thisRect, containersPanel.Size, ContentAlignment.MiddleCenter);

            
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private KlxPiaoPanel containersPanel = new();
        private KlxPiaoLabel selectLabel = new();
    }
}
