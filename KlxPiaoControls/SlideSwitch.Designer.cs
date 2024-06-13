namespace KlxPiaoControls
{
    partial class SlideSwitch
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            ItemsShow = new KlxPiaoPanel();
            SelectShow = new KlxPiaoLabel();
            SuspendLayout();
            // 
            // ItemsShow
            // 
            ItemsShow.BackColor = Color.White;
            ItemsShow.Cursor = Cursors.Hand;
            ItemsShow.Location = new Point(85, 19);
            ItemsShow.Name = "ItemsShow";
            ItemsShow.Size = new Size(199, 40);
            ItemsShow.TabIndex = 0;
            ItemsShow.Paint += ItemsShow_Paint;
            ItemsShow.MouseClick += ItemsShow_MouseClick;
            // 
            // SelectShow
            // 
            SelectShow.BackColor = Color.Black;
            SelectShow.ForeColor = Color.White;
            SelectShow.Location = new Point(35, 62);
            SelectShow.Name = "SelectShow";
            SelectShow.Size = new Size(88, 17);
            SelectShow.TabIndex = 2;
            SelectShow.Text = "KlxPiaoLabel1";
            SelectShow.TextAlign = ContentAlignment.MiddleCenter;
            SelectShow.MouseDown += SelectShow_MouseDown;
            SelectShow.MouseMove += SelectShow_MouseMove;
            SelectShow.MouseUp += SelectShow_MouseUp;
            // 
            // SlideSwitch
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(SelectShow);
            Controls.Add(ItemsShow);
            Name = "SlideSwitch";
            Size = new Size(284, 116);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private KlxPiaoPanel ItemsShow;
        private KlxPiaoLabel SelectShow;
    }
}
