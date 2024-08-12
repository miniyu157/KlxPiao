using KlxPiaoAPI;
using KlxPiaoControls;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace KlxPiaoDemo
{
    public partial class DemoForm : KlxPiaoForm
    {
        private const string githubLink= "https://github.com/miniyu157/KlxPiao";

        public DemoForm()
        {
            InitializeComponent();

            welcomeLabel.BackgroundPaint += (sender, e) =>
            {
                Color color1 = Color.FromArgb(255, 196, 225);
                Color color2 = Color.FromArgb(182, 234, 254);
                Rectangle rect = welcomeLabel.GetClientRectangle();

                using LinearGradientBrush brush = new(rect, color1, color2, LinearGradientMode.ForwardDiagonal);
                e.Graphics.FillRectangle(brush, rect);
            };

            githubButton.Click += (sender, e) => Process.Start(new ProcessStartInfo() { FileName = githubLink, UseShellExecute = true });

            Text = $"{KlxPiaoControlsInfo.GetProductName()} & {KlxPiaoAPIInfo.GetProductName()} {KlxPiaoControlsInfo.GetProductVersion()} Demo";

            InitializeComboBox(themeComboBox,                                 Theme,                                 value => Theme = value);
            InitializeComboBox(dragModeComboBox,                              DragMode,                              value => DragMode = value);
            InitializeComboBox(shortcutResizeModeComboBox,                    ShortcutResizeMode,                    value => ShortcutResizeMode = value);
            InitializeComboBox(titleButtonAlignComboBox,                      TitleButtonAlign,                      value => TitleButtonAlign = value);
            InitializeComboBox(titleButtonsComboBox,                          TitleButtons,                          value => TitleButtons = value);
            InitializeComboBox(titleTextAlignComboBox,                        TitleTextAlign,                        value => TitleTextAlign = value);

            InitializeCheckBox(enableResizeAnimationCheckBox,                 EnableResizeAnimation,                 value => EnableResizeAnimation = value);
            InitializeCheckBox(resizableCheckBox,                             Resizable,                             value => Resizable = value);
            InitializeCheckBox(enableChangeInactiveTitleBoxForeColorCheckBox, EnableChangeInactiveTitleBoxForeColor, value => EnableChangeInactiveTitleBoxForeColor = value);
            InitializeCheckBox(enableCloseButtonCheckBox,                     EnableCloseButton,                     value => EnableCloseButton = value);
            InitializeCheckBox(enableResizeButtonCheckBox,                    EnableResizeButton,                    value => EnableResizeButton = value);
            InitializeCheckBox(enableMinimizeButtonCheckBox,                  EnableMinimizeButton,                  value => EnableMinimizeButton = value);
            InitializeCheckBox(showIconCheckBox,                              ShowIcon,                              value => ShowIcon = value);

            InitializeTrackBar(interactionColorScaleTrackBar,                 InteractionColorScale,                 value => InteractionColorScale = value);
            InitializeTrackBar(titleBoxHeightTrackBar,                        TitleBoxHeight,                        value => TitleBoxHeight = (int)value);
            InitializeTrackBar(titleButtonWidthTrackBar,                      TitleButtonWidth,                      value => TitleButtonWidth = (int)value);
            InitializeTrackBar(titleTextMarginTrackBar,                       TitleTextMargin,                       value => TitleTextMargin = (int)value);
            InitializeTrackBar(titleButtonIconSizeTrackBar,                   TitleButtonIconSize.Width,             value => TitleButtonIconSize = new SizeF(value, value));
            
            InitializeColorPanel(titleBoxBackColorPanel,                      TitleBoxBackColor,                     color =>
            {
                SetThemeColor(color);
                titleBoxForeColorPanel.BackColor = TitleBoxForeColor;
            });
            InitializeColorPanel(titleBoxForeColorPanel,                      TitleBoxForeColor,                     color => TitleBoxForeColor = color);
            InitializeColorPanel(titleButtonDisabledColorPanel,               TitleButtonDisabledColor,              color => TitleButtonDisabledColor = color);
            InitializeColorPanel(backColorPanel,                              BackColor,                             color => BackColor = color);

            InitializePointBar(titleTextOffsetPointBar,                       TitleTextOffset,                       value => TitleTextOffset = value);
        }

        #region Initialize Method
        private static void InitializeComboBox<TEnum>(ComboBox comboBox, TEnum selectedValue, Action<TEnum> onSelectionChanged) where TEnum : Enum
        {
            comboBox.Items.Clear();
            EnumUtility.ForEachEnum<TEnum>(item => comboBox.Items.Add(item));
            comboBox.SelectedItem = selectedValue;
            comboBox.SelectedIndexChanged += (sender, e) => onSelectionChanged((TEnum)(object)comboBox.SelectedIndex);
        }

        private static void InitializeCheckBox(CheckBox checkBox, bool initialValue, Action<bool> onCheckedChanged)
        {
            checkBox.Checked = initialValue;
            checkBox.CheckedChanged += (sender, e) => onCheckedChanged(checkBox.Checked);
        }

        private static void InitializeTrackBar(KlxPiaoTrackBar klxPiaoTrackBar, float initialValue, Action<float> onValueChanged)
        {
            klxPiaoTrackBar.Value = initialValue;
            klxPiaoTrackBar.ValueChanged += (sender, e) => onValueChanged(klxPiaoTrackBar.Value);
        }

        private static void InitializePointBar(PointBar pointBar, Point initialValue, Action<Point> onValueChanged)
        {
            pointBar.Value = initialValue;
            pointBar.ValueChanged += (sender, e) => onValueChanged(pointBar.Value);
        }

        private static void InitializeColorPanel(Panel colorPanel, Color initialColor, Action<Color> onColorChanged)
        {
            colorPanel.BackColor = initialColor;
            colorPanel.Click += (sender, e) =>
            {
                using var colorDialog = new ColorDialog();
                colorDialog.FullOpen = true;
                colorDialog.Color = colorPanel.BackColor;

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color newColor = colorDialog.Color;
                    onColorChanged(newColor);
                    colorPanel.BackColor = newColor;
                }
            };
        }
        #endregion
    }
}