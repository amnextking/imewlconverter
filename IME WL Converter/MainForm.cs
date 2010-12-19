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
        #region Init
        public MainForm()
        {
            InitializeComponent();
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
            this.cbxFrom.Items.Add(ConstantString.SELF_DEFINING);
            this.cbxFrom.Items.Add(ConstantString.ZHENGMA);

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
                case ConstantString.ZHENGMA: return new Zhengma();
                case ConstantString.SELF_DEFINING: return new SelfDefining();
                default: throw new ArgumentException("导入词库的输入法错误");
            }
        }
        #endregion

        private bool exportDirectly = false;
        private bool ignoreSingleWord = false;
        private bool filterEnglish = true;
        private bool ignoreLongWord = false;
        private int ignoreWordLength = 5;
        private bool streamExport = false;
        private string exportPath = "";
        private string fileContent;
        private ParsePattern userSetPattern;
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
                if (cbxFrom.Text != ConstantString.SELF_DEFINING)
                {
                    cbxFrom.Text = FileOperationHelper.AutoMatchSourceWLType(openFileDialog1.FileName);
                }
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
           
            if (ignoreSingleWord)//过滤单个字
            {
                minLength = 2;
            }
            if (ignoreLongWord)
            {
                maxLength = ignoreWordLength;
            }
#if !DEBUG
            try
            {
#endif
            import = GetImportInterface(cbxFrom.Text);
            export = GetExportInterface(cbxTo.Text);
            if (import is SelfDefining)
            {
                ((SelfDefining)import).UserDefiningPattern = userSetPattern;
            }
            if (streamExport)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    exportPath = saveFileDialog1.FileName;
                else
                {
                    ShowStatusMessage("请选择词库保存的路径，否则将无法进行词库导出", true);
                    return;
                }
            }
            timer1.Enabled = true;
            backgroundWorker1.RunWorkerAsync();



#if !DEBUG
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
#endif
        }

        int minLength = 1;
        int maxLength = 9999;
        /// <summary>
        /// 词库过滤
        /// </summary>
        /// <param name="wlList"></param>
        /// <returns></returns>
        private WordLibraryList Filter(WordLibraryList wlList)
        {

            WordLibraryList newList = new WordLibraryList();
            newList.AddRange(wlList.FindAll(delegate(WordLibrary wl) { return WordFilterRetain(wl); }));
            return newList;

        }
        Regex englishRegex = new Regex("[a-z]", RegexOptions.IgnoreCase);
        /// <summary>
        /// 判断经过过滤规则后是否保留
        /// </summary>
        /// <param name="wl"></param>
        /// <returns></returns>
        private bool WordFilterRetain(WordLibrary wl)
        {
            if (minLength != 1 || maxLength != 9999)//设置了长度过滤
            {
                if (wl.Word.Length > maxLength || wl.Word.Length < minLength)
                {
                    return false;
                }
            }
            
            if (filterEnglish && englishRegex.IsMatch(wl.Word)) //过滤英文单词
            {
                return false;
            }
            return true;
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
                       ShowStatusMessage("保存成功，词库路径：" + saveFileDialog1.FileName,true);
                    }
                    else
                    {
                        ShowStatusMessage("保存失败",false);
                    }
                }
            }

        }
       

        private void cbxFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxFrom.Text == ConstantString.SELF_DEFINING)//弹出自定义窗口
            {
                SelfDefiningConverterForm selfDefining = new SelfDefiningConverterForm();
                var show=selfDefining.ShowDialog();
                if (show != System.Windows.Forms.DialogResult.OK)
                {
                    cbxFrom.SelectedText = "";
                    return;
                }
                else//选了自定义
                {
                    userSetPattern = selfDefining.SelectedParsePattern;
                }

            }

            if (this.cbxFrom.Text == ConstantString.SOUGOU_XIBAO_SCEL)
            {
                this.openFileDialog1.Filter = "细胞词库|*.scel|文本文件|*.txt|所有文件|*.*";
            }
            else
            {
                this.openFileDialog1.Filter = "文本文件|*.txt|细胞词库|*.scel|所有文件|*.*";
            }
        }

        #region 菜单操作
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
       
        private void toolStripMenuItemFilterEnglish_Click(object sender, EventArgs e)
        {
            filterEnglish = toolStripMenuItemFilterEnglish.Checked;
        }

        private void toolStripMenuItemIgnoreLongWord_Click(object sender, EventArgs e)
        {
            ignoreLongWord = toolStripMenuItemIgnoreLongWord.Checked;
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

        private void toolStripMenuItemStreamExport_Click(object sender, EventArgs e)
        {
            streamExport = toolStripMenuItemStreamExport.Checked;
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (import != null)
            {
                ShowStatusMessage( "转换进度："+import.CurrentStatus + "/" + import.CountWord,false);
                this.toolStripProgressBar1.Maximum = import.CountWord;
                this.toolStripProgressBar1.Value = import.CurrentStatus;
            }           
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] files = txbWLPath.Text.Split('|');
            foreach (string file in files)
            {
                var txt = FileOperationHelper.ReadFile(file.Trim());
                if (streamExport)//流转换
                {
                    var stream= FileOperationHelper.GetWriteFileStream(exportPath, export.Encoding);
                    WordLibraryStream wlStream = new WordLibraryStream(import, export, txt,stream);
                    wlStream.ConvertWordLibrary(WordFilterRetain);
                    stream.Close();
                }
                else
                {
                    var wlList = import.Import(txt);
                    wlList = Filter(wlList);
                    allWlList.AddRange(wlList);
                    fileContent = export.Export(allWlList);
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer1.Enabled = false;
          
           ShowStatusMessage( "转换完成",false);
            if(streamExport)
            {
                ShowStatusMessage("转换完成,词库保存到文件："+exportPath, true);
                return;
            }
            if (exportDirectly)
            {
                richTextBox1.Text = "为提高处理速度，高级设置中默认设置为直接导出，本文本框中不显示转换后的结果，若要查看、修改转换后的结果再导出请取消该设置。";
            }
            else
            {

                richTextBox1.Text = fileContent;
                btnExport.Enabled = true;
            }

            if (MessageBox.Show("是否将导入的" + allWlList.Count + "条词库保存到本地硬盘上？", "是否保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveFileDialog1.DefaultExt = ".txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    if (FileOperationHelper.WriteFile(saveFileDialog1.FileName, export.Encoding, fileContent))
                    {
                        ShowStatusMessage("保存成功，词库路径：" + saveFileDialog1.FileName,true);
                    }
                    else
                    {
                        ShowStatusMessage("保存失败",true);
                    }
                }
            }
        }
        /// <summary>
        /// 在状态上显示消息
        /// </summary>
        /// <param name="statusMessage">消息内容</param>
        /// <param name="showMessageBox">是否弹出窗口显示消息</param>
        private void ShowStatusMessage(string statusMessage,bool showMessageBox)
        {
            this.toolStripStatusLabel1.Text = statusMessage;
            if(showMessageBox)
            {
                MessageBox.Show(statusMessage);
            }
        }


    }
}
