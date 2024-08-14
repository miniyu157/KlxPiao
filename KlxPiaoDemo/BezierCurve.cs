using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoDemo
{
    /// <summary>
    /// 用于绘制贝塞尔曲线，可交互的组件。
    /// </summary>
    [DefaultEvent("ControlPointChanged")]
    public partial class BezierCurve : Control
    {
        #region enums
        /// <summary>
        /// 一个枚举类型，用于表示辅助线绘制的方式
        /// </summary>
        public enum GuidelineDrawMode
        {
            /// <summary>
            /// 两端绘制为实线，中间绘制为虚线
            /// </summary>
            BothEndsSolid_MiddleDashed,
            /// <summary>
            /// 仅绘制两端的辅助线
            /// </summary>
            DrawOnlyBothEndsGuidelines,
            /// <summary>
            /// 不绘制辅助线
            /// </summary>
            DoNotDraw
        }

        /// <summary>
        /// 表示 <see cref="BezierCurve"/> 控制点拖动时的限制方式的枚举。
        /// </summary>
        public enum ConstraintMode
        {
            /// <summary>
            /// 限制 x 为 0 到 1 之间。
            /// </summary>
            XAxisOnly,
            /// <summary>
            /// 限制 Y 为 0 到 1 之间。
            /// </summary>
            YAxisOnly,
            /// <summary>
            /// 限制 X 和 Y 为 0 到 1 之间。
            /// </summary>
            XYBoth,
            /// <summary>
            /// 不做任何限制。
            /// </summary>
            None
        }
        #endregion

        #region private properties
        //基本外观
        private int _borderSize;
        private Color _borderColor;
        private Size _zeroToOneSize;
        private ContentAlignment _zeroToOnePosition;
        private Point _ZeroToOneOffset;

        //曲线外观
        private Color _curveColor;
        private Color _controlPointColor;
        private Color _guidelineColor;
        private Color _startAndEndPointColor;
        private int _curveSize;
        private int _controlPointSize;
        private int _guidelineSize;
        private GuidelineDrawMode _guidelineDraw;
        private float _drawingAccuracy; //绘制精度
        private bool _isDisplayControlPoint;

        //基本属性
        private List<PointF> _controlPoints;
        private int _decimalPlaces; //保留小数位数
        private bool _isStartAndEndPointDraggable;
        private bool _isEnableAutoAdsorption; //自动吸附
        private ConstraintMode _dragConstraint; //拖动限制
        private bool _isDisplayControlPointTextWhileDragging; //拖动时显示文本
        private string _controlPointTextDisplayFormat;
        #endregion

        #region events
        /// <summary>
        /// 表示当控制点发生变化时引发的事件。
        /// </summary>
        /// <remarks>
        /// 使用指定的索引和控制点初始化 <see cref="ControlPointChangedEvent"/> 类的新实例。
        /// </remarks>
        public class ControlPointChangedEvent(int index, PointF controlPoint) : EventArgs
        {
            /// <summary>
            /// 获取正在拖动的控制点的索引。
            /// </summary>
            public int DragIndex { get; } = index;

            /// <summary>
            /// 获取正在拖动的控制点。
            /// </summary>
            public PointF DragControlPoint { get; } = controlPoint;
        }

        /// <summary>
        /// 当用户更改控制点时引发的事件。
        /// </summary>
        public event EventHandler<ControlPointChangedEvent>? ControlPointChanged;

        /// <summary>
        /// 引发 <see cref="ControlPointChanged"/> 事件。
        /// </summary>
        protected virtual void OnControlPointChanged(ControlPointChangedEvent e)
        {
            ControlPointChanged?.Invoke(this, e);
        }

        /// <summary>
        /// 非用户更改控制点时引发的事件。
        /// </summary>
        public event EventHandler<EventArgs>? ControlPointChangedByCode;

        /// <summary>
        /// 引发 <see cref="ControlPointChangedByCode"/> 事件。
        /// </summary>
        protected virtual void OnControlPointChangedByCode(EventArgs e)
        {
            ControlPointChangedByCode?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        public BezierCurve()
        {
            InitializeComponent();

            _borderSize = 2;
            _borderColor = Color.Gray;
            _zeroToOneSize = new Size(200, 200);
            _zeroToOnePosition = ContentAlignment.MiddleCenter;
            _ZeroToOneOffset = Point.Empty;

            _curveColor = Color.Red;
            _controlPointColor = Color.Red;
            _guidelineColor = Color.Black;
            _startAndEndPointColor = Color.Blue;
            _curveSize = 2;
            _controlPointSize = 8;
            _guidelineSize = 1;
            _guidelineDraw = GuidelineDrawMode.BothEndsSolid_MiddleDashed;
            _drawingAccuracy = 0.005F;
            _isDisplayControlPoint = true;

            _controlPoints = [new PointF(0, 0), new PointF(0.85F, 0.15F), new PointF(0.15F, 0.85F), new PointF(1, 1)];
            _decimalPlaces = 2;
            _isStartAndEndPointDraggable = false;
            _isEnableAutoAdsorption = true;
            _dragConstraint = ConstraintMode.XAxisOnly;
            _isDisplayControlPointTextWhileDragging = true;
            _controlPointTextDisplayFormat = "({index}) X:{x},Y:{y}";

            DoubleBuffered = true;
            Size = new Size(200, 200);

            SetStyle(ControlStyles.Selectable, true);
        }

        #region BezierCurve Basic Appearance
        /// <summary>
        /// 获取或设置边框的大小。
        /// </summary>
        [Category("BezierCurve Basic Appearance")]
        [Description("边框的大小")]
        [DefaultValue(2)]
        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的颜色。
        /// </summary>
        [Category("BezierCurve Basic Appearance")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "Gray")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置内部的大小。
        /// </summary>
        [Category("BezierCurve Basic Appearance")]
        [Description("0-1范围内所占用的大小（像素）")]
        [DefaultValue(typeof(Size), "200,200")]
        public Size ZeroToOneSize
        {
            get => _zeroToOneSize;
            set { _zeroToOneSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置内部的位置。
        /// </summary>
        [Category("BezierCurve Basic Appearance")]
        [Description("0-1范围内矩形所在的位置")]
        [DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment ZeroToOnePosition
        {
            get => _zeroToOnePosition;
            set { _zeroToOnePosition = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置内部位置的偏移。
        /// </summary>
        [Category("BezierCurve Basic Appearance")]
        [Description("0-1范围内矩形位置的偏移")]
        [DefaultValue(typeof(Point), "0,0")]
        public Point ZeroToOneOffset
        {
            get => _ZeroToOneOffset;
            set { _ZeroToOneOffset = value; Invalidate(); }
        }
        #endregion

        #region BezierCurve Curve Appearance
        /// <summary>
        /// 获取或设置曲线的颜色。
        /// </summary>
        [Category("BezierCurve Curve Appearance")]
        [Description("曲线的颜色")]
        [DefaultValue(typeof(Color), "Red")]
        public Color CurveColor
        {
            get => _curveColor;
            set { _curveColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置控制点的颜色。
        /// </summary>
        [Category("BezierCurve Curve Appearance")]
        [Description("控制点的颜色")]
        [DefaultValue(typeof(Color), "Red")]
        public Color ControlPointColor
        {
            get => _controlPointColor;
            set { _controlPointColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置辅助线的颜色。
        /// </summary>
        [Category("BezierCurve Curve Appearance")]
        [Description("辅助线的颜色")]
        [DefaultValue(typeof(Color), "Black")]
        public Color GuidelineColor
        {
            get => _guidelineColor;
            set { _guidelineColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置开始点或结束点的颜色。
        /// </summary>
        [Category("BezierCurve Curve Appearance")]
        [Description("开始点和结束点的颜色")]
        [DefaultValue(typeof(Color), "Blue")]
        public Color StartAndEndPointColor
        {
            get => _startAndEndPointColor;
            set { _startAndEndPointColor = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置曲线的大小。
        /// </summary>
        [Category("BezierCurve Curve Appearance")]
        [Description("曲线的大小（宽度）")]
        [DefaultValue(2)]
        public int CurveSize
        {
            get => _curveSize;
            set { _curveSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置控制点的大小。
        /// </summary>
        [Category("BezierCurve Curve Appearance")]
        [Description("控制点的大小")]
        [DefaultValue(8)]
        public int ControlPointSize
        {
            get => _controlPointSize;
            set { _controlPointSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置辅助线的大小。
        /// </summary>
        [Category("BezierCurve Curve Appearance")]
        [Description("辅助线的大小")]
        [DefaultValue(1)]
        public int GuidelineSize
        {
            get => _guidelineSize;
            set { _guidelineSize = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置辅助线绘制方式。
        /// </summary>
        [Category("BezierCurve Curve Appearance")]
        [Description("是否辅助线显示方式")]
        [DefaultValue(typeof(GuidelineDrawMode), "BothEndsSolid_MiddleDashed")]
        public GuidelineDrawMode GuidelineDraw
        {
            get => _guidelineDraw;
            set { _guidelineDraw = value; Invalidate(); }
        }
        /// <summary>
        /// 曲线绘制的精度，数值越小精度越高。
        /// </summary>
        [Category("BezierCurve Curve Appearance")]
        [Description("曲线绘制的精度，数值越小精度越高")]
        [DefaultValue(0.005F)]
        public float DrawingAccuracy
        {
            get => _drawingAccuracy;
            set { _drawingAccuracy = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否显示控制点。
        /// </summary>
        [Category("BezierCurve Curve Appearance")]
        [Description("是否显示控制点")]
        [DefaultValue(true)]
        public bool IsDisplayControlPoint
        {
            get => _isDisplayControlPoint;
            set { _isDisplayControlPoint = value; Invalidate(); }
        }
        #endregion

        #region BezierCurve Basic Properties
        /// <summary>
        /// 获取或设置控制点集合。
        /// </summary>
        [Category("BezierCurve Basic Properties")]
        [Description("贝塞尔曲线的控制点集合")]
        public List<PointF> ControlPoints
        {
            get => _controlPoints;
            set
            {
                _controlPoints = value;
                Invalidate();

                OnControlPointChangedByCode(new EventArgs());
            }
        }
        /// <summary>
        /// 拖动设置控制点的值精度。
        /// </summary>
        [Category("BezierCurve Basic Properties")]
        [Description("每个控制点保留的小数位数")]
        [DefaultValue(2)]
        public int DecimalPlaces
        {
            get => _decimalPlaces;
            set { _decimalPlaces = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否可拖动开始点和结束点。
        /// </summary>
        [Category("BezierCurve Basic Properties")]
        [Description("是否可拖动曲线的两端（开始点和结束点）")]
        [DefaultValue(false)]
        public bool IsStartAndEndPointDraggable
        {
            get => _isStartAndEndPointDraggable;
            set { _isStartAndEndPointDraggable = value; Invalidate(); }
        }
        /// <summary>
        ///控制点拖动时是否自动吸附顶点。
        /// </summary>
        [Category("BezierCurve Basic Properties")]
        [Description("拖动时是否自动吸附顶点")]
        [DefaultValue(true)]
        public bool IsEnableAutoAdsorption
        {
            get => _isEnableAutoAdsorption;
            set => _isEnableAutoAdsorption = value;
        }
        /// <summary>
        /// 对控制点拖动时做出的限制。
        /// </summary>
        [Category("BezierCurve Basic Properties")]
        [Description("表示控制点拖动时的限制方式。")]
        [DefaultValue(typeof(ConstraintMode), "XAxisOnly")]
        public ConstraintMode DragConstraint
        {
            get => _dragConstraint;
            set => _dragConstraint = value;
        }
        /// <summary>
        /// 获取或设置拖动时是否显示控制点信息。
        /// </summary>
        [Category("BezierCurve Basic Properties")]
        [Description("拖动时是否显示控制点坐标")]
        [DefaultValue(true)]
        public bool IsDisplayControlPointTextWhileDragging
        {
            get => _isDisplayControlPointTextWhileDragging;
            set => _isDisplayControlPointTextWhileDragging = value;
        }
        /// <summary>
        /// 获取或设置显示控制点信息的格式。
        /// </summary>
        /// <remarks>占位符：<br/>- {index}：索引<br/>- {x}：X<br/>- {y}：Y。</remarks>
        [Category("BezierCurve Basic Properties")]
        [Description("控制点显示的格式，拖动时显示控制点信息为True时生效")]
        [DefaultValue("({index}) X:{x},Y:{y}")]
        public string ControlPointTextDisplayFormat
        {
            get => _controlPointTextDisplayFormat;
            set { _controlPointTextDisplayFormat = value; Invalidate(); }
        }
        #endregion

        [DefaultValue(typeof(Size), "200,200")]
        public new Size Size
        {
            get => base.Size;
            set => base.Size = value;
        }

        #region method
        /// <summary>
        /// 设置指定索引处的控制点。
        /// </summary>
        /// <param name="index">控制点索引。</param>
        /// <param name="value">要设置的控制点。</param>
        public void SetControlPoint(int index, PointF value)
        {
            if (index >= ControlPoints.Count)
            {
                throw new Exception("给定的索引不存在");
            }

            ControlPoints[index] = value;
            Invalidate();
        }

        /// <summary>
        /// 添加一个控制点到数组中。
        /// </summary>
        /// <param name="value">要添加的控制点。</param>
        /// <param name="index">要插入的索引位置，默认为-1，表示在数组末尾添加。</param>
        public void AddControlPoint(PointF value, int index = -1)
        {
            if (index == -1)
            {
                ControlPoints.Add(value);
            }
            else
            {
                ControlPoints.Insert(index, value);
            }
            Invalidate();
        }

        /// <summary>
        /// 移除指定索引处的控制点。
        /// </summary>
        /// <param name="index">要移除的控制点的索引，默认为-1，表示移除最后一个控制点。</param>
        public void RemoveControlPoint(int index = -1)
        {
            if (ControlPoints.Count > 2 && index <= ControlPoints.Count - 1)
            {
                if (index == -1)
                {
                    ControlPoints.RemoveAt(ControlPoints.Count - 1);
                }
                else
                {
                    ControlPoints.RemoveAt(index);
                }
                Invalidate();
            }
            else
            {
                throw new Exception("控制点数量至少为2，或指定的索引不存在");
            }
        }

        /// <summary>
        /// 获取内部区域除边框外的工作区矩形
        /// </summary>
        public RectangleF GetClientRectangle() => drawRect;

        /// <summary>
        /// 获取正在拖动到控制点索引
        /// </summary>
        public int GetDragIndex() => dragingIndex;
        #endregion

        private readonly List<PointF> drawPoints = [];
        private RectangleF drawRect;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle thisRect = new(0, 0, Width, Height);
            RectangleF clientRect = new(LayoutUtilities.CalculateAlignedPosition(thisRect, ZeroToOneSize, ZeroToOnePosition, ZeroToOneOffset), ZeroToOneSize);

            //draw border
            if (BorderSize != 0)
            {
                g.DrawRectangle(new Pen(BorderColor, BorderSize), clientRect);
            }

            //client rectangle(no border)
            drawRect = new(clientRect.X + BorderSize / 2, clientRect.Y + BorderSize / 2, clientRect.Width - BorderSize, clientRect.Height - BorderSize);

            //control points convert to draw points
            drawPoints.Clear();
            foreach (PointF pointF in ControlPoints)
            {
                PointF newPointF = new(drawRect.X + pointF.X * drawRect.Width, drawRect.Y + (1 - pointF.Y) * drawRect.Height);
                drawPoints.Add(newPointF);
            }

            //draw guideline
            if (GuidelineDraw != GuidelineDrawMode.DoNotDraw && GuidelineSize != 0)
            {
                DrawGuideline(g, drawPoints);

                void DrawGuideline(Graphics g, List<PointF> points)
                {
                    Pen pen1 = new(GuidelineColor, GuidelineSize);                           //实线画笔
                    Pen pen2 = new(GuidelineColor, GuidelineSize) { DashPattern = [8, 4] };  //虚线画笔

                    if (points.Count <= 2) return;  //控制点数量小于等于2时不绘制辅助线

                    switch (GuidelineDraw)
                    {
                        case GuidelineDrawMode.BothEndsSolid_MiddleDashed:
                            for (int i = 0; i < points.Count - 1; i++)
                            {
                                Pen drawPen = (i != 0 && i != points.Count - 2) ? pen2 : pen1;  //两端以外使用虚线
                                g.DrawLine(drawPen, points[i], points[i + 1]);
                            }
                            break;

                        case GuidelineDrawMode.DrawOnlyBothEndsGuidelines:
                            g.DrawLine(pen1, points[0], points[1]);
                            g.DrawLine(pen1, points[^1], points[^2]);  //old: [points.Count - 1], points[points.Count - 2]
                            break;
                    }
                }
            }

            //draw curve
            for (float i = 0; i <= 1; i += DrawingAccuracy)
            {
                PointF pointF = KlxPiaoAPI.BezierCurve.CalculateBezierPointByTime(i, [.. ControlPoints]);
                SolidBrush bezBrush = new(CurveColor);
                g.FillEllipse(bezBrush, new RectangleF(
                    new PointF(
                        drawRect.X + pointF.X * drawRect.Width - CurveSize / 2,
                        drawRect.Y + (1 - pointF.Y) * drawRect.Height - CurveSize / 2),
                    new SizeF(CurveSize, CurveSize)
                    ));
            }

            //draw control point
            if (IsDisplayControlPoint)
            {
                //使其控制点能覆盖端点
                DrawPointControl(0);
                DrawPointControl(drawPoints.Count - 1);

                for (int i = 1; i < drawPoints.Count - 1; i++)
                {
                    DrawPointControl(i);
                }

                void DrawPointControl(int index)
                {
                    Color drawColor = (index == 0 || index == drawPoints.Count - 1) ? StartAndEndPointColor : ControlPointColor;
                    PointF pointF = drawPoints[index];
                    SolidBrush controlPointBrush = new(drawColor);
                    g.FillEllipse(controlPointBrush, new RectangleF(
                        new PointF(
                            pointF.X - ControlPointSize / 2,
                            pointF.Y - ControlPointSize / 2),
                        new Size(ControlPointSize, ControlPointSize)
                        ));
                }
            }

            //draw control point text
            if (IsDisplayControlPointTextWhileDragging)
            {
                PointF dragPointF = ControlPoints[dragingIndex];

                var replacements = new Dictionary<string, string>
                {
                    { "{index}", dragingIndex.ToString() },
                    { "{x}", dragPointF.X.ToString() },
                    { "{y}", dragPointF.Y.ToString() }
                };

                g.DrawString(ControlPointTextDisplayFormat.ReplaceMultiple(replacements), Font, new SolidBrush(ForeColor), drawPoints[dragingIndex]);
            }

            base.OnPaint(e);
        }

        //draw control point
        private bool isDraging;
        private int dragingIndex;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                float minDistance = float.MaxValue;
                dragingIndex = -1;

                int startIndex = IsStartAndEndPointDraggable ? 0 : 1;
                int endIndex = IsStartAndEndPointDraggable ? drawPoints.Count - 1 : drawPoints.Count - 2;

                //计算距离最近的点，进行拖动
                for (int i = startIndex; i <= endIndex; i++)
                {
                    PointF controlPoint = drawPoints[i];
                    float distance = (e.X - controlPoint.X) * (e.X - controlPoint.X) +
                                     (e.Y - controlPoint.Y) * (e.Y - controlPoint.Y);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        dragingIndex = i;
                    }
                }

                if (dragingIndex != -1)
                {
                    isDraging = !(!IsStartAndEndPointDraggable && (dragingIndex == 0 || dragingIndex == ControlPoints.Count - 1));
                }

                Focus();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isDraging)
            {
                OnControlPointChanged(new ControlPointChangedEvent(dragingIndex, ControlPoints[dragingIndex]));

                float newX = (float)Math.Round((e.X - drawRect.X + 1) / drawRect.Width, DecimalPlaces);
                float newY = (float)Math.Round(1 - (e.Y - drawRect.Y + 1) / drawRect.Height, DecimalPlaces);

                switch (DragConstraint)
                {
                    case ConstraintMode.XAxisOnly:
                        if (newX < 0) newX = 0;
                        if (newX > 1) newX = 1;
                        break;

                    case ConstraintMode.YAxisOnly:
                        if (newY < 0) newY = 0;
                        if (newY > 1) newY = 1;
                        break;

                    case ConstraintMode.XYBoth:
                        if (newX < 0) newX = 0;
                        if (newX > 1) newX = 1;
                        if (newY < 0) newY = 0;
                        if (newY > 1) newY = 1;
                        break;
                }

                PointF pointF = new(newX, newY);

                if (IsEnableAutoAdsorption)
                {
                    //定义顶点的坐标
                    PointF[] vertices = [new(0, 0), new(1, 0), new(0, 1), new(1, 1)];

                    //检查是否在任意顶点的圆形范围内
                    float snapRadius = 10F / drawRect.Width; // 转换为0-1范围的单位
                    foreach (var vertex in vertices)
                    {
                        if (Math.Sqrt(Math.Pow(pointF.X - vertex.X, 2) + Math.Pow(pointF.Y - vertex.Y, 2)) <= snapRadius)
                        {
                            pointF = vertex;
                            break;
                        }
                    }
                }

                SetControlPoint(dragingIndex, pointF);
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isDraging = false;

            base.OnMouseUp(e);
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (dragingIndex != -1)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Left:
                    case Keys.Right:
                        e.IsInputKey = true;

                        float newX = ControlPoints[dragingIndex].X;
                        float newY = ControlPoints[dragingIndex].Y;

                        switch (e.KeyCode)
                        {
                            case Keys.Up:
                                newY += 0.01F;
                                break;

                            case Keys.Down:
                                newY -= 0.01F;
                                break;

                            case Keys.Left:
                                newX -= 0.01F;
                                break;

                            case Keys.Right:
                                newX += 0.01F;
                                break;
                        }

                        switch (DragConstraint)
                        {
                            case ConstraintMode.XAxisOnly:
                                if (newX < 0) newX = 0;
                                if (newX > 1) newX = 1;
                                break;

                            case ConstraintMode.YAxisOnly:
                                if (newY < 0) newY = 0;
                                if (newY > 1) newY = 1;
                                break;

                            case ConstraintMode.XYBoth:
                                if (newX < 0) newX = 0;
                                if (newX > 1) newX = 1;
                                if (newY < 0) newY = 0;
                                if (newY > 1) newY = 1;
                                break;
                        }

                        SetControlPoint(dragingIndex, new PointF((float)Math.Round(newX, DecimalPlaces), (float)Math.Round(newY, DecimalPlaces)));
                        OnControlPointChanged(new ControlPointChangedEvent(dragingIndex, ControlPoints[dragingIndex]));
                        break;

                    default:
                        e.IsInputKey = false;
                        break;
                }
            }

            base.OnPreviewKeyDown(e);
        }
    }
}