namespace KlxPiaoDemo
{
    partial class KlxPiaoLabelDemoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            KlxPiaoControls.KlxPiaoTrackBar.InteractionStyleClass interactionStyleClass1 = new KlxPiaoControls.KlxPiaoTrackBar.InteractionStyleClass();
            KlxPiaoControls.KlxPiaoTrackBar.InteractionStyleClass interactionStyleClass2 = new KlxPiaoControls.KlxPiaoTrackBar.InteractionStyleClass();
            KlxPiaoControls.KlxPiaoTrackBar.InteractionStyleClass interactionStyleClass3 = new KlxPiaoControls.KlxPiaoTrackBar.InteractionStyleClass();
            labelDemo = new KlxPiaoControls.KlxPiaoLabel();
            groupBox1 = new GroupBox();
            klxPiaoLabel3 = new KlxPiaoControls.KlxPiaoLabel();
            klxPiaoLabel2 = new KlxPiaoControls.KlxPiaoLabel();
            边框外部Panel = new KlxPiaoControls.KlxPiaoPanel();
            边框颜色Panel = new KlxPiaoControls.KlxPiaoPanel();
            label9 = new Label();
            label10 = new Label();
            圆角Track = new KlxPiaoControls.KlxPiaoTrackBar();
            边框Track = new KlxPiaoControls.KlxPiaoTrackBar();
            panel1 = new Panel();
            groupBox2 = new GroupBox();
            klxPiaoLinkLabel1 = new KlxPiaoControls.KlxPiaoLinkLabel();
            klxPiaoLabel4 = new KlxPiaoControls.KlxPiaoLabel();
            klxPiaoLabel5 = new KlxPiaoControls.KlxPiaoLabel();
            前景Panel = new KlxPiaoControls.KlxPiaoPanel();
            背景Panel = new KlxPiaoControls.KlxPiaoPanel();
            label1 = new Label();
            label2 = new Label();
            字号Track = new KlxPiaoControls.KlxPiaoTrackBar();
            groupBox3 = new GroupBox();
            klxPiaoLabel6 = new KlxPiaoControls.KlxPiaoLabel();
            pointBar1 = new KlxPiaoControls.PointBar();
            投影颜色Panel = new KlxPiaoControls.KlxPiaoPanel();
            高质量Check = new CheckBox();
            颜色减淡Check = new CheckBox();
            投影连线Check = new CheckBox();
            启用投影Check = new CheckBox();
            textBox1 = new TextBox();
            groupBox4 = new GroupBox();
            klxPiaoButton2 = new KlxPiaoControls.KlxPiaoButton();
            klxPiaoButton1 = new KlxPiaoControls.KlxPiaoButton();
            label4 = new Label();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            label3 = new Label();
            label5 = new Label();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // labelDemo
            // 
            labelDemo.AutoSize = false;
            labelDemo.BackColor = Color.White;
            labelDemo.BorderSize = 0;
            labelDemo.ForeColor = Color.Black;
            labelDemo.IsEnableBorder = true;
            labelDemo.Location = new Point(25, 27);
            labelDemo.Name = "labelDemo";
            labelDemo.Size = new Size(100, 23);
            labelDemo.TabIndex = 7;
            labelDemo.Text = "KlxPiaoLabel";
            labelDemo.TextAlign = ContentAlignment.MiddleCenter;
            labelDemo.SizeChanged += LabelDemo_SizeChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(klxPiaoLabel3);
            groupBox1.Controls.Add(klxPiaoLabel2);
            groupBox1.Controls.Add(边框外部Panel);
            groupBox1.Controls.Add(边框颜色Panel);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(圆角Track);
            groupBox1.Controls.Add(边框Track);
            groupBox1.Location = new Point(681, 44);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(271, 190);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "边框";
            // 
            // klxPiaoLabel3
            // 
            klxPiaoLabel3.AutoSize = false;
            klxPiaoLabel3.BackColor = Color.White;
            klxPiaoLabel3.ForeColor = Color.Black;
            klxPiaoLabel3.Location = new Point(141, 149);
            klxPiaoLabel3.Name = "klxPiaoLabel3";
            klxPiaoLabel3.Size = new Size(90, 23);
            klxPiaoLabel3.TabIndex = 26;
            klxPiaoLabel3.Text = "边框外部颜色";
            klxPiaoLabel3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // klxPiaoLabel2
            // 
            klxPiaoLabel2.AutoSize = false;
            klxPiaoLabel2.BackColor = Color.White;
            klxPiaoLabel2.ForeColor = Color.Black;
            klxPiaoLabel2.Location = new Point(60, 149);
            klxPiaoLabel2.Name = "klxPiaoLabel2";
            klxPiaoLabel2.Size = new Size(50, 23);
            klxPiaoLabel2.TabIndex = 25;
            klxPiaoLabel2.Text = "边框颜色";
            klxPiaoLabel2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // 边框外部Panel
            // 
            边框外部Panel.BackColor = Color.White;
            边框外部Panel.CornerRadius = new KlxPiaoAPI.CornerRadius(15F, 15F, 15F, 15F);
            边框外部Panel.Cursor = Cursors.Hand;
            边框外部Panel.IsEnableShadow = false;
            边框外部Panel.Location = new Point(161, 90);
            边框外部Panel.Name = "边框外部Panel";
            边框外部Panel.ShadowDirection = KlxPiaoControls.KlxPiaoPanel.ShadowDirectionEnum.LeftBottomRight;
            边框外部Panel.ShadowLength = 6;
            边框外部Panel.Size = new Size(50, 50);
            边框外部Panel.TabIndex = 24;
            // 
            // 边框颜色Panel
            // 
            边框颜色Panel.BackColor = Color.LightGray;
            边框颜色Panel.CornerRadius = new KlxPiaoAPI.CornerRadius(15F, 15F, 15F, 15F);
            边框颜色Panel.Cursor = Cursors.Hand;
            边框颜色Panel.IsEnableShadow = false;
            边框颜色Panel.Location = new Point(60, 90);
            边框颜色Panel.Name = "边框颜色Panel";
            边框颜色Panel.ShadowDirection = KlxPiaoControls.KlxPiaoPanel.ShadowDirectionEnum.LeftBottomRight;
            边框颜色Panel.ShadowLength = 6;
            边框颜色Panel.Size = new Size(50, 50);
            边框颜色Panel.TabIndex = 23;
            // 
            // label9
            // 
            label9.Location = new Point(21, 30);
            label9.Name = "label9";
            label9.Size = new Size(56, 17);
            label9.TabIndex = 18;
            label9.Text = "边框大小";
            // 
            // label10
            // 
            label10.Location = new Point(21, 56);
            label10.Name = "label10";
            label10.Size = new Size(56, 17);
            label10.TabIndex = 19;
            label10.Text = "圆角大小";
            // 
            // 圆角Track
            // 
            圆角Track.BackColor = Color.White;
            圆角Track.BorderColor = Color.DarkGray;
            圆角Track.BorderSize = 1;
            interactionStyleClass1.FocusBorderColor = Color.FromArgb(128, 128, 255);
            interactionStyleClass1.FocusBorderSize = null;
            interactionStyleClass1.FocusTrackBackColor = null;
            interactionStyleClass1.FocusTrackForeColor = null;
            interactionStyleClass1.MouseOverBorderColor = null;
            interactionStyleClass1.MouseOverBorderSize = null;
            interactionStyleClass1.MouseOverTrackBackColor = null;
            interactionStyleClass1.MouseOverTrackForeColor = Color.LightGray;
            圆角Track.InteractionStyle = interactionStyleClass1;
            圆角Track.IsDrawValueText = true;
            圆角Track.Location = new Point(92, 58);
            圆角Track.Name = "圆角Track";
            圆角Track.Size = new Size(158, 15);
            圆角Track.TabIndex = 22;
            圆角Track.Text = "klxPiaoTrackBar2";
            圆角Track.TrackBackColor = Color.White;
            圆角Track.TrackForeColor = Color.Gainsboro;
            圆角Track.ValueTextDisplayFormat = "{value}%";
            // 
            // 边框Track
            // 
            边框Track.BackColor = Color.White;
            边框Track.BorderColor = Color.DarkGray;
            边框Track.BorderSize = 1;
            interactionStyleClass2.FocusBorderColor = Color.FromArgb(128, 128, 255);
            interactionStyleClass2.FocusBorderSize = null;
            interactionStyleClass2.FocusTrackBackColor = null;
            interactionStyleClass2.FocusTrackForeColor = null;
            interactionStyleClass2.MouseOverBorderColor = null;
            interactionStyleClass2.MouseOverBorderSize = null;
            interactionStyleClass2.MouseOverTrackBackColor = null;
            interactionStyleClass2.MouseOverTrackForeColor = Color.LightGray;
            边框Track.InteractionStyle = interactionStyleClass2;
            边框Track.IsDrawValueText = true;
            边框Track.Location = new Point(92, 32);
            边框Track.Name = "边框Track";
            边框Track.Size = new Size(158, 15);
            边框Track.TabIndex = 21;
            边框Track.Text = "klxPiaoTrackBar1";
            边框Track.TrackBackColor = Color.White;
            边框Track.TrackForeColor = Color.Gainsboro;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Controls.Add(labelDemo);
            panel1.Location = new Point(12, 44);
            panel1.Name = "panel1";
            panel1.Size = new Size(386, 386);
            panel1.TabIndex = 20;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(klxPiaoLinkLabel1);
            groupBox2.Controls.Add(klxPiaoLabel4);
            groupBox2.Controls.Add(klxPiaoLabel5);
            groupBox2.Controls.Add(前景Panel);
            groupBox2.Controls.Add(背景Panel);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(字号Track);
            groupBox2.Location = new Point(404, 44);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(271, 190);
            groupBox2.TabIndex = 24;
            groupBox2.TabStop = false;
            groupBox2.Text = "基础";
            // 
            // klxPiaoLinkLabel1
            // 
            klxPiaoLinkLabel1.ActiveLinkColor = Color.Black;
            klxPiaoLinkLabel1.BackColor = Color.White;
            klxPiaoLinkLabel1.DisabledLinkColor = Color.FromArgb(210, 210, 210);
            klxPiaoLinkLabel1.ForeColor = Color.Black;
            klxPiaoLinkLabel1.LinkBehavior = LinkBehavior.HoverUnderline;
            klxPiaoLinkLabel1.LinkColor = Color.Black;
            klxPiaoLinkLabel1.Location = new Point(92, 56);
            klxPiaoLinkLabel1.Name = "klxPiaoLinkLabel1";
            klxPiaoLinkLabel1.Size = new Size(158, 17);
            klxPiaoLinkLabel1.TabIndex = 27;
            klxPiaoLinkLabel1.TabStop = true;
            klxPiaoLinkLabel1.Text = "klxPiaoLinkLabel1";
            klxPiaoLinkLabel1.TextAlign = ContentAlignment.MiddleCenter;
            klxPiaoLinkLabel1.LinkClicked += KlxPiaoLinkLabel1_LinkClicked;
            // 
            // klxPiaoLabel4
            // 
            klxPiaoLabel4.AutoSize = false;
            klxPiaoLabel4.BackColor = Color.White;
            klxPiaoLabel4.ForeColor = Color.Black;
            klxPiaoLabel4.Location = new Point(161, 149);
            klxPiaoLabel4.Name = "klxPiaoLabel4";
            klxPiaoLabel4.Size = new Size(50, 23);
            klxPiaoLabel4.TabIndex = 26;
            klxPiaoLabel4.Text = "前景色";
            klxPiaoLabel4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // klxPiaoLabel5
            // 
            klxPiaoLabel5.AutoSize = false;
            klxPiaoLabel5.BackColor = Color.White;
            klxPiaoLabel5.ForeColor = Color.Black;
            klxPiaoLabel5.Location = new Point(60, 149);
            klxPiaoLabel5.Name = "klxPiaoLabel5";
            klxPiaoLabel5.Size = new Size(50, 23);
            klxPiaoLabel5.TabIndex = 25;
            klxPiaoLabel5.Text = "背景色";
            klxPiaoLabel5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // 前景Panel
            // 
            前景Panel.BackColor = Color.Black;
            前景Panel.CornerRadius = new KlxPiaoAPI.CornerRadius(15F, 15F, 15F, 15F);
            前景Panel.Cursor = Cursors.Hand;
            前景Panel.IsEnableShadow = false;
            前景Panel.Location = new Point(161, 90);
            前景Panel.Name = "前景Panel";
            前景Panel.ShadowDirection = KlxPiaoControls.KlxPiaoPanel.ShadowDirectionEnum.LeftBottomRight;
            前景Panel.ShadowLength = 6;
            前景Panel.Size = new Size(50, 50);
            前景Panel.TabIndex = 24;
            // 
            // 背景Panel
            // 
            背景Panel.BackColor = Color.White;
            背景Panel.CornerRadius = new KlxPiaoAPI.CornerRadius(15F, 15F, 15F, 15F);
            背景Panel.Cursor = Cursors.Hand;
            背景Panel.IsEnableShadow = false;
            背景Panel.Location = new Point(60, 90);
            背景Panel.Name = "背景Panel";
            背景Panel.ShadowDirection = KlxPiaoControls.KlxPiaoPanel.ShadowDirectionEnum.LeftBottomRight;
            背景Panel.ShadowLength = 6;
            背景Panel.Size = new Size(50, 50);
            背景Panel.TabIndex = 23;
            // 
            // label1
            // 
            label1.Location = new Point(21, 30);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 18;
            label1.Text = "字号";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Location = new Point(21, 56);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 19;
            label2.Text = "字体";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // 字号Track
            // 
            字号Track.BackColor = Color.White;
            字号Track.BorderColor = Color.DarkGray;
            字号Track.BorderSize = 1;
            interactionStyleClass3.FocusBorderColor = Color.FromArgb(128, 128, 255);
            interactionStyleClass3.FocusBorderSize = null;
            interactionStyleClass3.FocusTrackBackColor = null;
            interactionStyleClass3.FocusTrackForeColor = null;
            interactionStyleClass3.MouseOverBorderColor = null;
            interactionStyleClass3.MouseOverBorderSize = null;
            interactionStyleClass3.MouseOverTrackBackColor = null;
            interactionStyleClass3.MouseOverTrackForeColor = Color.LightGray;
            字号Track.InteractionStyle = interactionStyleClass3;
            字号Track.IsDrawValueText = true;
            字号Track.Location = new Point(92, 32);
            字号Track.MinValue = 1F;
            字号Track.Name = "字号Track";
            字号Track.Size = new Size(158, 15);
            字号Track.TabIndex = 21;
            字号Track.Text = "klxPiaoTrackBar4";
            字号Track.TrackBackColor = Color.White;
            字号Track.TrackForeColor = Color.Gainsboro;
            字号Track.Value = 9F;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(klxPiaoLabel6);
            groupBox3.Controls.Add(pointBar1);
            groupBox3.Controls.Add(投影颜色Panel);
            groupBox3.Controls.Add(高质量Check);
            groupBox3.Controls.Add(颜色减淡Check);
            groupBox3.Controls.Add(投影连线Check);
            groupBox3.Controls.Add(启用投影Check);
            groupBox3.Location = new Point(404, 240);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(271, 190);
            groupBox3.TabIndex = 27;
            groupBox3.TabStop = false;
            groupBox3.Text = "投影";
            // 
            // klxPiaoLabel6
            // 
            klxPiaoLabel6.AutoSize = false;
            klxPiaoLabel6.BackColor = Color.White;
            klxPiaoLabel6.ForeColor = Color.Black;
            klxPiaoLabel6.Location = new Point(60, 143);
            klxPiaoLabel6.Name = "klxPiaoLabel6";
            klxPiaoLabel6.Size = new Size(50, 23);
            klxPiaoLabel6.TabIndex = 29;
            klxPiaoLabel6.Text = "投影颜色";
            klxPiaoLabel6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pointBar1
            // 
            pointBar1.BackColor = Color.White;
            pointBar1.CoordinateDisplayFormat = "Offset: X:{X} Y:{Y}";
            pointBar1.CoordinateTextAlign = ContentAlignment.BottomCenter;
            pointBar1.CoordinateTextOffset = new Point(0, -3);
            pointBar1.CornerRadius = new KlxPiaoAPI.CornerRadius(15F, 15F, 15F, 15F);
            pointBar1.Font = new Font("Microsoft YaHei UI", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pointBar1.Location = new Point(143, 66);
            pointBar1.MaxValue = new Point(250, 250);
            pointBar1.MinValue = new Point(-250, -250);
            pointBar1.Name = "pointBar1";
            pointBar1.Size = new Size(100, 100);
            pointBar1.TabIndex = 28;
            pointBar1.Value = new Point(2, 2);
            pointBar1.ValueChanged += PointBar1_值Changed;
            // 
            // 投影颜色Panel
            // 
            投影颜色Panel.BackColor = Color.DarkGray;
            投影颜色Panel.CornerRadius = new KlxPiaoAPI.CornerRadius(15F, 15F, 15F, 15F);
            投影颜色Panel.Cursor = Cursors.Hand;
            投影颜色Panel.IsEnableShadow = false;
            投影颜色Panel.Location = new Point(60, 84);
            投影颜色Panel.Name = "投影颜色Panel";
            投影颜色Panel.ShadowDirection = KlxPiaoControls.KlxPiaoPanel.ShadowDirectionEnum.LeftBottomRight;
            投影颜色Panel.ShadowLength = 6;
            投影颜色Panel.Size = new Size(50, 50);
            投影颜色Panel.TabIndex = 28;
            // 
            // 高质量Check
            // 
            高质量Check.AutoSize = true;
            高质量Check.Location = new Point(21, 49);
            高质量Check.Name = "高质量Check";
            高质量Check.Size = new Size(63, 21);
            高质量Check.TabIndex = 3;
            高质量Check.Text = "高质量";
            高质量Check.UseVisualStyleBackColor = true;
            // 
            // 颜色减淡Check
            // 
            颜色减淡Check.AutoSize = true;
            颜色减淡Check.Location = new Point(102, 22);
            颜色减淡Check.Name = "颜色减淡Check";
            颜色减淡Check.Size = new Size(75, 21);
            颜色减淡Check.TabIndex = 2;
            颜色减淡Check.Text = "颜色减淡";
            颜色减淡Check.UseVisualStyleBackColor = true;
            // 
            // 投影连线Check
            // 
            投影连线Check.AutoSize = true;
            投影连线Check.Checked = true;
            投影连线Check.CheckState = CheckState.Checked;
            投影连线Check.Location = new Point(183, 22);
            投影连线Check.Name = "投影连线Check";
            投影连线Check.Size = new Size(75, 21);
            投影连线Check.TabIndex = 1;
            投影连线Check.Text = "投影连线";
            投影连线Check.UseVisualStyleBackColor = true;
            // 
            // 启用投影Check
            // 
            启用投影Check.AutoSize = true;
            启用投影Check.Location = new Point(21, 22);
            启用投影Check.Name = "启用投影Check";
            启用投影Check.Size = new Size(75, 21);
            启用投影Check.TabIndex = 0;
            启用投影Check.Text = "启用投影";
            启用投影Check.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Linen;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(62, 7);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 31;
            textBox1.TextChanged += TextBox1_TextChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(klxPiaoButton2);
            groupBox4.Controls.Add(klxPiaoButton1);
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(textBox3);
            groupBox4.Controls.Add(textBox2);
            groupBox4.Controls.Add(label3);
            groupBox4.Location = new Point(681, 240);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(271, 190);
            groupBox4.TabIndex = 35;
            groupBox4.TabStop = false;
            groupBox4.Text = "导出";
            // 
            // klxPiaoButton2
            // 
            klxPiaoButton2.FlatAppearance.BorderColor = Color.Gainsboro;
            klxPiaoButton2.FlatAppearance.MouseDownBackColor = Color.FromArgb(230, 230, 230);
            klxPiaoButton2.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
            klxPiaoButton2.FlatStyle = FlatStyle.Flat;
            klxPiaoButton2.IsReceiveFocus = false;
            klxPiaoButton2.Location = new Point(21, 117);
            klxPiaoButton2.Name = "klxPiaoButton2";
            klxPiaoButton2.Size = new Size(229, 40);
            klxPiaoButton2.TabIndex = 5;
            klxPiaoButton2.Text = "导出到文件";
            klxPiaoButton2.UseVisualStyleBackColor = true;
            klxPiaoButton2.Click += KlxPiaoButton2_Click;
            // 
            // klxPiaoButton1
            // 
            klxPiaoButton1.FlatAppearance.BorderColor = Color.Gainsboro;
            klxPiaoButton1.FlatAppearance.MouseDownBackColor = Color.FromArgb(230, 230, 230);
            klxPiaoButton1.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
            klxPiaoButton1.FlatStyle = FlatStyle.Flat;
            klxPiaoButton1.IsReceiveFocus = false;
            klxPiaoButton1.Location = new Point(21, 71);
            klxPiaoButton1.Name = "klxPiaoButton1";
            klxPiaoButton1.Size = new Size(229, 40);
            klxPiaoButton1.TabIndex = 4;
            klxPiaoButton1.Text = "复制到剪贴板";
            klxPiaoButton1.UseVisualStyleBackColor = true;
            klxPiaoButton1.Click += KlxPiaoButton1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(164, 22);
            label4.Name = "label4";
            label4.Size = new Size(17, 17);
            label4.TabIndex = 3;
            label4.Text = "×";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(190, 19);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(60, 23);
            textBox3.TabIndex = 2;
            textBox3.Text = "386";
            textBox3.TextAlign = HorizontalAlignment.Center;
            textBox3.TextChanged += TextBox3_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(92, 20);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(60, 23);
            textBox2.TabIndex = 1;
            textBox2.Text = "386";
            textBox2.TextAlign = HorizontalAlignment.Center;
            textBox2.TextChanged += TextBox2_TextChanged;
            // 
            // label3
            // 
            label3.Location = new Point(21, 23);
            label3.Name = "label3";
            label3.Size = new Size(56, 17);
            label3.TabIndex = 0;
            label3.Text = "图像尺寸";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Linen;
            label5.Location = new Point(12, 9);
            label5.Name = "label5";
            label5.Size = new Size(44, 17);
            label5.TabIndex = 39;
            label5.Text = "内容：";
            // 
            // KlxPiaoLabelDemoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(991, 450);
            Controls.Add(label5);
            Controls.Add(groupBox4);
            Controls.Add(textBox1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(panel1);
            Controls.Add(groupBox1);
            EnableResizeButton = false;
            Name = "KlxPiaoLabelDemoForm";
            Resizable = false;
            ShowIcon = false;
            Text = "KlxPiaoLabelDemoForm";
            TitleBoxBackColor = Color.Linen;
            TitleTextAlign = HorizontalAlignment.Center;
            Load += KlxPiaoLabelDemoForm_Load;
            groupBox1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private KlxPiaoControls.KlxPiaoLabel labelDemo;
        private GroupBox groupBox1;
        private Label label9;
        private Label label10;
        private KlxPiaoControls.KlxPiaoTrackBar 圆角Track;
        private KlxPiaoControls.KlxPiaoTrackBar 边框Track;
        private Panel panel1;
        private KlxPiaoControls.KlxPiaoPanel 边框外部Panel;
        private KlxPiaoControls.KlxPiaoPanel 边框颜色Panel;
        private KlxPiaoControls.KlxPiaoLabel klxPiaoLabel3;
        private KlxPiaoControls.KlxPiaoLabel klxPiaoLabel2;
        private GroupBox groupBox2;
        private KlxPiaoControls.KlxPiaoLabel klxPiaoLabel4;
        private KlxPiaoControls.KlxPiaoLabel klxPiaoLabel5;
        private KlxPiaoControls.KlxPiaoPanel 前景Panel;
        private KlxPiaoControls.KlxPiaoPanel 背景Panel;
        private Label label1;
        private Label label2;
        private KlxPiaoControls.KlxPiaoTrackBar 字号Track;
        private KlxPiaoControls.KlxPiaoLinkLabel klxPiaoLinkLabel1;
        private GroupBox groupBox3;
        private CheckBox 启用投影Check;
        private KlxPiaoControls.KlxPiaoLabel klxPiaoLabel6;
        private KlxPiaoControls.PointBar pointBar1;
        private KlxPiaoControls.KlxPiaoPanel 投影颜色Panel;
        private CheckBox 高质量Check;
        private CheckBox 颜色减淡Check;
        private CheckBox 投影连线Check;
        private TextBox textBox1;
        private GroupBox groupBox4;
        private TextBox textBox2;
        private Label label3;
        private Label label4;
        private TextBox textBox3;
        private Label label5;
        private KlxPiaoControls.KlxPiaoButton klxPiaoButton2;
        private KlxPiaoControls.KlxPiaoButton klxPiaoButton1;
    }
}