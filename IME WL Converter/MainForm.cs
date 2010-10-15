using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;

namespace Studyzy.IMEWLConverter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private bool exportDirectly = false;

        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //this.txbWLPath.Text = openFileDialog1.FileName;
                string files = "";
                foreach (string file in openFileDialog1.FileNames)
                {
                    files += file + " | ";
                }
                this.txbWLPath.Text = files.Remove(files.Length - 3);

                cbxFrom.Text =FileOperationHelper.AutoMatchSourceWLType(openFileDialog1.FileName);
            }

        }
        IWordLibraryImport import;
        IWordLibraryExport export;
        private WordLibraryList allWlList = new WordLibraryList();
        private void btnConvert_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            allWlList.Clear();
            ignoreWordLength = Convert.ToInt32(toolStripComboBoxIgnoreWordLength.Text);
            timer1.Enabled = true;
#if !DEBUG
            try
            {
#endif
                import = GetImportInterface(cbxFrom.Text);
                export = GetExportInterface(cbxTo.Text);
                string[] files = txbWLPath.Text.Split('|');
                foreach (string file in files)
                {
                    txt =FileOperationHelper. ReadFile(file.Trim());
                    //ImportWordLibrary();
                    //backgroundWorker1_RunWorkerCompleted(null,null);
                    backgroundWorker1.RunWorkerAsync();
                    //Thread thread = new Thread(new ThreadStart(ImportWordLibrary));
                    //thread.Start();
                }
               
#if !DEBUG
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
#endif
        }


        /// <summary>
        /// 词库过滤
        /// </summary>
        /// <param name="wlList"></param>
        /// <returns></returns>
        private WordLibraryList Filter(WordLibraryList wlList)
        {
            List<WordLibrary> temp = new List<WordLibrary>();
            //if (toolStripMenuItemIgnoreMutiPinyin.Checked)//多音字过滤
            //{
            //    Dictionary<string, WordLibrary> dic = new Dictionary<string, WordLibrary>();
            //    foreach (WordLibrary wl in wlList)
            //    {
            //        if (!dic.ContainsKey(wl.Word))
            //        {
            //            dic.Add(wl.Word, wl);
            //        }
            //    }
            //    temp.AddRange(dic.Values);
            //}
            //else
            //{
                temp = wlList;
            //}
            int minLength = 1;
            int maxLength = 9999;
            if (ignoreSingleWord)//过滤单个字
            {
                minLength = 2;
            }
            if (ignoreLongWord)
            {
                maxLength = ignoreWordLength;
            }
            if (minLength != 1 || maxLength != 9999)//设置了长度过滤
            {
                temp= temp.FindAll(delegate(WordLibrary wl)
                {
                    return wl.Word.Length >= minLength && wl.Word.Length <= maxLength;
                });
            }
            if (filterEnglish)//过滤英文单词
            {
                Regex r = new Regex("[a-z]", RegexOptions.IgnoreCase);
                temp.RemoveAll(delegate(WordLibrary wl) {
                    return r.IsMatch(wl.Word);
                });
            }
            WordLibraryList newList = new WordLibraryList();
             newList.AddRange(temp);
             return newList;

        }

        private string txt;

        private void ImportWordLibrary()
        {
            var wlList = import.Import(txt);
            
            wlList = Filter(wlList);
            allWlList.AddRange(wlList);
           
        }

        private void MainiForm_Load(object sender, EventArgs e)
        {
            this.cbxFrom.Items.Add(ConstantString.BAIDU_SHOUJI);
            this.cbxFrom.Items.Add(ConstantString.QQ_SHOUJI);
            this.cbxFrom.Items.Add(ConstantString.SOUGOU_PINYIN);
            this.cbxFrom.Items.Add(ConstantString.SOUGOU_WUBI);
            this.cbxFrom.Items.Add(ConstantString.QQ_PINYIN);
            this.cbxFrom.Items.Add(ConstantString.SINA_PINYIN);
            this.cbxFrom.Items.Add(ConstantString.GOOGLE_PINYIN);
            this.cbxFrom.Items.Add(ConstantString.ZIGUANG_PINYIN);
            this.cbxFrom.Items.Add(ConstantString.PINYIN_JIAJIA);
            this.cbxFrom.Items.Add(ConstantString.WORD_ONLY);
            this.cbxFrom.Items.Add(ConstantString.SOUGOU_XIBAO_SCEL);

            this.cbxTo.Items.Add(ConstantString.BAIDU_SHOUJI);
            this.cbxTo.Items.Add(ConstantString.QQ_SHOUJI);
            this.cbxTo.Items.Add(ConstantString.SOUGOU_PINYIN);
            this.cbxTo.Items.Add(ConstantString.SOUGOU_WUBI);
            this.cbxTo.Items.Add(ConstantString.QQ_PINYIN);
            this.cbxTo.Items.Add(ConstantString.SINA_PINYIN);
            this.cbxTo.Items.Add(ConstantString.GOOGLE_PINYIN);
            this.cbxTo.Items.Add(ConstantString.ZIGUANG_PINYIN);
            this.cbxTo.Items.Add(ConstantString.PINYIN_JIAJIA);
            //this.cbxTo.Items.Add("FIT");
            this.cbxTo.Items.Add(ConstantString.WORD_ONLY);
        }
        private IWordLibraryExport GetExportInterface(string str)
        {
             switch (str)
            {
                case ConstantString.BAIDU_SHOUJI: return new BaiduShouji();
                case ConstantString.QQ_SHOUJI: return new QQShouji();
                case ConstantString.SOUGOU_PINYIN: return new SougouPinyin();
                case ConstantString.SOUGOU_WUBI: return new SougouWubi();
                case ConstantString.QQ_PINYIN: return new QQPinyin();
                case ConstantString.GOOGLE_PINYIN: return new GooglePinyin();
                case ConstantString.WORD_ONLY: return new SougouPinyinWL();
                case ConstantString.ZIGUANG_PINYIN: return new ZiGuangPinyin();
                case ConstantString.PINYIN_JIAJIA: return new PinyinJiaJia();
                case ConstantString.SINA_PINYIN: return new SinaPinyin();
                default: throw new ArgumentException("导出词库的输入法错误");
            }
        }
        private IWordLibraryImport GetImportInterface(string str)
        {
            switch (str)
            {
                case ConstantString.BAIDU_SHOUJI: return new BaiduShouji();
                case ConstantString.QQ_SHOUJI: return new QQShouji();
                case ConstantString.SOUGOU_PINYIN: return new SougouPinyin();
                case ConstantString.SOUGOU_WUBI: return new SougouWubi();
                case ConstantString.QQ_PINYIN: return new QQPinyin();
                case ConstantString.GOOGLE_PINYIN: return new GooglePinyin();
                case ConstantString.ZIGUANG_PINYIN: return new ZiGuangPinyin();
                case ConstantString.PINYIN_JIAJIA: return new PinyinJiaJia();
                case ConstantString.WORD_ONLY: return new SougouPinyinWL();
                case ConstantString.SINA_PINYIN: return new SinaPinyin();
                case ConstantString.SOUGOU_XIBAO_SCEL: return new SougouPinyinScel();
                default: throw new ArgumentException("导入词库的输入法错误");
            }
        }



        private void btnExport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否将文本框中的所有词条保存到本地硬盘上？", "是否保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveFileDialog1.DefaultExt = ".txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (FileOperationHelper. WriteFile(saveFileDialog1.FileName, export.Encoding, richTextBox1.Text))
                    {
                        MessageBox.Show("保存成功，词库路径：" + saveFileDialog1.FileName);
                    }
                    else
                    {
                        MessageBox.Show("保存失败");
                    }
                }
            }

        }
        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutBox a = new AboutBox();
            a.Show();
        }
        private void ToolStripMenuItemHelp_Click(object sender, EventArgs e)
        {
            HelpForm help = new HelpForm();
            help.Show();
        }

        private void cbxFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxFrom.Text == ConstantString.WORD_ONLY)
            {
                this.openFileDialog1.Filter = "细胞词库|*.scel|文本文件|*.txt|所有文件|*.*";
            }
            else
            {
                this.openFileDialog1.Filter = "文本文件|*.txt|细胞词库|*.scel|所有文件|*.*";
            }
        }

        private void toolStripMenuItemEnableMutiConvert_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Multiselect = toolStripMenuItemEnableMutiConvert.Checked;
        }

        private void ToolStripMenuItemAccessWebSite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/imewlconverter/"); 
        }

        private void toolStripMenuItemExportDirectly_Click(object sender, EventArgs e)
        {
            exportDirectly = this.toolStripMenuItemExportDirectly.Checked;
        }

        private void toolStripMenuItemIgnoreSingleWord_Click(object sender, EventArgs e)
        {
            ignoreSingleWord = toolStripMenuItemIgnoreSingleWord.Checked;
        }
        private bool ignoreSingleWord = false;
        private bool filterEnglish = true;
        private bool ignoreLongWord = false;
        private int ignoreWordLength = 5;
        private void toolStripMenuItemFilterEnglish_Click(object sender, EventArgs e)
        {
            filterEnglish = toolStripMenuItemFilterEnglish.Checked;
        }

        private void toolStripMenuItemIgnoreLongWord_Click(object sender, EventArgs e)
        {
            ignoreLongWord = toolStripMenuItemIgnoreLongWord.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (import != null)
            {
                this.toolStripStatusLabel1.Text = "转换进度："+import.CurrentStatus + "/" + import.CountWord;
                this.toolStripProgressBar1.Maximum = import.CountWord;
                this.toolStripProgressBar1.Value = import.CurrentStatus;
            }           
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ImportWordLibrary();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer1.Enabled = false;
            string newWl = "";// export.Export(allWlList);
            this.toolStripStatusLabel1.Text = "转换完成";
            if (exportDirectly)
            {
                richTextBox1.Text = "为提高处理速度，高级设置中默认设置为直接导出，本文本框中不显示转换后的结果，若要查看、修改转换后的结果再导出请取消该设置。";
            }
            else
            {

                richTextBox1.Text = newWl;
                btnExport.Enabled = true;
            }

            if (MessageBox.Show("是否将导入的" + allWlList.Count + "条词库保存到本地硬盘上？", "是否保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveFileDialog1.DefaultExt = ".txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    if (FileOperationHelper.WriteFile(saveFileDialog1.FileName, export.Encoding, newWl))
                    {
                        MessageBox.Show("保存成功，词库路径：" + saveFileDialog1.FileName);
                    }
                    else
                    {
                        MessageBox.Show("保存失败");
                    }
                }
            }
        }
    }
}
