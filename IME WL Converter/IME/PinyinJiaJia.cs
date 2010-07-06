using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Studyzy.IMEWLConverter
{
   public class PinyinJiaJia:IWordLibraryExport,IWordLibraryImport
    {
        #region IWordLibraryExport 成员

        public string Export(WordLibraryList wlList)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < wlList.Count; i++)
            {
                sb.Append(wlList[i].Word);
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

        public Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }

        #endregion

        #region IWordLibraryImport 成员

        public bool OnlySinglePinyin
        {
            get;
            set;
        }

        public WordLibraryList Import(string str)
        {
            SinglePinyin single = new SinglePinyin();
            WordLibraryList wlList = new WordLibraryList();
            string[] words = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                try
                {
                   Regex reg=new Regex("[a-z]+", RegexOptions.IgnoreCase);
                    var py= reg.Matches(words[i]);
                    var ws=reg.Split(words[i]);
                    WordLibrary wl = new WordLibrary();
                    if (py.Count == 0)
                    {
                        wl.Word = words[i];
                        wl.PinYin = single.GetPinYinListOfString(wl.Word)[0];
                        wlList.Add(wl);
                    }
                    else//有多音字
                    {
                        string word = "";
                        List<string> pinyin = new List<string>();
                        for (int j = 0; j < ws.Length; j++)
                        {
                            word += ws[j];
                            for (int k = 0; k < ws[j].Length; k++)
                            {
                                char c = ws[j][k];
                                if (k == ws[j].Length - 1&&py.Count-1==j)//最后一个字(多音字)
                                {
                                    pinyin.Add(py[j].Value);
                                }
                                else
                                {
                                    pinyin.Add(single.GetPinYinOfChar(c)[0]);
                                }
                            }
                        }
                        wl.Word = word;
                        wl.PinYin = pinyin;
                        wlList.Add(wl);
                    }
                }
                catch
                {
                }
            }
            return wlList;
        }

        #endregion
    }
}
