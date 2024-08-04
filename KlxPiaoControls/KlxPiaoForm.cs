using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 一个美化的 WinForms 窗体控件，提供多种自定义风格和功能选项。
    /// </summary>
    /// <remarks><see cref="KlxPiaoForm"/> 继承自 <see cref="Form"/>，涵盖了原版 <see cref="Form"/> 的功能。</remarks>
    public partial class KlxPiaoForm : Form
    {
        #region Enums
        /// <summary>
        /// 表示横向两端的枚举。
        /// </summary>
        public enum HorizontalEnds
        {
            Left,
            Right
        }

        /// <summary>
        /// 表示标题按钮样式的枚举类型。
        /// </summary>
        public enum TitleButtonStyle
        {
            /// <summary>
            /// 全部显示。
            /// </summary>
            ShowAll,

            /// <summary>
            /// 关闭和最小化。
            /// </summary>
            CloseAndMinimize,

            /// <summary>
            /// 仅关闭。
            /// </summary>
            CloseOnly,

            /// <summary>
            /// 不显示。
            /// </summary>
            Hide
        }

        /// <summary>
        /// 表示窗体位置的枚举类型。
        /// </summary>
        public enum WindowPosition
        {
            /// <summary>
            /// 仅标题框。
            /// </summary>
            TitleBarOnly,

            /// <summary>
            /// 整个窗体。
            /// </summary>
            EntireWindow,

            /// <summary>
            /// 不启用。
            /// </summary>
            Disabled
        }

        /// <summary>
        /// 表示风格的枚举类型。
        /// </summary>
        public enum Style
        {
            /// <summary>
            /// Windows 风格。
            /// </summary>
            Windows,

            /// <summary>
            /// Mac 风格。
            /// </summary>
            Mac
        }

        /// <summary>
        /// 表示关闭按钮的功能的枚举类型。
        /// </summary>
        public enum CloseButtonAction
        {
            /// <summary>
            /// 关闭窗体。
            /// </summary>
            CloseWindow,

            /// <summary>
            /// 隐藏窗体。
            /// </summary>
            HideWindow,

            /// <summary>
            /// 退出应用程序。
            /// </summary>
            ExitApplication
        }

        /// <summary>
        /// 表示启动顺序的枚举。
        /// </summary>
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
        #endregion

        private Color _borderColor;
        private Style _theme;

        private int _titleBoxHeight;
        private Color _titleBoxBackColor;
        private Color _titleBoxForeColor;
        private Font _titleFont;
        private HorizontalAlignment _titleTextAlign;
        private int _titleTextMargin;
        private TitleButtonStyle _titleButtons;
        private int _titleButtonWidth;
        private HorizontalEnds _titleButtonAlign;
        private SizeF _titleButtonIconSize;
        private float _interactionColorScale;
        private bool _enableCloseButton;
        private bool _enableResizeButton;
        private bool _enableMinimizeButton;
        private Color _titleButtonDisabledColor;

        private WindowPosition _dragMode;
        private WindowPosition _shortcutResizeMode;
        private bool _enableStartupAnimation;
        private bool _enableCloseAnimation;
        private bool _enableResizeAnimation;
        private bool _autoHideWindowBorder;
        private bool _resizable;
        private CloseButtonAction _closeButtonFunction;
        private StartupSequence _startupOrder;

        private Color _inactiveTitleBoxBackColor;
        private Color _inactiveTitleBoxForeColor;
        private Color _inactiveBorderColor;

        private void InitializeTitleButton()
        {
            CloseButton.Name = "C";
            ResizeButton.Name = "R";
            MinimizeButton.Name = "M";

            CloseButton.Click += CloseButton_Click;
            ResizeButton.Click += ResizeButton_Click;
            MinimizeButton.Click += MinimizeButton_Click;

            CloseButton.Paint += TitleButtons_Paint;
            ResizeButton.Paint += TitleButtons_Paint;
            MinimizeButton.Paint += TitleButtons_Paint;

            CloseButton.MouseDown += TitleButton_MouseDown;
            ResizeButton.MouseDown += TitleButton_MouseDown;
            MinimizeButton.MouseDown += TitleButton_MouseDown;

            CloseButton.MouseMove += TitleButton_MouseMove;
            ResizeButton.MouseMove += TitleButton_MouseMove;
            MinimizeButton.MouseMove += TitleButton_MouseMove;

            CloseButton.MouseUp += TitleButton_MouseUp;
            ResizeButton.MouseUp += TitleButton_MouseUp;
            MinimizeButton.MouseUp += TitleButton_MouseUp;

            CloseButton.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular);
            ResizeButton.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular);
            MinimizeButton.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular);

            CloseButton.Top = 1;
            ResizeButton.Top = 1;
            MinimizeButton.Top = 1;

            CloseButton.FlatAppearance.BorderSize = 0;
            ResizeButton.FlatAppearance.BorderSize = 0;
            MinimizeButton.FlatAppearance.BorderSize = 0;

            CloseButton.IsReceiveFocus = false;
            ResizeButton.IsReceiveFocus = false;
            MinimizeButton.IsReceiveFocus = false;
        }

        #region Events
        /// <summary>
        /// 用户单击关闭按钮的事件。
        /// </summary>
        public event EventHandler<EventArgs>? CloseButtonClick;

        /// <summary>
        /// 引发 <see cref="CloseButtonClick"/> 事件。
        /// </summary>
        protected virtual void OnCloseButtonClick(EventArgs e)
        {
            CloseButtonClick?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        private void TitleButtons_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            PointF IconPosition = new((CloseButton.Width - TitleButtonIconSize.Width) / 2, (CloseButton.Height - TitleButtonIconSize.Height) / 2);
            RectangleF IconRectangle = new(IconPosition, TitleButtonIconSize);

            Pen pen = new(TitleBoxForeColor, 1);
            Pen disablePen = new(TitleButtonDisabledColor, 1);

            Pen p1 = EnableCloseButton ? pen : disablePen;
            Pen p2 = EnableResizeButton ? pen : disablePen;
            Pen p3 = EnableMinimizeButton ? pen : disablePen;

            if (sender is Control but)
            {
                switch (but.Name)
                {
                    case "C":
                        switch (Theme)
                        {
                            case Style.Windows:
                                g.SmoothingMode = SmoothingMode.HighQuality;
                                g.DrawLine(p1, IconRectangle.X, IconRectangle.Y, IconRectangle.Right - 1, IconRectangle.Bottom - 1);
                                g.DrawLine(p1, IconRectangle.Right - 1, IconRectangle.Y, IconRectangle.X, IconRectangle.Bottom - 1);
                                break;

                            case Style.Mac:
                                g.SmoothingMode = SmoothingMode.HighQuality;
                                g.FillEllipse(new SolidBrush(Color.Red), IconRectangle);
                                break;
                        }
                        break;

                    case "R":
                        switch (Theme)
                        {
                            case Style.Windows:
                                g.SmoothingMode = SmoothingMode.Default;
                                switch (WindowState)
                                {
                                    case FormWindowState.Normal:
                                        g.DrawRectangle(p2, new RectangleF(IconRectangle.X, IconRectangle.Y, IconRectangle.Width - 1, IconRectangle.Height - 1));
                                        break;

                                    case FormWindowState.Maximized:
                                        GraphicsPath path = new();
                                        path.AddLine(IconRectangle.X + IconRectangle.Width * 0.2F, IconRectangle.Y + IconRectangle.Width * 0.2F, IconRectangle.X, IconRectangle.Y + IconRectangle.Width * 0.2F);
                                        path.AddLine(IconRectangle.X, IconRectangle.Bottom - 1, IconRectangle.Right - IconRectangle.Width * 0.2F - 1, IconRectangle.Bottom - 1);
                                        path.AddLine(IconRectangle.Right - IconRectangle.Width * 0.2F - 1, IconRectangle.Y + IconRectangle.Width * 0.2F, IconRectangle.X + IconRectangle.Width * 0.2F, IconRectangle.Y + IconRectangle.Width * 0.2F);
                                        path.AddLine(IconRectangle.X + IconRectangle.Width * 0.2F, IconRectangle.Y, IconRectangle.Right - 1, IconRectangle.Y);
                                        path.AddLine(IconRectangle.Right - 1, IconRectangle.Bottom - IconRectangle.Width * 0.2F - 1, IconRectangle.Right - IconRectangle.Width * 0.2F, IconRectangle.Bottom - IconRectangle.Width * 0.2F - 1);
                                        g.DrawPath(p2, path);
                                        break;
                                }
                                break;

                            case Style.Mac:
                                g.SmoothingMode = SmoothingMode.HighQuality;
                                g.FillEllipse(new SolidBrush(Color.Orange), IconRectangle);
                                break;
                        }
                        break;

                    case "M":
                        switch (Theme)
                        {
                            case Style.Windows:
                                g.SmoothingMode = SmoothingMode.Default;
                                g.DrawLine(p3, IconRectangle.X, IconPosition.Y + IconRectangle.Height / 2 - 1, IconRectangle.Right - 1, IconRectangle.Y + IconRectangle.Height / 2 - 1);
                                break;

                            case Style.Mac:
                                g.SmoothingMode = SmoothingMode.HighQuality;
                                g.FillEllipse(new SolidBrush(Color.DarkGray), IconRectangle);
                                break;
                        }
                        break;
                }
            }
        }

        public KlxPiaoForm()
        {
            InitializeComponent();
            InitializeTitleButton();

            _borderColor = SystemColors.WindowFrame;
            _theme = Style.Windows;

            _titleBoxHeight = 31;
            _titleBoxBackColor = Color.FromArgb(224, 224, 224);
            _titleBoxForeColor = Color.Black;
            _titleFont = new Font("Microsoft YaHei UI", 9);
            _titleTextAlign = HorizontalAlignment.Left;
            _titleTextMargin = 11;
            _titleButtons = TitleButtonStyle.ShowAll;
            _titleButtonWidth = 40;
            _titleButtonAlign = HorizontalEnds.Right;
            _titleButtonIconSize = new SizeF(10F, 10F);
            _interactionColorScale = -0.04F;
            _enableCloseButton = true;
            _enableResizeButton = true;
            _enableMinimizeButton = true;
            _titleButtonDisabledColor = Color.DarkGray;

            _dragMode = WindowPosition.TitleBarOnly;
            _shortcutResizeMode = WindowPosition.TitleBarOnly;
            _enableStartupAnimation = true;
            _enableCloseAnimation = true;
            _enableResizeAnimation = true;
            _autoHideWindowBorder = true;
            _resizable = true;
            _closeButtonFunction = CloseButtonAction.CloseWindow;
            _startupOrder = StartupSequence.WaitOnLoadThenAnimate;

            _inactiveTitleBoxBackColor = Color.Transparent;
            _inactiveTitleBoxForeColor = SystemColors.WindowFrame;
            _inactiveBorderColor = Color.Silver;

            BackColor = Color.White;
            Text = Name;
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(700, 450);
            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
        }

        #region KlxPiaoForm外观
        /// <summary>
        /// 获取或设置边框的颜色。
        /// </summary>
        [Category("KlxPiaoForm外观")]
        [Description("窗体边框的颜色")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置主题风格。
        /// </summary>
        [Category("KlxPiaoForm外观")]
        [Description("窗体的视觉风格")]
        public Style Theme
        {
            get { return _theme; }
            set { _theme = value; Refresh(); }
        }
        #endregion

        #region KlxPiaoForm标题框
        /// <summary>
        /// 获取或设置标题框的高度。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("标题框的高度")]
        public int TitleBoxHeight
        {
            get { return _titleBoxHeight; }
            set { _titleBoxHeight = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置标题框的背景色。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("标题框的背景色")]
        public Color TitleBoxBackColor
        {
            get { return _titleBoxBackColor; }
            set { _titleBoxBackColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置标题框的前景色。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("标题框的前景色")]
        public Color TitleBoxForeColor
        {
            get { return _titleBoxForeColor; }
            set { _titleBoxForeColor = value; Refresh(); }
        }
        /// <summary>
        /// 获取或设置标题的字体。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("标题的字体")]
        public Font TitleFont
        {
            get { return _titleFont; }
            set { _titleFont = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置标题文字的位置。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("标题文字位于标题框的位置")]
        public HorizontalAlignment TitleTextAlign
        {
            get { return _titleTextAlign; }
            set { _titleTextAlign = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置标题文字的边距。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("标题文字位于标题框的左右边距")]
        public int TitleTextMargin
        {
            get { return _titleTextMargin; }
            set { _titleTextMargin = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置标题按钮的呈现形式。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("显示在标题框的按钮")]
        public TitleButtonStyle TitleButtons
        {
            get { return _titleButtons; }
            set { _titleButtons = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置标题按钮的宽度。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("标题按钮的宽度")]
        public int TitleButtonWidth
        {
            get { return _titleButtonWidth; }
            set { _titleButtonWidth = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置标题按钮的位置。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("标题按钮的位置")]
        public HorizontalEnds TitleButtonAlign
        {
            get { return _titleButtonAlign; }
            set { _titleButtonAlign = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置标题按钮的图标大小。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("标题按钮图标绘制大小")]
        public SizeF TitleButtonIconSize
        {
            get { return _titleButtonIconSize; }
            set { _titleButtonIconSize = value; Refresh(); }
        }
        /// <summary>
        /// 获取或设置标题按钮鼠标交互时颜色改变的大小。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("决定标题按钮移入和按下的背景色，范围 -1.00 到 +1.00")]
        public float InteractionColorScale
        {
            get { return _interactionColorScale; }
            set { _interactionColorScale = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否启用关闭按钮。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("是否启用关闭按钮")]
        public bool EnableCloseButton
        {
            get { return _enableCloseButton; }
            set { _enableCloseButton = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否启用缩放按钮。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("是否启用缩放按钮")]
        public bool EnableResizeButton
        {
            get { return _enableResizeButton; }
            set { _enableResizeButton = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否启用最小化按钮。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("是否启用最小化按钮")]
        public bool EnableMinimizeButton
        {
            get { return _enableMinimizeButton; }
            set { _enableMinimizeButton = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置标题按钮未启用时的前景色。
        /// </summary>
        [Category("KlxPiaoForm标题框")]
        [Description("标题按钮未启用时的前景色")]
        public Color TitleButtonDisabledColor
        {
            get { return _titleButtonDisabledColor; }
            set { _titleButtonDisabledColor = value; Refresh(); }
        }
        #endregion

        #region KlxPiaoForm特性
        /// <summary>
        /// 获取或设置窗体拖动的方式。
        /// </summary>
        [Category("KlxPiaoForm特性")]
        [Description("窗体拖动的方式")]
        public WindowPosition DragMode
        {
            get { return _dragMode; }
            set { _dragMode = value; }
        }
        /// <summary>
        /// 获取或设置最大化或还原的快捷方式。
        /// </summary>
        [Category("KlxPiaoForm特性")]
        [Description("最大化或还原的快捷方式，仅缩放按钮显示并启用时生效")]
        public WindowPosition ShortcutResizeMode
        {
            get { return _shortcutResizeMode; }
            set { _shortcutResizeMode = value; }
        }
        /// <summary>
        /// 获取或设置是否启用启动动画。
        /// </summary>
        [Category("KlxPiaoForm特性")]
        [Description("是否启用启动动画")]
        public bool EnableStartupAnimation
        {
            get { return _enableStartupAnimation; }
            set { _enableStartupAnimation = value; }
        }
        /// <summary>
        /// 获取或设置是否启用关闭动画。
        /// </summary>
        [Category("KlxPiaoForm特性")]
        [Description("是否启用关闭动画")]
        public bool EnableCloseAnimation
        {
            get { return _enableCloseAnimation; }
            set { _enableCloseAnimation = value; }
        }
        /// <summary>
        /// 获取或设置是否启用 最大化/还原 动画。
        /// </summary>
        [Category("KlxPiaoForm特性")]
        [Description("是否启用最大化或还原的动画")]
        public bool EnableResizeAnimation
        {
            get { return _enableResizeAnimation; }
            set { _enableResizeAnimation = value; }
        }
        /// <summary>
        /// 获取或设置是否在全屏时自动隐藏窗体边框。
        /// </summary>
        [Category("KlxPiaoForm特性")]
        [Description("设置为True后，全屏时自动隐藏窗体边框")]
        public bool AutoHideWindowBorder
        {
            get { return _autoHideWindowBorder; }
            set { _autoHideWindowBorder = value; }
        }
        /// <summary>
        /// 获取或设置是否可调整大小。
        /// </summary>
        [Category("KlxPiaoForm特性")]
        [Description("窗体是否可调整大小")]
        public bool Resizable
        {
            get { return _resizable; }
            set { _resizable = value; }
        }
        /// <summary>
        /// 获取或设置关闭按钮的功能。
        /// </summary>
        [Category("KlxPiaoForm特性")]
        [Description("用户单击关闭按钮时执行的操作")]
        public CloseButtonAction CloseButtonFunction
        {
            get { return _closeButtonFunction; }
            set { _closeButtonFunction = value; }
        }
        /// <summary>
        /// 获取或设置启动动画和 OnLoad 事件的顺序，以 <see cref="StartupSequence"/> 枚举类型表示。
        /// </summary>
        [Category("KlxPiaoForm特性")]
        [Description("启动动画和Load事件的顺序")]
        public StartupSequence StartupOrder
        {
            get { return _startupOrder; }
            set { _startupOrder = value; Invalidate(); }
        }
        #endregion

        #region KlxPiaoForm焦点
        /// <summary>
        /// 获取或设置窗体未激活时标题框的背景色。
        /// </summary>
        [Category("KlxPiaoForm焦点")]
        [Description("窗体未激活时标题框的背景色，Transparent：未激活时不改变背景色")]
        public Color InactiveTitleBoxBackColor
        {
            get { return _inactiveTitleBoxBackColor; }
            set { _inactiveTitleBoxBackColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置窗体未激活时标题框的前景色。
        /// </summary>
        [Category("KlxPiaoForm焦点")]
        [Description("窗体未激活时标题文字前景色和窗体按钮的前景色，Transparent：未激活时不改变前景色")]
        public Color InactiveTitleBoxForeColor
        {
            get { return _inactiveTitleBoxForeColor; }
            set { _inactiveTitleBoxForeColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置窗体未激活时的边框颜色。
        /// </summary>
        [Category("KlxPiaoForm焦点")]
        [Description("窗体未激活时边框的颜色，Transparent：未激活时不改变边框颜色")]
        public Color InactiveBorderColor
        {
            get { return _inactiveBorderColor; }
            set { _inactiveBorderColor = value; Invalidate(); }
        }
        #endregion

        [Browsable(false)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = value; }
        }

        private readonly KlxPiaoButton CloseButton = new();
        private readonly KlxPiaoButton ResizeButton = new();
        private readonly KlxPiaoButton MinimizeButton = new();

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            Rectangle thisRect = new(0, 0, Width, Height);

            g.Clear(BackColor);

            //绘制标题框
            using (Brush brush = new SolidBrush(TitleBoxBackColor))
            {
                g.FillRectangle(brush, new Rectangle(thisRect.X, thisRect.Y, thisRect.Width, TitleBoxHeight));
            }

            //绘制边框
            if (!(AutoHideWindowBorder && WindowState == FormWindowState.Maximized))
            {
                using Pen borderPen = new(BorderColor, 1);
                g.DrawRectangle(borderPen, new Rectangle(thisRect.X, thisRect.Y, thisRect.Width - 1, thisRect.Height - 1));
            }

            //更新标题按钮属性
            UpdateTitleButtonProperty([CloseButton, ResizeButton, MinimizeButton]);
            switch (TitleButtonAlign)
            {
                case HorizontalEnds.Right:
                    CloseButton.Left = Width - TitleButtonWidth - 1;
                    ResizeButton.Left = Width - TitleButtonWidth * 2 - 1;
                    break;
                case HorizontalEnds.Left:
                    CloseButton.Left = 1;
                    ResizeButton.Left = TitleButtonWidth + 1;
                    break;
            }

            //更新标题按钮显示
            switch (TitleButtons)
            {
                case TitleButtonStyle.ShowAll:
                    MinimizeButton.Left = TitleButtonAlign switch
                    {
                        HorizontalEnds.Right => Width - TitleButtonWidth * 3 - 1,
                        HorizontalEnds.Left => MinimizeButton.Left = TitleButtonWidth * 2 + 1,
                        _ => 0
                    };
                    Controls.Add(CloseButton);
                    Controls.Add(ResizeButton);
                    Controls.Add(MinimizeButton);
                    break;

                case TitleButtonStyle.CloseAndMinimize:
                    MinimizeButton.Left = TitleButtonAlign switch
                    {
                        HorizontalEnds.Right => Width - TitleButtonWidth * 2 - 1,
                        HorizontalEnds.Left => TitleButtonWidth + 1,
                        _ => 0
                    };
                    Controls.Add(CloseButton);
                    Controls.Remove(ResizeButton);
                    Controls.Add(MinimizeButton);
                    break;

                case TitleButtonStyle.CloseOnly:
                    MinimizeButton.Left = TitleButtonAlign switch
                    {
                        HorizontalEnds.Right => MinimizeButton.Left = Width - TitleButtonWidth - 1,
                        HorizontalEnds.Left => MinimizeButton.Left = 1,
                        _ => 0
                    };
                    Controls.Add(CloseButton);
                    Controls.Remove(ResizeButton);
                    Controls.Remove(MinimizeButton);
                    break;

                case TitleButtonStyle.Hide:
                    Controls.Remove(CloseButton);
                    Controls.Remove(ResizeButton);
                    Controls.Remove(MinimizeButton);
                    break;
            }

            //绘制标题文字
            using (Brush brush = new SolidBrush(TitleBoxForeColor))
            {
                SizeF TextSize = g.MeasureString(Text, TitleFont);
                int x = 0;

                switch (TitleTextAlign)
                {
                    case HorizontalAlignment.Left:
                        x = TitleButtonAlign == HorizontalEnds.Left
                            ? TitleButtons switch
                            {
                                TitleButtonStyle.Hide => TitleTextMargin,
                                TitleButtonStyle.CloseOnly => TitleTextMargin + TitleButtonWidth,
                                TitleButtonStyle.CloseAndMinimize => TitleTextMargin + TitleButtonWidth * 2,
                                TitleButtonStyle.ShowAll => TitleTextMargin + TitleButtonWidth * 3,
                                _ => 0
                            }
                            : TitleTextMargin;
                        break;

                    case HorizontalAlignment.Center:
                        x = (int)((Width - TextSize.Width) / 2);
                        break;

                    case HorizontalAlignment.Right:
                        x = TitleButtonAlign == HorizontalEnds.Right
                            ? TitleButtons switch
                            {
                                TitleButtonStyle.Hide => (int)(Width - TextSize.Width - TitleTextMargin),
                                TitleButtonStyle.CloseOnly => (int)(Width - TextSize.Width - TitleTextMargin - TitleButtonWidth),
                                TitleButtonStyle.CloseAndMinimize => (int)(Width - TextSize.Width - TitleTextMargin - TitleButtonWidth * 2),
                                TitleButtonStyle.ShowAll => (int)(Width - TextSize.Width - TitleTextMargin - TitleButtonWidth * 3),
                                _ => 0
                            }
                            : (int)(Width - TextSize.Width - TitleTextMargin);
                        break;
                }

                int y = (int)((TitleBoxHeight - TextSize.Height) / 2);

                //根据图标位置进行偏移
                int drawX = (Icon != null && ShowIcon && TitleTextAlign == HorizontalAlignment.Left)
                    ? x + (TitleBoxHeight - Icon.ToBitmap().Height) + 1
                    : x;

                g.DrawString(Text, TitleFont, brush, new Point(drawX, y));
            }

            //绘制图标
            if (Icon != null && ShowIcon)
            {
                using Bitmap icon = Icon.ToBitmap();
                g.DrawImage(icon, new Point((TitleBoxHeight - icon.Height) / 2 + 1, (TitleBoxHeight - icon.Height) / 2));
            }

            base.OnPaint(pe);
        }

        private void UpdateTitleButtonProperty(KlxPiaoButton[] buttons)
        {
            foreach (KlxPiaoButton b in buttons)
            {
                b.Size = new Size(TitleButtonWidth, TitleBoxHeight - 1);
                b.BackColor = TitleBoxBackColor;
                b.FlatAppearance.MouseOverBackColor = ColorProcessor.AdjustBrightness(TitleBoxBackColor, InteractionColorScale);
                b.FlatAppearance.MouseDownBackColor = ColorProcessor.AdjustBrightness(b.FlatAppearance.MouseOverBackColor, InteractionColorScale);

                CloseButton.Enabled = EnableCloseButton;
                ResizeButton.Enabled = EnableResizeButton;
                MinimizeButton.Enabled = EnableMinimizeButton;
            }
        }

        //关闭
        bool isClosing = false;
        /// <summary>
        /// 关闭窗体（带有动画）。
        /// </summary>
        public void CloseForm()
        {
            if (!isClosing)
            {
                isClosing = true;

                if (EnableCloseAnimation)
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
                    if (CloseButtonFunction != CloseButtonAction.HideWindow) Controls.Clear();
                    Controls.Add(picbox);

                    Size oldSize = Size;
                    Point oldPosition = Location;
                    float Length = 7F;

                    var fadeOutTask = Task.Run(async () =>
                    {
                        for (float i = 1; i >= 0; i -= (1 / Length))
                        {
                            Invoke(() => { Opacity = i; });
                            await Task.Delay(10);
                        }
                    });

                    var shrinkTask = Task.Run(async () =>
                    {
                        for (int i = 0; i <= Length; i++)
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

                    Action action = CloseButtonFunction switch
                    {
                        CloseButtonAction.CloseWindow => new Action(Close),
                        CloseButtonAction.HideWindow => new Action(() =>
                        {
                            Hide();
                            Controls.Remove(picbox);
                            Opacity = 1;
                            Size = oldSize;
                            Location = oldPosition;
                            isClosing = false;
                        }),
                        CloseButtonAction.ExitApplication => new Action(Application.Exit),
                        _ => new Action(() => { }),
                    };

                    Task.WhenAll(fadeOutTask, shrinkTask).ContinueWith(t => Invoke(action));
                }
                else
                {
                    Action action = CloseButtonFunction switch
                    {
                        CloseButtonAction.CloseWindow => new Action(Close),
                        CloseButtonAction.HideWindow => new Action(Hide),
                        CloseButtonAction.ExitApplication => new Action(Application.Exit),
                        _ => new Action(() => { }),
                    };

                    action();
                }
            }
        }

        #region 标题按钮事件
        private void CloseButton_Click(object? sender, EventArgs e)
        {
            OnCloseButtonClick(EventArgs.Empty);
            CloseForm();
        }

        private void ResizeButton_Click(object? sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            switch (WindowState)
            {
                case FormWindowState.Normal:
                    if (EnableResizeAnimation)
                    {
                        Opacity = 0;
                        Task.Run(async () =>
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
                    if (EnableResizeAnimation)
                    {
                        Opacity = 0;
                        Task.Run(async () =>
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

        private void MinimizeButton_Click(object? sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        #endregion

        //启动
        protected override void OnLoad(EventArgs e)
        {
            if (!EnableStartupAnimation)
            {
                base.OnLoad(e);
                return;
            }

            switch (StartupOrder)
            {
                case StartupSequence.WaitOnLoadThenAnimate:
                    base.OnLoad(e);
                    Animation();
                    break;

                case StartupSequence.AnimateThenOnLoad:
                    Animation();
                    base.OnLoad(e);
                    break;
            }

            void Animation()
            {
                //临时转变为 FixedDialog，使无边框窗体的启动动画生效
                FormBorderStyle = FormBorderStyle.FixedDialog;
                FormBorderStyle = FormBorderStyle.None;

                //防止启动闪烁
                Refresh();
            }
        }

        #region 关闭事件
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
        #endregion

        #region 外观改变时重绘
        protected override void OnTextChanged(EventArgs e)
        {
            Refresh();

            base.OnTextChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Refresh();

            base.OnSizeChanged(e);
        }
        #endregion

        //快捷缩放放手机（双击）
        protected override void OnDoubleClick(EventArgs e)
        {
            if (TitleButtons == TitleButtonStyle.ShowAll && EnableResizeButton)
            {
                if (ShortcutResizeMode == WindowPosition.EntireWindow)
                {
                    ResizeButton_Click(ResizeButton, e);
                }
                else if (ShortcutResizeMode == WindowPosition.TitleBarOnly && 按下位置.Y <= TitleBoxHeight)
                {
                    ResizeButton_Click(ResizeButton, e);
                }
            }

            base.OnDoubleClick(e);
        }

        #region 窗体拖动和调整大小
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
            if (Resizable && WindowState != FormWindowState.Maximized)
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
                if (DragMode == WindowPosition.EntireWindow || (DragMode == WindowPosition.TitleBarOnly && 按下位置.Y <= TitleBoxHeight))
                {
                    Location = new Point(Location.X + e.X - 按下位置.X, Location.Y + e.Y - 按下位置.Y);
                }
            }

            //适应光标
            if (Resizable && WindowState != FormWindowState.Maximized)
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
        private void TitleButton_MouseDown(object? sender, MouseEventArgs e)
        {
            //传递正在调整的位置
            if (Resizable && WindowState != FormWindowState.Maximized)
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

        private void TitleButton_MouseMove(object? sender, MouseEventArgs e)
        {
            //适应光标
            if (Resizable && WindowState != FormWindowState.Maximized)
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

        private void TitleButton_MouseUp(object? sender, MouseEventArgs e)
        {
            按下位置 = Point.Empty;
            正在调整位置 = 调整位置.调完了;
        }
        #endregion

        #region 焦点反馈
        private Color oldBackColor = Color.Empty;
        private Color oldForeColor = Color.Empty;
        private Color oldBorderColor = Color.Empty;
        protected override void OnActivated(EventArgs e)
        {
            if (oldBackColor == Color.Empty)
            {
                oldBackColor = TitleBoxBackColor;
            }
            if (oldForeColor == Color.Empty)
            {
                oldForeColor = TitleBoxForeColor;
            }
            if (oldBorderColor == Color.Empty)
            {
                oldBorderColor = BorderColor;
            }
            TitleBoxBackColor = oldBackColor;
            TitleBoxForeColor = oldForeColor;
            BorderColor = oldBorderColor;

            Refresh();

            base.OnActivated(e);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            oldBackColor = TitleBoxBackColor;
            oldForeColor = TitleBoxForeColor;
            oldBorderColor = BorderColor;

            if (InactiveTitleBoxBackColor != Color.Transparent)
            {
                TitleBoxBackColor = InactiveTitleBoxBackColor;
            }
            if (InactiveTitleBoxForeColor != Color.Transparent)
            {
                TitleBoxForeColor = InactiveTitleBoxForeColor;
            }
            if (InactiveBorderColor != Color.Transparent)
            {
                BorderColor = InactiveBorderColor;
            }

            Refresh();

            base.OnDeactivate(e);
        }
        #endregion

        /// <summary>
        /// 获取工作区的大小。
        /// </summary>
        /// <returns>用户区域的大小。</returns>
        public Size GetClientSize()
        {
            return new Size(Width - 2, Height - TitleBoxHeight - 1);
        }

        /// <summary>
        /// 获取工作区的矩形。
        /// </summary>
        /// <returns>用户区域的矩形。</returns>
        public Rectangle GetClientRectangle()
        {
            return new Rectangle(new Point(1, TitleBoxHeight), GetClientSize());
        }

        /// <summary>
        /// 将指定的字体应用与窗体的每个控件，不改变字体大小（可选）
        /// </summary>
        /// <param name="fontFamily"></param>
        /// <param name="fontSizeCorrection">要应用到每个控件的字体大小的偏移量，默认为0</param>
        public void SetGlobalFont(FontFamily fontFamily, float fontSizeCorrection = 0)
        {
            this.ForEachControl<Control>(control =>
            {
                control.Font = new Font(fontFamily, control.Font.Size + fontSizeCorrection, control.Font.Style);
            }, true);

            TitleFont = new Font(fontFamily, TitleFont.Size, TitleFont.Style);
        }

        /// <summary>
        /// 指定一个主题色，应用全局主题。
        /// </summary>
        /// <param name="themeColor">主题色。</param>
        /// <param name="IsApplyToControls">是否应用于控件。</param>
        public void SetGlobalTheme(Color? themeColor = null, bool IsApplyToControls = true)
        {
            themeColor = themeColor == null ? TitleBoxBackColor : themeColor;

            TitleBoxBackColor = (Color)themeColor;
            TitleBoxForeColor = ColorProcessor.GetBrightness((Color)themeColor) > 127 ? Color.Black : Color.White;

            if (IsApplyToControls)
            {
                Color butBorderColor = ColorProcessor.SetBrightness((Color)themeColor, 210);
                Color mouseOverColor = ColorProcessor.SetBrightness(butBorderColor, 250);
                Color mouseDownColor = ColorProcessor.SetBrightness(butBorderColor, 240);

                this.ForEachControl<RoundedButton>(but =>
                {
                    if (but.InteractionStyle.OverForeColor != Color.Empty) //移入改变明暗主题
                    {
                        but.InteractionStyle.OverBorderColor = butBorderColor;
                        but.InteractionStyle.DownBackColor = mouseDownColor;
                    }
                    else if (but.InteractionStyle.DownForeColor != Color.Empty) //按下改变明暗主题
                    {
                        but.InteractionStyle.DownBorderColor = butBorderColor;
                    }
                    else if (but.InteractionStyle.OverForeColor == Color.Empty && but.InteractionStyle.DownForeColor == Color.Empty) //不改变明暗主题
                    {
                        but.InteractionStyle.OverBackColor = mouseOverColor;
                        but.InteractionStyle.DownBackColor = mouseDownColor;
                        but.BorderColor = butBorderColor;
                    }
                }, true);

                this.ForEachControl<KlxPiaoButton>(but =>
                {
                    but.FlatAppearance.BorderColor = butBorderColor;
                    but.FlatAppearance.MouseOverBackColor = mouseOverColor;
                    but.FlatAppearance.MouseDownBackColor = mouseDownColor;
                }, true);

                this.ForEachControl<KlxPiaoPanel>(panel =>
                {
                    if (!panel.IsEnableShadow)
                    {
                        panel.BorderColor = butBorderColor;
                    }
                }, true);
            }
        }
    }
}