using KlxPiaoAPI;

namespace KlxPiaoControls
{
    /// <summary>
    /// 自定义消息框类，用于显示带有自定义按钮的对话框。
    /// </summary>
    /// <remarks>
    /// 初始化一个新的 <see cref="KlxPiaoMessageBox"/> 实例。
    /// </remarks>
    /// <param name="baseForm">用于设置对话框样式和其他属性的基窗体。</param>
    public class KlxPiaoMessageBox(KlxPiaoForm baseForm)
    {
        #region basic properties
        /// <summary>
        /// 获取或设置用于设置对话框主题和其他属性的基窗体。
        /// </summary>
        public KlxPiaoForm BaseForm { get; set; } = baseForm;

        /// <summary>
        /// 获取或设置对话框窗体。
        /// </summary>
        public KlxPiaoForm DialogForm { get; set; } = new();

        /// <summary>
        /// 获取或设置对话框显示的内容文本。
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置对话框的标题文本。
        /// </summary>
        public string Title { get; set; } = string.Empty;
        #endregion

        #region other properties
        /// <summary>
        /// 获取或设置是否使用 <see cref="Control.Invoke"/> 在基窗体上显示对话框。
        /// </summary>
        public bool UseInvoke { get; set; } = false;

        /// <summary>
        /// 获取或设置是否根据基窗体自动初始化一基本些默认值（例如标题框颜色）。
        /// </summary>
        public bool InitializeDefaultValue { get; set; } = true;

        /// <summary>
        /// 获取或设置要根据基窗体同步到对话框窗体的属性（使用反射）。
        /// </summary>
        public string[] SyncedFormProperties { get; set; } = [];
        #endregion

        #region basic appearance
        /// <summary>
        /// 获取或设置按钮的大小。
        /// </summary>
        public Size ButtonSize { get; set; } = new(88, 33);

        /// <summary>
        /// 获取或设置按钮的文本内容。如果为 null，则使用默认值。
        /// </summary>
        public string[]? ButtonTexts { get; set; }

        /// <summary>
        /// 获取或设置对话框的按钮类型。
        /// </summary>
        public MessageBoxButtons Buttons { get; set; } = MessageBoxButtons.OK;

        /// <summary>
        /// 获取或设置对话框的启动位置。
        /// </summary>
        public FormStartPosition StartPosition { get; set; } = FormStartPosition.CenterParent;
        #endregion

        #region offset
        /// <summary>
        /// 获取或设置按钮底边距。
        /// </summary>
        public int ButtonBottomMargin { get; set; } = 20;

        /// <summary>
        /// 获取或设置按钮之间的距离。
        /// </summary>
        public int ButtonSpacing { get; set; } = 12;

        /// <summary>
        /// 获取或设置按钮和文本之间的距离。
        /// </summary>
        public int ButtonTextSpacing { get; set; } = 40;

        /// <summary>
        /// 获取或设置按钮或正文与对话框边缘的横向边距（取最大值）。
        /// </summary>
        public int ContentOrButtonHorizontalMargin { get; set; } = 30;

        /// <summary>
        /// 获取或设置正文的上边距。
        /// </summary>
        public int ContentTopMargin { get; set; } = 23;
        #endregion

        #region button properties
        /// <summary>
        /// 获取或设置按钮的交互样式。
        /// </summary>
        public RoundedButton.InteractionStyleClass? InteractionStyle { get; set; } = null;

        /// <summary>
        /// 获取或设置按钮的边框颜色。
        /// </summary>
        public Color? ButtonBorderColor { get; set; } = null;

        /// <summary>
        /// 获取或设置按钮的背景颜色，若未设置，则使用基窗体的背景颜色。
        /// </summary>
        public Color? ButtonBackColor { get; set; } = null;

        /// <summary>
        /// 获取或设置按钮的前景颜色，若未设置，则使用基窗体的前景颜色。
        /// </summary>
        public Color? ButtonForeColor { get; set; } = null;
        #endregion

