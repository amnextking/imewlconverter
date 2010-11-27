using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Studyzy.IMEWLConverter
{
    public partial class SelfDefiningConverterForm : Form
    {
        public SelfDefiningConverterForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        private List<string> fromWords = new List<string>();
        public List<string> FromWords
        {
            get
            {
                return fromWords;
            }
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            if (SelectedParsePattern == null)
            {
                MessageBox.Show("请点击右上角按钮选择匹配规则");
                return;
            }
            rtbTo.Clear();
            string[] fromList = rtbFrom.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in fromList)
            {
                var s = str.Trim();
                var wl= SelectedParsePattern.BuildWordLibrary(s);
                rtbTo.AppendText(wl.ToDisplayString() + "\r\n");
            }
           
        }
        public ParsePattern SelectedParsePattern { get; set; }
        private void btnHelpBuild_Click(object sender, EventArgs e)
        {
            HelpBuildParsePatternForm builder = new HelpBuildParsePatternForm();
            if (builder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectedParsePattern = builder.SelectedParsePattern;
                this.txbParsePattern.Text = SelectedParsePattern.BuildWLStringSample();
            }
        }

        private void SelfDefiningConverterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
