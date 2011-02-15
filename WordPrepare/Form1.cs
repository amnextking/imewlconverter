using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WordPrepare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitSinglePinyin();
        }
        private Dictionary<char, string> dic;
       private void InitSinglePinyin()
       {
           dic = new Dictionary<char, string>();
           string singlePinYin = ReadFile("SinglePinYin.txt", Encoding.Default);
           string[] pyList = singlePinYin.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

           for (int i = 0; i < pyList.Length; i++)
           {
               string[] hzpy = pyList[i].Split(',');
               char hz = Convert.ToChar(hzpy[0]);
               string py = hzpy[1];
               dic.Add(hz, py);
           }
       }
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
            }
        }
        private void LoadData()
        {
        string str = ReadFile(this.textBox1.Text, Encoding.Default);
            string[] lines = str.Split(new string[] {"\r\n"},StringSplitOptions.RemoveEmptyEntries);
          
            foreach (string line in lines)
            {
                string[] hzpy = line.Split(' ');
                string py = hzpy[0];
                string hz = hzpy[1];
                if (NeedSave(hz, py))
                {
               
                //多音字做如下处理
                this.richTextBox1.AppendText(py+" "+hz+ "\r\n");
                }
            }

        }
        private string ReadFile(string fileName,Encoding e)
        {
            using (StreamReader sr = new StreamReader(fileName,e))
            {
                string str = sr.ReadToEnd();
                return str;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void Test2()
        {
        string str = ReadFile("dict.txt", Encoding.UTF8);
            string[] lines = str.Split(new string[] {"\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                //多音字做如下处理
                string[] wl = line.Split(' ');
                string word = wl[0];
                string py = wl[2];
                if (NeedSave(word, py))
                {
                    //多音字做如下处理
                    this.richTextBox1.AppendText("'"+py+" "+ word + "\r\n");
                }

            }
        }

        /// <summary>
        /// 这个单词的拼音是否需要保存
        /// </summary>
        /// <param name="word"></param>
        /// <param name="py"></param>
        /// <returns></returns>
        private bool NeedSave(string word,List<string> py)
        {
            try
            {
                for (int i = 0; i < word.Length; i++)
                {
                    char c = word[i];
                    if (dic[c] != py[i])
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
          private bool NeedSave(string word,string py)
          {
              var pylist = py.Split(new string[] {"'"}, StringSplitOptions.RemoveEmptyEntries);
              return NeedSave(word, new List<string>(pylist));
          }

          private void button3_Click(object sender, EventArgs e)
          {

          }
    }
}
