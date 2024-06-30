using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 滑动开关组件，用于选择不同的选项。
    /// </summary>
    /// <remarks><see cref="SlideSwitch"/> 继承自 <see cref="UserControl"/>，是由 <see cref="KlxPiaoLabel"/> 和 <see cref="KlxPiaoPanel"/> 构成的。</remarks>
    [DefaultEvent("SelectIndexChanged")]
    public partial class SlideSwitch : UserControl
    {
        /// <summary>
        /// 外观属性的枚举。
        /// </summary>
        public enum StyleProperties
        {
            BackColor,
            ForeColor,
            BorderColor,
            NoChange
        }

        private String[] _Items;
        private Size _ItemSize;
        private Size _SelectSize;
        private int _SelectIndex;
        private Color[] _ChangeColors;
        private StyleProperties _ChangeProperty;
        private bool _Draggable;
        private bool _AllowDragOutOfBounds;
        private bool _UpdateTextOnDrag;
        private bool _EnableMouseWheel;
        private bool _IsAnimationEnabled;
        private Animation _TransAnim;
        private Animation _ColorAnim;

        public SlideSwitch()
        {
            InitializeComponent();

            _Items = ["Item1", "Item2"];
            _ItemSize = new Size(58, 38);
            _SelectSize = new Size(50, 46);
            _SelectIndex = 0;
            _ChangeColors = [
                Color.FromArgb(17, 178, 48), //参考自 Phigros 难度选择器-EZ背景色
                Color.FromArgb(0, 117, 184), //参考自 Phigros 难度选择器-HD背景色
                Color.FromArgb(207, 19, 18)  //参考自 Phigros 难度选择器-IN背景色
                ];
            _ChangeProperty = StyleProperties.BackColor;
            _Draggable = true;
            _AllowDragOutOfBounds = false;
            _UpdateTextOnDrag = true;
            _EnableMouseWheel = true;
            _IsAnimationEnabled = true;
            _TransAnim = new Animation(200, 100, "0, 1, 0.67, 1");
            _ColorAnim = new Animation(150, 30, "0, 0, 1, 1");

            DoubleBuffered = true;
            //
            //下面这个玄学Bug不知道怎么解决）
            //
            //初始化外观（后面的注释表示组件的默认值，当用户设置为这些值时，启动时会自动应用为下列代码设置的属性值）
            // 
            // ItemsShow
            // 
            ItemsShow.CornerRadius = new CornerRadius(10); //0
            ItemsShow.IsEnableShadow = false; //true
            // 
            // SelectShow
            // 
            SelectShow.BorderSize = 1; //5
            SelectShow.CornerRadius = new CornerRadius(10); //0
            SelectShow.SmoothingMode = SmoothingMode.HighQuality; //Default
            SelectShow.PixelOffsetMode = PixelOffsetMode.HighQuality; //Default
            SelectShow.IsEnableBorder = true; //false
            SelectShow.TextAlign = ContentAlignment.MiddleCenter; //TopLeft
            SelectShow.AutoSize = false; //true
            SelectShow.Location = new(3, 0);
            SelectShow.BackColor = _ChangeColors[0];
            SelectShow.Text = _Items[0];
        }

        #region SlideSwitch外观
        /// <summary>
        /// 获取或设置选中的选项卡的外观。这是通过 <see cref="KlxPiaoLabel"/> 实现的。
        /// </summary>
        [Category("SlideSwitch外观")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("选中的选项卡的属性")]
        public KlxPiaoLabel SelectItemStyle => SelectShow;

        /// <summary>
        /// 获取或设置选项卡容器的外观。这是通过 <see cref="KlxPiaoPanel"/> 实现的。
        /// </summary>
        [Category("SlideSwitch外观")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("选项卡容器的属性")]
        public KlxPiaoPanel ItemsStyle => ItemsShow;

        /// <summary>
        /// 获取或设置选项卡的大小。
        /// </summary>
        [Category("SlideSwitch外观")]
        [Description("每个选项卡的大小")]
        [DefaultValue("58,38")]
        public Size ItemSize
        {
            get { return _ItemSize; }
            set { _ItemSize = value; Refresh(); RefreshSelectShowLocation(); }
        }
        /// <summary>
        /// 获取或设置选中的选项卡大小。
        /// </summary>
        [Category("SlideSwitch外观")]
        [Description("选中选项卡的大小")]
        [DefaultValue("50,46")]
        public Size SelectSize
        {
            get { return _SelectSize; }
            set { _SelectSize = value; Refresh(); RefreshSelectShowLocation(); }
        }
        /// <summary>
        /// 获取或设置选项卡被选中时的外观数组。
        /// </summary>
        [Category("SlideSwitch外观")]
        [Description("分别选中每个选项卡改变的颜色数组")]
        public Color[] ChangeColors
        {
            get { return _ChangeColors; }
            set
            {
                _ChangeColors = value;
                //更新当前的外观
                if (_ChangeProperty != StyleProperties.NoChange)
                {
                    SelectShow.SetOrGetPropertyValue(GetChangePropertyValue(), value[_SelectIndex]);
                }
                Invalidate();
            }
        }
        /// <summary>
        /// 获取或设置选项卡被选中时改变的外观。
        /// </summary>
        [Category("SlideSwitch外观")]
        [Description("选中每个选项卡时改变的属性外观")]
        [DefaultValue("BackColor")]
        public StyleProperties ChangeProperty
        {
            get { return _ChangeProperty; }
            set
            {
                _ChangeProperty = value;
                //更新当前的外观
                if (_ChangeProperty != StyleProperties.NoChange)
                {
                    SelectShow.SetOrGetPropertyValue(GetChangePropertyValue(), _ChangeColors[_SelectIndex]);
                }
                Invalidate();
            }
        }
        #endregion

        #region SlideSwitch属性
        /// <summary>
        /// 获取或设置选项卡集合。
        /// </summary>
        [Category("SlideSwitch属性")]
        [Description("选项卡的集合")]
        public String[] Items
        {
            get { return _Items; }
            set
            {
                if (value.Length < 1)
                {
                    throw new ArgumentException("至少保留一项。");
                }

                _Items = value;
                //更新当前文本
                SelectShow.Text = value[_SelectIndex];
                Invalidate();
            }
        }
        /// <summary>
        /// 获取或设置选中的索引。
        /// </summary>
        [Category("SlideSwitch属性")]
        [Description("选中选项卡的索引")]
        [DefaultValue(0)]
        public int SelectIndex
        {
            get { return _SelectIndex; }
            set
            {
                //仅属性改变时触发
                if (_SelectIndex != value)
                {
                    _SelectIndex = value;
                    RefreshSelectShowLocation();
                    OnSelectIndexChangedByCode(new IndexChangedEventArgs(value, Items[value]));
                    Invalidate();
                }
            }
        }
        /// <summary>
        /// 是否允许拖动。
        /// </summary>
        [Category("SlideSwitch属性")]
        [Description("是否允许拖动以调整选项卡索引")]
        [DefaultValue(true)]
        public bool Draggable
        {
            get { return _Draggable; }
            set { _Draggable = value; }
        }
        /// <summary>
        /// 是否允许拖动时超出边界。
        /// </summary>
        [Category("SlideSwitch属性")]
        [Description("是否允许拖动选项卡超出边界")]
        [DefaultValue(false)]
        public bool AllowDragOutOfBounds
        {
            get { return _AllowDragOutOfBounds; }
            set { _AllowDragOutOfBounds = value; }
        }
        /// <summary>
        /// 是否在拖动时即时更新文本。
        /// </summary>
        [Category("SlideSwitch属性")]
        [Description("在拖动过程中是否即时更新文本")]
        [DefaultValue(true)]
        public bool UpdateTextOnDrag
        {
            get { return _UpdateTextOnDrag; }
            set { _UpdateTextOnDrag = value; }
        }
        /// <summary>
        /// 是否响应鼠标滚轮事件。
        /// </summary>
        [Category("SlideSwitch属性")]
        [Description("是否响应鼠标滚轮事件")]
        [DefaultValue(true)]
        public bool EnableMouseWheel
        {
            get { return _EnableMouseWheel; }
            set { _EnableMouseWheel = value; }
        }
        #endregion

        #region SlideSwitch动画
        /// <summary>
        /// 是否启用动画。
        /// </summary>
        [Category("SlideSwitch动画")]
        [Description("是否启用动画")]
        [DefaultValue(true)]
        public bool IsAnimationEnabled
        {
            get { return _IsAnimationEnabled; }
            set { _IsAnimationEnabled = value; }
        }
        /// <summary>
        /// 位移动画的配置，以 <see cref="Animation"/> 结构体表示。
        /// </summary>
        [Category("SlideSwitch动画")]
        [Description("位移动画的配置")]
        [DefaultValue(typeof(Animation), "200, 100, [0 1;0.67 1]")]
        public Animation TransAnim
        {
            get { return _TransAnim; }
            set { _TransAnim = value; }
        }
        /// <summary>
        /// 颜色动画的配置，以 <see cref="Animation"/> 结构体表示。
        /// </summary>
        [Category("SlideSwitch动画")]
        [Description("颜色动画的配置")]
        [DefaultValue(typeof(Animation), "150, 30, [0 0;1 1]")]
        public Animation ColorAnim
        {
            get { return _ColorAnim; }
            set { _ColorAnim = value; }
        }
        #endregion

        #region SlideSwitch事件
        /// <summary>
        /// 选项卡改变事件。
        /// </summary>
        /// <param name="index">索引。</param>
        /// <param name="text">索引处选项卡的文本。</param>
        public class IndexChangedEventArgs(int index, String text) : EventArgs
        {
            public int SelectIndex { get; } = index;
            public string SelectItem { get; } = text;
        }

        public event EventHandler<IndexChangedEventArgs>? SelectIndexChanged;
        /// <summary>
        /// 触发选项卡改变事件。
        /// </summary>
        /// <param name="e">选项卡改变事件。</param>
        protected virtual void OnSelectIndexChanged(IndexChangedEventArgs e)
        {
            SelectIndexChanged?.Invoke(this, e);
        }

        public event EventHandler<IndexChangedEventArgs>? SelectIndexChangedByCode;
        /// <summary>
        /// 非用户交互触发的选项卡改变事件。
        /// </summary>
        /// <param name="e">选项卡改变事件。</param>
        protected virtual void OnSelectIndexChangedByCode(IndexChangedEventArgs e)
        {
            SelectIndexChanged?.Invoke(this, e);
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            RefreshItemsRect();

            ItemsShow.Size = new Size(ItemSize.Width * Items.Length, ItemSize.Height);
            ItemsShow.Location = new Point(0, Math.Abs(SelectShow.Height - ItemSize.Height) / 2);
            SelectShow.Size = SelectSize;
            Size = new Size(Math.Max(SelectSize.Width * Items.Length, ItemSize.Width * Items.Length), Math.Max(SelectSize.Height, ItemSize.Height));

            base.OnPaint(e);
        }

        private readonly List<Rectangle> itemsRect = [];

        //刷新每个选项卡的矩形集合
        private void RefreshItemsRect()
        {
            itemsRect.Clear();
            for (int i = 0; i < Items.Length; i++)
            {
                //响应Padding
                int newWidth = (Width - Padding.Horizontal) / Items.Length;
                itemsRect.Add(new Rectangle(new Point(i * newWidth + Padding.Left, 0), new Size(newWidth, ItemSize.Height)));
            }
        }

        //绘制Items
        private void ItemsShow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            for (int i = 0; i < Items.Length; i++)
            {
                SizeF fontSize = g.MeasureString(Items[i], Font);

                g.DrawString(Items[i], Font, new SolidBrush(ItemsStyle.ForeColor), new Point(
                    (int)(itemsRect[i].X + (itemsRect[i].Width - fontSize.Width) / 2),
                    (int)((itemsRect[i].Height - fontSize.Height) / 2))
                    );
            }
        }

        //items单击
        private void ItemsShow_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int 单击的索引 = GetClickIndex(new Point(e.X, e.Y), itemsRect);

                if (单击的索引 != -1)
                {
                    //不触发OnSelectIndexChanged，防止进行两次过渡动画
                    _SelectIndex = 单击的索引;

                    RefreshSelectShowLocation();
                    OnSelectIndexChangedByCode(new IndexChangedEventArgs(单击的索引, Items[单击的索引]));
                }
            }
        }

        //获取单击的索引
        private static int GetClickIndex(Point 单击位置, List<Rectangle> 集合)
        {
            for (int i = 0; i < 集合.Count; i++)
            {
                if (单击位置.X > 集合[i].X && 单击位置.Y > 集合[i].Y && 单击位置.X < 集合[i].Right && 单击位置.Y < 集合[i].Bottom)
                {
                    return i;
                }
            }

            return -1;
        }

        //获取ChangeProperty属性的值
        private string GetChangePropertyValue()
        {
            return Enum.GetNames(typeof(StyleProperties))[(int)ChangeProperty];
        }

        //刷新选项卡位置（动画）
        private CancellationTokenSource cts = new();
        private void RefreshSelectShowLocation()
        {
            cts.Cancel();
            cts = new CancellationTokenSource();

            RefreshItemsRect();

            SelectShow.Text = Items[SelectIndex];

            Rectangle 单击的矩形 = itemsRect[SelectIndex];
            int centerX = 单击的矩形.X + 单击的矩形.Width / 2;
            int centerY = 单击的矩形.Y + 单击的矩形.Height / 2;
            int newX = centerX - SelectShow.Width / 2;
            int newY = centerY - SelectShow.Height / 2;

            Point targetPosition = new(newX + ItemsShow.Left, newY + ItemsShow.Top);
            Color targetColor = SelectIndex < ChangeColors.Length ? ChangeColors[SelectIndex] : Color.Black;

            if (IsAnimationEnabled)
            {
                //位移
                _ = SelectShow.BezierTransition("Location", null, targetPosition, TransAnim, default, true, cts.Token);

                //颜色
                if (ChangeProperty != StyleProperties.NoChange)
                {
                    _ = SelectShow.BezierTransition(GetChangePropertyValue(), null,
                        targetColor, ColorAnim, default, true, cts.Token);
                }
            }
            else
            {
                //位移
                SelectShow.Location = new Point(newX + ItemsShow.Left, newY + ItemsShow.Top);

                //颜色
                if (ChangeProperty != StyleProperties.NoChange)
                {
                    SelectShow.SetOrGetPropertyValue(GetChangePropertyValue(), targetColor);
                }
            }
        }

        //Padding属性更新时刷新选项卡位置
        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);

            RefreshSelectShowLocation();
        }

        #region 鼠标拖动
        private bool Draging = false;
        private Point MouseDownPoint;

        private void SelectShow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Draggable)
            {
                Draging = true;
                MouseDownPoint = e.Location;

                cts.Cancel();
            }
        }

        private void SelectShow_MouseMove(object sender, MouseEventArgs e)
        {
            if (Draging)
            {
                int offsetX = e.X - MouseDownPoint.X;
                int newX = SelectShow.Location.X + offsetX;

                //限制范围
                if (!AllowDragOutOfBounds)
                {
                    int leftendpoint = ItemsShow.Left;
                    int rightendpoint = ItemsShow.Right - SelectShow.Width;

                    if (newX < leftendpoint) newX = leftendpoint;
                    if (newX > rightendpoint) newX = rightendpoint;
                }

                SelectShow.Location = new Point(newX, SelectShow.Location.Y);

                //即时更新文本
                if (UpdateTextOnDrag)
                {
                    //计算距离最近的索引
                    int closestIndex = GetClosestIndex();

                    if (closestIndex != -1)
                    {
                        SelectShow.Text = Items[closestIndex];
                    }
                }
            }
        }

        private void SelectShow_MouseUp(object sender, MouseEventArgs e)
        {
            if (Draging)
            {
                Draging = false;

                //计算距离最近的索引
                int closestIndex = GetClosestIndex();

                if (closestIndex != -1 && closestIndex != SelectIndex)
                {
                    SelectIndex = closestIndex;
                }
                else
                {
                    //如果索引没有变化，则重置到最近的位置
                    RefreshSelectShowLocation();
                }
            }
        }

        //计算距离最近的索引
        private int GetClosestIndex()
        {
            int closestIndex = -1;
            int smallestDistance = int.MaxValue;
            for (int i = 0; i < itemsRect.Count; i++)
            {
                int centerX = itemsRect[i].X + itemsRect[i].Width / 2;
                int distance = Math.Abs(SelectShow.Location.X + SelectShow.Width / 2 - centerX);
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }
        #endregion

        //响应鼠标滚轮
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (EnableMouseWheel && !Draging)
            {
                switch (e.Delta)
                {
                    case > 100:
                        if (SelectIndex != Items.Length - 1)
                        {
                            SelectIndex += 1;
                        }
                        break;
                    case < -100:
                        if (SelectIndex != 0)
                        {
                            SelectIndex -= 1;
                        }
                        break;
                }
            }
        }
    }
}