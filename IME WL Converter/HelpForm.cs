﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Studyzy.IMEWLConverter
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            string helpString = "1.1版支持搜狗的细胞词库（scel格式）的转换，您可以到搜狗网站下载细胞词库导入到您其他输入法或者手机输入法中！\r\nQQ的分类词库格式还没有研究出来怎么解析，有任何问题和建议请联系我：studyzy@163.com";
            this.richTextBox1.Text = helpString;
        }
    }
}