using System.Text.RegularExpressions;

namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供 <see cref="RichTextBox"/> 用于快速操作的扩展方法。
    /// </summary>
    public static class RichTextBoxExtensions
    {
        /// <summary>
        /// 在 <see cref="RichTextBox"/> 中插入指定颜色和大小的文本。
        /// </summary>
        /// <param name="richTextBox">要插入文本的 <see cref="RichTextBox"/> 控件。</param>
        /// <param name="text">要插入的文本。</param>
        /// <param name="color">文本的颜色，若未指定，则使用当前 <see cref="RichTextBox"/> 的文本颜色。</param>
        /// <param name="fontSize">字体的大小，若未指定，则使用当前 <see cref="RichTextBox"/> 的字体大小。</param>
        public static void InsertText(this RichTextBox richTextBox, string text, Color? color = null, float? fontSize = null)
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionLength = 0;
            richTextBox.SelectionColor = color == null ? richTextBox.ForeColor : color.Value;
            richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, fontSize == null ? richTextBox.Font.Size : fontSize.Value);
            richTextBox.AppendText(text);
            richTextBox.SelectionColor = richTextBox.ForeColor;
            richTextBox.SelectionFont = richTextBox.Font;
        }

        /// <summary>
        /// 检查 <see cref="RichTextBox"/> 是否包含图像。
        /// </summary>
        /// <param name="richTextBox"><see cref="RichTextBox"/> 对象。</param>
        /// <returns>若包含图像，则返回 true, 否则返回 false。</returns>
        public static bool ContainsImage(this RichTextBox richTextBox)
        {
            string? rtfContent = richTextBox.Rtf;
            if (rtfContent == null) return false;
            string imagePattern = @"\\pict";
            Regex regex = new(imagePattern);
            MatchCollection matches = regex.Matches(rtfContent);

            return matches.Count > 0;
        }
    }
}