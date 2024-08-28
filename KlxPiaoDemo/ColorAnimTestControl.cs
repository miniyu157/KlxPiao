using KlxPiaoAPI;
using System.ComponentModel;

namespace KlxPiaoDemo
{
    public partial class ColorAnimTestControl : Control
    {
        public ColorAnimTestControl()
        {
            InitializeComponent();

            _animationConfig = new AnimationInfo(200, 30, EasingType.Linear);

            _interactionStyleClass.OverBackColor = Color.Red;
            _interactionStyleClass.DownBackColor = Color.Blue;

            BackColor = Color.White;
        }

        private Color _drawBackColor;

        //用于绘制的背景色
        [Browsable(false)]
        public Color DrawBackColor
        {
            get => _drawBackColor;
            set { _drawBackColor = value; Invalidate(); }
        }

        //储存原始属性的背景色
        public new Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                DrawBackColor = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(DrawBackColor);

            base.OnPaint(e);
        }

        private InteractionStyleClass _interactionStyleClass = new();
        private AnimationInfo _animationConfig;

        public InteractionStyleClass InteractionStyle
        {
            get => _interactionStyleClass;
            set => _interactionStyleClass = value;
        }

        public AnimationInfo AnimationConfig
        {
            get => _animationConfig;
            set => _animationConfig = value;
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class InteractionStyleClass
        {
            //不要使用 DefaultValue 设置默认值，否则窗体设计器中将无法设置为指定的默认值
            public Color OverBackColor { get; set; }

            public Color DownBackColor { get; set; }
        }

        private CancellationTokenSource cts = new();

        protected override void OnMouseEnter(EventArgs e)
        {
            Color newColor = InteractionStyle.OverBackColor;
            if (newColor != Color.Empty)
            {
                cts.Cancel();
                cts = new();
                _ = ControlAnimator.BezierTransition(DrawBackColor, newColor, AnimationConfig, value => DrawBackColor = value, true, cts.Token);
            }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            cts.Cancel();
            cts = new();
            _ = ControlAnimator.BezierTransition(DrawBackColor, BackColor, AnimationConfig, value => DrawBackColor = value, true, cts.Token);

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Color newColor = InteractionStyle.DownBackColor;
            if (newColor != Color.Empty)
            {
                cts.Cancel();
                cts = new();
                _ = ControlAnimator.BezierTransition(DrawBackColor, newColor, AnimationConfig, value => DrawBackColor = value, true, cts.Token);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            cts.Cancel();
            cts = new();

            Color restoreColor = InteractionStyle.OverBackColor == Color.Empty ? BackColor : InteractionStyle.OverBackColor;
            _ = ControlAnimator.BezierTransition(DrawBackColor, restoreColor, AnimationConfig, value => DrawBackColor = value, true, cts.Token);

            base.OnMouseUp(e);
        }
    }
}