        /// <summary>
        /// 显示对话框并返回用户选择的结果。
        /// </summary>
        /// <returns>对话框的结果。</returns>
        public DialogResult Show()
        {
            DialogResult result = DialogResult.None;

            DialogForm.Text = Title;
            DialogForm.StartPosition = StartPosition;

            //初始化固定值
            DialogForm.TitleButtons = KlxPiaoForm.TitleButtonStyle.CloseOnly;
            DialogForm.Resizable = false;
            DialogForm.ShowInTaskbar = false;

            //根据基窗体初始化基本默认值
            if (InitializeDefaultValue)
            {
                DialogForm.Theme = BaseForm.Theme;
                DialogForm.TitleTextAlign = BaseForm.TitleTextAlign;
                DialogForm.TitleBoxBackColor = BaseForm.TitleBoxBackColor;
                DialogForm.TitleBoxForeColor = BaseForm.TitleBoxForeColor;
                DialogForm.InteractionColorScale = BaseForm.InteractionColorScale;
            }

            //同步基窗体的属性
            foreach (var property in SyncedFormProperties)
            {
                DialogForm.SetOrGetPropertyValue(property, BaseForm.SetOrGetPropertyValue(property));
            }

            KlxPiaoLabel contentLabel = new()
            {
                Text = Content,
                AutoSize = false,
                TextAlign = ContentAlignment.TopCenter,
                DrawTextOffset = new Point(0, ContentTopMargin),
                BackColor = DialogForm.BackColor,
                ForeColor = DialogForm.ForeColor,
            };

            using Bitmap bitmap = new(1, 1);
            using Graphics graphics = Graphics.FromImage(bitmap);

            void CreateButton(Control control, string[] buttonText, DialogResult[] dialogResults)
            {
                int length = buttonText.Length;
                int buttonWidth = ButtonSize.Width;
                int buttonHeight = ButtonSize.Height;
                int buttonWidthRange = buttonWidth * length + ButtonSpacing * (length - 1);
                SizeF textSize = graphics.MeasureString(Content, contentLabel.Font);

                DialogForm.Width = Math.Max((int)textSize.Width, buttonWidthRange) + ContentOrButtonHorizontalMargin * 2;
                DialogForm.Height = DialogForm.TitleBoxHeight + (int)textSize.Height + buttonHeight + ButtonBottomMargin + ButtonTextSpacing;

                contentLabel.Location = DialogForm.GetClientLocation();
                contentLabel.Size = DialogForm.GetClientSize() + new Size(1, 1); //更正偏移

                for (int index = 0; index < length; index++)
                {
                    RoundedButton newButton = new()
                    {
                        Text = buttonText[index],
                        Size = ButtonSize,
                        BaseBackColor = DialogForm.BackColor
                    };

                    //设置按钮的属性（若可用）
                    if (InteractionStyle != null) newButton.InteractionStyle = InteractionStyle;
                    if (ButtonBorderColor != null) newButton.BorderColor = ButtonBorderColor.Value;
                    if (ButtonBackColor != null) newButton.BackColor = ButtonBackColor.Value;
                    if (ButtonForeColor != null) newButton.ForeColor = ButtonForeColor.Value;

                    Point CalcButtonLocation(int length, int index) => new(
                            (buttonWidth + ButtonSpacing) * index + (contentLabel.Width - buttonWidthRange) / 2,
                            contentLabel.Height - buttonHeight - ButtonBottomMargin);

                    newButton.Location = CalcButtonLocation(length, index);
                    DialogResult dialogResult = dialogResults[index];
                    newButton.Click += (sender, e) =>
                    {
                        result = dialogResult;
                        DialogForm.CloseForm();
                    };
                    contentLabel.Controls.Add(newButton);
                }
            }

            switch (Buttons)
            {
                case MessageBoxButtons.OK:
                    CreateButton(contentLabel, ButtonTexts ?? ["Ok"], [DialogResult.OK]);
                    break;

                case MessageBoxButtons.OKCancel:
                    CreateButton(contentLabel, ButtonTexts ?? ["Ok", "Cancel"], [DialogResult.OK, DialogResult.Cancel]);
                    break;

                case MessageBoxButtons.YesNo:
                    CreateButton(contentLabel, ButtonTexts ?? ["Yes", "No"], [DialogResult.Yes, DialogResult.No]);
                    break;

                case MessageBoxButtons.YesNoCancel:
                    CreateButton(contentLabel, ButtonTexts ?? ["Yes", "No", "Cancel"], [DialogResult.Yes, DialogResult.No, DialogResult.Cancel]);
                    break;

                case MessageBoxButtons.RetryCancel:
                    CreateButton(contentLabel, ButtonTexts ?? ["Retry", "Cancel"], [DialogResult.Retry, DialogResult.Cancel]);
                    break;

                case MessageBoxButtons.AbortRetryIgnore:
                    CreateButton(contentLabel, ButtonTexts ?? ["Abort", "Retry", "Ignore"], [DialogResult.Abort, DialogResult.Retry, DialogResult.Ignore]);
                    break;
            }

            DialogForm.Controls.Add(contentLabel);

            if (UseInvoke)
            {
                BaseForm.Invoke(DialogForm.ShowDialog);
            }
            else
            {
                DialogForm.ShowDialog();
            }

            return result;
        }
    }
}