using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Studyzy.IMEWLConverter
{
    public partial class HelpBuildParsePatternForm : Form
    {
       
        public HelpBuildParsePatternForm()
        {
            InitializeComponent();
            InitParsePattern();
        }
        public ParsePattern SelectedParsePattern
        {
            get;
            set;
        }
        private void InitParsePattern()
        {
            SelectedParsePattern = new ParsePattern();
            SelectedParsePattern.ContainCipin = true;
            SelectedParsePattern.ContainPinyin = true;
            SelectedParsePattern.PinyinSplitString = ",";
            SelectedParsePattern.PinyinSplitType = BuildType.None;
            SelectedParsePattern.Sort = new List<int>() { 1, 2, 3 };
            SelectedParsePattern.SplitString = " ";
            

         
            ShowSample();
        }

        private void HelpBuildParsePatternForm_Load(object sender, EventArgs e)
        {

        }

        private void cbxIncludePinyin_CheckedChanged(object sender, EventArgs e)
        {
            SelectedParsePattern.ContainPinyin = cbxIncludePinyin.Checked;
            numOrderPinyin.Visible = cbxIncludePinyin.Checked;
            ShowSample();
        }

        private void cbxIncludeCipin_CheckedChanged(object sender, EventArgs e)
        {
            SelectedParsePattern.ContainCipin = cbxIncludeCipin.Checked;
            numOrderCipin.Visible = cbxIncludeCipin.Checked;
            ShowSample();
        }
        private void ShowSample()
        {
            this.txbSample.Text = SelectedParsePattern.BuildWLStringSample();
        }

        private void cbbxPinyinSplitString_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str="";
            if (cbbxPinyinSplitString.Text == "空格")
            {
                str = " ";
            }
            if (cbbxPinyinSplitString.Text == "Tab")
            {
                str = "\t";
            }
            else
            {
                str = cbbxPinyinSplitString.Text;
            }
            SelectedParsePattern.PinyinSplitString = str;
            ShowSample();
        }

        private void cbbxSplitString_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            if (cbbxSplitString.Text == "空格")
            {
                str = " ";
            }
            if (cbbxSplitString.Text == "Tab")
            {
                str = "\t";
            }
            else
            {
                str = cbbxSplitString.Text;
            }
            SelectedParsePattern.SplitString = str;
            ShowSample();
        }

        private void cbxPinyinSplitBefore_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxPinyinSplitBefore.Checked && cbxPinyinSplitBehind.Checked)
            {
                SelectedParsePattern.PinyinSplitType = BuildType.FullContain;
            }
            else if (cbxPinyinSplitBefore.Checked)
            {
                SelectedParsePattern.PinyinSplitType = BuildType.LeftContain;
            }
            else if (cbxPinyinSplitBehind.Checked)
            {
                SelectedParsePattern.PinyinSplitType = BuildType.RightContain;
            }
            else
            {
                SelectedParsePattern.PinyinSplitType = BuildType.None;
            }
            ShowSample();
        }

        private void numOrderPinyin_ValueChanged(object sender, EventArgs e)
        {
            int number = (int)(sender as NumericUpDown).Value;
            List<int> sort = new List<int>();
            sort.Add((int)numOrderPinyin.Value);
            sort.Add((int)numOrderHanzi.Value);
            sort.Add((int)numOrderCipin.Value);
            //待完善重复键值问题
            //if (sort.FindAll(i => i == number).Count > 1)//重复的排序值
            //{
            //    foreach (NumericUpDown n in groupBox1.Controls)
            //    {
            //        if (n.Value >= number && n != sender)
            //        {
            //            n.Value = n.Value + 1;
            //        }
            //    }
            //    numOrderPinyin_ValueChanged(sender, e);
            //    return;
            //}
            SelectedParsePattern.Sort = sort;
            ShowSample();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

    }
}
