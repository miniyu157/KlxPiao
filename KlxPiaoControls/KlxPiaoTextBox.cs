using KlxPiaoAPI;
using System.ComponentModel;

namespace KlxPiaoControls
{
    /// <summary>
    /// 带有圆角或投影的文本框控件。
    /// </summary>
    /// <remarks>
    /// <see cref="KlxPiaoTextBox"/> 继承自 <see cref="KlxPiaoPanel"/>，是通过嵌入的 <see cref="System.Windows.Forms.TextBox"/> 实现的。
    /// </remarks>
    [DefaultEvent("TextChanged")]
    public partial class KlxPiaoTextBox : KlxPiaoPanel
    {
        private readonly TextBox baseTextBox = new();

        private ContentAlignment _TextBoxAlign;
        private Point _TextBoxOffset;
        private bool _IsFillAndMultiline;

        public KlxPiaoTextBox()
        {
            InitializeComponent();

            //初始化 KlxPiaoTextBox 的属性
            TextBoxAlign = ContentAlignment.MiddleCenter; //触发 RefreshControlRect()
            _TextBoxOffset = Point.Empty;
            _IsFillAndMultiline = false;
            //
            // KlxPiaoPanel（继承自）
            //
            启用投影 = false;
            边框颜色 = Color.Gray;
            圆角大小 = new CornerRadius(35);
            BackColor = Color.White;
            Size = new Size(144, 26);
            Cursor = Cursors.IBeam;
            //
            // baseTextBox
            //
            baseTextBox.BorderStyle = BorderStyle.None;
            baseTextBox.Location = new Point(0, 0);
            baseTextBox.Size = Size;
            baseTextBox.BackColor = Color.White;
            baseTextBox.TextAlignChanged += BaseTextBox_TextAlignChanged;
            baseTextBox.MultilineChanged += BaseTextBox_MultilineChanged;
            baseTextBox.TextChanged += BaseTextBox_TextChanged;
            //
            // KlxPiaoTextBox
            //
            Controls.Add(baseTextBox);

            RefreshControlRect();
        }

        #region Properties
        /// <summary>
        /// 获取或设置文本框的外观。这是通过 <see cref="System.Windows.Forms.TextBox"/> 实现的。
        /// </summary>
        [Category("TextBox")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("文本框外观")]
        public TextBox TextBox => baseTextBox;

