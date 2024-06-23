namespace KlxPiaoDemo
{
    partial class BezierCurveCodeShow
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
            KlxPiaoControls.RoundedButton.交互样式类 交互样式类1 = new KlxPiaoControls.RoundedButton.交互样式类();
            KlxPiaoAPI.Animation animation1 = new KlxPiaoAPI.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BezierCurveCodeShow));
            KlxPiaoAPI.Animation animation2 = new KlxPiaoAPI.Animation();
            KlxPiaoControls.RoundedButton.交互样式类 交互样式类2 = new KlxPiaoControls.RoundedButton.交互样式类();
            KlxPiaoAPI.Animation animation3 = new KlxPiaoAPI.Animation();
            KlxPiaoAPI.Animation animation4 = new KlxPiaoAPI.Animation();
            klxPiaoPanel1 = new KlxPiaoControls.KlxPiaoPanel();
            pointfshowTextBox = new TextBox();
            roundedButton1 = new KlxPiaoControls.RoundedButton();
            roundedButton2 = new KlxPiaoControls.RoundedButton();
            klxPiaoPanel2 = new KlxPiaoControls.KlxPiaoPanel();
            componentModelText = new TextBox();
            klxPiaoPanel1.SuspendLayout();
            klxPiaoPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // klxPiaoPanel1
            // 
            klxPiaoPanel1.BackColor = Color.White;
            klxPiaoPanel1.Controls.Add(pointfshowTextBox);
            klxPiaoPanel1.Location = new Point(34, 55);
            klxPiaoPanel1.Name = "klxPiaoPanel1";
            klxPiaoPanel1.Size = new Size(173, 173);
            klxPiaoPanel1.TabIndex = 4;
            klxPiaoPanel1.启用投影 = false;
            klxPiaoPanel1.圆角大小 = new KlxPiaoAPI.CornerRadius(35F, 35F, 35F, 35F);
            klxPiaoPanel1.边框颜色 = Color.Gainsboro;
            // 
            // pointfshowTextBox
            // 
            pointfshowTextBox.BorderStyle = BorderStyle.None;
            pointfshowTextBox.Location = new Point(7, 7);
            pointfshowTextBox.Multiline = true;
            pointfshowTextBox.Name = "pointfshowTextBox";
            pointfshowTextBox.Size = new Size(160, 160);
            pointfshowTextBox.TabIndex = 0;
            pointfshowTextBox.Text = "PointF[]";
            pointfshowTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(80, 80, 80);
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(34, 241);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(173, 44);
            roundedButton1.TabIndex = 1;
            roundedButton1.Text = "Copy";
            交互样式类1.按下前景色 = Color.Black;
            交互样式类1.按下大小 = new Size(0, 0);
            交互样式类1.按下背景色 = Color.White;
            交互样式类1.按下边框颜色 = Color.Empty;
            交互样式类1.移入前景色 = Color.Empty;
            交互样式类1.移入大小 = new Size(0, 0);
            交互样式类1.移入背景色 = SystemColors.WindowFrame;
            交互样式类1.移入边框颜色 = Color.Empty;
            roundedButton1.交互样式 = 交互样式类1;
            roundedButton1.启用动画 = true;
            roundedButton1.圆角大小 = new KlxPiaoAPI.CornerRadius(35F, 35F, 35F, 35F);
            animation1.Easing = new PointF[]
    {
    (PointF)resources.GetObject("animation1.Easing"),
    (PointF)resources.GetObject("animation1.Easing1"),
    (PointF)resources.GetObject("animation1.Easing2"),
    (PointF)resources.GetObject("animation1.Easing3")
    };
            animation1.FPS = 100;
            animation1.Time = 300;
            roundedButton1.大小过渡配置 = animation1;
            animation2.Easing = new PointF[]
    {
    (PointF)resources.GetObject("animation2.Easing"),
    (PointF)resources.GetObject("animation2.Easing1"),
    (PointF)resources.GetObject("animation2.Easing2"),
    (PointF)resources.GetObject("animation2.Easing3")
    };
            animation2.FPS = 30;
            animation2.Time = 150;
            roundedButton1.颜色过渡配置 = animation2;
            roundedButton1.Click += RoundedButton1_Click;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.FromArgb(80, 80, 80);
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(243, 241);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(173, 44);
            roundedButton2.TabIndex = 8;
            roundedButton2.Text = "Copy";
            交互样式类2.按下前景色 = Color.Black;
            交互样式类2.按下大小 = new Size(0, 0);
            交互样式类2.按下背景色 = Color.White;
            交互样式类2.按下边框颜色 = Color.Empty;
            交互样式类2.移入前景色 = Color.Empty;
            交互样式类2.移入大小 = new Size(0, 0);
            交互样式类2.移入背景色 = SystemColors.WindowFrame;
            交互样式类2.移入边框颜色 = Color.Empty;
            roundedButton2.交互样式 = 交互样式类2;
            roundedButton2.启用动画 = true;
            roundedButton2.圆角大小 = new KlxPiaoAPI.CornerRadius(35F, 35F, 35F, 35F);
            animation3.Easing = new PointF[]
    {
    (PointF)resources.GetObject("animation3.Easing"),
    (PointF)resources.GetObject("animation3.Easing1"),
    (PointF)resources.GetObject("animation3.Easing2"),
    (PointF)resources.GetObject("animation3.Easing3")
    };
            animation3.FPS = 100;
            animation3.Time = 300;
            roundedButton2.大小过渡配置 = animation3;
            animation4.Easing = new PointF[]
    {
    (PointF)resources.GetObject("animation4.Easing"),
    (PointF)resources.GetObject("animation4.Easing1"),
    (PointF)resources.GetObject("animation4.Easing2"),
    (PointF)resources.GetObject("animation4.Easing3")
    };
            animation4.FPS = 30;
            animation4.Time = 150;
            roundedButton2.颜色过渡配置 = animation4;
            roundedButton2.Click += RoundedButton2_Click;
            // 
            // klxPiaoPanel2
            // 
            klxPiaoPanel2.BackColor = Color.White;
            klxPiaoPanel2.Controls.Add(componentModelText);
            klxPiaoPanel2.Location = new Point(243, 55);
            klxPiaoPanel2.Name = "klxPiaoPanel2";
            klxPiaoPanel2.Size = new Size(173, 173);
            klxPiaoPanel2.TabIndex = 9;
            klxPiaoPanel2.启用投影 = false;
            klxPiaoPanel2.圆角大小 = new KlxPiaoAPI.CornerRadius(35F, 35F, 35F, 35F);
            klxPiaoPanel2.边框颜色 = Color.Gainsboro;
            // 
            // componentModelText
            // 
            componentModelText.BorderStyle = BorderStyle.None;
            componentModelText.Location = new Point(7, 7);
            componentModelText.Multiline = true;
            componentModelText.Name = "componentModelText";
            componentModelText.Size = new Size(160, 160);
            componentModelText.TabIndex = 0;
            componentModelText.Text = "ComponentModel";
            componentModelText.TextAlign = HorizontalAlignment.Center;
            // 
            // BezierCurveCodeShow
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(452, 316);
            Controls.Add(roundedButton2);
            Controls.Add(klxPiaoPanel2);
            Controls.Add(roundedButton1);
            Controls.Add(klxPiaoPanel1);
            Name = "BezierCurveCodeShow";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "CodeShowWindow";
            可调整大小 = false;
            标题位置 = 位置.中;
            标题按钮显示 = 标题按钮样式.仅关闭;
            边框颜色 = Color.FromArgb(128, 128, 255);
            klxPiaoPanel1.ResumeLayout(false);
            klxPiaoPanel1.PerformLayout();
            klxPiaoPanel2.ResumeLayout(false);
            klxPiaoPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private KlxPiaoControls.KlxPiaoPanel klxPiaoPanel1;
        private TextBox pointfshowTextBox;
        private KlxPiaoControls.RoundedButton roundedButton1;
        private KlxPiaoControls.RoundedButton roundedButton2;
        private KlxPiaoControls.KlxPiaoPanel klxPiaoPanel2;
        private TextBox componentModelText;
    }
}