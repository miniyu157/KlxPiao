using KlxPiaoControls;

namespace KlxPiaoDemo
{
    public partial class BezierCurveCodeShow : KlxPiaoForm
    {
        public BezierCurveCodeShow(string pointfshow, string comptext, Color themeCcolor)
        {
            InitializeComponent();

            pointfshowTextBox.Text = pointfshow;
            componentModelText.Text = comptext;

            SetGlobalTheme(themeCcolor);
        }

        private void RoundedButton1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(pointfshowTextBox.Text);
        }

        private void RoundedButton2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(componentModelText.Text);
        }
    }
}