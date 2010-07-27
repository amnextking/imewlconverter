namespace Studyzy.IMEWLConverter
{
    partial class MainiForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainiForm));
            this.btnConvert = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.txbWLPath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnOpenFileDialog = new System.Windows.Forms.Button();
            this.cbxFrom = new System.Windows.Forms.ComboBox();
            this.cbxTo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemIgnoreMutiPinyin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemIgnoreSingleWord = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFilterEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEnableMutiConvert = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemIgnoreLongWord = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxIgnoreWordLength = new System.Windows.Forms.ToolStripComboBox();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExport = new System.Windows.Forms.Button();
            this.ToolStripMenuItemAccessWebSite = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(465, 27);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.Text = "转 换";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 87);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(528, 309);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // txbWLPath
            // 
            this.txbWLPath.Location = new System.Drawing.Point(12, 27);
            this.txbWLPath.Name = "txbWLPath";
            this.txbWLPath.Size = new System.Drawing.Size(396, 21);
            this.txbWLPath.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "文本文件|*.txt|细胞词库|*.scel|所有文件|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "文本文件|*.txt|所有文件|*.*";
            // 
            // btnOpenFileDialog
            // 
            this.btnOpenFileDialog.Location = new System.Drawing.Point(414, 26);
            this.btnOpenFileDialog.Name = "btnOpenFileDialog";
            this.btnOpenFileDialog.Size = new System.Drawing.Size(33, 23);
            this.btnOpenFileDialog.TabIndex = 3;
            this.btnOpenFileDialog.Text = "...";
            this.btnOpenFileDialog.UseVisualStyleBackColor = true;
            this.btnOpenFileDialog.Click += new System.EventHandler(this.btnOpenFileDialog_Click);
            // 
            // cbxFrom
            // 
            this.cbxFrom.FormattingEnabled = true;
            this.cbxFrom.Location = new System.Drawing.Point(12, 58);
            this.cbxFrom.Name = "cbxFrom";
            this.cbxFrom.Size = new System.Drawing.Size(121, 20);
            this.cbxFrom.TabIndex = 4;
            this.cbxFrom.SelectedIndexChanged += new System.EventHandler(this.cbxFrom_SelectedIndexChanged);
            // 
            // cbxTo
            // 
            this.cbxTo.FormattingEnabled = true;
            this.cbxTo.Location = new System.Drawing.Point(194, 58);
            this.cbxTo.Name = "cbxTo";
            this.cbxTo.Size = new System.Drawing.Size(121, 20);
            this.cbxTo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "-->>";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(549, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemIgnoreMutiPinyin,
            this.toolStripMenuItemIgnoreSingleWord,
            this.toolStripMenuItemFilterEnglish,
            this.toolStripMenuItemEnableMutiConvert,
            this.toolStripSeparator1,
            this.toolStripMenuItemIgnoreLongWord,
            this.toolStripComboBoxIgnoreWordLength});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(65, 20);
            this.toolStripMenuItem1.Text = "高级设置";
            // 
            // toolStripMenuItemIgnoreMutiPinyin
            // 
            this.toolStripMenuItemIgnoreMutiPinyin.Checked = true;
            this.toolStripMenuItemIgnoreMutiPinyin.CheckOnClick = true;
            this.toolStripMenuItemIgnoreMutiPinyin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemIgnoreMutiPinyin.Name = "toolStripMenuItemIgnoreMutiPinyin";
            this.toolStripMenuItemIgnoreMutiPinyin.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItemIgnoreMutiPinyin.Text = "忽略多音字";
            // 
            // toolStripMenuItemIgnoreSingleWord
            // 
            this.toolStripMenuItemIgnoreSingleWord.CheckOnClick = true;
            this.toolStripMenuItemIgnoreSingleWord.Name = "toolStripMenuItemIgnoreSingleWord";
            this.toolStripMenuItemIgnoreSingleWord.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItemIgnoreSingleWord.Text = "忽略一个字的词";
            // 
            // toolStripMenuItemFilterEnglish
            // 
            this.toolStripMenuItemFilterEnglish.Checked = true;
            this.toolStripMenuItemFilterEnglish.CheckOnClick = true;
            this.toolStripMenuItemFilterEnglish.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemFilterEnglish.Name = "toolStripMenuItemFilterEnglish";
            this.toolStripMenuItemFilterEnglish.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItemFilterEnglish.Text = "忽略包含英文的词";
            // 
            // toolStripMenuItemEnableMutiConvert
            // 
            this.toolStripMenuItemEnableMutiConvert.CheckOnClick = true;
            this.toolStripMenuItemEnableMutiConvert.Name = "toolStripMenuItemEnableMutiConvert";
            this.toolStripMenuItemEnableMutiConvert.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItemEnableMutiConvert.Text = "允许批量词库转换";
            this.toolStripMenuItemEnableMutiConvert.Click += new System.EventHandler(this.toolStripMenuItemEnableMutiConvert_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // toolStripMenuItemIgnoreLongWord
            // 
            this.toolStripMenuItemIgnoreLongWord.CheckOnClick = true;
            this.toolStripMenuItemIgnoreLongWord.Name = "toolStripMenuItemIgnoreLongWord";
            this.toolStripMenuItemIgnoreLongWord.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItemIgnoreLongWord.Text = "忽略过长的词";
            // 
            // toolStripComboBoxIgnoreWordLength
            // 
            this.toolStripComboBoxIgnoreWordLength.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.toolStripComboBoxIgnoreWordLength.Name = "toolStripComboBoxIgnoreWordLength";
            this.toolStripComboBoxIgnoreWordLength.Size = new System.Drawing.Size(121, 20);
            this.toolStripComboBoxIgnoreWordLength.Text = "5";
            this.toolStripComboBoxIgnoreWordLength.ToolTipText = "忽略词的长度";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemHelp,
            this.ToolStripMenuItemAbout,
            this.ToolStripMenuItemAccessWebSite});
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.关于ToolStripMenuItem.Text = "帮助";
            // 
            // ToolStripMenuItemHelp
            // 
            this.ToolStripMenuItemHelp.Name = "ToolStripMenuItemHelp";
            this.ToolStripMenuItemHelp.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemHelp.Text = "帮助";
            this.ToolStripMenuItemHelp.Click += new System.EventHandler(this.ToolStripMenuItemHelp_Click);
            // 
            // ToolStripMenuItemAbout
            // 
            this.ToolStripMenuItemAbout.Name = "ToolStripMenuItemAbout";
            this.ToolStripMenuItemAbout.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemAbout.Text = "关于";
            this.ToolStripMenuItemAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(465, 56);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // ToolStripMenuItemAccessWebSite
            // 
            this.ToolStripMenuItemAccessWebSite.Name = "ToolStripMenuItemAccessWebSite";
            this.ToolStripMenuItemAccessWebSite.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemAccessWebSite.Text = "查看最新版本";
            this.ToolStripMenuItemAccessWebSite.Click += new System.EventHandler(this.ToolStripMenuItemAccessWebSite_Click);
            // 
            // MainiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 408);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxTo);
            this.Controls.Add(this.cbxFrom);
            this.Controls.Add(this.btnOpenFileDialog);
            this.Controls.Add(this.txbWLPath);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainiForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "深蓝词库转换1.2";
            this.Load += new System.EventHandler(this.MainiForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox txbWLPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnOpenFileDialog;
        private System.Windows.Forms.ComboBox cbxFrom;
        private System.Windows.Forms.ComboBox cbxTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemIgnoreMutiPinyin;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemIgnoreSingleWord;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemIgnoreLongWord;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxIgnoreWordLength;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEnableMutiConvert;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilterEnglish;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAccessWebSite;
    }
}