        [Category("TextBox")]
        [Browsable(true)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new string Text
        {
            get => baseTextBox.Text;
            set
            {
                //同步文本
                base.Text = value;
                baseTextBox.Text = value;

            }
        }

        /// <summary>
        /// 获取或设置嵌入文本框的位置。
        /// </summary>
        [Category("TextBox")]
        [Description("嵌入文本框的位置")]
        [DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment TextBoxAlign
        {
            get => _TextBoxAlign;
            set
            {
                //同步Align
                baseTextBox.TextAlign = LayoutUtilities.GetHorizontalAlignment(value);

                _TextBoxAlign = value;
                RefreshControlRect();
            }
        }

        /// <summary>
        /// 获取或设置嵌入文本框位置的偏移。
        /// </summary>
        [Category("TextBox")]
        [Description("嵌入文本框的位置的偏移")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point TextBoxOffset
        {
            get => _TextBoxOffset;
            set
            {
                _TextBoxOffset = value;
                RefreshControlRect();
            }
        }

        /// <summary>
        /// 是否开启多行并使嵌入的文本框填充组件。
        /// </summary>
        [Category("TextBox")]
        [Description("是否开启多行并使嵌入文本框填充")]
        [DefaultValue(false)]
        public bool IsFillAndMultiline
        {
            get => _IsFillAndMultiline;
            set
            {
                baseTextBox.Multiline = value;
                _IsFillAndMultiline = value;
                RefreshControlRect();
            }
        }

        [DefaultValue(typeof(Size), "144,26")]
        public new Size Size
        {
            get => base.Size;
            set => base.Size = value;
        }

        #region 同步的属性
        public new Color BackColor
        {
            get => baseTextBox.BackColor;
            set
            {
                base.BackColor = value;
                baseTextBox.BackColor = value;
            }
        }

        [Browsable(true)]
        public new Color ForeColor
        {
            get => baseTextBox.ForeColor;
            set
            {
                base.ForeColor = value;
                baseTextBox.ForeColor = value;
            }
        }
        #endregion

        #region 会触发外观刷新或覆盖默认值的属性
        public new int 边框大小
        {
            get => base.边框大小;
            set
            {
                if (base.边框大小 != value)
                {
                    base.边框大小 = value;
                    RefreshControlRect();
                }
            }
        }

        [DefaultValue(typeof(Color), "Gray")]
        public new Color 边框颜色
        {
            get => base.边框颜色;
            set
            {
                if (base.边框颜色 != value)
                {
                    base.边框颜色 = value;
                }
            }
        }

        [DefaultValue(typeof(CornerRadius), "13,13,13,13")]
        public new CornerRadius 圆角大小
        {
            get => base.圆角大小;
            set
            {
                if (base.圆角大小 != value)
                {
                    base.圆角大小 = value;
                    RefreshControlRect();
                }
            }
        }

        [DefaultValue(false)]
        public new bool 启用投影
        {
            get => base.启用投影;
            set
            {
                if (base.启用投影 != value)
                {
                    base.启用投影 = value;
                    RefreshControlRect();
                }
            }
        }

        public new 方向 投影方向
        {
            get => base.投影方向;
            set
            {
                if (base.投影方向 != value)
                {
                    base.投影方向 = value;
                    RefreshControlRect();
                }
            }
        }
        #endregion
        #endregion

        private void BaseTextBox_TextAlignChanged(object? sender, EventArgs e)
        {
            TextBoxAlign = LayoutUtilities.AdjustContentAlignment(baseTextBox.TextAlign, TextBoxAlign);
        }

        private void BaseTextBox_MultilineChanged(object? sender, EventArgs e)
        {
            _IsFillAndMultiline = baseTextBox.Multiline;
        }

        private void BaseTextBox_TextChanged(object? sender, EventArgs e)
        {
            OnTextChanged(EventArgs.Empty);
        }

        private void RefreshControlRect()
        {
            Rectangle thisRect = new(0, 0, Width, Height);
            Rectangle baseTextBoxRect = 启用投影
                ? 获取工作区矩形().ScaleRectangle(-2)
                : thisRect.ScaleRectangle(-边框大小 * 2).GetInnerFitRectangle(圆角大小);

            if (IsFillAndMultiline)
            {
                baseTextBox.Size = baseTextBoxRect.Size -= new Size(TextBoxOffset.X, TextBoxOffset.Y);
                baseTextBox.Location = baseTextBoxRect.Location += new Size(TextBoxOffset.X, TextBoxOffset.Y);
            }
            else
            {
                baseTextBox.Width = baseTextBoxRect.Width - TextBoxOffset.X;
                baseTextBox.Location = LayoutUtilities.CalculateAlignedPosition(baseTextBoxRect, baseTextBox.Size, TextBoxAlign, TextBoxOffset);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            RefreshControlRect();

            base.OnSizeChanged(e);
        }

        #region 键盘选中
        bool isSelect = false;
        int oldPos;
        protected override void OnDoubleClick(EventArgs e)
        {
            baseTextBox.SelectAll();

            base.OnDoubleClick(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                baseTextBox.Focus();

                oldPos = baseTextBox.GetCharIndexFromPosition(new Point(baseTextBox.Left + e.X, 0));
                baseTextBox.Select(oldPos, 0);
                isSelect = true;
            }

            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isSelect)
            {
                int newPos = baseTextBox.GetCharIndexFromPosition(new Point(baseTextBox.Left + e.X, 0));
                baseTextBox.Select(Math.Min(oldPos, newPos), Math.Abs(oldPos - newPos) + 1); //防止选中不到最后一个字符
            }

            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            isSelect = false;
            oldPos = 0;
            base.OnMouseUp(e);
        }
        #endregion
    }
}