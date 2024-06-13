using KlxPiaoAPI;
using KlxPiaoControls;
using KlxPiaoDemo.Properties;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;

namespace KlxPiaoDemo
{
    public partial class 主窗体 : KlxPiaoForm
    {
        public 主窗体()
        {
            InitializeComponent();
        }

        private readonly int[] 随机颜色范围 = [195, 255];
        private readonly Random rand = new();
        private void 主窗体_Load(object sender, EventArgs e)
        {
            Text = $"{关于KlxPiaoControls.产品名称()} & {关于KlxPiaoAPI.产品名称()} {关于KlxPiaoControls.产品版本()} Demo";

            //控件.PictureBox
            Pic_BorderTrackBar.值 = klxPiaoPictureBox1.边框大小;
            Pic_FilletTrackBar.值 = klxPiaoPictureBox1.圆角百分比;
            Pic_SizeTrackBar.值 = klxPiaoPictureBox1.Width;

            Pic_SizeTrackBar.最小值 = klxPiaoPictureBox1.Width / 2;
            Pic_SizeTrackBar.最大值 = (float)(klxPiaoPictureBox1.Width * 1.2F);

            Pic_BorderTrackBar.值Changed += Pic_Track_值Changed;
            Pic_FilletTrackBar.值Changed += Pic_Track_值Changed;
            Pic_SizeTrackBar.值Changed += Pic_Track_值Changed;
            //控件.Panel
            foreach (Control p in klxPiaoPanel8.Controls)
            {
                p.Click += Controls_Panel_Click;
            }
            //初始化皮肤编辑器
            foreach (Control p in klxPiaoPanel7.Controls)
            {
                p.Click += ThemeEditor_Click;
                p.Paint += ThemeEditor_Paint;
            }
            klxPiaoTrackBar4.值 = 标题按钮颜色反馈;
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
                        边框大小 = 1,
                        启用投影 = false,
                        圆角大小 = new CornerRadius(0.36F),
                        Size = new Size(45, 45),
                        Cursor = Cursors.Hand,
                        BackColor = Color.FromArgb(rand.Next(随机颜色范围[0], 随机颜色范围[1]), rand.Next(随机颜色范围[0], 随机颜色范围[1]), rand.Next(随机颜色范围[0], 随机颜色范围[1]))
                    };
                    p.Location = new Point(位置.X + x * (p.Width + 横边距), 位置.Y + y * (p.Height + 纵边距));
                    p.MouseClick += RandomColorPanel_MouseClick;
                    klxPiaoPanel10.Controls.Add(p);
                }
            }
            //初始化菜单
            foreach (标题按钮样式 value in Enum.GetValues(typeof(标题按钮样式)))
            {
                comboBox1.Items.Add(value.ToString());
            }
            comboBox1.SelectedIndex = (int)标题按钮显示;
            foreach (窗体位置 value in Enum.GetValues(typeof(窗体位置)))
            {
                comboBox2.Items.Add(value.ToString());
            }
            comboBox2.SelectedIndex = (int)快捷缩放方式;
            foreach (窗体位置 value in Enum.GetValues(typeof(窗体位置)))
            {
                comboBox3.Items.Add(value.ToString());
            }
            comboBox3.SelectedIndex = (int)拖动方式;
            foreach (位置 value in Enum.GetValues(typeof(位置)))
            {
                comboBox4.Items.Add(value.ToString());
            }
            comboBox4.SelectedIndex = (int)标题位置;
            foreach (两端 value in Enum.GetValues(typeof(两端)))
            {
                comboBox5.Items.Add(value.ToString());
            }
            comboBox5.SelectedIndex = (int)标题按钮位置;
            foreach (风格 value in Enum.GetValues(typeof(风格)))
            {
                comboBox6.Items.Add(value.ToString());
            }
            comboBox6.SelectedIndex = (int)主题;

            klxPiaoTrackBar1.值 = 标题框高度;
            klxPiaoTrackBar2.值 = 标题左右边距;
            klxPiaoTrackBar3.值 = 标题按钮宽度;
            klxPiaoTrackBar5.值 = 标题按钮图标大小.Width;

            checkBox1.Checked = 启用缩放动画;
            checkBox2.Checked = 可调整大小;
            checkBox3.Checked = ShowIcon;
        }

        //属性代码生成器
        private void KlxPiaoButton1_Click(object sender, EventArgs e)
        {
            string 名称 = textBox1.Text;
            string 类型 = textBox2.Text;
            string 默认值 = textBox6.Text;

            textBox3.Text = $"private {类型} _{名称};";
            textBox4.Text = $"_{名称} = {默认值};";
            textBox5.Text = $"        public {类型} {名称}\r\n        {{\r\n            get {{ return _{名称}; }}\r\n            set {{ _{名称} = value; Invalidate(); }}\r\n        }}";
        }

        #region 控件.PictureBox
        private void Pic_Track_值Changed(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is KlxPiaoTrackBar c)
            {
                switch (c.Name)
                {
                    case "Pic_BorderTrackBar":
                        klxPiaoPictureBox1.边框大小 = (int)c.值;
                        break;
                    case "Pic_FilletTrackBar":
                        klxPiaoPictureBox1.圆角百分比 = c.值;
                        break;
                    case "Pic_SizeTrackBar":
                        klxPiaoPictureBox1.Size = new Size((int)c.值, (int)c.值);
                        panel2.Left = klxPiaoPictureBox1.Left + 25 + klxPiaoPictureBox1.Width;
                        break;
                }
            }
        }
        #endregion

        #region 控件.Panel
        //显示工作区矩形
        private void Controls_Panel_Click(object? sender, EventArgs e)
        {
            if (sender is KlxPiaoPanel panel)
            {
                Graphics g = panel.CreateGraphics();
                g.DrawRectangle(new Pen(Color.Red, 1), panel.获取工作区矩形());
            }
        }
        #endregion

        #region 调整菜单
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            标题按钮显示 = (标题按钮样式)comboBox1.SelectedIndex;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            快捷缩放方式 = (窗体位置)comboBox2.SelectedIndex;
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            拖动方式 = (窗体位置)comboBox3.SelectedIndex;
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            标题位置 = (位置)comboBox4.SelectedIndex;
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            标题按钮位置 = (两端)comboBox5.SelectedIndex;
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            主题 = (风格)comboBox6.SelectedIndex;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            启用缩放动画 = checkBox1.Checked;
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            可调整大小 = checkBox2.Checked;
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            ShowIcon = checkBox3.Checked;
        }

        private void KlxPiaoTrackBar1_值Changed(object sender, PropertyChangedEventArgs e)
        {
            标题框高度 = (int)klxPiaoTrackBar1.值;
            tabControl1.Top = 标题框高度 + 9;
        }

        private void KlxPiaoTrackBar2_值Changed(object sender, PropertyChangedEventArgs e)
        {
            标题左右边距 = (int)klxPiaoTrackBar2.值;
        }

        private void KlxPiaoTrackBar3_值Changed(object sender, PropertyChangedEventArgs e)
        {
            标题按钮宽度 = (int)klxPiaoTrackBar3.值;
        }

        private void KlxPiaoTrackBar5_值Changed(object sender, PropertyChangedEventArgs e)
        {
            标题按钮图标大小 = new SizeF(klxPiaoTrackBar5.值, klxPiaoTrackBar5.值);
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
                            标题框背景色 = selectColor;
                            标题框前景色 = 颜色.获取亮度(selectColor) > 127 ? Color.Black : Color.White;
                            break;
                        case "Edit_标题框前景色":
                            标题框前景色 = selectColor;
                            break;
                        case "Edit_边框颜色":
                            边框颜色 = selectColor;
                            break;
                        case "Edit_未激活标题框背景色":
                            未激活标题框背景色 = selectColor;
                            break;
                        case "Edit_未激活标题框前景色":
                            未激活标题框前景色 = selectColor;
                            break;
                        case "Edit_未激活边框颜色":
                            未激活边框颜色 = selectColor;
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
                        c.BackColor = 标题框背景色;
                        break;
                    case "Edit_标题框前景色":
                        c.BackColor = 标题框前景色;
                        break;
                    case "Edit_边框颜色":
                        c.BackColor = 边框颜色;
                        break;
                    case "Edit_未激活标题框背景色":
                        c.BackColor = 未激活标题框背景色;
                        break;
                    case "Edit_未激活标题框前景色":
                        c.BackColor = 未激活标题框前景色;
                        break;
                    case "Edit_未激活边框颜色":
                        c.BackColor = 未激活边框颜色;
                        break;
                }
            }
        }
        private void Show_按钮背景_Paint(object sender, PaintEventArgs e)
        {
            Show_按钮背景.BackColor = 标题框背景色;
            Show_按钮移入.BackColor = 颜色.调整亮度(Show_按钮背景.BackColor, 标题按钮颜色反馈);
            Show_按钮按下.BackColor = 颜色.调整亮度(Show_按钮移入.BackColor, 标题按钮颜色反馈);
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
        private void KlxPiaoTrackBar4_值Changed(object sender, PropertyChangedEventArgs e)
        {
            标题按钮颜色反馈 = klxPiaoTrackBar4.值;
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
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        标题框背景色 = c.BackColor;
                        标题框前景色 = 颜色.获取亮度(c.BackColor) > 127 ? Color.Black : Color.White;
                        break;
                    case MouseButtons.Right:
                        KlxPiaoForm klxfm = new()
                        {
                            边框颜色 = Color.FromArgb(147, 135, 248),
                            标题框背景色 = 标题框背景色,
                            标题框前景色 = 标题框前景色,
                            标题按钮显示 = 标题按钮样式.仅关闭,
                            可调整大小 = false,
                            Text = "提示：",
                            Size = new Size(250, 150),
                            ShowIcon = false,
                        };
                        KlxPiaoLabel tip = new()
                        {
                            AutoSize = false,
                            Size = klxfm.获取工作区大小(),
                            Text = $"RGB [{c.BackColor.R} {c.BackColor.G} {c.BackColor.B}]",
                            Location = klxfm.获取工作区矩形().Location,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Padding = new Padding(0, 0, 0, klxfm.标题框高度 / 2)
                        };
                        klxfm.Controls.Add(tip);
                        klxfm.ShowDialog();
                        break;
                }
            }
        }
        #endregion

        //切换到皮肤编辑器
        private void KlxPiaoButton2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3; //皮肤编辑器的索引
        }

        #region 矩阵排板计算工具
        private void KlxPiaoButton4_Click(object sender, EventArgs e)
        {
            try
            {
                SizeF 容器大小 = new(float.Parse(textBox7.Text), float.Parse(textBox8.Text));
                SizeF 单元大小 = new(float.Parse(textBox12.Text), float.Parse(textBox11.Text));
                Padding 边距 = new(int.Parse(textBox14.Text), int.Parse(textBox13.Text), int.Parse(textBox17.Text), int.Parse(textBox16.Text));
                Size 矩阵大小 = new(int.Parse(textBox10.Text), int.Parse(textBox9.Text));

                List<PointF> points = 数学.计算网格点(容器大小, 单元大小, 矩阵大小, 边距);
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

        //控件.Label
        private void KlxPiaoButton5_Click(object sender, EventArgs e)
        {
            KlxPiaoLabelDemoForm labeldemo = new();
            labeldemo.Show();
        }
        //控件.Form(功能)

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
                    FontFamily fontFamily = 文件.加载字体(selectFile.FileName);
                    设置全局字体(fontFamily);
                    控件.遍历<KlxPiaoLabel>(this, label => label.文本呈现质量 = TextRenderingHint.AntiAliasGridFit);
                }
            }
            catch
            {
                MessageBox.Show("选择文件不是字体文件，或文件已存坏");
            }

        }

        #region KlxPiaoAPI.控件
        private void BezierCurve1_控制点拖动(object sender, KlxPiaoControls.BezierCurve.ControlPointDrag? e) //不会读取e的信息，因此声明可为null
        {
            StringBuilder pointsList = new();

            for (int i = 0; i < bezierCurve1.控制点集合.Count; i++)
            {
                pointsList.Append($"{bezierCurve1.控制点集合[i].X},{bezierCurve1.控制点集合[i].Y}");

                if (i != bezierCurve1.控制点集合.Count - 1) pointsList.AppendLine();
            }

            textBox18.Text = pointsList.ToString();
        }
        private void 播放But_Click(object sender, EventArgs e)
        {
            if (位置过渡Check.Checked)
            {
                Point 目标位置 = 控件动画Panel.Location == new Point(24, 271) ? new Point(435, 253) : new Point(24, 271);
                _ = 控件动画Panel.贝塞尔过渡动画("Location", null, 目标位置, (int)klxPiaoTrackBar12.值, [.. bezierCurve1.控制点集合]);
            }
            if (大小过渡Check.Checked)
            {
                Size 目标位置 = 控件动画Panel.Size == new Size(70, 70) ? new Size(130, 130) : new Size(70, 70);
                _ = 控件动画Panel.贝塞尔过渡动画("Size", null, 目标位置, (int)klxPiaoTrackBar12.值, [.. bezierCurve1.控制点集合]);
            }
            if (颜色过渡Check.Checked)
            {
                Color 目标颜色 = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
                _ = 控件动画Panel.贝塞尔过渡动画("BackColor", null, 目标颜色, (int)klxPiaoTrackBar12.值, null);
            }
        }
        private void 停止But_Click(object sender, EventArgs e)
        {

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
                    bezierCurve1.控制点集合 = points;
                }
                else
                {
                    g.DrawString("请添加控制点", Font, new SolidBrush(Color.Red), bezierCurve1.获取工作区矩形().Location);
                }
            }
            catch
            {
                g.DrawString("输入的控制点有误", Font, new SolidBrush(Color.Red), bezierCurve1.获取工作区矩形().Location);
            }
        }
        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            bezierCurve1.可拖动两端 = checkBox7.Checked;
        }
        private void CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            bezierCurve1.拖动时显示控制点信息 = checkBox9.Checked;
        }
        private void KlxPiaoTrackBar11_值Changed(object sender, PropertyChangedEventArgs e)
        {
            bezierCurve1.绘制精度 = klxPiaoTrackBar11.值;
        }
        //添加，删除
        private void KlxPiaoButton7_Click(object sender, EventArgs e)
        {
            bezierCurve1.AddControlPoint(new PointF(0.5F, 0.5F), bezierCurve1.控制点集合.Count / 2);
            BezierCurve1_控制点拖动(sender, null); //刷新文本框
        }
        private void KlxPiaoButton8_Click(object sender, EventArgs e)
        {
            if (bezierCurve1.控制点集合.Count != 3)
            {
                bezierCurve1.RemoveControlPoint(bezierCurve1.控制点集合.Count / 2);
                BezierCurve1_控制点拖动(sender, null); //刷新文本框
            }
        }
        //辅助线
        private void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            bezierCurve1.辅助线显示方式 = checkBox8.Checked switch
            {
                true => KlxPiaoControls.BezierCurve.辅助线绘制.两端实线_中间虚线,
                false => KlxPiaoControls.BezierCurve.辅助线绘制.不绘制
            };
        }
        #endregion

        //转换器代码生成器
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
                通用方法参数.Append($"{结构成员类型Text.Text} {成员列表Text.Lines[i].方法参数处理("value")}");

                if (i != 成员列表Text.Lines.Length - 1)
                {
                    通用方法参数.Append(", ");
                }
            }

            StringBuilder 通用方法过程 = new();
            for (int i = 0; i < 成员列表Text.Lines.Length; i++)
            {
                通用方法过程.Append($"            {成员列表Text.Lines[i]} = {成员列表Text.Lines[i].方法参数处理("value")};");

                if (i != 成员列表Text.Lines.Length - 1)
                {
                    通用方法过程.AppendLine();
                }
            }

            textBox23.Text = Resources.结构.批量替换(replacements)
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

            textBox25.Text = Resources.转换器.批量替换(replacements)
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
    }
}