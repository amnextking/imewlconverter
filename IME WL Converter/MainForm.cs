using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
            cbxFrom.Items.Add(ConstantString.BAIDU_SHOUJI);
            cbxFrom.Items.Add(ConstantString.QQ_SHOUJI);
            cbxFrom.Items.Add(ConstantString.SOUGOU_PINYIN);
            cbxFrom.Items.Add(ConstantString.SOUGOU_WUBI);
            cbxFrom.Items.Add(ConstantString.QQ_PINYIN);
            cbxFrom.Items.Add(ConstantString.SINA_PINYIN);
            cbxFrom.Items.Add(ConstantString.GOOGLE_PINYIN);
            cbxFrom.Items.Add(ConstantString.ZIGUANG_PINYIN);
            cbxFrom.Items.Add(ConstantString.PINYIN_JIAJIA);
            cbxFrom.Items.Add(ConstantString.WORD_ONLY);
            cbxFrom.Items.Add(ConstantString.SOUGOU_XIBAO_SCEL);
            cbxFrom.Items.Add(ConstantString.SELF_DEFINING);
            cbxFrom.Items.Add(ConstantString.ZHENGMA);

            cbxTo.Items.Add(ConstantString.BAIDU_SHOUJI);
            cbxTo.Items.Add(ConstantString.QQ_SHOUJI);
            cbxTo.Items.Add(ConstantString.SOUGOU_PINYIN);
            cbxTo.Items.Add(ConstantString.SOUGOU_WUBI);
            cbxTo.Items.Add(ConstantString.QQ_PINYIN);
            cbxTo.Items.Add(ConstantString.SINA_PINYIN);
            cbxTo.Items.Add(ConstantString.GOOGLE_PINYIN);
            cbxTo.Items.Add(ConstantString.ZIGUANG_PINYIN);
            cbxTo.Items.Add(ConstantString.PINYIN_JIAJIA);

            cbxTo.Items.Add(ConstantString.WORD_ONLY);
        }

        private IWordLibraryExport GetExportInterface(string str)
        {
            switch (str)
            {
                case ConstantString.BAIDU_SHOUJI:
                    return new BaiduShouji();
                case ConstantString.QQ_SHOUJI:
                    return new QQShouji();
                case ConstantString.SOUGOU_PINYIN:
                    return new SougouPinyin();
                case ConstantString.SOUGOU_WUBI:
                    return new SougouWubi();
                case ConstantString.QQ_PINYIN:
                    return new QQPinyin();
                case ConstantString.GOOGLE_PINYIN:
                    return new GooglePinyin();
                case ConstantString.WORD_ONLY:
                    return new NoPinyinWordOnly();
                case ConstantString.ZIGUANG_PINYIN:
                    return new ZiGuangPinyin();
                case ConstantString.PINYIN_JIAJIA:
                    return new PinyinJiaJia();
                case ConstantString.SINA_PINYIN:
                    return new SinaPinyin();
                default:
                    throw new ArgumentException("导出词库的输入法错误");
            }
        }

        private IWordLibraryImport GetImportInterface(string str)
        {
            switch (str)
            {
                case ConstantString.BAIDU_SHOUJI:
                    return new BaiduShouji();
                case ConstantString.QQ_SHOUJI:
                    return new QQShouji();
                case ConstantString.SOUGOU_PINYIN:
                    return new SougouPinyin();
                case ConstantString.SOUGOU_WUBI:
                    return new SougouWubi();
                case ConstantString.QQ_PINYIN:
                    return new QQPinyin();
                case ConstantString.GOOGLE_PINYIN:
                    return new GooglePinyin();
                case ConstantString.ZIGUANG_PINYIN:
                    return new ZiGuangPinyin();
                case ConstantString.PINYIN_JIAJIA:
                    return new PinyinJiaJia();
                case ConstantString.WORD_ONLY:
                    return new NoPinyinWordOnly();
                case ConstantString.SINA_PINYIN:
                    return new SinaPinyin();
                case ConstantString.SOUGOU_XIBAO_SCEL:
                    return new SougouPinyinScel();
                case ConstantString.ZHENGMA:
                    return new Zhengma();
                case ConstantString.SELF_DEFINING:
                    return new SelfDefining();
                default:
                    throw new ArgumentException("导入词库的输入法错误");
            }
        }

        #endregion

        private readonly WordLibraryList allWlList = new WordLibraryList();
        private readonly Regex englishRegex = new Regex("[a-z]", RegexOptions.IgnoreCase);
        private IWordLibraryExport export;
        private bool exportDirectly;
        private string exportPath = "";
        private string fileContent;
        private bool filterEnglish = true;
        private bool ignoreLongWord;
        private bool ignoreSingleWord;
        private int ignoreWordLength = 5;
        private IWordLibraryImport import;
        private int maxLength = 9999;
        private int minLength = 1;
        private bool streamExport;
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
                txbWLPath.Text = files.Remove(files.Length - 3);
                if (cbxFrom.Text != ConstantString.SELF_DEFINING)
                {
                    cbxFrom.Text = FileOperationHelper.AutoMatchSourceWLType(openFileDialog1.FileName);
                }
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            allWlList.Clear();
            ignoreWordLength = Convert.ToInt32(toolStripComboBoxIgnoreWordLength.Text);

            if (ignoreSingleWord) //过滤单个字
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
                ((SelfDefining) import).UserDefiningPattern = userSetPattern;
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

        /// <summary>
        /// 词库过滤
        /// </summary>
        /// <param name="wlList"></param>
        /// <returns></returns>
        private WordLibraryList Filter(WordLibraryList wlList)
        {
            var newList = new WordLibraryList();
            newList.AddRange(wlList.FindAll(delegate(WordLibrary wl) { return WordFilterRetain(wl); }));
            return newList;
        }

        /// <summary>
        /// 判断经过过滤规则后是否保留
        /// </summary>
        /// <param name="wl"></param>
        /// <returns></returns>
        private bool WordFilterRetain(WordLibrary wl)
        {
            if (minLength != 1 || maxLength != 9999) //设置了长度过滤
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
            if (MessageBox.Show("是否将文本框中的所有词条保存到本地硬盘上？", "是否保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                saveFileDialog1.DefaultExt = ".txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (FileOperationHelper.WriteFile(saveFileDialog1.FileName, export.Encoding, richTextBox1.Text))
                    {
                        ShowStatusMessage("保存成功，词库路径：" + saveFileDialog1.FileName, true);
                    }
                    else
                    {
                        ShowStatusMessage("保存失败", false);
                    }
                }
            }
        }


        private void cbxFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxFrom.Text == ConstantString.SELF_DEFINING) //弹出自定义窗口
            {
                var selfDefining = new SelfDefiningConverterForm();
                DialogResult show = selfDefining.ShowDialog();
                if (show != DialogResult.OK)
                {
                    cbxFrom.SelectedText = "";
                    return;
                }
                else //选了自定义
                {
                    userSetPattern = selfDefining.SelectedParsePattern;
                }
            }

            if (cbxFrom.Text == ConstantString.SOUGOU_XIBAO_SCEL)
            {
                openFileDialog1.Filter = "细胞词库|*.scel|文本文件|*.txt|所有文件|*.*";
            }
            else
            {
                openFileDialog1.Filter = "文本文件|*.txt|细胞词库|*.scel|所有文件|*.*";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (import != null)
            {
                ShowStatusMessage("转换进度：" + import.CurrentStatus + "/" + import.CountWord, false);
                toolStripProgressBar1.Maximum = import.CountWord;
                toolStripProgressBar1.Value = import.CurrentStatus;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] files = txbWLPath.Text.Split('|');
            foreach (string file in files)
            {
                string txt = FileOperationHelper.ReadFile(file.Trim());
                if (streamExport) //流转换
                {
                    StreamWriter stream = FileOperationHelper.GetWriteFileStream(exportPath, export.Encoding);
                    var wlStream = new WordLibraryStream(import, export, txt, stream);
                    wlStream.ConvertWordLibrary(WordFilterRetain);
                    stream.Close();
                }
                else
                {
                    WordLibraryList wlList = import.Import(txt);
                    wlList = Filter(wlList);
                    allWlList.AddRange(wlList);
                    fileContent = export.Export(allWlList);
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer1.Enabled = false;

            ShowStatusMessage("转换完成", false);
            if (streamExport)
            {
                ShowStatusMessage("转换完成,词库保存到文件：" + exportPath, true);
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

            if (
                MessageBox.Show("是否将导入的" + allWlList.Count + "条词库保存到本地硬盘上？", "是否保存", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveFileDialog1.DefaultExt = ".txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (FileOperationHelper.WriteFile(saveFileDialog1.FileName, export.Encoding, fileContent))
                    {
                        ShowStatusMessage("保存成功，词库路径：" + saveFileDialog1.FileName, true);
                    }
                    else
                    {
                        ShowStatusMessage("保存失败", true);
                    }
                }
            }
        }

        /// <summary>
        /// 在状态上显示消息
        /// </summary>
        /// <param name="statusMessage">消息内容</param>
        /// <param name="showMessageBox">是否弹出窗口显示消息</param>
        private void ShowStatusMessage(string statusMessage, bool showMessageBox)
        {
            toolStripStatusLabel1.Text = statusMessage;
            if (showMessageBox)
            {
                MessageBox.Show(statusMessage);
            }
        }

        #region 菜单操作

        private void toolStripMenuItemEnableMutiConvert_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = toolStripMenuItemEnableMutiConvert.Checked;
        }

        private void ToolStripMenuItemAccessWebSite_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/p/imewlconverter/");
        }

        private void toolStripMenuItemExportDirectly_Click(object sender, EventArgs e)
        {
            exportDirectly = toolStripMenuItemExportDirectly.Checked;
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
            var a = new AboutBox();
            a.Show();
        }

        private void ToolStripMenuItemHelp_Click(object sender, EventArgs e)
        {
            var help = new HelpForm();
            help.Show();
        }

        private void toolStripMenuItemStreamExport_Click(object sender, EventArgs e)
        {
            streamExport = toolStripMenuItemStreamExport.Checked;
        }

        private void ToolStripMenuItemCreatePinyinWL_Click(object sender, EventArgs e)
        {
            var f = new CreatePinyinWLForm();
            f.Show();
        }

        #endregion

      
    }
}