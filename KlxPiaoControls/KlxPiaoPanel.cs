using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 表示一个具有自定义外观和投影效果的面板控件。
    /// </summary>
    /// <remarks>
    /// <see cref="KlxPiaoPanel"/> 继承自 <see cref="Panel"/>，是原版 <see cref="Panel"/> 的增强版本。
    /// </remarks>
    [DefaultEvent("Click")]
    public partial class KlxPiaoPanel : Panel
    {
        /// <summary>
        /// 表示投影方向的枚举。
        /// </summary>
        public enum ShadowDirectionEnum
        {
            /// <summary>
            /// 右下。
            /// </summary>
            BottomRight,
            /// <summary>
            /// 坐下。
            /// </summary>
            BottomLeft,
            /// <summary>
            /// 左下右。
            /// </summary>
            LeftBottomRight
        }

        private Color _borderColor;
        private Color _baseBackColor;
        private int _borderSize;
        private CornerRadius _cornerRadius;
        private bool _isEnableShadow;
        private int _shadowLength;
        private Color _shadowColor;
        private ShadowDirectionEnum _shadowDirection;

        public KlxPiaoPanel()
        {
            InitializeComponent();

            _borderColor = Color.FromArgb(199, 199, 199);
            _baseBackColor = Color.White;
            _borderSize = 1;
            _isEnableShadow = true;
            _shadowLength = 5;
            _shadowColor = Color.FromArgb(142, 142, 142);
            _shadowDirection = ShadowDirectionEnum.BottomRight;
            _cornerRadius = new CornerRadius(0);

            BackColor = Color.White;
            BorderStyle = BorderStyle.None;
            Size = new Size(100, 100);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        #region KlxPiaoPanel Appearance
        /// <summary>
        /// 获取或设置边框的颜色。
        /// </summary>
        [Category("KlxPiaoPanel Appearance")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "199,199,199")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框外部的颜色。
        /// </summary>
        [Category("KlxPiaoPanel Appearance")]
        [Description("边框外部的颜色，通常与父容器背景色相同")]
        [DefaultValue(typeof(Color), "White")]
        public Color BaseBackColor
        {
            get { return _baseBackColor; }
            set { _baseBackColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框大小。
        /// </summary>
        [Category("KlxPiaoPanel Appearance")]
        [Description("边框的大小，启用阴影时失效")]
        [DefaultValue(1)]
        public int BorderSize
        {
            get { return _borderSize; }
            set { _borderSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置圆角的大小，以 <see cref="KlxPiaoAPI.CornerRadius"/> 结构体表示。
        /// </summary>
        [Category("KlxPiaoPanel Appearance")]
        [Description("每个角的圆角大小，自动检测是百分比大小还是像素大小。")]
        [DefaultValue(typeof(CornerRadius), "0,0,0,0")]
        public CornerRadius CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否启用投影。
        /// </summary>
        [Category("KlxPiaoPanel Appearance")]
        [Description("是否启用投影")]
        [DefaultValue(true)]
        public bool IsEnableShadow
        {
            get { return _isEnableShadow; }
            set { _isEnableShadow = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置投影的长度。
        /// </summary>
        [Category("KlxPiaoPanel Appearance")]
        [Description("投影的长度")]
        [DefaultValue(5)]
        public int ShadowLength
        {
            get { return _shadowLength; }
            set { _shadowLength = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置投影的颜色。
        /// </summary>
        [Category("KlxPiaoPanel Appearance")]
        [Description("投影的颜色，减淡到白色")]
        [DefaultValue(typeof(Color), "142,142,142")]
        public Color ShadowColor
        {
            get { return _shadowColor; }
            set { _shadowColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置投影的方向，以 <see cref="ShadowDirectionEnum"/> 枚举类型表示。
        /// </summary>
        [Category("KlxPiaoPanel Appearance")]
        [Description("投影的方向")]
        [DefaultValue(typeof(ShadowDirectionEnum), "BottomRight")]
        public ShadowDirectionEnum ShadowDirection
        {
            get { return _shadowDirection; }
            set { _shadowDirection = value; Invalidate(); }
        }
        #endregion

        [DefaultValue(typeof(Size), "100,100")]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; Invalidate(); }
        }

        [Browsable(false)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            using Bitmap bitmap = new(Width, Height);
            {
                Rectangle thisRect = new(0, 0, Width, Height);
                using Graphics g = Graphics.FromImage(bitmap);

                g.Clear(BaseBackColor);

                Color startColor = ShadowColor;
                Color endColor = BaseBackColor;
                int decrementValue = ShadowLength == 0 ? 0 : 255 / ShadowLength;

                if (IsEnableShadow)
                {
                    //shadow
                    for (int i = 0; i <= ShadowLength; i++)
                    {
                        //1.1.1.7 开发日志
                        //这里使用 Interpolator 插值的逻辑会出错，不知道是为什么
                        using SolidBrush brush = new(Color.FromArgb(decrementValue,
                            Math.Max(startColor.R, endColor.R) - i * (Math.Abs(endColor.R - startColor.R) / ShadowLength),
                            Math.Max(startColor.G, endColor.G) - i * (Math.Abs(endColor.G - startColor.G) / ShadowLength),
                            Math.Max(startColor.B, endColor.B) - i * (Math.Abs(endColor.B - startColor.B) / ShadowLength)));
                        g.FillRectangle(brush, new Rectangle(ShadowLength * 2 - i, ShadowLength - i, Width - ShadowLength * 2, Height - ShadowLength));
                        g.FillRectangle(brush, new Rectangle(i, ShadowLength - i, Width - ShadowLength * 2, Height - ShadowLength));
                    }

                    //border
                    using Pen borderPen = new(BorderColor, 1);
                    g.DrawRectangle(borderPen, GetClientRectangle());

                    //background
                    using SolidBrush backBrush = new(BackColor);
                    g.FillRectangle(backBrush, AdjustRectangle(GetClientRectangle(), -1));
                }
                else
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    //background
                    using SolidBrush backBrush = new(BackColor);
                    g.FillRectangle(backBrush, thisRect);

                    g.DrawRounded(thisRect, CornerRadius, BaseBackColor, new Pen(BorderColor, BorderSize));
                }
                pe.Graphics.DrawImage(bitmap, 0, 0);
            }

            base.OnPaint(pe);
        }

        #region public method
        /// <summary>
        /// 获取工作区的大小。
        /// </summary>
        /// <returns>除投影或边框内的大小。</returns>
        public Size GetClientSize()
        {
            if (IsEnableShadow)
            {
                return ShadowDirection switch
                {
                    ShadowDirectionEnum.BottomLeft => new Size(Width - ShadowLength - 1, Height - ShadowLength - 1),
                    ShadowDirectionEnum.BottomRight => new Size(Width - ShadowLength - 1, Height - ShadowLength - 1),
                    ShadowDirectionEnum.LeftBottomRight => new Size(Width - ShadowLength * 2 - 1, Height - ShadowLength - 1),
                    _ => Size
                };
            }
            else
            {
                return new Size(Width - BorderSize * 2, Height - BorderSize * 2);
            }
        }

        /// <summary>
        /// 获取工作区的左上角坐标。
        /// </summary>
        /// <returns>用户区域的大小。</returns>
        public Point GetClientLocation()
        {
            if (IsEnableShadow)
            {
                return ShadowDirection switch
                {
                    ShadowDirectionEnum.BottomLeft => new Point(ShadowLength, 0),
                    ShadowDirectionEnum.BottomRight => new Point(0, 0),
                    ShadowDirectionEnum.LeftBottomRight => new Point(ShadowLength, 0),
                    _ => Point.Empty
                };
            }
            else
            {
                return new Point(BorderSize, BorderSize);
            }
        }

        /// <summary>
        /// 获取工作区的矩形。
        /// </summary>
        /// <returns>除投影或边框内的矩形。</returns>
        public Rectangle GetClientRectangle() => new(GetClientLocation(), GetClientSize());
        #endregion

        private static Rectangle AdjustRectangle(Rectangle rectangle, int adjust)
        {
            return new Rectangle(rectangle.X - adjust, rectangle.Y - adjust, rectangle.Width + adjust, rectangle.Height + adjust);
        }
    }
}