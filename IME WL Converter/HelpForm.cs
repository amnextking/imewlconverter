using System;
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
            Close();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            string helpString = "1.1版支持搜狗的细胞词库（scel格式）的转换，您可以到搜狗网站下载细胞词库导入到您其他输入法或者手机输入法中！\r\nQQ的分类词库格式还没有研究出来怎么解析。\r\n";
            helpString += "1.2版支持了紫光拼音输入法和拼音加加输入法的词库导入导出功能。增加了批量导入的功能。修复了有些scel格式词库导入时报错\r\n";
            helpString += "1.3版改进汉字自动注音功能，可以对纯汉字的词库进行注音和转换；并可设置不显示转换结果而直接导出结果以提高超大词库的转换效率\r\n";
            helpString += "1.4版增加了对触宝输入法的支持，增加了拖拽功能。";
            helpString += "1.5版增加了百度分类词库bdict格式的转换，增加了命令行调用功能。";
            helpString += "有任何问题和建议请联系我：studyzy@163.com\r\n";
            helpString += "\r\nPS:QQ分类词库（QPYD格式）的词库我不知道怎么解析，谁有办法还请帮帮忙，指点一下！";
            richTextBox1.Text = helpString;
        }
    }
}