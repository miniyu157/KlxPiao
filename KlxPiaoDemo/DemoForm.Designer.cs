using KlxPiaoControls;

namespace KlxPiaoDemo
{
    partial class DemoForm
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
            RoundedButton.AnimationConfigClass animationConfigClass1 = new RoundedButton.AnimationConfigClass();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoForm));
            RoundedButton.DisabledStyleClass disabledStyleClass1 = new RoundedButton.DisabledStyleClass();
            RoundedButton.InteractionStyleClass interactionStyleClass1 = new RoundedButton.InteractionStyleClass();
            KlxPiaoTrackBar.InteractionStyleClass interactionStyleClass2 = new KlxPiaoTrackBar.InteractionStyleClass();
            KlxPiaoTrackBar.InteractionStyleClass interactionStyleClass3 = new KlxPiaoTrackBar.InteractionStyleClass();
            KlxPiaoTrackBar.InteractionStyleClass interactionStyleClass4 = new KlxPiaoTrackBar.InteractionStyleClass();
            KlxPiaoTrackBar.InteractionStyleClass interactionStyleClass5 = new KlxPiaoTrackBar.InteractionStyleClass();
            KlxPiaoTrackBar.InteractionStyleClass interactionStyleClass6 = new KlxPiaoTrackBar.InteractionStyleClass();
            KlxPiaoTrackBar.InteractionStyleClass interactionStyleClass7 = new KlxPiaoTrackBar.InteractionStyleClass();
            KlxPiaoTrackBar.InteractionStyleClass interactionStyleClass8 = new KlxPiaoTrackBar.InteractionStyleClass();
            RoundedButton.AnimationConfigClass animationConfigClass2 = new RoundedButton.AnimationConfigClass();
            RoundedButton.DisabledStyleClass disabledStyleClass2 = new RoundedButton.DisabledStyleClass();
            RoundedButton.InteractionStyleClass interactionStyleClass9 = new RoundedButton.InteractionStyleClass();
            RoundedButton.AnimationConfigClass animationConfigClass3 = new RoundedButton.AnimationConfigClass();
            RoundedButton.DisabledStyleClass disabledStyleClass3 = new RoundedButton.DisabledStyleClass();
            RoundedButton.InteractionStyleClass interactionStyleClass10 = new RoundedButton.InteractionStyleClass();
            mainTabControl = new TabControl();
            homeTabPage = new TabPage();
            githubButton = new RoundedButton();
            welcomeLabel = new KlxPiaoLabel();
            homeTitleImage = new ImageBox();
            homeTitleLabel = new KlxPiaoLabel();
            propertiesTabPage = new TabPage();
            groupBox3 = new GroupBox();
            label9 = new Label();
            backColorPanel = new KlxPiaoPanel();
            label8 = new Label();
            titleButtonDisabledColorPanel = new KlxPiaoPanel();
            label10 = new Label();
            titleBoxForeColorPanel = new KlxPiaoPanel();
            label7 = new Label();
            titleBoxBackColorPanel = new KlxPiaoPanel();
            groupBox2 = new GroupBox();
            titleButtonCornerRadiusTrackBar = new KlxPiaoTrackBar();
            titleButtonHeightTrackBar = new KlxPiaoTrackBar();
            showIconCheckBox = new CheckBox();
            titleTextMarginTrackBar = new KlxPiaoTrackBar();
            label6 = new Label();
            titleTextAlignComboBox = new ComboBox();
            titleButtonWidthTrackBar = new KlxPiaoTrackBar();
            label5 = new Label();
            enableChangeInactiveTitleBoxForeColorCheckBox = new CheckBox();
            enableMinimizeButtonCheckBox = new CheckBox();
            titleButtonsComboBox = new ComboBox();
            titleButtonIconSizeTrackBar = new KlxPiaoTrackBar();
            titleButtonAlignComboBox = new ComboBox();
            enableCloseButtonCheckBox = new CheckBox();
            label4 = new Label();
            enableResizeButtonCheckBox = new CheckBox();
            titleBoxHeightTrackBar = new KlxPiaoTrackBar();
            interactionColorScaleTrackBar = new KlxPiaoTrackBar();
            groupBox1 = new GroupBox();
            label1 = new Label();
            shortcutResizeModeComboBox = new ComboBox();
            themeComboBox = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            resizableCheckBox = new CheckBox();
            dragModeComboBox = new ComboBox();
            enableResizeAnimationCheckBox = new CheckBox();
            controlsTabPage = new TabPage();
            klxPiaoPanel1 = new KlxPiaoPanel();
            klxPiaoListBox1 = new KlxPiaoListBox();
            roundedButton2 = new RoundedButton();
            roundedButton1 = new RoundedButton();
            titleTextOffsetPointBar = new PointBar();
            iconDrawOffsetPointBar = new PointBar();
            mainTabControl.SuspendLayout();
            homeTabPage.SuspendLayout();
            propertiesTabPage.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            controlsTabPage.SuspendLayout();
            klxPiaoPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(homeTabPage);
            mainTabControl.Controls.Add(propertiesTabPage);
            mainTabControl.Controls.Add(controlsTabPage);
            mainTabControl.Location = new Point(13, 47);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(735, 428);
            mainTabControl.TabIndex = 7;
            mainTabControl.Visible = false;
            // 
            // homeTabPage
            // 
            homeTabPage.BackColor = Color.White;
            homeTabPage.Controls.Add(githubButton);
            homeTabPage.Controls.Add(welcomeLabel);
            homeTabPage.Controls.Add(homeTitleImage);
            homeTabPage.Controls.Add(homeTitleLabel);
            homeTabPage.Location = new Point(4, 26);
            homeTabPage.Name = "homeTabPage";
            homeTabPage.Padding = new Padding(3);
            homeTabPage.Size = new Size(727, 398);
            homeTabPage.TabIndex = 1;
            homeTabPage.Text = "Home";
            // 
            // githubButton
            // 
            animationConfigClass1.BorderColorAnimation = (KlxPiaoAPI.AnimationInfo)resources.GetObject("animationConfigClass1.BorderColorAnimation");
            animationConfigClass1.BorderSizeAnimation = (KlxPiaoAPI.AnimationInfo)resources.GetObject("animationConfigClass1.BorderSizeAnimation");
            animationConfigClass1.ColorAnimation = (KlxPiaoAPI.AnimationInfo)resources.GetObject("animationConfigClass1.ColorAnimation");
            githubButton.AnimationConfig = animationConfigClass1;
            githubButton.BackColor = Color.White;
            githubButton.BorderSize = 1F;
            disabledStyleClass1.BackColor = Color.Empty;
            disabledStyleClass1.BorderColor = Color.Empty;
            disabledStyleClass1.ForeColor = Color.Empty;
            githubButton.DisabledStyle = disabledStyleClass1;
            githubButton.DrawBackColor = Color.White;
            githubButton.DrawBorderColor = Color.Gainsboro;
            githubButton.DrawBorderSize = 1F;
            githubButton.DrawForeColor = Color.Black;
            githubButton.ForeColor = Color.Black;
            githubButton.Image = Properties.Resources.github_128x128;
            githubButton.ImageAlign = ContentAlignment.MiddleLeft;
            githubButton.ImageOffset = new Point(8, 0);
            githubButton.ImageResizing = new SizeF(0.75F, 0.75F);
            githubButton.ImageResizingFormat = KlxPiaoAPI.ResizeMode.Percentage;
            interactionStyleClass1.DownBackColor = Color.FromArgb(235, 235, 235);
            interactionStyleClass1.DownBorderColor = Color.Empty;
            interactionStyleClass1.DownBorderSize = 0F;
            interactionStyleClass1.DownForeColor = Color.Empty;
            interactionStyleClass1.OverBackColor = Color.FromArgb(245, 245, 245);
            interactionStyleClass1.OverBorderColor = Color.Empty;
            interactionStyleClass1.OverBorderSize = 0F;
            interactionStyleClass1.OverForeColor = Color.Empty;
            githubButton.InteractionStyle = interactionStyleClass1;
            githubButton.IsEnableAnimation = true;
            githubButton.Location = new Point(6, 364);
            githubButton.Name = "githubButton";
            githubButton.Size = new Size(87, 28);
            githubButton.TabIndex = 4;
            githubButton.Text = "Github";
            githubButton.TextAlign = ContentAlignment.MiddleRight;
            githubButton.TextOffset = new Point(-8, 0);
            // 
            // welcomeLabel
            // 
            welcomeLabel.AutoSize = false;
            welcomeLabel.BackColor = Color.White;
            welcomeLabel.BorderColor = Color.FromArgb(199, 199, 199);
            welcomeLabel.BorderSize = 0;
            welcomeLabel.CornerRadius = new KlxPiaoAPI.CornerRadius(24F, 24F, 24F, 24F);
            welcomeLabel.DrawTextOffset = new Point(12, 12);
            welcomeLabel.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            welcomeLabel.ForeColor = Color.FromArgb(50, 50, 50);
            welcomeLabel.IsEnableBorder = true;
            welcomeLabel.IsEnableColorFading = true;
            welcomeLabel.IsEnableShadow = true;
            welcomeLabel.Location = new Point(6, 130);
            welcomeLabel.Name = "welcomeLabel";
            welcomeLabel.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            welcomeLabel.ShadowColor = Color.FromArgb(50, 50, 50);
            welcomeLabel.ShadowPosition = new Point(3, 3);
            welcomeLabel.Size = new Size(715, 227);
            welcomeLabel.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            welcomeLabel.TabIndex = 1;
            welcomeLabel.Text = "欢迎使用 KlxPiaoControls!\r\n\r\n主要针对于原版强化进行创作，目前 KlxPiaoControl 仍在不断优化中。\r\n你所看到的窗体实际上是 WinForms 无边框窗体 (FormBorderStyle = None)，所以任何元素都是可以接管的\r\n\r\n切换选项卡以预览 KlxPiaoControls 的功能";
            // 
            // homeTitleImage
            // 
            homeTitleImage.BackColor = Color.Gainsboro;
            homeTitleImage.BaseBackColor = Color.Gainsboro;
            homeTitleImage.Image = Properties.Resources.KLXPIAO;
            homeTitleImage.ImageCornerRadius = new KlxPiaoAPI.CornerRadius(1F, 1F, 1F, 1F);
            homeTitleImage.ImageResizing = new SizeF(0.75F, 0.75F);
            homeTitleImage.ImageResizingFormat = KlxPiaoAPI.ResizeMode.Percentage;
            homeTitleImage.Location = new Point(21, 8);
            homeTitleImage.Name = "homeTitleImage";
            homeTitleImage.Size = new Size(232, 116);
            homeTitleImage.TabIndex = 3;
            homeTitleImage.Text = "imageBox1";
            // 
            // homeTitleLabel
            // 
            homeTitleLabel.AutoSize = false;
            homeTitleLabel.BackColor = Color.Gainsboro;
            homeTitleLabel.BorderColor = Color.FromArgb(199, 199, 199);
            homeTitleLabel.BorderSize = 0;
            homeTitleLabel.CornerRadius = new KlxPiaoAPI.CornerRadius(24F, 24F, 24F, 24F);
            homeTitleLabel.DrawTextOffset = new Point(50, 0);
            homeTitleLabel.Font = new Font("Microsoft YaHei UI", 30F, FontStyle.Regular, GraphicsUnit.Point, 134);
            homeTitleLabel.ForeColor = Color.White;
            homeTitleLabel.IsEnableBorder = true;
            homeTitleLabel.IsEnableShadow = true;
            homeTitleLabel.Location = new Point(6, 8);
            homeTitleLabel.Name = "homeTitleLabel";
            homeTitleLabel.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            homeTitleLabel.ShadowColor = Color.Gray;
            homeTitleLabel.ShadowPosition = new Point(71, 71);
            homeTitleLabel.Size = new Size(715, 116);
            homeTitleLabel.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            homeTitleLabel.TabIndex = 0;
            homeTitleLabel.Text = "KlxPiaoControls";
            homeTitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            homeTitleLabel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            // 
            // propertiesTabPage
            // 
            propertiesTabPage.BackColor = Color.White;
            propertiesTabPage.Controls.Add(groupBox3);
            propertiesTabPage.Controls.Add(groupBox2);
            propertiesTabPage.Controls.Add(groupBox1);
            propertiesTabPage.Location = new Point(4, 26);
            propertiesTabPage.Name = "propertiesTabPage";
            propertiesTabPage.Padding = new Padding(3);
            propertiesTabPage.Size = new Size(727, 398);
            propertiesTabPage.TabIndex = 0;
            propertiesTabPage.Text = "Form Properties";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(backColorPanel);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(titleButtonDisabledColorPanel);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(titleBoxForeColorPanel);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(titleBoxBackColorPanel);
            groupBox3.Location = new Point(21, 19);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(290, 195);
            groupBox3.TabIndex = 15;
            groupBox3.TabStop = false;
            groupBox3.Text = "Colors";
            // 
            // label9
            // 
            label9.Font = new Font("Microsoft YaHei UI", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label9.Location = new Point(152, 161);
            label9.Name = "label9";
            label9.Size = new Size(130, 16);
            label9.TabIndex = 7;
            label9.Text = "BackColor";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // backColorPanel
            // 
            backColorPanel.BackColor = Color.White;
            backColorPanel.BorderSize = 1F;
            backColorPanel.CornerRadius = new KlxPiaoAPI.CornerRadius(12F, 12F, 12F, 12F);
            backColorPanel.Cursor = Cursors.Hand;
            backColorPanel.IsEnableShadow = false;
            backColorPanel.Location = new Point(192, 108);
            backColorPanel.Name = "backColorPanel";
            backColorPanel.Size = new Size(50, 50);
            backColorPanel.TabIndex = 6;
            // 
            // label8
            // 
            label8.Font = new Font("Microsoft YaHei UI", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label8.Location = new Point(152, 83);
            label8.Name = "label8";
            label8.Size = new Size(130, 16);
            label8.TabIndex = 5;
            label8.Text = "TitleButtonDisabledColor";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // titleButtonDisabledColorPanel
            // 
            titleButtonDisabledColorPanel.BackColor = Color.White;
            titleButtonDisabledColorPanel.BorderSize = 1F;
            titleButtonDisabledColorPanel.CornerRadius = new KlxPiaoAPI.CornerRadius(12F, 12F, 12F, 12F);
            titleButtonDisabledColorPanel.Cursor = Cursors.Hand;
            titleButtonDisabledColorPanel.IsEnableShadow = false;
            titleButtonDisabledColorPanel.Location = new Point(192, 30);
            titleButtonDisabledColorPanel.Name = "titleButtonDisabledColorPanel";
            titleButtonDisabledColorPanel.Size = new Size(50, 50);
            titleButtonDisabledColorPanel.TabIndex = 4;
            // 
            // label10
            // 
            label10.Font = new Font("Microsoft YaHei UI", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label10.Location = new Point(6, 161);
            label10.Name = "label10";
            label10.Size = new Size(132, 16);
            label10.TabIndex = 3;
            label10.Text = "TitleBoxForeColor";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // titleBoxForeColorPanel
            // 
            titleBoxForeColorPanel.BackColor = Color.White;
            titleBoxForeColorPanel.BorderSize = 1F;
            titleBoxForeColorPanel.CornerRadius = new KlxPiaoAPI.CornerRadius(12F, 12F, 12F, 12F);
            titleBoxForeColorPanel.Cursor = Cursors.Hand;
            titleBoxForeColorPanel.IsEnableShadow = false;
            titleBoxForeColorPanel.Location = new Point(48, 108);
            titleBoxForeColorPanel.Name = "titleBoxForeColorPanel";
            titleBoxForeColorPanel.Size = new Size(50, 50);
            titleBoxForeColorPanel.TabIndex = 2;
            // 
            // label7
            // 
            label7.Font = new Font("Microsoft YaHei UI", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label7.Location = new Point(6, 83);
            label7.Name = "label7";
            label7.Size = new Size(136, 16);
            label7.TabIndex = 1;
            label7.Text = "(ThemeColor)TitleBoxBackColor";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // titleBoxBackColorPanel
            // 
            titleBoxBackColorPanel.BackColor = Color.White;
            titleBoxBackColorPanel.BorderSize = 1F;
            titleBoxBackColorPanel.CornerRadius = new KlxPiaoAPI.CornerRadius(12F, 12F, 12F, 12F);
            titleBoxBackColorPanel.Cursor = Cursors.Hand;
            titleBoxBackColorPanel.IsEnableShadow = false;
            titleBoxBackColorPanel.Location = new Point(48, 30);
            titleBoxBackColorPanel.Name = "titleBoxBackColorPanel";
            titleBoxBackColorPanel.Size = new Size(50, 50);
            titleBoxBackColorPanel.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(titleButtonCornerRadiusTrackBar);
            groupBox2.Controls.Add(titleButtonHeightTrackBar);
            groupBox2.Controls.Add(showIconCheckBox);
            groupBox2.Controls.Add(titleTextMarginTrackBar);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(titleTextAlignComboBox);
            groupBox2.Controls.Add(titleButtonWidthTrackBar);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(enableChangeInactiveTitleBoxForeColorCheckBox);
            groupBox2.Controls.Add(enableMinimizeButtonCheckBox);
            groupBox2.Controls.Add(titleButtonsComboBox);
            groupBox2.Controls.Add(titleButtonIconSizeTrackBar);
            groupBox2.Controls.Add(titleButtonAlignComboBox);
            groupBox2.Controls.Add(enableCloseButtonCheckBox);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(enableResizeButtonCheckBox);
            groupBox2.Controls.Add(titleBoxHeightTrackBar);
            groupBox2.Controls.Add(interactionColorScaleTrackBar);
            groupBox2.Location = new Point(324, 19);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(382, 360);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "Title Box";
            // 
            // titleButtonCornerRadiusTrackBar
            // 
            titleButtonCornerRadiusTrackBar.BackColor = Color.White;
            titleButtonCornerRadiusTrackBar.BorderColor = Color.Gray;
            titleButtonCornerRadiusTrackBar.BorderSize = 1;
            titleButtonCornerRadiusTrackBar.CornerRadius = 6F;
            titleButtonCornerRadiusTrackBar.DecimalPlaces = 2;
            titleButtonCornerRadiusTrackBar.Font = new Font("Microsoft YaHei UI", 8.25F);
            interactionStyleClass2.FocusBorderColor = null;
            interactionStyleClass2.FocusBorderSize = null;
            interactionStyleClass2.FocusTrackBackColor = null;
            interactionStyleClass2.FocusTrackForeColor = null;
            interactionStyleClass2.MouseOverBorderColor = null;
            interactionStyleClass2.MouseOverBorderSize = null;
            interactionStyleClass2.MouseOverTrackBackColor = null;
            interactionStyleClass2.MouseOverTrackForeColor = null;
            titleButtonCornerRadiusTrackBar.InteractionStyle = interactionStyleClass2;
            titleButtonCornerRadiusTrackBar.IsDrawValueText = true;
            titleButtonCornerRadiusTrackBar.Location = new Point(19, 111);
            titleButtonCornerRadiusTrackBar.Name = "titleButtonCornerRadiusTrackBar";
            titleButtonCornerRadiusTrackBar.ResponseSize = 0.01F;
            titleButtonCornerRadiusTrackBar.Size = new Size(166, 21);
            titleButtonCornerRadiusTrackBar.TabIndex = 28;
            titleButtonCornerRadiusTrackBar.TrackBackColor = Color.White;
            titleButtonCornerRadiusTrackBar.TrackForeColor = Color.LightGray;
            titleButtonCornerRadiusTrackBar.ValueTextDisplayFormat = "TitleButtonCornerRadius: {value}";
            // 
            // titleButtonHeightTrackBar
            // 
            titleButtonHeightTrackBar.BackColor = Color.White;
            titleButtonHeightTrackBar.BorderColor = Color.Gray;
            titleButtonHeightTrackBar.BorderSize = 1;
            titleButtonHeightTrackBar.CornerRadius = 6F;
            titleButtonHeightTrackBar.Font = new Font("Microsoft YaHei UI", 8.25F);
            interactionStyleClass3.FocusBorderColor = null;
            interactionStyleClass3.FocusBorderSize = null;
            interactionStyleClass3.FocusTrackBackColor = null;
            interactionStyleClass3.FocusTrackForeColor = null;
            interactionStyleClass3.MouseOverBorderColor = null;
            interactionStyleClass3.MouseOverBorderSize = null;
            interactionStyleClass3.MouseOverTrackBackColor = null;
            interactionStyleClass3.MouseOverTrackForeColor = null;
            titleButtonHeightTrackBar.InteractionStyle = interactionStyleClass3;
            titleButtonHeightTrackBar.IsDrawValueText = true;
            titleButtonHeightTrackBar.Location = new Point(197, 57);
            titleButtonHeightTrackBar.Name = "titleButtonHeightTrackBar";
            titleButtonHeightTrackBar.Size = new Size(166, 21);
            titleButtonHeightTrackBar.TabIndex = 27;
            titleButtonHeightTrackBar.TrackBackColor = Color.White;
            titleButtonHeightTrackBar.TrackForeColor = Color.LightGray;
            titleButtonHeightTrackBar.ValueTextDisplayFormat = "TitleButtonHeight: {value}";
            // 
            // showIconCheckBox
            // 
            showIconCheckBox.AutoSize = true;
            showIconCheckBox.Location = new Point(19, 147);
            showIconCheckBox.Name = "showIconCheckBox";
            showIconCheckBox.Size = new Size(83, 21);
            showIconCheckBox.TabIndex = 26;
            showIconCheckBox.Text = "ShowIcon";
            showIconCheckBox.UseVisualStyleBackColor = true;
            // 
            // titleTextMarginTrackBar
            // 
            titleTextMarginTrackBar.BackColor = Color.White;
            titleTextMarginTrackBar.BorderColor = Color.Gray;
            titleTextMarginTrackBar.BorderSize = 1;
            titleTextMarginTrackBar.CornerRadius = 6F;
            titleTextMarginTrackBar.Font = new Font("Microsoft YaHei UI", 8.25F);
            interactionStyleClass4.FocusBorderColor = null;
            interactionStyleClass4.FocusBorderSize = null;
            interactionStyleClass4.FocusTrackBackColor = null;
            interactionStyleClass4.FocusTrackForeColor = null;
            interactionStyleClass4.MouseOverBorderColor = null;
            interactionStyleClass4.MouseOverBorderSize = null;
            interactionStyleClass4.MouseOverTrackBackColor = null;
            interactionStyleClass4.MouseOverTrackForeColor = null;
            titleTextMarginTrackBar.InteractionStyle = interactionStyleClass4;
            titleTextMarginTrackBar.IsDrawValueText = true;
            titleTextMarginTrackBar.Location = new Point(197, 84);
            titleTextMarginTrackBar.Name = "titleTextMarginTrackBar";
            titleTextMarginTrackBar.Size = new Size(166, 21);
            titleTextMarginTrackBar.TabIndex = 24;
            titleTextMarginTrackBar.TrackBackColor = Color.White;
            titleTextMarginTrackBar.TrackForeColor = Color.LightGray;
            titleTextMarginTrackBar.ValueTextDisplayFormat = "TitleTextMargin: {value}";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(17, 248);
            label6.Name = "label6";
            label6.Size = new Size(85, 17);
            label6.TabIndex = 22;
            label6.Text = "TitleTextAlign";
            // 
            // titleTextAlignComboBox
            // 
            titleTextAlignComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            titleTextAlignComboBox.FormattingEnabled = true;
            titleTextAlignComboBox.Location = new Point(120, 245);
            titleTextAlignComboBox.Name = "titleTextAlignComboBox";
            titleTextAlignComboBox.Size = new Size(121, 25);
            titleTextAlignComboBox.TabIndex = 23;
            // 
            // titleButtonWidthTrackBar
            // 
            titleButtonWidthTrackBar.BackColor = Color.White;
            titleButtonWidthTrackBar.BorderColor = Color.Gray;
            titleButtonWidthTrackBar.BorderSize = 1;
            titleButtonWidthTrackBar.CornerRadius = 6F;
            titleButtonWidthTrackBar.Font = new Font("Microsoft YaHei UI", 8.25F);
            interactionStyleClass5.FocusBorderColor = null;
            interactionStyleClass5.FocusBorderSize = null;
            interactionStyleClass5.FocusTrackBackColor = null;
            interactionStyleClass5.FocusTrackForeColor = null;
            interactionStyleClass5.MouseOverBorderColor = null;
            interactionStyleClass5.MouseOverBorderSize = null;
            interactionStyleClass5.MouseOverTrackBackColor = null;
            interactionStyleClass5.MouseOverTrackForeColor = null;
            titleButtonWidthTrackBar.InteractionStyle = interactionStyleClass5;
            titleButtonWidthTrackBar.IsDrawValueText = true;
            titleButtonWidthTrackBar.Location = new Point(197, 30);
            titleButtonWidthTrackBar.Name = "titleButtonWidthTrackBar";
            titleButtonWidthTrackBar.Size = new Size(166, 21);
            titleButtonWidthTrackBar.TabIndex = 21;
            titleButtonWidthTrackBar.TrackBackColor = Color.White;
            titleButtonWidthTrackBar.TrackForeColor = Color.LightGray;
            titleButtonWidthTrackBar.ValueTextDisplayFormat = "TitleButtonWidth: {value}";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 310);
            label5.Name = "label5";
            label5.Size = new Size(76, 17);
            label5.TabIndex = 19;
            label5.Text = "TitleButtons";
            // 
            // enableChangeInactiveTitleBoxForeColorCheckBox
            // 
            enableChangeInactiveTitleBoxForeColorCheckBox.AutoSize = true;
            enableChangeInactiveTitleBoxForeColorCheckBox.Location = new Point(19, 198);
            enableChangeInactiveTitleBoxForeColorCheckBox.Name = "enableChangeInactiveTitleBoxForeColorCheckBox";
            enableChangeInactiveTitleBoxForeColorCheckBox.Size = new Size(154, 38);
            enableChangeInactiveTitleBoxForeColorCheckBox.TabIndex = 10;
            enableChangeInactiveTitleBoxForeColorCheckBox.Text = "EnableChangeInactive\r\nTitleBoxForeColor";
            enableChangeInactiveTitleBoxForeColorCheckBox.UseVisualStyleBackColor = true;
            // 
            // enableMinimizeButtonCheckBox
            // 
            enableMinimizeButtonCheckBox.AutoSize = true;
            enableMinimizeButtonCheckBox.Location = new Point(197, 177);
            enableMinimizeButtonCheckBox.Name = "enableMinimizeButtonCheckBox";
            enableMinimizeButtonCheckBox.Size = new Size(156, 21);
            enableMinimizeButtonCheckBox.TabIndex = 13;
            enableMinimizeButtonCheckBox.Text = "EnableMinimizeButton";
            enableMinimizeButtonCheckBox.UseVisualStyleBackColor = true;
            // 
            // titleButtonsComboBox
            // 
            titleButtonsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            titleButtonsComboBox.FormattingEnabled = true;
            titleButtonsComboBox.Location = new Point(120, 307);
            titleButtonsComboBox.Name = "titleButtonsComboBox";
            titleButtonsComboBox.Size = new Size(121, 25);
            titleButtonsComboBox.TabIndex = 20;
            // 
            // titleButtonIconSizeTrackBar
            // 
            titleButtonIconSizeTrackBar.BackColor = Color.White;
            titleButtonIconSizeTrackBar.BorderColor = Color.Gray;
            titleButtonIconSizeTrackBar.BorderSize = 1;
            titleButtonIconSizeTrackBar.CornerRadius = 6F;
            titleButtonIconSizeTrackBar.Font = new Font("Microsoft YaHei UI", 8.25F);
            interactionStyleClass6.FocusBorderColor = null;
            interactionStyleClass6.FocusBorderSize = null;
            interactionStyleClass6.FocusTrackBackColor = null;
            interactionStyleClass6.FocusTrackForeColor = null;
            interactionStyleClass6.MouseOverBorderColor = null;
            interactionStyleClass6.MouseOverBorderSize = null;
            interactionStyleClass6.MouseOverTrackBackColor = null;
            interactionStyleClass6.MouseOverTrackForeColor = null;
            titleButtonIconSizeTrackBar.InteractionStyle = interactionStyleClass6;
            titleButtonIconSizeTrackBar.IsDrawValueText = true;
            titleButtonIconSizeTrackBar.Location = new Point(19, 84);
            titleButtonIconSizeTrackBar.Name = "titleButtonIconSizeTrackBar";
            titleButtonIconSizeTrackBar.Size = new Size(166, 21);
            titleButtonIconSizeTrackBar.TabIndex = 18;
            titleButtonIconSizeTrackBar.TrackBackColor = Color.White;
            titleButtonIconSizeTrackBar.TrackForeColor = Color.LightGray;
            titleButtonIconSizeTrackBar.ValueTextDisplayFormat = "TitleButtonIconSize: {value}x{value}";
            // 
            // titleButtonAlignComboBox
            // 
            titleButtonAlignComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            titleButtonAlignComboBox.FormattingEnabled = true;
            titleButtonAlignComboBox.Location = new Point(120, 276);
            titleButtonAlignComboBox.Name = "titleButtonAlignComboBox";
            titleButtonAlignComboBox.Size = new Size(121, 25);
            titleButtonAlignComboBox.TabIndex = 17;
            // 
            // enableCloseButtonCheckBox
            // 
            enableCloseButtonCheckBox.AutoSize = true;
            enableCloseButtonCheckBox.Location = new Point(19, 177);
            enableCloseButtonCheckBox.Name = "enableCloseButtonCheckBox";
            enableCloseButtonCheckBox.Size = new Size(136, 21);
            enableCloseButtonCheckBox.TabIndex = 11;
            enableCloseButtonCheckBox.Text = "EnableCloseButton";
            enableCloseButtonCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 280);
            label4.Name = "label4";
            label4.Size = new Size(99, 17);
            label4.TabIndex = 16;
            label4.Text = "TitleButtonAlign";
            // 
            // enableResizeButtonCheckBox
            // 
            enableResizeButtonCheckBox.AutoSize = true;
            enableResizeButtonCheckBox.Location = new Point(197, 147);
            enableResizeButtonCheckBox.Name = "enableResizeButtonCheckBox";
            enableResizeButtonCheckBox.Size = new Size(141, 21);
            enableResizeButtonCheckBox.TabIndex = 12;
            enableResizeButtonCheckBox.Text = "EnableResizeButton";
            enableResizeButtonCheckBox.UseVisualStyleBackColor = true;
            // 
            // titleBoxHeightTrackBar
            // 
            titleBoxHeightTrackBar.BackColor = Color.White;
            titleBoxHeightTrackBar.BorderColor = Color.Gray;
            titleBoxHeightTrackBar.BorderSize = 1;
            titleBoxHeightTrackBar.CornerRadius = 6F;
            titleBoxHeightTrackBar.Font = new Font("Microsoft YaHei UI", 8.25F);
            interactionStyleClass7.FocusBorderColor = null;
            interactionStyleClass7.FocusBorderSize = null;
            interactionStyleClass7.FocusTrackBackColor = null;
            interactionStyleClass7.FocusTrackForeColor = null;
            interactionStyleClass7.MouseOverBorderColor = null;
            interactionStyleClass7.MouseOverBorderSize = null;
            interactionStyleClass7.MouseOverTrackBackColor = null;
            interactionStyleClass7.MouseOverTrackForeColor = null;
            titleBoxHeightTrackBar.InteractionStyle = interactionStyleClass7;
            titleBoxHeightTrackBar.IsDrawValueText = true;
            titleBoxHeightTrackBar.Location = new Point(19, 57);
            titleBoxHeightTrackBar.Name = "titleBoxHeightTrackBar";
            titleBoxHeightTrackBar.Size = new Size(166, 21);
            titleBoxHeightTrackBar.TabIndex = 15;
            titleBoxHeightTrackBar.TrackBackColor = Color.White;
            titleBoxHeightTrackBar.TrackForeColor = Color.LightGray;
            titleBoxHeightTrackBar.ValueTextDisplayFormat = "TitleBoxHeight: {value}";
            // 
            // interactionColorScaleTrackBar
            // 
            interactionColorScaleTrackBar.BackColor = Color.White;
            interactionColorScaleTrackBar.BorderColor = Color.Gray;
            interactionColorScaleTrackBar.BorderSize = 1;
            interactionColorScaleTrackBar.CornerRadius = 6F;
            interactionColorScaleTrackBar.DecimalPlaces = 2;
            interactionColorScaleTrackBar.Font = new Font("Microsoft YaHei UI", 8.25F);
            interactionStyleClass8.FocusBorderColor = null;
            interactionStyleClass8.FocusBorderSize = null;
            interactionStyleClass8.FocusTrackBackColor = null;
            interactionStyleClass8.FocusTrackForeColor = null;
            interactionStyleClass8.MouseOverBorderColor = null;
            interactionStyleClass8.MouseOverBorderSize = null;
            interactionStyleClass8.MouseOverTrackBackColor = null;
            interactionStyleClass8.MouseOverTrackForeColor = null;
            interactionColorScaleTrackBar.InteractionStyle = interactionStyleClass8;
            interactionColorScaleTrackBar.IsAutoComplete = true;
            interactionColorScaleTrackBar.IsDrawValueText = true;
            interactionColorScaleTrackBar.Location = new Point(19, 30);
            interactionColorScaleTrackBar.MaxValue = 1F;
            interactionColorScaleTrackBar.MinValue = -1F;
            interactionColorScaleTrackBar.Name = "interactionColorScaleTrackBar";
            interactionColorScaleTrackBar.ResponseSize = 0.01F;
            interactionColorScaleTrackBar.Size = new Size(166, 21);
            interactionColorScaleTrackBar.TabIndex = 14;
            interactionColorScaleTrackBar.TrackBackColor = Color.White;
            interactionColorScaleTrackBar.TrackForeColor = Color.LightGray;
            interactionColorScaleTrackBar.ValueTextDisplayFormat = "InteractionColorScale: {value}";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(shortcutResizeModeComboBox);
            groupBox1.Controls.Add(themeComboBox);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(resizableCheckBox);
            groupBox1.Controls.Add(dragModeComboBox);
            groupBox1.Controls.Add(enableResizeAnimationCheckBox);
            groupBox1.Location = new Point(21, 218);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(290, 161);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Properties";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 30);
            label1.Name = "label1";
            label1.Size = new Size(47, 17);
            label1.TabIndex = 1;
            label1.Text = "Theme";
            // 
            // shortcutResizeModeComboBox
            // 
            shortcutResizeModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            shortcutResizeModeComboBox.FormattingEnabled = true;
            shortcutResizeModeComboBox.Location = new Point(150, 89);
            shortcutResizeModeComboBox.Name = "shortcutResizeModeComboBox";
            shortcutResizeModeComboBox.Size = new Size(121, 25);
            shortcutResizeModeComboBox.TabIndex = 8;
            // 
            // themeComboBox
            // 
            themeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            themeComboBox.FormattingEnabled = true;
            themeComboBox.Location = new Point(150, 27);
            themeComboBox.Name = "themeComboBox";
            themeComboBox.Size = new Size(121, 25);
            themeComboBox.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 92);
            label3.Name = "label3";
            label3.Size = new Size(128, 17);
            label3.TabIndex = 7;
            label3.Text = "ShortcutResizeMode";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 61);
            label2.Name = "label2";
            label2.Size = new Size(72, 17);
            label2.TabIndex = 3;
            label2.Text = "DragMode";
            // 
            // resizableCheckBox
            // 
            resizableCheckBox.AutoSize = true;
            resizableCheckBox.Location = new Point(150, 126);
            resizableCheckBox.Name = "resizableCheckBox";
            resizableCheckBox.Size = new Size(82, 21);
            resizableCheckBox.TabIndex = 6;
            resizableCheckBox.Text = "Resizable";
            resizableCheckBox.UseVisualStyleBackColor = true;
            // 
            // dragModeComboBox
            // 
            dragModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            dragModeComboBox.FormattingEnabled = true;
            dragModeComboBox.Location = new Point(150, 58);
            dragModeComboBox.Name = "dragModeComboBox";
            dragModeComboBox.Size = new Size(121, 25);
            dragModeComboBox.TabIndex = 4;
            // 
            // enableResizeAnimationCheckBox
            // 
            enableResizeAnimationCheckBox.AutoSize = true;
            enableResizeAnimationCheckBox.Location = new Point(16, 126);
            enableResizeAnimationCheckBox.Name = "enableResizeAnimationCheckBox";
            enableResizeAnimationCheckBox.Size = new Size(122, 21);
            enableResizeAnimationCheckBox.TabIndex = 5;
            enableResizeAnimationCheckBox.Text = "ResizeAnimation";
            enableResizeAnimationCheckBox.UseVisualStyleBackColor = true;
            // 
            // controlsTabPage
            // 
            controlsTabPage.BackColor = Color.White;
            controlsTabPage.Controls.Add(klxPiaoPanel1);
            controlsTabPage.Controls.Add(roundedButton2);
            controlsTabPage.Controls.Add(roundedButton1);
            controlsTabPage.Location = new Point(4, 26);
            controlsTabPage.Name = "controlsTabPage";
            controlsTabPage.Padding = new Padding(3);
            controlsTabPage.Size = new Size(727, 398);
            controlsTabPage.TabIndex = 2;
            controlsTabPage.Text = "Controls";
            // 
            // klxPiaoPanel1
            // 
            klxPiaoPanel1.BackColor = Color.White;
            klxPiaoPanel1.BorderColor = Color.Pink;
            klxPiaoPanel1.BorderSize = 2F;
            klxPiaoPanel1.Controls.Add(klxPiaoListBox1);
            klxPiaoPanel1.CornerRadius = new KlxPiaoAPI.CornerRadius(20F, 20F, 20F, 20F);
            klxPiaoPanel1.IsEnableShadow = false;
            klxPiaoPanel1.Location = new Point(82, 107);
            klxPiaoPanel1.Name = "klxPiaoPanel1";
            klxPiaoPanel1.Size = new Size(161, 202);
            klxPiaoPanel1.TabIndex = 5;
            // 
            // klxPiaoListBox1
            // 
            klxPiaoListBox1.BorderStyle = BorderStyle.None;
            klxPiaoListBox1.DrawMode = DrawMode.OwnerDrawFixed;
            klxPiaoListBox1.FormattingEnabled = true;
            klxPiaoListBox1.ItemHeight = 30;
            klxPiaoListBox1.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
            klxPiaoListBox1.Location = new Point(2, 12);
            klxPiaoListBox1.Name = "klxPiaoListBox1";
            klxPiaoListBox1.SelectedBackColor = Color.Pink;
            klxPiaoListBox1.SelectedForeColor = Color.Empty;
            klxPiaoListBox1.Size = new Size(157, 180);
            klxPiaoListBox1.TabIndex = 4;
            // 
            // roundedButton2
            // 
            animationConfigClass2.BorderColorAnimation = (KlxPiaoAPI.AnimationInfo)resources.GetObject("animationConfigClass2.BorderColorAnimation");
            animationConfigClass2.BorderSizeAnimation = (KlxPiaoAPI.AnimationInfo)resources.GetObject("animationConfigClass2.BorderSizeAnimation");
            animationConfigClass2.ColorAnimation = (KlxPiaoAPI.AnimationInfo)resources.GetObject("animationConfigClass2.ColorAnimation");
            roundedButton2.AnimationConfig = animationConfigClass2;
            roundedButton2.BackColor = Color.White;
            roundedButton2.BorderSize = 1F;
            disabledStyleClass2.BackColor = Color.Empty;
            disabledStyleClass2.BorderColor = Color.Empty;
            disabledStyleClass2.ForeColor = Color.Empty;
            roundedButton2.DisabledStyle = disabledStyleClass2;
            roundedButton2.DrawBackColor = Color.White;
            roundedButton2.DrawBorderColor = Color.Gainsboro;
            roundedButton2.DrawBorderSize = 1F;
            roundedButton2.DrawForeColor = Color.Black;
            roundedButton2.ForeColor = Color.Black;
            interactionStyleClass9.DownBackColor = Color.Gainsboro;
            interactionStyleClass9.DownBorderColor = Color.Empty;
            interactionStyleClass9.DownBorderSize = 0F;
            interactionStyleClass9.DownForeColor = Color.Empty;
            interactionStyleClass9.OverBackColor = Color.Empty;
            interactionStyleClass9.OverBorderColor = Color.Pink;
            interactionStyleClass9.OverBorderSize = 3F;
            interactionStyleClass9.OverForeColor = Color.Empty;
            roundedButton2.InteractionStyle = interactionStyleClass9;
            roundedButton2.IsEnableAnimation = true;
            roundedButton2.Location = new Point(290, 46);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(129, 43);
            roundedButton2.TabIndex = 3;
            roundedButton2.Text = "KlxPiaoLabelDemo";
            // 
            // roundedButton1
            // 
            animationConfigClass3.BorderColorAnimation = (KlxPiaoAPI.AnimationInfo)resources.GetObject("animationConfigClass3.BorderColorAnimation");
            animationConfigClass3.BorderSizeAnimation = (KlxPiaoAPI.AnimationInfo)resources.GetObject("animationConfigClass3.BorderSizeAnimation");
            animationConfigClass3.ColorAnimation = (KlxPiaoAPI.AnimationInfo)resources.GetObject("animationConfigClass3.ColorAnimation");
            roundedButton1.AnimationConfig = animationConfigClass3;
            roundedButton1.BackColor = Color.White;
            roundedButton1.BorderSize = 1F;
            disabledStyleClass3.BackColor = Color.Empty;
            disabledStyleClass3.BorderColor = Color.Empty;
            disabledStyleClass3.ForeColor = Color.Empty;
            roundedButton1.DisabledStyle = disabledStyleClass3;
            roundedButton1.DrawBackColor = Color.White;
            roundedButton1.DrawBorderColor = Color.Gainsboro;
            roundedButton1.DrawBorderSize = 1F;
            roundedButton1.DrawForeColor = Color.Black;
            roundedButton1.ForeColor = Color.Black;
            interactionStyleClass10.DownBackColor = Color.FromArgb(235, 235, 235);
            interactionStyleClass10.DownBorderColor = Color.Empty;
            interactionStyleClass10.DownBorderSize = 0F;
            interactionStyleClass10.DownForeColor = Color.Empty;
            interactionStyleClass10.OverBackColor = Color.FromArgb(245, 245, 245);
            interactionStyleClass10.OverBorderColor = Color.Empty;
            interactionStyleClass10.OverBorderSize = 0F;
            interactionStyleClass10.OverForeColor = Color.Empty;
            roundedButton1.InteractionStyle = interactionStyleClass10;
            roundedButton1.IsEnableAnimation = true;
            roundedButton1.Location = new Point(82, 34);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(116, 43);
            roundedButton1.TabIndex = 2;
            roundedButton1.Text = "roundedButton1";
            // 
            // titleTextOffsetPointBar
            // 
            titleTextOffsetPointBar.BackColor = Color.White;
            titleTextOffsetPointBar.CoordinateDisplayFormat = "TitleTextOffset: {X}, {Y}";
            titleTextOffsetPointBar.CoordinateTextAlign = ContentAlignment.BottomCenter;
            titleTextOffsetPointBar.CoordinateTextOffset = new Point(0, -2);
            titleTextOffsetPointBar.CornerRadius = new KlxPiaoAPI.CornerRadius(1F, 1F, 1F, 1F);
            titleTextOffsetPointBar.Font = new Font("Microsoft YaHei UI", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            titleTextOffsetPointBar.Location = new Point(484, 11);
            titleTextOffsetPointBar.MaxValue = new Point(10, 10);
            titleTextOffsetPointBar.MinValue = new Point(-10, -10);
            titleTextOffsetPointBar.Name = "titleTextOffsetPointBar";
            titleTextOffsetPointBar.Size = new Size(105, 105);
            titleTextOffsetPointBar.TabIndex = 25;
            titleTextOffsetPointBar.Visible = false;
            // 
            // iconDrawOffsetPointBar
            // 
            iconDrawOffsetPointBar.BackColor = Color.White;
            iconDrawOffsetPointBar.CoordinateDisplayFormat = "IconDrawOffset: {X}, {Y}";
            iconDrawOffsetPointBar.CoordinateTextAlign = ContentAlignment.BottomCenter;
            iconDrawOffsetPointBar.CoordinateTextOffset = new Point(0, -2);
            iconDrawOffsetPointBar.CornerRadius = new KlxPiaoAPI.CornerRadius(6F, 6F, 6F, 6F);
            iconDrawOffsetPointBar.Font = new Font("Microsoft YaHei UI", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            iconDrawOffsetPointBar.Location = new Point(331, 11);
            iconDrawOffsetPointBar.MaxValue = new Point(10, 10);
            iconDrawOffsetPointBar.MinValue = new Point(-10, -10);
            iconDrawOffsetPointBar.Name = "iconDrawOffsetPointBar";
            iconDrawOffsetPointBar.Size = new Size(105, 105);
            iconDrawOffsetPointBar.TabIndex = 28;
            iconDrawOffsetPointBar.Visible = false;
            // 
            // DemoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = Properties.Resources.testbackground;
            ClientSize = new Size(760, 487);
            Controls.Add(titleTextOffsetPointBar);
            Controls.Add(iconDrawOffsetPointBar);
            Controls.Add(mainTabControl);
            EnableTitleButtonAnimation = true;
            Name = "DemoForm";
            SizeChangeRefreshMode = RefreshMode.Window;
            TitleBoxBackColor = Color.White;
            TitleBoxHeight = 50;
            mainTabControl.ResumeLayout(false);
            homeTabPage.ResumeLayout(false);
            propertiesTabPage.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            controlsTabPage.ResumeLayout(false);
            klxPiaoPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TabControl mainTabControl;
        private TabPage propertiesTabPage;
        private TabPage homeTabPage;
        private Label label1;
        private ComboBox themeComboBox;
        private ComboBox dragModeComboBox;
        private Label label2;
        private CheckBox enableResizeAnimationCheckBox;
        private CheckBox resizableCheckBox;
        private ComboBox shortcutResizeModeComboBox;
        private Label label3;
        private GroupBox groupBox1;
        private CheckBox enableChangeInactiveTitleBoxForeColorCheckBox;
        private CheckBox enableMinimizeButtonCheckBox;
        private CheckBox enableResizeButtonCheckBox;
        private CheckBox enableCloseButtonCheckBox;
        private GroupBox groupBox2;
        private KlxPiaoTrackBar interactionColorScaleTrackBar;
        private KlxPiaoTrackBar titleBoxHeightTrackBar;
        private ComboBox titleButtonAlignComboBox;
        private Label label4;
        private KlxPiaoTrackBar titleButtonIconSizeTrackBar;
        private ComboBox titleButtonsComboBox;
        private Label label5;
        private KlxPiaoTrackBar titleButtonWidthTrackBar;
        private Label label6;
        private ComboBox titleTextAlignComboBox;
        private KlxPiaoTrackBar titleTextMarginTrackBar;
        private PointBar titleTextOffsetPointBar;
        private GroupBox groupBox3;
        private KlxPiaoPanel titleBoxBackColorPanel;
        private Label label7;
        private Label label10;
        private KlxPiaoPanel titleBoxForeColorPanel;
        private Label label8;
        private KlxPiaoPanel titleButtonDisabledColorPanel;
        private Label label9;
        private KlxPiaoPanel backColorPanel;
        private CheckBox showIconCheckBox;
        private KlxPiaoLabel homeTitleLabel;
        private ImageBox homeTitleImage;
        private TabPage controlsTabPage;
        private KlxPiaoLabel welcomeLabel;
        private KlxPiaoTrackBar titleButtonHeightTrackBar;
        private PointBar iconDrawOffsetPointBar;
        private RoundedButton roundedButton1;
        private RoundedButton githubButton;
        private RoundedButton roundedButton2;
        private KlxPiaoListBox klxPiaoListBox1;
        private KlxPiaoPanel klxPiaoPanel1;
        private KlxPiaoTrackBar titleButtonCornerRadiusTrackBar;
    }
}