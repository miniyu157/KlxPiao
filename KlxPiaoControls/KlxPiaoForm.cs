﻿using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace KlxPiaoControls
{
    /// <summary>
    /// 一个美化的 WinForms 窗体控件，提供多种自定义风格和功能选项。
    /// </summary>
    /// <remarks><see cref="KlxPiaoForm"/> 继承自 <see cref="Form"/>，是原版 <see cref="Form"/> 的增强版本。</remarks>
    public partial class KlxPiaoForm : Form
    {
        #region rounded and shadow
        [LibraryImport("dwmapi.dll")]
        public static partial int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [LibraryImport("dwmapi.dll")]
        public static partial int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [LibraryImport("dwmapi.dll")]
        public static partial int DwmIsCompositionEnabled(ref int pfEnabled);

        private bool m_aeroEnabled;                     // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        private static bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1);
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            if (EnableShadow)
            {
                switch (m.Msg)
                {
                    case WM_NCPAINT:                        // box shadow
                        if (m_aeroEnabled)
                        {
                            var v = 2;
                            DwmSetWindowAttribute(Handle, 2, ref v, 4);
                            MARGINS margins = new()
                            {
                                bottomHeight = 1,
                                leftWidth = 1,
                                rightWidth = 1,
                                topHeight = 1
                            };
                            DwmExtendFrameIntoClientArea(Handle, ref margins);
                        }
                        break;

                    default:
                        break;
                }
            }
            base.WndProc(ref m);
        }
        #endregion

        #region enums
        /// <summary>
        /// 定义刷新方式的枚举。
        /// </summary>
        public enum RefreshMode
        {
            /// <summary>
            /// 仅刷新标题栏。
            /// </summary>
            TitleBarOnly,

            /// <summary>
            /// 仅刷新窗体（不包含子控件）。
            /// </summary>
            Window,

            /// <summary>
            /// 刷新窗体及其子控件。
            /// </summary>
            WindowAndChildren
        }


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

        #region private properties
        private Color _borderColor;
        private Style _theme;

        private int _titleButtonHeight;
        private Color _titleBoxBackColor;
        private Color _titleBoxForeColor;
        private Font _titleFont;
        private HorizontalAlignment _titleTextAlign;
        private int _titleTextMargin;
        private Point _titleTextOffset;
        private TitleButtonStyle _titleButtons;
        private int _titleButtonWidth;
        private HorizontalEnds _titleButtonAlign;
        private Size _titleButtonIconSize;
        private float _interactionColorScale;
        private bool _enableCloseButton;
        private bool _enableResizeButton;
        private bool _enableMinimizeButton;
        private Color _titleButtonDisabledColor;
        private Point _iconDrawOffset;
        private int _titleBoxHeight;
        private CornerRadius _titleButtonCornerRadius;
        private bool _enableTitleButtonAnimation;

        private WindowPosition _dragMode;
        private WindowPosition _shortcutResizeMode;
        private RefreshMode _sizeChangeRefreshMode;
        private bool _enableStartupAnimation;
        private bool _enableCloseAnimation;
        private bool _enableResizeAnimation;
        private bool _autoHideWindowBorder;
        private bool _resizable;
        private CloseButtonAction _closeButtonFunction;
        private StartupSequence _startupOrder;
        private bool _enableShadow;
        private bool _enableChangeInactiveTitleBoxForeColor;
        #endregion

        #region override properties
        [Browsable(false)]
        public new FormBorderStyle FormBorderStyle
        {
            get => base.FormBorderStyle;
            set => base.FormBorderStyle = value;
        }

        [Browsable(false)]
        public new bool HelpButton
        {
            get => base.HelpButton;
            set => base.HelpButton = value;
        }

        [Browsable(false)]
        public new bool ControlBox
        {
            get => base.ControlBox;
            set => base.ControlBox = value;
        }
        #endregion

        #region events
        /// <summary>
        /// 用户单击关闭按钮的事件。
        /// </summary>
        public event EventHandler<EventArgs>? CloseButtonClick;

        /// <summary>
        /// 引发 <see cref="OnCloseButtonClick(EventArgs)"/> 事件。
        /// </summary>
        protected virtual void OnCloseButtonClick(EventArgs e)
        {
            CloseButtonClick?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 背景绘制事件。
        /// </summary>
        public event PaintEventHandler? BackgroundPaint;

        /// <summary>
        /// 引发 <see cref="OnBackgroundPaint(Graphics)"/> 事件
        /// </summary>
        protected virtual void OnBackgroundPaint(Graphics g)
        {
            var pe = new PaintEventArgs(g, new Rectangle(0, 0, Width, Height));
            BackgroundPaint?.Invoke(this, pe);
        }

        /// <summary>
        /// 关闭按钮绘制事件。
        /// </summary>
        public event PaintEventHandler? CloseButtonPaint;

        /// <summary>
        /// 引发 <see cref="OnCloseButtonPaint(PaintEventArgs)"/> 事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCloseButtonPaint(PaintEventArgs e)
        {
            CloseButtonPaint?.Invoke(this, e);
        }

        /// <summary>
        /// 最大化/还原 按钮绘制事件。
        /// </summary>
        public event PaintEventHandler? ResizeButtonPaint;

        /// <summary>
        /// 引发 <see cref="OnResizeButtonPaint(PaintEventArgs)"/> 事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnResizeButtonPaint(PaintEventArgs e)
        {
            ResizeButtonPaint?.Invoke(this, e);
        }

        /// <summary>
        /// 最小化按钮绘制事件。
        /// </summary>
        public event PaintEventHandler? MinimizeButtonPaint;

        /// <summary>
        /// 引发 <see cref="OnMinimizeButtonPaint(PaintEventArgs)"/> 事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMinimizeButtonPaint(PaintEventArgs e)
        {
            MinimizeButtonPaint?.Invoke(this, e);
        }
        #endregion

        #region create titlebutton
        private class TitleButton : RoundedButton
        {
            public TitleButton()
            {
                BorderSize = 0;
                BorderCornerRadius = new CornerRadius(0);
                IsEnableAnimation = false;

                SetStyle(ControlStyles.Selectable, false);
            }
        }

        private readonly TitleButton CloseButton = new();
        private readonly TitleButton ResizeButton = new();
        private readonly TitleButton MinimizeButton = new();
        #endregion

        private void InitializeTitleButton()
        {
            CloseButton.Name = "CloseButton";
            ResizeButton.Name = "ResizeButton";
            MinimizeButton.Name = "MinimizeButton";

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

            CloseButton.Top = 1;
            ResizeButton.Top = 1;
            MinimizeButton.Top = 1;
        }

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

            if (sender is TitleButton but)
            {
                Bitmap? GetUserPaintBackGround()
                {
                    if (BackgroundPaint == null)
                    {
                        return null;
                    }

                    Bitmap bitmap = new(Width, Height);
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        OnBackgroundPaint(graphics);
                    }
                    return bitmap;
                }

                using (var backgroundBitmap = GetUserPaintBackGround())
                {
                    if (BackgroundImage != null)
                    {
                        g.DrawImage(
                            BackgroundImage,
                            new Rectangle(0, 0, but.Width, but.Height),
                            but.Location.X, but.Location.Y, but.Width, but.Height,
                            GraphicsUnit.Pixel);
                    }

                    if (backgroundBitmap != null)
                    {
                        g.DrawImage(
                            backgroundBitmap,
                            new Rectangle(0, 0, but.Width, but.Height),
                            but.Location.X, but.Location.Y, but.Width, but.Height,
                            GraphicsUnit.Pixel);
                    }
                }

                switch (but.Name)
                {
                    case "CloseButton":
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
                        OnCloseButtonPaint(e);
                        break;

                    case "ResizeButton":
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
                        OnResizeButtonPaint(e);
                        break;

                    case "MinimizeButton":
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
                        OnMinimizeButtonPaint(e);
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

            _titleBoxBackColor = Color.FromArgb(224, 224, 224);
            _titleBoxForeColor = Color.Black;
            _titleFont = new Font("Microsoft YaHei UI", 9);
            _titleTextAlign = HorizontalAlignment.Left;
            _titleTextMargin = 11;
            _titleTextOffset = Point.Empty;
            _titleButtons = TitleButtonStyle.ShowAll;
            _titleBoxHeight = 31;
            _titleButtonHeight = 31;
            _titleButtonWidth = 46;
            _titleButtonAlign = HorizontalEnds.Right;
            _titleButtonIconSize = new Size(10, 10);
            _interactionColorScale = -0.04F;
            _enableCloseButton = true;
            _enableResizeButton = true;
            _enableMinimizeButton = true;
            _titleButtonDisabledColor = Color.DarkGray;
            _enableChangeInactiveTitleBoxForeColor = true;
            _iconDrawOffset = Point.Empty;
            _titleButtonCornerRadius = new CornerRadius(0);
            _enableTitleButtonAnimation = false;

            _dragMode = WindowPosition.TitleBarOnly;
            _shortcutResizeMode = WindowPosition.TitleBarOnly;
            _sizeChangeRefreshMode = RefreshMode.TitleBarOnly;
            _enableStartupAnimation = true;
            _enableCloseAnimation = true;
            _enableResizeAnimation = true;
            _autoHideWindowBorder = true;
            _resizable = true;
            _closeButtonFunction = CloseButtonAction.CloseWindow;
            _startupOrder = StartupSequence.WaitOnLoadThenAnimate;
            _enableShadow = true;

            BackColor = Color.White;
            Text = Name;
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(700, 450);
            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;

            MinimumSize = new Size(TitleButtonWidth, TitleButtonHeight);
        }

        #region KlxPiaoForm Appearance
        /// <summary>
        /// 获取或设置边框的颜色。
        /// </summary>
        [Category("KlxPiaoForm Appearance")]
        [Description("窗体边框的颜色")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置主题风格。
        /// </summary>
        [Category("KlxPiaoForm Appearance")]
        [Description("窗体的视觉风格")]
        public Style Theme
        {
            get => _theme;
            set { _theme = value; RefreshTitleBoxButton(); }
        }
        #endregion

        #region KlxPiaoForm Title Box
        /// <summary>
        /// 获取或设置标题按钮的高度。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("标题按钮的高度")]
        public int TitleButtonHeight
        {
            get => _titleButtonHeight;
            set { _titleButtonHeight = value; RefreshTitleButtonProperties(); }
        }
        /// <summary>
        /// 获取或设置标题框的背景色。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("标题框的背景色")]
        public Color TitleBoxBackColor
        {
            get => _titleBoxBackColor;
            set { _titleBoxBackColor = value; InvalidateTitleBox(); }
        }
        /// <summary>
        /// 获取或设置标题框的前景色。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("标题框的前景色")]
        public Color TitleBoxForeColor
        {
            get => _titleBoxForeColor;
            set { _titleBoxForeColor = value; InvalidateTitleBox(); RefreshTitleBoxButton(); }
        }
        /// <summary>
        /// 获取或设置标题的字体。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("标题的字体")]
        public Font TitleFont
        {
            get => _titleFont;
            set { _titleFont = value; InvalidateTitleBox(); }
        }
        /// <summary>
        /// 获取或设置标题文字的位置。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("标题文字位于标题框的位置")]
        public HorizontalAlignment TitleTextAlign
        {
            get => _titleTextAlign;
            set { _titleTextAlign = value; InvalidateTitleBox(); }
        }
        /// <summary>
        /// 获取或设置标题文字的边距。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("标题文字位于标题框的左右边距")]
        public int TitleTextMargin
        {
            get => _titleTextMargin;
            set { _titleTextMargin = value; InvalidateTitleBox(); }
        }
        /// <summary>
        /// 获取或设置标题位置的偏移。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("标题位置的偏移")]
        public Point TitleTextOffset
        {
            get => _titleTextOffset;
            set { _titleTextOffset = value; InvalidateTitleBox(); }
        }
        /// <summary>
        /// 获取或设置标题按钮的呈现形式。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("显示在标题框的按钮")]
        public TitleButtonStyle TitleButtons
        {
            get => _titleButtons;
            set { _titleButtons = value; InvalidateTitleBox(); }
        }
        /// <summary>
        /// 获取或设置标题按钮的宽度。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("标题按钮的宽度")]
        public int TitleButtonWidth
        {
            get => _titleButtonWidth;
            set { _titleButtonWidth = value; InvalidateTitleBox(); }
        }
        /// <summary>
        /// 获取或设置标题按钮的位置。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("标题按钮的位置")]
        public HorizontalEnds TitleButtonAlign
        {
            get => _titleButtonAlign;
            set { _titleButtonAlign = value; InvalidateTitleBox(); }
        }
        /// <summary>
        /// 获取或设置标题按钮的图标大小。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("标题按钮图标绘制大小")]
        public Size TitleButtonIconSize
        {
            get => _titleButtonIconSize;
            set { _titleButtonIconSize = value; RefreshTitleBoxButton(); }
        }
        /// <summary>
        /// 获取或设置标题按钮鼠标交互时颜色改变的大小。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("决定标题按钮移入和按下的背景色，范围 -1.00 到 +1.00")]
        public float InteractionColorScale
        {
            get => _interactionColorScale;
            set { _interactionColorScale = value; RefreshTitleButtonProperties(); }
        }
        /// <summary>
        /// 获取或设置是否启用关闭按钮。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("是否启用关闭按钮")]
        public bool EnableCloseButton
        {
            get => _enableCloseButton;
            set { _enableCloseButton = value; InvalidateTitleBox(); }
        }
        /// <summary>
        /// 获取或设置是否启用缩放按钮。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("是否启用缩放按钮")]
        public bool EnableResizeButton
        {
            get => _enableResizeButton;
            set { _enableResizeButton = value; InvalidateTitleBox(); }
        }
        /// <summary>
        /// 获取或设置是否启用最小化按钮。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("是否启用最小化按钮")]
        public bool EnableMinimizeButton
        {
            get => _enableMinimizeButton;
            set { _enableMinimizeButton = value; InvalidateTitleBox(); }
        }
        /// <summary>
        /// 获取或设置标题按钮未启用时的前景色。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("标题按钮未启用时的前景色")]
        public Color TitleButtonDisabledColor
        {
            get => _titleButtonDisabledColor;
            set { _titleButtonDisabledColor = value; RefreshTitleBoxButton(); }
        }
        /// <summary>
        /// 获取或设置窗体未激活时是否改变标题框的前景色。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("窗体未激活时是否改变标题框的前景色")]
        public bool EnableChangeInactiveTitleBoxForeColor
        {
            get => _enableChangeInactiveTitleBoxForeColor;
            set => _enableChangeInactiveTitleBoxForeColor = value;
        }
        /// <summary>
        /// 获取或设置图标绘制的偏移。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("图标绘制的偏移")]
        public Point IconDrawOffset
        {
            get => _iconDrawOffset;
            set { _iconDrawOffset = value; InvalidateTitleBox(); }
        }
        /// <summary>
        /// 获取或设置标题框的高度。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("标题框的高度")]
        public int TitleBoxHeight
        {
            get => _titleBoxHeight;
            set { InvalidateTitleBox(); _titleBoxHeight = value; }
        }
        /// <summary>
        /// 获取或设置标题按钮的角半径，以 <see cref="CornerRadius"/> 结构体表示。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("标题按钮的角半径")]
        public CornerRadius TitleButtonCornerRadius
        {
            get => _titleButtonCornerRadius;
            set { _titleButtonCornerRadius = value; RefreshTitleButtonProperties(); }
        }
        /// <summary>
        /// 获取或设置是否启用标题按钮过渡动画。
        /// </summary>
        [Category("KlxPiaoForm Title Box")]
        [Description("是否启用标题按钮过渡动画")]
        public bool EnableTitleButtonAnimation
        {
            get => _enableTitleButtonAnimation;
            set => _enableTitleButtonAnimation = value;
        }
        #endregion

        #region KlxPiaoForm Properties
        /// <summary>
        /// 获取或设置窗体拖动的方式。
        /// </summary>
        [Category("KlxPiaoForm Properties")]
        [Description("窗体拖动的方式")]
        public WindowPosition DragMode
        {
            get => _dragMode;
            set => _dragMode = value;
        }
        /// <summary>
        /// 获取或设置最大化或还原的快捷方式。
        /// </summary>
        [Category("KlxPiaoForm Properties")]
        [Description("最大化或还原的快捷方式，仅缩放按钮显示并启用时生效")]
        public WindowPosition ShortcutResizeMode
        {
            get => _shortcutResizeMode;
            set => _shortcutResizeMode = value;
        }
        /// <summary>
        /// 获取或设置窗体大小改变时的刷新方式。
        /// </summary>
        [Category("KlxPiaoForm Properties")]
        [Description("窗体大小改变时的刷新方式")]
        public RefreshMode SizeChangeRefreshMode
        {
            get => _sizeChangeRefreshMode;
            set => _sizeChangeRefreshMode = value;
        }
        /// <summary>
        /// 获取或设置是否启用启动动画。
        /// </summary>
        [Category("KlxPiaoForm Properties")]
        [Description("是否启用启动动画")]
        public bool EnableStartupAnimation
        {
            get => _enableStartupAnimation;
            set => _enableStartupAnimation = value;
        }
        /// <summary>
        /// 获取或设置是否启用关闭动画。
        /// </summary>
        [Category("KlxPiaoForm Properties")]
        [Description("是否启用关闭动画")]
        public bool EnableCloseAnimation
        {
            get => _enableCloseAnimation;
            set => _enableCloseAnimation = value;
        }
        /// <summary>
        /// 获取或设置是否启用 最大化/还原 动画。
        /// </summary>
        [Category("KlxPiaoForm Properties")]
        [Description("是否启用最大化或还原的动画")]
        public bool EnableResizeAnimation
        {
            get => _enableResizeAnimation;
            set => _enableResizeAnimation = value;
        }
        /// <summary>
        /// 获取或设置是否在全屏时自动隐藏窗体边框。
        /// </summary>
        [Category("KlxPiaoForm Properties")]
        [Description("设置为True后，全屏时自动隐藏窗体边框")]
        public bool AutoHideWindowBorder
        {
            get => _autoHideWindowBorder;
            set => _autoHideWindowBorder = value;
        }
        /// <summary>
        /// 获取或设置是否可调整大小。
        /// </summary>
        [Category("KlxPiaoForm Properties")]
        [Description("窗体是否可调整大小")]
        public bool Resizable
        {
            get => _resizable;
            set => _resizable = value;
        }
        /// <summary>
        /// 获取或设置关闭按钮的功能。
        /// </summary>
        [Category("KlxPiaoForm Properties")]
        [Description("用户单击关闭按钮时执行的操作")]
        public CloseButtonAction CloseButtonFunction
        {
            get => _closeButtonFunction;
            set => _closeButtonFunction = value;
        }
        /// <summary>
        /// 获取或设置启动动画和 OnLoad 事件的顺序，以 <see cref="StartupSequence"/> 枚举类型表示。
        /// </summary>
        [Category("KlxPiaoForm Properties")]
        [Description("启动动画和Load事件的顺序")]
        public StartupSequence StartupOrder
        {
            get => _startupOrder;
            set => _startupOrder = value;
        }
        /// <summary>
        /// 获取或设置是否启用 Windows 窗体的圆角和阴影(会使窗体边框功能失效)。
        /// </summary>
        [Category("KlxPiaoForm Properties")]
        [Description("启用 Windows 窗体的圆角和阴影(会使窗体边框颜色属性失效)")]
        [DefaultValue(true)]
        public bool EnableShadow
        {
            get => _enableShadow;
            set => _enableShadow = value;
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pe)
        {
            Rectangle thisRect = new(0, 0, Width, Height);
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.Clear(BackColor);

            //draw titlebox
            using (Brush brush = new SolidBrush(TitleBoxBackColor))
            {
                g.FillRectangle(brush, new Rectangle(thisRect.X, thisRect.Y, thisRect.Width, TitleBoxHeight));
            }

            //draw background
            if (BackgroundImage != null)
            {
                g.DrawImage(BackgroundImage, new Rectangle(0, 0, BackgroundImage.Width, BackgroundImage.Height));
            }

            //user drawing
            OnBackgroundPaint(g);

            //draw border
            if (!(AutoHideWindowBorder && WindowState == FormWindowState.Maximized) && !EnableShadow)
            {
                using Pen borderPen = new(BorderColor, 1);
                g.DrawRectangle(borderPen, new Rectangle(thisRect.X, thisRect.Y, thisRect.Width - 1, thisRect.Height - 1));
            }

            //update title button properties
            RefreshTitleButtonProperties();

            //update title button pos
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

            //update title button show
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

            //draw title text
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

                //offset
                int drawX = (Icon != null && ShowIcon && TitleTextAlign == HorizontalAlignment.Left)
                    ? x + (TitleBoxHeight - Icon.ToBitmap().Height) + 1
                    : x;

                g.DrawString(Text, TitleFont, brush, new Point(drawX + TitleTextOffset.X, y + TitleTextOffset.Y));
            }

            //draw icon
            if (Icon != null && ShowIcon)
            {
                using Bitmap icon = Icon.ToBitmap();
                Point iconDrawPos = new((TitleBoxHeight - icon.Height) / 2 + 1 + IconDrawOffset.X, (TitleBoxHeight - icon.Height) / 2 + IconDrawOffset.Y);
                g.DrawImage(icon, iconDrawPos);
            }

            //recovery cursor
            if (adjustBorder == AdjustBorder.Adjusted)
            {
                Cursor = Cursors.Default;
            }

            base.OnPaint(pe);
        }

        #region title button click
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

        #region OnClosing OnClosed
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

        #region OnActivated OnDeactivate
        private Color oldColor = Color.Empty;
        protected override void OnActivated(EventArgs e)
        {
            if (EnableChangeInactiveTitleBoxForeColor && oldColor != Color.Empty)
            {
                TitleBoxForeColor = oldColor;
            }
            base.OnActivated(e);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            static int Clamp(int value, int min, int max) => Math.Max(min, Math.Min(max, value));

            if (EnableChangeInactiveTitleBoxForeColor)
            {
                oldColor = TitleBoxForeColor;
                int brightnessAdjustment = TitleBoxForeColor.GetBrightness() > 0.5 ? -75 : 125;

                int newR = Clamp(oldColor.R + brightnessAdjustment, 0, 225);
                int newG = Clamp(oldColor.G + brightnessAdjustment, 0, 225);
                int newB = Clamp(oldColor.B + brightnessAdjustment, 0, 225);

                TitleBoxForeColor = Color.FromArgb(newR, newG, newB);
            }
            base.OnDeactivate(e);
        }
        #endregion

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

                //防止启动时闪烁黑色区域
                Refresh();
            }
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            this.ForEachControl<Control>(control =>
            {
                var property = control.GetType().GetProperty("BaseBackColor");

                if (property != null && property.CanWrite)
                {
                    property.SetValue(control, BackColor);
                }
            });

            base.OnBackColorChanged(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            InvalidateTitleBox();

            base.OnTextChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            switch (SizeChangeRefreshMode)
            {
                case RefreshMode.TitleBarOnly:
                    InvalidateTitleBox();
                    Update();
                    break;

                case RefreshMode.Window:
                    Invalidate(false);
                    Update();
                    break;

                case RefreshMode.WindowAndChildren:
                    Refresh();
                    break;
            }

            base.OnSizeChanged(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            if (TitleButtons == TitleButtonStyle.ShowAll && EnableResizeButton)
            {
                if (ShortcutResizeMode == WindowPosition.EntireWindow)
                {
                    ResizeButton_Click(ResizeButton, e);
                }
                else if (ShortcutResizeMode == WindowPosition.TitleBarOnly && mouseDownPos.Y <= TitleBoxHeight)
                {
                    ResizeButton_Click(ResizeButton, e);
                }
            }

            base.OnDoubleClick(e);
        }

        #region drag and resize
        private enum AdjustBorder
        {
            left, top, right, bottom, Adjusted, leftTop, rightTop, leftBottom, rightBottom
        }

        private Point mouseDownPos = Point.Empty;
        private readonly int determinationSize = 7; //判定大小
        private AdjustBorder adjustBorder = AdjustBorder.Adjusted;
        private Point oldPos = Point.Empty;
        private Size oldSize = Size.Empty;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            mouseDownPos = e.Location;

            //传递正在调整的位置
            if (Resizable && WindowState != FormWindowState.Maximized)
            {
                oldPos = Location;
                oldSize = Size;

                bool left = e.X <= determinationSize;
                bool right = e.X >= Width - determinationSize;
                bool top = e.Y <= determinationSize;
                bool bottom = e.Y >= Height - determinationSize;

                if (left)
                {
                    if (top) { adjustBorder = AdjustBorder.leftTop; }
                    else if (bottom) { adjustBorder = AdjustBorder.leftBottom; }
                    else { adjustBorder = AdjustBorder.left; }
                }
                else if (right)
                {
                    if (top) { adjustBorder = AdjustBorder.rightTop; }
                    else if (bottom) { adjustBorder = AdjustBorder.rightBottom; }
                    else { adjustBorder = AdjustBorder.right; }
                }
                else if (top)
                {
                    adjustBorder = AdjustBorder.top;
                }
                else if (bottom)
                {
                    adjustBorder = AdjustBorder.bottom;
                }
                else
                {
                    adjustBorder = AdjustBorder.Adjusted;
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //拖动
            if (e.Button == MouseButtons.Left
                && mouseDownPos != Point.Empty
                && adjustBorder == AdjustBorder.Adjusted
                && WindowState != FormWindowState.Maximized
                && (DragMode == WindowPosition.EntireWindow ||
                   (DragMode == WindowPosition.TitleBarOnly
                   && mouseDownPos.Y <= TitleBoxHeight)))
            {
                Location = new Point(Location.X + e.X - mouseDownPos.X, Location.Y + e.Y - mouseDownPos.Y);
            }

            //适应光标
            if (Resizable && WindowState != FormWindowState.Maximized)
            {
                bool left = e.X <= determinationSize;
                bool right = e.X >= Width - determinationSize;
                bool top = e.Y <= determinationSize;
                bool bottom = e.Y >= Height - determinationSize;
                if (left)
                {
                    if (top) { Cursor = Cursors.SizeNWSE; }
                    else if (bottom) { Cursor = Cursors.SizeNESW; }
                    else { Cursor = Cursors.SizeWE; }
                }
                else if (right)
                {
                    if (top) { Cursor = Cursors.SizeNESW; }
                    else if (bottom) { Cursor = Cursors.SizeNWSE; }
                    else { Cursor = Cursors.SizeWE; }
                }
                else if (top || bottom)
                {
                    Cursor = Cursors.SizeNS;
                }
                else
                {
                    Cursor = Cursors.Default;
                }

                //调整大小
                if (adjustBorder != AdjustBorder.Adjusted)
                {
                    switch (adjustBorder)
                    {
                        case AdjustBorder.right:
                            Width = Cursor.Position.X - Left;
                            break;

                        case AdjustBorder.bottom:
                            Height = Cursor.Position.Y - Top;
                            break;

                        case AdjustBorder.left:
                            Left = Cursor.Position.X;
                            Width = oldSize.Width + oldPos.X - Cursor.Position.X;
                            break;

                        case AdjustBorder.top:
                            Top = Cursor.Position.Y;
                            Height = oldSize.Height + oldPos.Y - Cursor.Position.Y;
                            break;

                        case AdjustBorder.leftTop:
                            Location = Cursor.Position;
                            Width = oldSize.Width + oldPos.X - Cursor.Position.X;
                            Height = oldSize.Height + oldPos.Y - Cursor.Position.Y;
                            break;

                        case AdjustBorder.leftBottom:
                            Left = Cursor.Position.X;
                            Width = oldSize.Width + oldPos.X - Cursor.Position.X;
                            Height = Cursor.Position.Y - Top;
                            break;

                        case AdjustBorder.rightTop:
                            Top = Cursor.Position.Y;
                            Width = Cursor.Position.X - Left;
                            Height = oldSize.Height + oldPos.Y - Cursor.Position.Y;
                            break;

                        case AdjustBorder.rightBottom:
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
            mouseDownPos = Point.Empty;
            adjustBorder = AdjustBorder.Adjusted;
            base.OnMouseUp(e);
        }

        //对于标题按钮特殊处理，只响应 top 和 right
        private void TitleButton_MouseDown(object? sender, MouseEventArgs e)
        {
            //传递正在调整的位置
            if (Resizable && WindowState != FormWindowState.Maximized)
            {
                oldPos = Location;
                oldSize = Size;

                bool right = e.X >= Width - determinationSize;
                bool top = e.Y <= determinationSize;

                if (right && top)
                {
                    adjustBorder = AdjustBorder.rightTop;
                }
                else if (right)
                {
                    adjustBorder = AdjustBorder.right;
                }
                else if (top)
                {
                    adjustBorder = AdjustBorder.top;
                }
            }
        }

        private void TitleButton_MouseMove(object? sender, MouseEventArgs e)
        {
            //适应光标
            if (Resizable && WindowState != FormWindowState.Maximized)
            {
                bool right = e.X >= Width - determinationSize;
                bool top = e.Y <= determinationSize;

                if (right && top)
                {
                    Cursor = Cursors.SizeNESW;
                }
                else if (right)
                {
                    Cursor = Cursors.SizeWE;
                }
                else if (top)
                {
                    Cursor = Cursors.SizeNS;
                }
                else
                {
                    Cursor = Cursors.Default;
                }

                //调整大小
                if (adjustBorder != AdjustBorder.Adjusted)
                {
                    switch (adjustBorder)
                    {
                        case AdjustBorder.right:
                            Width = Cursor.Position.X - Left;
                            break;

                        case AdjustBorder.top:
                            Top = Cursor.Position.Y;
                            Height = oldSize.Height + oldPos.Y - Cursor.Position.Y;
                            break;

                        case AdjustBorder.rightTop:
                            Top = Cursor.Position.Y;
                            Width = Cursor.Position.X - Left;
                            Height = oldSize.Height + oldPos.Y - Cursor.Position.Y;
                            break;
                    }
                }
            }
        }

        private void TitleButton_MouseUp(object? sender, MouseEventArgs e)
        {
            mouseDownPos = Point.Empty;
            adjustBorder = AdjustBorder.Adjusted;
        }
        #endregion

        private bool isClosing = false;

        #region public method
        /// <summary>
        /// 刷新标题按钮的属性。
        /// </summary>
        public void RefreshTitleButtonProperties()
        {
            TitleButton[] buttons = [CloseButton, ResizeButton, MinimizeButton];

            foreach (TitleButton b in buttons)
            {
                b.Size = new Size(TitleButtonWidth, TitleButtonHeight - 1);
                b.BackColor = TitleBoxBackColor;
                b.BaseBackColor = TitleBoxBackColor;
                b.BorderCornerRadius = TitleButtonCornerRadius;
                b.IsEnableAnimation = EnableTitleButtonAnimation;

                b.InteractionStyle.OverBackColor = TitleBoxBackColor.AdjustBrightness(InteractionColorScale);
                b.InteractionStyle.DownBackColor = b.InteractionStyle.OverBackColor.AdjustBrightness(InteractionColorScale);

            }

            CloseButton.Enabled = EnableCloseButton;
            ResizeButton.Enabled = EnableResizeButton;
            MinimizeButton.Enabled = EnableMinimizeButton;
        }

        /// <summary>
        /// 切换 WindowState (带有动画)。
        /// </summary>
        public void ResizeButtonPerformClick() => ResizeButton.PerformClick();

        /// <summary>
        /// 根据 <see cref="CloseButtonFunction"/> 属性执行关闭动画。
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

        /// <summary>
        /// 立即重绘标题按钮。
        /// </summary>
        public void RefreshTitleBoxButton()
        {
            CloseButton.Refresh();
            ResizeButton.Refresh();
            MinimizeButton.Refresh();
        }

        /// <summary>
        /// 通知标题框重绘。
        /// </summary>
        public void InvalidateTitleBox()
        {
            Invalidate(new Rectangle(0, 0, Width, TitleBoxHeight));
        }

        /// <summary>
        /// 获取工作区的矩形。
        /// </summary>
        /// <returns>用户区域的矩形。</returns>
        public Rectangle GetClientRectangle()
        {
            return new Rectangle(GetClientLocation(), GetClientSize());
        }

        /// <summary>
        /// 获取工作区的大小。
        /// </summary>
        /// <returns>用户区域的大小。</returns>
        public Size GetClientSize()
        {
            return new Size(Width - 3, Height - TitleBoxHeight - 2);
        }

        /// <summary>
        /// 获取工作区的左上角坐标。
        /// </summary>
        /// <returns>用户区域的大小。</returns>
        public Point GetClientLocation()
        {
            return new Point(1, TitleBoxHeight);
        }

        /// <summary>
        /// 获取标题框的矩形。
        /// </summary>
        /// <returns>标题框的矩形。</returns>
        public Rectangle GetTitleBoxRectangle()
        {
            return new Rectangle(0, 0, Width, TitleBoxHeight);
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
        /// 设置主题颜色。
        /// </summary>
        /// <param name="color">主题色。</param>
        public void SetThemeColor(Color color)
        {
            TitleBoxBackColor = color;
            TitleBoxForeColor = color.GetBrightness() >= 0.5 ? Color.Black : Color.White;
        }
        #endregion
    }
}