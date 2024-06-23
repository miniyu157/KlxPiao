using KlxPiaoAPI;
using System.ComponentModel;

namespace KlxPiaoControls
{
    public partial class KlxPiaoTextBox : UserControl
    {
        private KlxPiaoPanel basePanel = new();
        private TextBox baseTextBox = new();

        public KlxPiaoTextBox()
        {
            InitializeComponent();
            //
            // basePanel
            //
            basePanel.启用投影 = false;
            basePanel.边框大小 = 1;
            basePanel.边框颜色 = Color.Gainsboro;
            basePanel.边框外部颜色 = Color.White;
            basePanel.圆角大小 = new CornerRadius(35);
            basePanel.Location = new Point(0, 0);
            basePanel.Size = Size;
            basePanel.BackColor = Color.White;
            //
            // baseTextBox
            //
            baseTextBox.BorderStyle = BorderStyle.None;
            baseTextBox.Multiline = true;
            baseTextBox.Text = Name;
            baseTextBox.Location = new Point(0, 0);
            baseTextBox.Size = Size;
            baseTextBox.BackColor = Color.White; 
            //
            // KlxPiaoTextBox
            //
            Controls.Add(basePanel);
            Controls.Add(baseTextBox);
            baseTextBox.BringToFront();
        }


        /// <summary>
        /// 获取或设置边框的外观。这是通过 <see cref="KlxPiaoPanel"/> 实现的。
        /// </summary>
        [Category("KlxPiaoTextBox控件")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("边框外观")]
        public KlxPiaoPanel Border => basePanel;

        /// <summary>
        /// 获取或设置文本框的外观。这是通过 <see cref="TextBox"/> 实现的。
        /// </summary>
        [Category("KlxPiaoTextBox控件")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("文本框外观")]
        public TextBox TextBox => baseTextBox;

        private void refreshControlsSize()
        {
            basePanel.Size = Size;

            Rectangle thisRect = new(0, 0, Width, Height);
            Rectangle baseTextBoxRect = thisRect.ScaleRectangle(-basePanel.边框大小 * 2).GetInnerFitRectangle(basePanel.圆角大小); //边框内部的内接矩形

            baseTextBox.Size = baseTextBoxRect.Size;
            baseTextBox.Location = baseTextBoxRect.Location;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            refreshControlsSize();

            base.OnSizeChanged(e);
        }
    }
}