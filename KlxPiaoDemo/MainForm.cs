using KlxPiaoAPI;
using KlxPiaoControls;
using KlxPiaoDemo.Properties;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;

namespace KlxPiaoDemo
{
    public partial class MainForm : KlxPiaoForm
    {
        public MainForm()
        {
            InitializeComponent();
            Load += MainWindow_Load;
        }

        private readonly int[] 随机颜色范围 = [195, 255];
        private readonly Random rand = new();
        private void MainWindow_Load(object? sender, EventArgs e)
        {
            //暂时解决渲染错误
            slideSwitch9.SelectIndex = 1;
            slideSwitch9.SelectIndex = 0;

            Text = $"{KlxPiaoControlsInfo.GetProductName()} & {KlxPiaoAPIInfo.GetProductName()} {KlxPiaoControlsInfo.GetProductVersion()} Demo";

            #region KlxPiaoControls.PictureBox
            Pic_SizeTrack.Value = klxPiaoPictureBox1.Width;
            Pic_SizeTrack.MinValue = klxPiaoPictureBox1.Width / 2;
            Pic_SizeTrack.MaxValue = (float)(klxPiaoPictureBox1.Width * 1.2F);
            Pic_BorderTrack.Value = klxPiaoPictureBox1.BorderSize;
            Pic_RoundedTrack.Value = klxPiaoPictureBox1.BorderCornerRadius.TopLeft;

            Pic_BorderTrack.ValueChanged += Pic_Track_ValueChanged;
            Pic_RoundedTrack.ValueChanged += Pic_Track_ValueChanged;
            Pic_SizeTrack.ValueChanged += Pic_Track_ValueChanged;

            slideSwitch9.SelectIndexChanged += SlideSwitch9_SelectIndexChanged;


            void SlideSwitch9_SelectIndexChanged(object? sender, SlideSwitch.IndexChangedEventArgs e)
            {
                klxPiaoPictureBox1.BorderCornerRadius = slideSwitch9.SelectIndex == 0
                                ? new CornerRadius(Pic_RoundedTrack.Value)
                                : new CornerRadius(Pic_RoundedTrack.Value * klxPiaoPictureBox1.Width);
            }

            void Pic_Track_ValueChanged(object? sender, KlxPiaoTrackBar.ValueChangedEventArgs e)
            {
                if (sender is KlxPiaoTrackBar c)
                {
                    switch (c.Name)
                    {
                        //边框大小
                        case "Pic_BorderTrack":
                            klxPiaoPictureBox1.BorderSize = (int)e.Value;
                            break;

                        //圆角大小
                        case "Pic_RoundedTrack":
                            klxPiaoPictureBox1.BorderCornerRadius = slideSwitch9.SelectIndex == 0
                                ? new CornerRadius(e.Value)
                                : new CornerRadius(e.Value * klxPiaoPictureBox1.Width);
                            break;

                        //大小
                        case "Pic_SizeTrack":
                            klxPiaoPictureBox1.Size = new Size((int)e.Value, (int)e.Value);
                            panel2.Left = klxPiaoPictureBox1.Left + 25 + klxPiaoPictureBox1.Width;
                            break;
                    }
                }
            }
            #endregion

            #region KlxPiaoControls.Panel
            TabPage_UI_Panel.ForEachControl<KlxPiaoPanel>(panel => panel.Click += UI_Panel_Click);

            void UI_Panel_Click(object? sender, EventArgs e)
            {
                if (sender is KlxPiaoPanel panel)
                {
                    Graphics g = panel.CreateGraphics();
                    g.DrawRectangle(new Pen(Color.Red, 1), panel.GetClientRectangle());
                }
            }
            #endregion

            #region ThemeEidt
            foreach (Control p in klxPiaoPanel7.Controls)
            {
                p.Click += ThemeEditor_Click;
                p.Paint += ThemeEditor_Paint;
            }
            klxPiaoTrackBar4.Value = InteractionColorScale;

            //生成随机的颜色
            int 横边距 = 6;
            int 纵边距 = 6;
            Point 位置 = new(11, 19);
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 5; y++)
                {
                    KlxPiaoPanel p = new()
                    {
                        BorderSize = 1,
                        IsEnableShadow = false,
                        CornerRadius = new CornerRadius(0.36F),
                        Size = new Size(45, 45),
                        Cursor = Cursors.Hand,
                        BackColor = Color.FromArgb(rand.Next(随机颜色范围[0], 随机颜色范围[1]), rand.Next(随机颜色范围[0], 随机颜色范围[1]), rand.Next(随机颜色范围[0], 随机颜色范围[1]))
                    };
                    p.Location = new Point(位置.X + x * (p.Width + 横边距), 位置.Y + y * (p.Height + 纵边距));
                    p.MouseClick += RandomColorPanel_MouseClick;
                    klxPiaoPanel10.Controls.Add(p);
                }
            }
            #endregion

