using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Studyzy.IMEWLConverter
{
    public partial class SelfDefiningConverterForm : Form
    {
        private readonly List<string> fromWords = new List<string>();

        public SelfDefiningConverterForm()
        {
            InitializeComponent();
        }

        public List<string> FromWords
        {
            get { return fromWords; }
        }

        /// <summary>
        /// 用户自定义的匹配模式
        /// </summary>
        public ParsePattern SelectedParsePattern { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            if (SelectedParsePattern == null)
            {
                MessageBox.Show("请点击右上角按钮选择匹配规则");
                return;
            }
            rtbTo.Clear();
            string[] fromList = rtbFrom.Text.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in fromList)
            {
                string s = str.Trim();
                WordLibrary wl = SelectedParsePattern.BuildWordLibrary(s);
                rtbTo.AppendText(wl.ToDisplayString() + "\r\n");
            }
        }

        private void btnHelpBuild_Click(object sender, EventArgs e)
        {
            var builder = new HelpBuildParsePatternForm();
            if (builder.ShowDialog() == DialogResult.OK)
            {
                SelectedParsePattern = builder.SelectedParsePattern;
                txbParsePattern.Text = SelectedParsePattern.BuildWLStringSample();
            }
        }

        private void SelfDefiningConverterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}