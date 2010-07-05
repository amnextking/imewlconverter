﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Studyzy.IMEWLConverter
{
    public partial class MainiForm : Form
    {
        public MainiForm()
        {
            InitializeComponent();
        }

        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txbWLPath.Text = openFileDialog1.FileName;
                if (Path.GetExtension(txbWLPath.Text) == ".scel")
                {
                    cbxFrom.Text = "搜狗细胞词库scel";
                }
            }

        }
        IWordLibraryImport import;
        IWordLibraryExport export;
        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                string wlTxt = ReadFile(txbWLPath.Text);
                import = GetImportInterface(cbxFrom.Text);

                import.OnlySinglePinyin = toolStripMenuItemIgnoreMutiPinyin.Checked;

                var wlList = import.Import(wlTxt);
                export = GetExportInterface(cbxTo.Text);
                wlList = Filter(wlList);
                richTextBox1.Clear();
                richTextBox1.AppendText(export.Export(wlList));
                btnExport.Enabled = true;
                if (MessageBox.Show("是否将导入的" + wlList.Count + "条词库保存到本地硬盘上？", "是否保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    saveFileDialog1.DefaultExt = ".txt";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (WriteFile(saveFileDialog1.FileName, export.Encoding, richTextBox1.Text))
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            if (toolStripMenuItemIgnoreSingleWord.Checked)//过滤单个字
            {
                minLength = 2;
            }
            if (toolStripMenuItemIgnoreLongWord.Checked)
            {
                maxLength = Convert.ToInt32(toolStripComboBoxIgnoreWordLength.Text);
            }
            if (minLength != 1 || maxLength != 9999)//设置了长度过滤
            {
                temp= temp.FindAll(delegate(WordLibrary wl)
                {
                    return wl.Word.Length >= minLength && wl.Word.Length <= maxLength;
                });
            }
            WordLibraryList newList = new WordLibraryList();
             newList.AddRange(temp);
             return newList;

        }
        private string ReadFile(string path)
        {
            string ext=Path.GetExtension(path);
            if (ext == ".scel" && cbxFrom.Text == "搜狗细胞词库scel")//搜狗细胞词库
            {
                return SougouPinyinScel.ReadScel(path);
            }
            else//文本
            {
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        private bool WriteFile(string path,Encoding coding, string content)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path,false, coding))
                {
                    sw.Write(content);
                    sw.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void MainiForm_Load(object sender, EventArgs e)
        {
            this.cbxFrom.Items.Add("百度手机");
            this.cbxFrom.Items.Add("QQ手机");
            this.cbxFrom.Items.Add("搜狗拼音");
            this.cbxFrom.Items.Add("搜狗五笔");
            this.cbxFrom.Items.Add("QQ拼音");
            this.cbxFrom.Items.Add("谷歌拼音");
            this.cbxFrom.Items.Add("紫光拼音");
            this.cbxFrom.Items.Add("搜狗细胞词库Txt");
            this.cbxFrom.Items.Add("搜狗细胞词库scel");

            this.cbxTo.Items.Add("百度手机");
            this.cbxTo.Items.Add("QQ手机");
            this.cbxTo.Items.Add("搜狗拼音");
            this.cbxTo.Items.Add("搜狗五笔");
            this.cbxTo.Items.Add("QQ拼音");
            this.cbxTo.Items.Add("谷歌拼音");
            this.cbxTo.Items.Add("紫光拼音");
            this.cbxTo.Items.Add("搜狗细胞词库Txt");
        }
        private IWordLibraryExport GetExportInterface(string str)
        {
             switch (str)
            {
                case "百度手机": return new BaiduShouji();
                case "QQ手机": return new QQShouji();
                case "搜狗拼音": return new SougouPinyin();
                case "搜狗五笔": return new SougouWubi();
                case "QQ拼音": return new QQPinyin();
                case "谷歌拼音": return new GooglePinyin();
                case "搜狗细胞词库Txt": return new SougouPinyinWL();
                case "紫光拼音": return new ZiGuangPinyin();
                default: throw new ArgumentException("导出词库的输入法错误");
            }
        }
        private IWordLibraryImport GetImportInterface(string str)
        {
            switch (str)
            {
                case "百度手机": return new BaiduShouji();
                case "QQ手机": return new QQShouji();
                case "搜狗拼音": return new SougouPinyin();
                case "搜狗五笔": return new SougouWubi();
                case "QQ拼音": return new QQPinyin();
                case "谷歌拼音": return new GooglePinyin();
                case "紫光拼音": return new ZiGuangPinyin();
                case "搜狗细胞词库Txt": return new SougouPinyinWL();
                case "搜狗细胞词库scel": return new SougouPinyinScel();
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
                    if (WriteFile(saveFileDialog1.FileName, export.Encoding, richTextBox1.Text))
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
            if (this.cbxFrom.Text == "搜狗细胞词库scel")
            {
                this.openFileDialog1.Filter = "细胞词库|*.scel|文本文件|*.txt|所有文件|*.*";
            }
            else
            {
                this.openFileDialog1.Filter = "文本文件|*.txt|细胞词库|*.scel|所有文件|*.*";
            }
        }

    }
}