            #region KlxPiaoControls.TextBox
            checkBox10.CheckedChanged += CheckBox10_CheckedChanged;
            void CheckBox10_CheckedChanged(object? sender, EventArgs e) =>
                klxPiaoTextBox1.IsFillAndMultiline = checkBox10.Checked;

            checkBox11.CheckedChanged += CheckBox11_CheckedChanged;
            void CheckBox11_CheckedChanged(object? sender, EventArgs e) =>
                klxPiaoTextBox1.TextBox.BackColor = checkBox11.Checked ? Color.DarkGray : Color.White;

            EnumUtility.ForEachEnum<ContentAlignment>(value => comboBox7.Items.Add(value));

            comboBox7.SelectedIndex = 0;
            comboBox7.SelectedIndexChanged += ComboBox7_SelectedIndexChanged;
            pointBar1.ValueChanged += PointBar1_值Changed;

            void PointBar1_值Changed(object? sender, PointBar.ValueChangedEvent e)
            {
                klxPiaoTextBox1.TextBoxOffset = e.Point;
            }

            void ComboBox7_SelectedIndexChanged(object? sender, EventArgs e)
            {
                klxPiaoTextBox1.TextBoxAlign = EnumUtility.ReorderEnumValues<ContentAlignment>(comboBox7.SelectedIndex);
            }
            #endregion

            #region Home
            EnumUtility.ForEachEnum<TitleButtonStyle>(value => comboBox1.Items.Add(value));
            comboBox1.SelectedIndex = (int)TitleButtons;

            EnumUtility.ForEachEnum<WindowPosition>(value => comboBox2.Items.Add(value));
            comboBox2.SelectedIndex = (int)ShortcutResizeMode;

            EnumUtility.ForEachEnum<WindowPosition>(value => comboBox3.Items.Add(value));
            comboBox3.SelectedIndex = (int)DragMode;

            EnumUtility.ForEachEnum<HorizontalAlignment>(value => comboBox4.Items.Add(value));
            comboBox4.SelectedIndex = (int)TitleTextAlign;

            EnumUtility.ForEachEnum<HorizontalEnds>(value => comboBox5.Items.Add(value));
            comboBox5.SelectedIndex = (int)TitleButtonAlign;

            EnumUtility.ForEachEnum<Style>(value => comboBox6.Items.Add(value));
            comboBox6.SelectedIndex = (int)Theme;

            klxPiaoTrackBar1.Value = TitleBoxHeight;
            klxPiaoTrackBar2.Value = TitleTextMargin;
            klxPiaoTrackBar3.Value = TitleButtonWidth;
            klxPiaoTrackBar5.Value = TitleButtonIconSize.Width;

            checkBox1.Checked = EnableResizeAnimation;
            checkBox2.Checked = Resizable;
            checkBox3.Checked = ShowIcon;
            #endregion

