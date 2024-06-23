using KlxPiaoAPI;
using KlxPiaoControls;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace KlxPiaoDemo
{
    public partial class KlxPiaoLabelDemoForm : KlxPiaoForm
    {
        public KlxPiaoLabelDemoForm()
        {
            InitializeComponent();
        }

        private void KlxPiaoLabelDemoForm_Load(object sender, EventArgs e)
        {
            Text = $"KlxPiaoLabel Demo - {KlxPiaoControlsInfo.GetProductName()} {KlxPiaoControlsInfo.GetProductVersion()}";

            labelDemo.Location = Point.Empty;
            labelDemo.Size = panel1.Size;
            textBox1.Text = labelDemo.Text;

            klxPiaoLinkLabel1.Text = labelDemo.Font.FontFamily.Name;

            this.ForEachControl<KlxPiaoTrackBar>(trackBar => trackBar.ValueChanged += TrackBars_Changed, true);
            this.ForEachControl<KlxPiaoPanel>(panel => panel.Click += Panels_Click, true);
            this.ForEachControl<CheckBox>(checkBox => checkBox.CheckedChanged += CheckBoxs_Checked, true);
        }

        private void TrackBars_Changed(object? sender, KlxPiaoTrackBar.ValueChangedEventArgs e)
        {
            if (sender is KlxPiaoTrackBar trackBar)
            {
                switch (trackBar.Name)
                {
                    case "字号Track":
                        labelDemo.Font = new Font(labelDemo.Font.FontFamily, (int)e.Value, labelDemo.Font.Style);
                        break;
                    case "边框Track":
                        labelDemo.边框大小 = (int)e.Value;
                        break;
                    case "圆角Track":
                        labelDemo.圆角大小 = (int)e.Value / 100F;
                        break;
                }
            }
        }
        private void Panels_Click(object? sender, EventArgs e)
        {
            if (sender is KlxPiaoPanel panel)
            {
                ColorDialog selectColor = new()
                {
                    FullOpen = true,
                    Color = panel.BackColor
                };

                if (selectColor.ShowDialog() == DialogResult.OK)
                {
                    Color color = selectColor.Color;

                    switch (panel.Name)
                    {
                        case "背景Panel":
                            labelDemo.BackColor = color;
                            break;
                        case "前景Panel":
                            labelDemo.ForeColor = color;
                            break;
                        case "边框颜色Panel":
                            labelDemo.边框颜色 = color;
                            break;
                        case "边框外部Panel":
                            labelDemo.边框外部颜色 = color;
                            break;
                        case "投影颜色Panel":
                            labelDemo.投影颜色 = color;
                            break;
                    }

                    panel.BackColor = color;
                }
            }
        }
        private void CheckBoxs_Checked(object? sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                switch (checkBox.Name)
                {
                    case "启用投影Check":
                        labelDemo.启用投影 = checkBox.Checked;
                        break;
                    case "颜色减淡Check":
                        labelDemo.颜色减淡 = checkBox.Checked;
                        break;
                    case "投影连线Check":
                        labelDemo.投影连线 = checkBox.Checked;
                        break;
                    case "高质量Check":
                        if (checkBox.Checked)
                        {
                            labelDemo.文本呈现质量 = TextRenderingHint.AntiAliasGridFit;
                            labelDemo.抗锯齿 = SmoothingMode.HighQuality;
                            labelDemo.偏移方式 = PixelOffsetMode.HighQuality;
                        }
                        else
                        {
                            labelDemo.文本呈现质量 = TextRenderingHint.SystemDefault;
                            labelDemo.抗锯齿 = SmoothingMode.Default;
                            labelDemo.偏移方式 = PixelOffsetMode.Default;
                        }
                        break;
                }
            }
        }

        //呈现的文本
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            labelDemo.Text = textBox1.Text;
        }
        //投影长度
        private void PointBar1_值Changed(object sender, PropertyChangedEventArgs e)
        {
            labelDemo.偏移量 = pointBar1.值;
        }
        //字体
        private void KlxPiaoLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FontDialog selectFont = new()
            {
                Font = labelDemo.Font,
            };

            if (selectFont.ShowDialog() == DialogResult.OK)
            {
                labelDemo.Font = selectFont.Font;
                klxPiaoLinkLabel1.Text = labelDemo.Font.FontFamily.Name;
            }
        }
        //图像尺寸
        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                labelDemo.Width = int.Parse(textBox2.Text);
            }
            catch { }
        }
        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                labelDemo.Height = int.Parse(textBox3.Text);
            }
            catch { }
        }
        private void LabelDemo_SizeChanged(object sender, EventArgs e)
        {
            labelDemo.Location = new Point(
                (panel1.Width - labelDemo.Width) / 2,
                (panel1.Height - labelDemo.Height) / 2);
        }
        //复制到剪贴板
        private void KlxPiaoButton1_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(labelDemo.返回图像());
        }
        //导出到文件
        private void KlxPiaoButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "PNG|*.png|BMP|*.bmp|JPG|*.jpg",
                FileName = textBox1.Text,
                InitialDirectory = Environment.SpecialFolder.Desktop.ToString()
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string 扩展名 = Path.GetExtension(saveFileDialog.FileName).ToLower();

                ImageFormat imageFormat = 扩展名 switch
                {
                    ".png" => ImageFormat.Png,
                    ".bmp" => ImageFormat.Bmp,
                    ".jpg" => ImageFormat.Jpeg,
                    _ => throw new NotSupportedException("Unsupported file format")
                };

                labelDemo.返回图像().Save(saveFileDialog.FileName, imageFormat);
            }
        }
    }
}