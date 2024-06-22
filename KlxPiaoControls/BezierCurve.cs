using KlxPiaoAPI;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace KlxPiaoControls
{
    /// <summary>
    /// 用于绘制贝塞尔曲线，可交互的组件。
    /// </summary>
    [DefaultEvent("控制点拖动")]
    public partial class BezierCurve : Control
    {
        /// <summary>
        /// 一个枚举类型，用于控制辅助线显示的方式
        /// </summary>
        public enum 辅助线绘制
        {
            /// <summary>
            /// 两端绘制为实线，中间绘制为虚线
            /// </summary>
            两端实线_中间虚线,
            /// <summary>
            /// 仅绘制两端的辅助线
            /// </summary>
            仅绘制两端辅助线,
            /// <summary>
            /// 不绘制辅助线
            /// </summary>
            不绘制
        }

        /// <summary>
        /// 表示 <see cref="BezierCurve"/> 控制点拖动时的限制方式。
        /// </summary>
        public enum 限制方式
        {
            /// <summary>
            /// 限制x为0到1之间。
            /// </summary>
            仅X,
            /// <summary>
            /// 限制Y为0到1之间。
            /// </summary>
            仅Y,
            /// <summary>
            /// 限制X和Y为0到1之间。
            /// </summary>
            X和Y,
            /// <summary>
            /// 不做任何限制。
            /// </summary>
            不限制
        }

        private float _扫描线进度;
        private Color _扫描线颜色;

        /// <summary>
        /// 扫描线的进度。
        /// </summary>
        [Browsable(false)]
        public float 扫描线进度
        {
            get { return _扫描线进度; }
            set
            {
                _扫描线进度 = value;
                Refresh();
            }
        }

        [Category("BezierCurve属性")]
        [Description("扫描线的颜色，设置扫描线进度以绘制扫描线")]
        public Color 扫描线颜色
        {
            get { return _扫描线颜色; }
            set
            {
                _扫描线颜色 = value;
                Refresh();
            }
        }

        private List<PointF> _控制点集合;
        private float _绘制精度;
        private bool _可拖动两端;
        private Size _内部大小;
        private ContentAlignment _内部位置;
        private bool _拖动时显示控制点信息;
        private string _控制点显示格式;
        private int _保留小数位数;
        private bool _吸附顶点;
        private 限制方式 _拖动限制;

        private Color _曲线颜色;
        private Color _控制点颜色;
        private Color _辅助线颜色;
        private Color _开始点结束点颜色;
        private int _曲线大小;
        private int _控制点大小;
        private int _辅助线大小;
        private 辅助线绘制 _辅助线显示方式;
        private bool _显示控制点;
        private int _边框大小;
        private Color _边框颜色;

        public event EventHandler<ControlPointDrag>? 控制点拖动;
        protected virtual void OnControlPointDrag(ControlPointDrag e)
        {
            控制点拖动?.Invoke(this, e);
        }
        //控制点拖动事件
        public class ControlPointDrag(int index, PointF controlPoint) : EventArgs
        {
            public int DragIndex { get; } = index;
            public PointF DragControlPoint { get; } = controlPoint;
        }

        //控制点集合改变事件
        public event PropertyChangedEventHandler? 控制点改变;
        protected virtual void OnControlPointChanged(string propertyName)
        {
            控制点改变?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BezierCurve()
        {
            InitializeComponent();

            _控制点集合 = [new PointF(0, 0), new PointF(0.85F, 0.15F), new PointF(0.15F, 0.85F), new PointF(1, 1)];
            _绘制精度 = 0.005F;
            _可拖动两端 = false;
            _内部大小 = new Size(200, 200);
            _内部位置 = ContentAlignment.MiddleCenter;
            _拖动时显示控制点信息 = true;
            _控制点显示格式 = "({index}) X:{x},Y:{y}";
            _保留小数位数 = 2;
            _吸附顶点 = true;
            _拖动限制 = 限制方式.仅X;

            _曲线颜色 = Color.Black;
            _控制点颜色 = Color.Red;
            _开始点结束点颜色 = Color.Blue;
            _辅助线颜色 = Color.Black;
            _曲线大小 = 2;
            _控制点大小 = 8;
            _辅助线大小 = 1;
            _辅助线显示方式 = 辅助线绘制.两端实线_中间虚线;
            _显示控制点 = true;
            _边框大小 = 2;
            _边框颜色 = Color.Gray;

            DoubleBuffered = true;
            Size = new Size(200, 200);

            SetStyle(ControlStyles.Selectable, true);
        }

        /// <summary>
        /// 获取或设置控制点集合。
        /// </summary>
        [Category("BezierCurve属性")]
        [Description("贝塞尔曲线的控制点集合")]
        public List<PointF> 控制点集合
        {
            get { return _控制点集合; }
            set
            {
                _控制点集合 = value;
                Invalidate();
                OnControlPointChanged(nameof(控制点集合));
            }
        }

        #region BezierCurve属性
        /// <summary>
        /// 曲线绘制的精度，数值越小精度越高。
        /// </summary>
        [Category("BezierCurve属性")]
        [Description("曲线绘制的精度，数值越小精度越高")]
        [DefaultValue(0.005F)]
        public float 绘制精度
        {
            get { return _绘制精度; }
            set { _绘制精度 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否可拖动开始点和结束点。
        /// </summary>
        [Category("BezierCurve属性")]
        [Description("是否可拖动曲线的两端（开始点和结束点）")]
        [DefaultValue(false)]
        public bool 可拖动两端
        {
            get { return _可拖动两端; }
            set { _可拖动两端 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置内部的大小。
        /// </summary>
        [Category("BezierCurve属性")]
        [Description("0-1范围内所占用的大小（像素）")]
        [DefaultValue(typeof(Size), "200,200")]
        public Size 内部大小
        {
            get { return _内部大小; }
            set { _内部大小 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置内部的位置。
        /// </summary>
        [Category("BezierCurve属性")]
        [Description("0-1范围内矩形所在的位置")]
        [DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment 内部位置
        {
            get { return _内部位置; }
            set { _内部位置 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置拖动时是否显示控制点信息。
        /// </summary>
        [Category("BezierCurve属性")]
        [Description("拖动时是否显示控制点坐标")]
        [DefaultValue(true)]
        public bool 拖动时显示控制点信息
        {
            get { return _拖动时显示控制点信息; }
            set { _拖动时显示控制点信息 = value; }
        }
        /// <summary>
        /// 获取或设置显示控制点信息的格式。
        /// </summary>
        /// <remarks>占位符：<br/>- {index}：索引<br/>- {x}：X<br/>- {y}：Y。</remarks>
        [Category("BezierCurve属性")]
        [Description("控制点显示的格式，拖动时显示控制点信息为True时生效")]
        [DefaultValue("({index}) X:{x},Y:{y}")]
        public string 控制点显示格式
        {
            get { return _控制点显示格式; }
            set { _控制点显示格式 = value; Invalidate(); }
        }
        /// <summary>
        /// 拖动设置控制点的值精度。
        /// </summary>
        [Category("BezierCurve属性")]
        [Description("每个控制点保留的小数位数")]
        [DefaultValue(2)]
        public int 保留小数位数
        {
            get { return _保留小数位数; }
            set { _保留小数位数 = value; Invalidate(); }
        }
        /// <summary>
        ///控制点拖动时是否自动吸附顶点。
        /// </summary>
        [Category("BezierCurve属性")]
        [Description("拖动时是否自动吸附顶点")]
        [DefaultValue(true)]
        public bool 吸附顶点
        {
            get { return _吸附顶点; }
            set { _吸附顶点 = value; }
        }
        /// <summary>
        /// 对控制点拖动时做出的限制。
        /// </summary>
        [Category("BezierCurve属性")]
        [Description("表示控制点拖动时的限制方式。")]
        [DefaultValue(typeof(限制方式), "仅X")]
        public 限制方式 拖动限制
        {
            get { return _拖动限制; }
            set { _拖动限制 = value; }
        }
        #endregion

        #region BezierCurve外观
        /// <summary>
        /// 获取或设置曲线的颜色。
        /// </summary>
        [Category("BezierCurve外观")]
        [Description("曲线的颜色")]
        [DefaultValue(typeof(Color), "Black")]
        public Color 曲线颜色
        {
            get { return _曲线颜色; }
            set { _曲线颜色 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置控制点的颜色。
        /// </summary>
        [Category("BezierCurve外观")]
        [Description("控制点的颜色")]
        [DefaultValue(typeof(Color), "Red")]
        public Color 控制点颜色
        {
            get { return _控制点颜色; }
            set { _控制点颜色 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置开始点或结束点的颜色。
        /// </summary>
        [Category("BezierCurve外观")]
        [Description("开始点和结束点的颜色")]
        [DefaultValue(typeof(Color), "Blue")]
        public Color 开始点结束点颜色
        {
            get { return _开始点结束点颜色; }
            set { _开始点结束点颜色 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置辅助线的颜色。
        /// </summary>
        [Category("BezierCurve外观")]
        [Description("辅助线的颜色")]
        [DefaultValue(typeof(Color), "Black")]
        public Color 辅助线颜色
        {
            get { return _辅助线颜色; }
            set { _辅助线颜色 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置曲线的大小。
        /// </summary>
        [Category("BezierCurve外观")]
        [Description("曲线的大小（宽度）")]
        [DefaultValue(2)]
        public int 曲线大小
        {
            get { return _曲线大小; }
            set { _曲线大小 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置控制点的大小。
        /// </summary>
        [Category("BezierCurve外观")]
        [Description("控制点的大小")]
        [DefaultValue(8)]
        public int 控制点大小
        {
            get { return _控制点大小; }
            set { _控制点大小 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置辅助线的大小。
        /// </summary>
        [Category("BezierCurve外观")]
        [Description("辅助线的大小（宽度）")]
        [DefaultValue(1)]
        public int 辅助线大小
        {
            get { return _辅助线大小; }
            set { _辅助线大小 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置辅助线绘制方式。
        /// </summary>
        [Category("BezierCurve外观")]
        [Description("是否辅助线显示方式")]
        [DefaultValue(typeof(辅助线绘制), "两端实线_中间虚线")]
        public 辅助线绘制 辅助线显示方式
        {
            get { return _辅助线显示方式; }
            set { _辅助线显示方式 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置是否显示控制点。
        /// </summary>
        [Category("BezierCurve外观")]
        [Description("是否显示控制点")]
        [DefaultValue(true)]
        public bool 显示控制点
        {
            get { return _显示控制点; }
            set { _显示控制点 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的大小。
        /// </summary>
        [Category("BezierCurve外观")]
        [Description("边框的大小")]
        [DefaultValue(2)]
        public int 边框大小
        {
            get { return _边框大小; }
            set { _边框大小 = value; Invalidate(); }
        }
        /// <summary>
        /// 获取或设置边框的颜色。
        /// </summary>
        [Category("BezierCurve外观")]
        [Description("边框的颜色")]
        [DefaultValue(typeof(Color), "Gray")]
        public Color 边框颜色
        {
            get { return _边框颜色; }
            set { _边框颜色 = value; Invalidate(); }
        }
        #endregion

        [DefaultValue(typeof(Size), "200,200")]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }
        [DefaultValue(typeof(Padding), "0,0,0,0")]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; Refresh(); }
        }

        #region 方法
        /// <summary>
        /// 设置指定索引处的控制点。
        /// </summary>
        /// <param name="index">控制点索引。</param>
        /// <param name="value">要设置的控制点。</param>
        public void SetControlPoint(int index, PointF value)
        {
            if (index >= 控制点集合.Count)
            {
                throw new Exception("给定的索引不存在");
            }

            控制点集合[index] = value;
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
                控制点集合.Add(value);
            }
            else
            {
                控制点集合.Insert(index, value);
            }
            Invalidate();
        }
        /// <summary>
        /// 移除指定索引处的控制点。
        /// </summary>
        /// <param name="index">要移除的控制点的索引，默认为-1，表示移除最后一个控制点。</param>
        public void RemoveControlPoint(int index = -1)
        {
            if (控制点集合.Count > 2 && index <= 控制点集合.Count - 1)
            {
                if (index == -1)
                {
                    控制点集合.RemoveAt(控制点集合.Count - 1);
                }
                else
                {
                    控制点集合.RemoveAt(index);
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
        public RectangleF 获取工作区矩形()
        {
            return 绘制区域;
        }
        /// <summary>
        /// 获取正在拖动到控制点索引
        /// </summary>
        public int GetDragIndex()
        {
            return 拖动的索引;
        }
        #endregion

        private readonly List<PointF> 绘制数组 = [];
        private RectangleF 绘制区域;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle thisRect = new(0, 0, Width, Height);
            RectangleF 工作区 = new(LayoutUtilities.CalculateAlignedPosition(thisRect, 内部大小, 内部位置, Padding), 内部大小);

            //绘制边框
            if (边框大小 != 0)
            {
                g.DrawRectangle(new Pen(边框颜色, 边框大小), 工作区);
            }

            //除边框以外的工作区
            绘制区域 = new(工作区.X + 边框大小 / 2, 工作区.Y + 边框大小 / 2, 工作区.Width - 边框大小, 工作区.Height - 边框大小);

            //将控制点集合添加到绘制数组，以便后续绘制
            绘制数组.Clear();
            foreach (PointF pointF in 控制点集合)
            {
                PointF newPointF = new(绘制区域.X + pointF.X * 绘制区域.Width, 绘制区域.Y + (1 - pointF.Y) * 绘制区域.Height);
                绘制数组.Add(newPointF);
            }

            //绘制辅助线
            if (辅助线显示方式 != 辅助线绘制.不绘制 && 辅助线大小 != 0)
            {
                绘制辅助线(g, 绘制数组);

                void 绘制辅助线(Graphics g, List<PointF> points)
                {
                    Pen pen1 = new(辅助线颜色, 辅助线大小);                           //实线画笔
                    Pen pen2 = new(辅助线颜色, 辅助线大小) { DashPattern = [8, 4] };  //虚线画笔

                    if (points.Count <= 2) return;  //控制点数量小于等于2时不绘制辅助线

                    switch (辅助线显示方式)
                    {
                        case 辅助线绘制.两端实线_中间虚线:
                            for (int i = 0; i < points.Count - 1; i++)
                            {
                                Pen drawPen = (i != 0 && i != points.Count - 2) ? pen2 : pen1;  //两端以外使用虚线
                                g.DrawLine(drawPen, points[i], points[i + 1]);
                            }
                            break;
                        case 辅助线绘制.仅绘制两端辅助线:
                            g.DrawLine(pen1, points[0], points[1]);
                            g.DrawLine(pen1, points[^1], points[^2]);  //old: [points.Count - 1], points[points.Count - 2]
                            break;
                    }
                }
            }

            //绘制曲线
            for (float i = 0; i <= 1; i += 绘制精度)
            {
                PointF pointF = KlxPiaoAPI.BezierCurve.CalculateBezierPointByTime(i, [.. 控制点集合]);
                SolidBrush bezBrush = new(Color.Red);
                g.FillEllipse(bezBrush, new RectangleF(
                    new PointF(
                        绘制区域.X + pointF.X * 绘制区域.Width - 曲线大小 / 2,
                        绘制区域.Y + (1 - pointF.Y) * 绘制区域.Height - 曲线大小 / 2),
                    new SizeF(曲线大小, 曲线大小)
                    ));
            }

            //绘制控制点
            if (显示控制点)
            {
                //使其控制点能覆盖端点
                DrawPointControl(0);
                DrawPointControl(绘制数组.Count - 1);

                for (int i = 1; i < 绘制数组.Count - 1; i++)
                {
                    DrawPointControl(i);
                }

                void DrawPointControl(int index)
                {
                    Color drawColor = (index == 0 || index == 绘制数组.Count - 1) ? 开始点结束点颜色 : 控制点颜色;
                    PointF pointF = 绘制数组[index];
                    SolidBrush controlPointBrush = new(drawColor);
                    g.FillEllipse(controlPointBrush, new RectangleF(
                        new PointF(
                            pointF.X - 控制点大小 / 2,
                            pointF.Y - 控制点大小 / 2),
                        new Size(控制点大小, 控制点大小)
                        ));
                }
            }

            //绘制控制点信息
            if (拖动时显示控制点信息)
            {
                PointF dragPointF = 控制点集合[拖动的索引];

                var replacements = new Dictionary<string, string>
                {
                    { "{index}", 拖动的索引.ToString() },
                    { "{x}", dragPointF.X.ToString() },
                    { "{y}", dragPointF.Y.ToString() }
                };

                g.DrawString(控制点显示格式.ReplaceMultiple(replacements), Font, new SolidBrush(ForeColor), 绘制数组[拖动的索引]);
            }

            //绘制扫描线
            if (扫描线进度 > 0 && 扫描线进度 < 1)
            {
                g.DrawLine(new Pen(扫描线颜色, 1),
                    new Point((int)(绘制区域.X + 绘制区域.Width * 扫描线进度), (int)绘制区域.Y),
                    new Point((int)(绘制区域.X + 绘制区域.Width * 扫描线进度), (int)绘制区域.Bottom)
                    );
            }

            base.OnPaint(e);
        }

        //控制点拖动
        private bool 正在拖动;
        private int 拖动的索引;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                float minDistance = float.MaxValue;
                拖动的索引 = -1;

                int 开始索引 = 可拖动两端 ? 0 : 1;
                int 结束索引 = 可拖动两端 ? 绘制数组.Count - 1 : 绘制数组.Count - 2;

                //计算距离最近的点，进行拖动
                for (int i = 开始索引; i <= 结束索引; i++)
                {
                    PointF controlPoint = 绘制数组[i];
                    float distance = (e.X - controlPoint.X) * (e.X - controlPoint.X) +
                                     (e.Y - controlPoint.Y) * (e.Y - controlPoint.Y);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        拖动的索引 = i;
                    }
                }

                if (拖动的索引 != -1)
                {
                    正在拖动 = !(!可拖动两端 && (拖动的索引 == 0 || 拖动的索引 == 控制点集合.Count - 1));
                }

                Focus();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (正在拖动)
            {
                OnControlPointDrag(new ControlPointDrag(拖动的索引, 控制点集合[拖动的索引]));

                float newX = (float)Math.Round((e.X - 绘制区域.X + 1) / 绘制区域.Width, 保留小数位数);
                float newY = (float)Math.Round(1 - (e.Y - 绘制区域.Y + 1) / 绘制区域.Height, 保留小数位数);

                switch (拖动限制)
                {
                    case 限制方式.仅X:
                        if (newX < 0) newX = 0;
                        if (newX > 1) newX = 1;
                        break;

                    case 限制方式.仅Y:
                        if (newY < 0) newY = 0;
                        if (newY > 1) newY = 1;
                        break;

                    case 限制方式.X和Y:
                        if (newX < 0) newX = 0;
                        if (newX > 1) newX = 1;
                        if (newY < 0) newY = 0;
                        if (newY > 1) newY = 1;
                        break;
                }

                PointF pointF = new(newX, newY);

                if (吸附顶点)
                {
                    //定义顶点的坐标
                    PointF[] vertices = [new(0, 0), new(1, 0), new(0, 1), new(1, 1)];

                    //检查是否在任意顶点的圆形范围内
                    float snapRadius = 10F / 绘制区域.Width; // 转换为0-1范围的单位
                    foreach (var vertex in vertices)
                    {
                        if (Math.Sqrt(Math.Pow(pointF.X - vertex.X, 2) + Math.Pow(pointF.Y - vertex.Y, 2)) <= snapRadius)
                        {
                            pointF = vertex;
                            break;
                        }
                    }
                }

                SetControlPoint(拖动的索引, pointF);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            正在拖动 = false;
        }

        //键盘细微调整控制点
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (拖动的索引 != -1)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Left:
                    case Keys.Right:
                        e.IsInputKey = true;
                        PointF 移动后的值 = 控制点集合[拖动的索引];

                        switch (e.KeyCode)
                        {
                            case Keys.Up:
                                移动后的值.Y += 0.01F;
                                break;
                            case Keys.Down:
                                移动后的值.Y -= 0.01F;
                                break;
                            case Keys.Left:
                                移动后的值.X -= 0.01F;
                                break;
                            case Keys.Right:
                                移动后的值.X += 0.01F;
                                break;
                        }

                        SetControlPoint(拖动的索引, new PointF((float)Math.Round(移动后的值.X, 保留小数位数), (float)Math.Round(移动后的值.Y, 保留小数位数)));
                        OnControlPointDrag(new ControlPointDrag(拖动的索引, 控制点集合[拖动的索引]));
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