            #region 属性代码生成器
            klxPiaoPanel6.ForEachControl<TextBox>(textBox => { textBox.TextChanged += 生成代码; });
            #endregion
        }

        #region 属性代码生成器
        private void 生成代码(object? sender, EventArgs e)
        {
            string 名称 = textBox1.Text;
            string 类型 = textBox2.Text;
            string 默认值 = textBox6.Text;
            string 类别 = textBox20.Text;
            string 描述 = textBox19.Text;

            if (slideSwitch6.SelectIndex == 0)
            {
                textBox3.Text = $"private {类型} _{名称};";
                textBox4.Text = $"_{名称} = {默认值};";
                textBox5.Text = $"        /// <summary>\r\n        /// {描述}。\r\n        /// </summary>\r\n        [Category(\"{类别}\")]\r\n        [Description(\"{描述}\")]\r\n        [DefaultValue(typeof({类型}), \"{默认值}\")]\r\n        public {类型} {名称}\r\n        {{\r\n            get {{ return _{名称}; }}\r\n            set {{ _{名称} = value; Invalidate(); }}\r\n        }}";
            }
            else if (slideSwitch6.SelectIndex == 1)
            {
                textBox3.Text = $"Dim _{名称} As {类型}";
                textBox4.Text = $"_{名称} = {默认值}";
                textBox5.Text = $"        ''' <summary>\r\n        ''' {描述}。\r\n        ''' </summary>\r\n        <Category(\"{类别}\")>\r\n        <Description(\"{描述}\")>\r\n        <DefaultValue(GetType({类型}), \"{默认值}\")>\r\n        Public Property {名称} As {类型}\r\n            Get\r\n                Return _{名称}\r\n            End Get\r\n            Set(value As {类型})\r\n                _{名称} = value\r\n                Invalidate()\r\n            End Set\r\n        End Property";
            }
        }
        private void SlideSwitch6_SelectIndexChanged(object sender, EventArgs e)
        {
            生成代码(sender, e);
        }
        #endregion

        #region 调整菜单
        private void KlxPiaoButton2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3; //皮肤编辑器的索引
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TitleButtons = (TitleButtonStyle)comboBox1.SelectedIndex;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShortcutResizeMode = (WindowPosition)comboBox2.SelectedIndex;
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DragMode = (WindowPosition)comboBox3.SelectedIndex;
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            TitleTextAlign = (HorizontalAlignment)comboBox4.SelectedIndex;
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            TitleButtonAlign = (HorizontalEnds)comboBox5.SelectedIndex;
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            Theme = (Style)comboBox6.SelectedIndex;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            EnableResizeAnimation = checkBox1.Checked;
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            Resizable = checkBox2.Checked;
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            ShowIcon = checkBox3.Checked;
        }

        private void KlxPiaoTrackBar1_值Changed(object sender, KlxPiaoTrackBar.ValueChangedEventArgs e)
        {
            TitleBoxHeight = (int)klxPiaoTrackBar1.Value;
            tabControl1.Top = TitleBoxHeight + 9;
        }

        private void KlxPiaoTrackBar2_值Changed(object sender, KlxPiaoTrackBar.ValueChangedEventArgs e)
        {
            TitleTextMargin = (int)klxPiaoTrackBar2.Value;
        }

        private void KlxPiaoTrackBar3_值Changed(object sender, KlxPiaoTrackBar.ValueChangedEventArgs e)
        {
            TitleButtonWidth = (int)klxPiaoTrackBar3.Value;
        }

        private void KlxPiaoTrackBar5_值Changed(object sender, KlxPiaoTrackBar.ValueChangedEventArgs e)
        {
            TitleButtonIconSize = new SizeF(klxPiaoTrackBar5.Value, klxPiaoTrackBar5.Value);
        }
        #endregion

        #region 皮肤编辑器
        private void ThemeEditor_Click(object? sender, EventArgs e)
        {
            if (sender is KlxPiaoPanel c)
            {
                ColorDialog 选择颜色 = new()
                {
                    Color = c.BackColor,
                    FullOpen = true
                };

                if (选择颜色.ShowDialog() == DialogResult.OK)
                {
                    Color selectColor = 选择颜色.Color;

                    c.BackColor = selectColor;

                    switch (c.Name)
                    {
                        case "Edit_标题框背景色":
                            TitleBoxBackColor = selectColor;
                            TitleBoxForeColor = ColorProcessor.GetBrightness(selectColor) > 127 ? Color.Black : Color.White;
                            break;
                        case "Edit_标题框前景色":
                            TitleBoxForeColor = selectColor;
                            break;
                        case "Edit_边框颜色":
                            BorderColor = selectColor;
                            break;
                        case "Edit_未激活标题框背景色":
                            InactiveTitleBoxBackColor = selectColor;
                            break;
                        case "Edit_未激活标题框前景色":
                            InactiveTitleBoxForeColor = selectColor;
                            break;
                        case "Edit_未激活边框颜色":
                            InactiveBorderColor = selectColor;
                            break;
                    }
                }
            }
        }
        private void ThemeEditor_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is KlxPiaoPanel c)
            {

                switch (c.Name)
                {
                    case "Edit_标题框背景色":
                        c.BackColor = TitleBoxBackColor;
                        break;
                    case "Edit_标题框前景色":
                        c.BackColor = TitleBoxForeColor;
                        break;
                    case "Edit_边框颜色":
                        c.BackColor = BorderColor;
                        break;
                    case "Edit_未激活标题框背景色":
                        c.BackColor = InactiveTitleBoxBackColor;
                        break;
                    case "Edit_未激活标题框前景色":
                        c.BackColor = InactiveTitleBoxForeColor;
                        break;
                    case "Edit_未激活边框颜色":
                        c.BackColor = InactiveBorderColor;
                        break;
                }
            }
        }
        private void Show_按钮背景_Paint(object sender, PaintEventArgs e)
        {
            Show_按钮背景.BackColor = TitleBoxBackColor;
            Show_按钮移入.BackColor = ColorProcessor.AdjustBrightness(Show_按钮背景.BackColor, InteractionColorScale);
            Show_按钮按下.BackColor = ColorProcessor.AdjustBrightness(Show_按钮移入.BackColor, InteractionColorScale);
        }
        private void 主窗体_Activated(object sender, EventArgs e)
        {
            klxPiaoLabel12.Text = "已激活";
        }
        private void 主窗体_Deactivate(object sender, EventArgs e)
        {
            klxPiaoLabel12.Text = "未激活";
        }
        //修改反馈偏移
        private void KlxPiaoTrackBar4_值Changed(object sender, KlxPiaoTrackBar.ValueChangedEventArgs e)
        {
            InteractionColorScale = klxPiaoTrackBar4.Value;
            foreach (Control c in klxPiaoPanel9.Controls)
            {
                if (c is KlxPiaoPanel p)
                {
                    p.Refresh();
                }
            }
        }
        //刷新随机配色
        private void KlxPiaoButton3_Click(object sender, EventArgs e)
        {
            foreach (Control c in klxPiaoPanel10.Controls)
            {
                c.BackColor = Color.FromArgb(rand.Next(随机颜色范围[0], 随机颜色范围[1]), rand.Next(随机颜色范围[0], 随机颜色范围[1]), rand.Next(随机颜色范围[0], 随机颜色范围[1]));
            }
        }
        //将随机颜色应用为主题色
        private void RandomColorPanel_MouseClick(object? sender, MouseEventArgs e)
        {
            if (sender is KlxPiaoPanel c)
            {
                Color newThemeColor = c.BackColor;

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        SetGlobalTheme(newThemeColor, true);

                        break;
                    case MouseButtons.Right:
                        KlxPiaoForm klxfm = new()
                        {
                            BorderColor = Color.FromArgb(147, 135, 248),
                            TitleBoxBackColor = TitleBoxBackColor,
                            TitleBoxForeColor = TitleBoxForeColor,
                            TitleButtons = TitleButtonStyle.CloseOnly,
                            Resizable = false,
                            Text = "提示：",
                            Size = new Size(250, 150),
                            ShowIcon = false,
                        };
                        KlxPiaoLabel tip = new()
                        {
                            AutoSize = false,
                            Size = klxfm.GetClientSize(),
                            Text = $"RGB [{c.BackColor.R} {c.BackColor.G} {c.BackColor.B}]",
                            Location = klxfm.GetClientRectangle().Location,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Padding = new Padding(0, 0, 0, klxfm.TitleBoxHeight / 2)
                        };
                        klxfm.Controls.Add(tip);
                        klxfm.ShowDialog();
                        break;
                }
            }
        }
        #endregion

        #region 矩阵排板计算工具
        private void KlxPiaoButton4_Click(object sender, EventArgs e)
        {
            try
            {
                SizeF 容器大小 = new(float.Parse(textBox7.Text), float.Parse(textBox8.Text));
                SizeF 单元大小 = new(float.Parse(textBox12.Text), float.Parse(textBox11.Text));
                Padding 边距 = new(int.Parse(textBox14.Text), int.Parse(textBox13.Text), int.Parse(textBox17.Text), int.Parse(textBox16.Text));
                Size 矩阵大小 = new(int.Parse(textBox10.Text), int.Parse(textBox9.Text));

                List<PointF> points = LayoutUtilities.CalculateGridPoints(容器大小, 单元大小, 矩阵大小, 边距);
                StringBuilder showPoints = new();

                textBox15.Clear();
                for (int i = 0; i < points.Count; i++)
                {
                    if (checkBox5.Checked)
                    {
                        showPoints.AppendLine($"{(int)points[i].X},{(int)points[i].Y}");
                    }
                    else
                    {
                        showPoints.AppendLine($"{points[i].X},{points[i].Y}");
                    }
                }
                textBox15.Text = showPoints.ToString();

                Graphics g = panel1.CreateGraphics();
                Pen 容器Pen = new(Color.Black, 1);
                Pen 单元Pen = new(Color.Blue, 1);
                SolidBrush 文本Brush = new(Color.Black);
                Font 字体 = new("微软雅黑", 7);

                g.Clear(panel1.BackColor);
                g.DrawRectangle(容器Pen, new RectangleF(new PointF(0, 0), 容器大小));

                for (int i = 0; i < points.Count; i++)
                {
                    g.DrawRectangle(单元Pen, new RectangleF(points[i], 单元大小));

                    if (checkBox6.Checked)
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.FillEllipse(new SolidBrush(Color.Red), new RectangleF(points[i] - new Size(2, 2), new Size(4, 4)));
                        g.SmoothingMode = SmoothingMode.Default;
                    }

                    if (checkBox4.Checked)
                    {
                        if (checkBox5.Checked)
                        {
                            g.DrawString($"{(int)points[i].X},{(int)points[i].Y}", 字体, 文本Brush, points[i]);
                        }
                        else
                        {
                            g.DrawString($"{points[i].X},{points[i].Y}", 字体, 文本Brush, points[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Graphics g = panel1.CreateGraphics();
                g.Clear(panel1.BackColor);
                g.DrawString($"错误：{ex.Message}", new("微软雅黑", 9), new SolidBrush(Color.Red), new Point(0, 0));
            }
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            klxPiaoButton4.PerformClick();
        }

        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                klxPiaoButton4.PerformClick();
            }
        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            klxPiaoButton4.PerformClick();
        }
        #endregion

        #region KlxPiaoControls.Label
        private void KlxPiaoButton5_Click(object sender, EventArgs e)
        {
            KlxPiaoLabelDemoForm labeldemo = new();
            labeldemo.Show();
        }
        #endregion

        #region KlxPiaoControls.Form (Func)
        private void KlxPiaoButton6_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog selectFile = new()
                {
                    InitialDirectory = Application.StartupPath,
                    Filter = "TTF|*.ttf"
                };
                if (selectFile.ShowDialog() == DialogResult.OK)
                {
                    FontFamily fontFamily = FileUtils.LoadFontFamily(selectFile.FileName);
                    SetGlobalFont(fontFamily);
                    this.ForEachControl<KlxPiaoLabel>(label => label.TextRenderingHint = TextRenderingHint.AntiAliasGridFit);
                }
            }
            catch
            {
                MessageBox.Show("选择文件不是字体文件，或文件已存坏");
            }

        }
        #endregion

        #region KlxPiaoAPI.Control
        private void KlxPiaoButton1_Click(object sender, EventArgs e)
        {
            StringBuilder pointfsshowtext = new();
            StringBuilder comptext = new();
            PointF[] pointFs = [.. bezierCurve1.ControlPoints];
            pointfsshowtext.Append('[');
            comptext.Append("[DefaultValue(typeof(Animation), \"Time, FPS, [");
            for (int i = 0; i < pointFs.Length; i++)
            {
                pointfsshowtext.Append($"new({pointFs[i].X}F, {pointFs[i].Y}F)");
                comptext.Append($"{pointFs[i].X} {pointFs[i].Y}");

                if (i != pointFs.Length - 1)
                {
                    pointfsshowtext.Append(", ");
                    comptext.Append(';');
                }
                else
                {
                    pointfsshowtext.Append(']');
                    comptext.Append("]\")]");
                }
            }

            BezierCurveCodeShow form = new(pointfsshowtext.ToString(), comptext.ToString(), TitleBoxBackColor)
            {
                Theme = Theme
            };
            form.ShowDialog();
        }
        private void BezierCurve1_控制点拖动(object sender, KlxPiaoControls.BezierCurve.ControlPointChangedEvent? e) //不会读取e的信息，因此声明可为null
        {
            StringBuilder pointsList = new();

            for (int i = 0; i < bezierCurve1.ControlPoints.Count; i++)
            {
                pointsList.Append($"{bezierCurve1.ControlPoints[i].X},{bezierCurve1.ControlPoints[i].Y}");

                if (i != bezierCurve1.ControlPoints.Count - 1) pointsList.AppendLine();
            }

            textBox18.Text = pointsList.ToString();
        }

        private CancellationTokenSource cts = new();
        private void 播放But_Click(object sender, EventArgs e)
        {
            cts.Cancel();
            cts = new CancellationTokenSource();

            if (位置过渡Check.Checked)
            {
                Point 目标位置 = 控件动画Panel.Location == new Point(24, 271) ? new Point(435, 253) : new Point(24, 271);
                _ = 控件动画Panel.BezierTransition("Location",
                    null, 目标位置, new Animation((int)klxPiaoTrackBar12.Value, 100, [.. bezierCurve1.ControlPoints]),
                    default, false, cts.Token);
            }
            if (大小过渡Check.Checked)
            {
                Size 目标大小 = 控件动画Panel.Size == new Size(70, 70) ? new Size(130, 130) : new Size(70, 70);
                _ = 控件动画Panel.BezierTransition("Size",
                    null, 目标大小, (int)klxPiaoTrackBar12.Value, [.. bezierCurve1.ControlPoints], 100,
                    default, false, cts.Token);
            }
            if (颜色过渡Check.Checked)
            {
                Color 目标颜色 = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
                _ = 控件动画Panel.BezierTransition("BackColor",
                    null, 目标颜色, (int)klxPiaoTrackBar12.Value, null, 100,
                    default, false, cts.Token);
            }
        }
        private void 停止But_Click(object sender, EventArgs e)
        {
            if (cts != default)
            {
                cts.Cancel();
            }
        }

        //修改基础数据
        private void TextBox18_TextChanged(object sender, EventArgs e)
        {
            Graphics g = bezierCurve1.CreateGraphics();

            try
            {
                List<PointF> points = [];

                for (int i = 0; i < textBox18.Lines.Length; i++)
                {
                    PointF pointF = new(
                        float.Parse(textBox18.Lines[i].Split(",")[0]),
                        float.Parse(textBox18.Lines[i].Split(",")[1]));

                    points.Add(pointF);
                }

                if (points.Count != 0)
                {
                    bezierCurve1.ControlPoints = points;
                }
                else
                {
                    g.DrawString("请添加控制点", Font, new SolidBrush(Color.Red), bezierCurve1.GetClientRectangle().Location);
                }
            }
            catch
            {
                g.DrawString("输入的控制点有误", Font, new SolidBrush(Color.Red), bezierCurve1.GetClientRectangle().Location);
            }
        }
        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            bezierCurve1.IsStartAndEndPointDraggable = checkBox7.Checked;
        }
        private void CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            bezierCurve1.IsDisplayControlPointTextWhileDragging = checkBox9.Checked;
        }
        private void KlxPiaoTrackBar11_值Changed(object sender, KlxPiaoTrackBar.ValueChangedEventArgs e)
        {
            bezierCurve1.DrawingAccuracy = klxPiaoTrackBar11.Value;
        }
        //添加，删除
        private void KlxPiaoButton7_Click(object sender, EventArgs e)
        {
            bezierCurve1.AddControlPoint(new PointF(0.5F, 0.5F), bezierCurve1.ControlPoints.Count / 2);
            BezierCurve1_控制点拖动(sender, null); //刷新文本框
        }
        private void KlxPiaoButton8_Click(object sender, EventArgs e)
        {
            if (bezierCurve1.ControlPoints.Count != 3)
            {
                bezierCurve1.RemoveControlPoint(bezierCurve1.ControlPoints.Count / 2);
                BezierCurve1_控制点拖动(sender, null); //刷新文本框
            }
        }
        //辅助线
        private void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            bezierCurve1.GuidelineDraw = checkBox8.Checked switch
            {
                true => KlxPiaoControls.BezierCurve.GuidelineDrawMode.BothEndsSolid_MiddleDashed,
                false => KlxPiaoControls.BezierCurve.GuidelineDrawMode.DoNotDraw
            };
        }
        #endregion

        #region 转换器代码生成器
        private void KlxPiaoButton9_Click(object sender, EventArgs e)
        {
            //结构
            label21.Text = $"{结构名称Text.Text}.cs";

            var replacements = new Dictionary<string, string>{
               {"{命名空间}",命名空间Text.Text},
               {"{转换器名称}",转换器名称Text.Text},
               {"{结构名称}",结构名称Text.Text},
               {"{成员类型}",结构成员类型Text.Text}
            };

            StringBuilder 结构成员声明 = new();
            foreach (string 成员 in 成员列表Text.Lines)
            {
                结构成员声明.AppendLine($"        public {结构成员类型Text.Text} {成员} {{ get; set; }}");
            }

            StringBuilder 统一赋值方法 = new();
            统一赋值方法.Append("            ");
            foreach (string 成员 in 成员列表Text.Lines)
            {
                统一赋值方法.Append($"{成员} = ");
            }
            统一赋值方法.Append("uniform;");

            StringBuilder 通用方法参数 = new();
            for (int i = 0; i < 成员列表Text.Lines.Length; i++)
            {
                通用方法参数.Append($"{结构成员类型Text.Text} {成员列表Text.Lines[i].ProcessFirstChar("value")}");

                if (i != 成员列表Text.Lines.Length - 1)
                {
                    通用方法参数.Append(", ");
                }
            }

            StringBuilder 通用方法过程 = new();
            for (int i = 0; i < 成员列表Text.Lines.Length; i++)
            {
                通用方法过程.Append($"            {成员列表Text.Lines[i]} = {成员列表Text.Lines[i].ProcessFirstChar("value")};");

                if (i != 成员列表Text.Lines.Length - 1)
                {
                    通用方法过程.AppendLine();
                }
            }

            textBox23.Text = Resources.结构.ReplaceMultiple(replacements)
                .Replace("{结构成员声明}", 结构成员声明.ToString())
                .Replace("{统一赋值方法}", 统一赋值方法.ToString())
                .Replace("{通用方法参数}", 通用方法参数.ToString())
                .Replace("{通用方法过程}", 通用方法过程.ToString());

            //转换器
            label23.Text = $"{转换器名称Text.Text}.cs";

            StringBuilder 成员列表1 = new();
            foreach (string 成员 in 成员列表Text.Lines)
            {
                成员列表1.AppendLine($"                    array[num++] = converter.ConvertToString(context, culture, {结构名称Text.Text}1.{成员});");
            }

            StringBuilder 成员类型列表 = new();
            for (int i = 0; i < 成员列表Text.Lines.Length; i++)
            {
                成员类型列表.Append($"                        typeof({结构成员类型Text.Text})");

                if (i != 成员列表Text.Lines.Length - 1)
                {
                    成员类型列表.Append(",\r\n");
                }
            }

            StringBuilder 成员列表2 = new();
            for (int i = 0; i < 成员列表Text.Lines.Length; i++)
            {
                成员列表2.Append($"{结构名称Text.Text}2.{成员列表Text.Lines[i]}");

                if (i != 成员列表Text.Lines.Length - 1)
                {
                    成员列表2.Append(", ");
                }
            }

            StringBuilder 成员列表3 = new();
            for (int i = 0; i < 成员列表Text.Lines.Length; i++)
            {
                成员列表3.Append($"                ({结构成员类型Text.Text})propertyValues[\"{成员列表Text.Lines[i]}\"]");

                if (i != 成员列表Text.Lines.Length - 1)
                {
                    成员列表3.Append(",\r\n");
                }
            }

            StringBuilder 成员列表4 = new();
            for (int i = 0; i < 成员列表Text.Lines.Length; i++)
            {
                成员列表4.Append($"\"{成员列表Text.Lines[i]}\"");

                if (i != 成员列表Text.Lines.Length - 1)
                {
                    成员列表4.Append(", ");
                }
            }

            textBox25.Text = Resources.转换器.ReplaceMultiple(replacements)
                .Replace("{成员数量}", 成员列表Text.Lines.Length.ToString())
                .Replace("{成员列表1}", 成员列表1.ToString())
                .Replace("{成员类型列表}", 成员类型列表.ToString())
                .Replace("{成员列表2}", 成员列表2.ToString())
                .Replace("{成员列表3}", 成员列表3.ToString())
                .Replace("{成员列表4}", 成员列表4.ToString())
                .Replace("{结构名称Value}", 结构名称Text.Text + "Value")
                .Replace("{结构名称1}", 结构名称Text.Text + "1")
                .Replace("{结构名称2}", 结构名称Text.Text + "2");
        }
        #endregion

        #region KlxPiaoControls.RoundedButton
        private void SlideSwitch7_SelectIndexChanged(object sender, SlideSwitch.IndexChangedEventArgs e)
        {
            switch (e.SelectIndex)
            {
                case 0:
                    SetRoundedButton(true);
                    break;

                case 1:
                    SetRoundedButton(false);
                    break;
            }
        }
        private void SetRoundedButton(bool check)
        {
            TabPage_UI_RoundedButton.ForEachControl<RoundedButton>(roundedbutton => { roundedbutton.IsEnableAnimation = check; });
        }
        #endregion

    }
}