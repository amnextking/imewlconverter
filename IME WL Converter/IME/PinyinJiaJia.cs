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
                string str=wlList[i].Word;
                for(int j=0;j<str.Length;j++)
                {
                   sb.Append( str[j]+wlList[i].PinYin[j]);
                }
                sb.Append( "\r\n");
            }
            return sb.ToString();
        }

        public Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }

        #endregion

        #region IWordLibraryImport 成员

        public int CountWord        {            get;            set;        }
        public int CurrentStatus { get; set; }

       /// <summary>
        /// 形如：冷血xue动物
        /// 只有多音字才注音，一般的字不注音，就使用默认读音即可
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
        public WordLibraryList Import(string str)
        {
            SinglePinyin single = new SinglePinyin();
            WordLibraryList wlList = new WordLibraryList();
            string[] words = str.Split(new char[] { '\r','\n' }, StringSplitOptions.RemoveEmptyEntries);
            CountWord = words.Length;
            for (int i = 0; i < words.Length; i++)
            {
                CurrentStatus = i;
                try
                {
                    #region 效率低，不采用了
                    //Regex reg=new Regex("[a-z]+", RegexOptions.IgnoreCase);
                    // var py= reg.Matches(words[i]);
                    // var ws=reg.Split(words[i]);
                    // WordLibrary wl = new WordLibrary();
                    // if (py.Count == 0)
                    // {
                    //     wl.Word = words[i];
                    //     wl.PinYin = single.GetPinYinListOfString(wl.Word)[0];
                    //     wlList.Add(wl);
                    // }
                    // else//有多音字
                    // {
                    //     string word = "";
                    //     List<string> pinyin = new List<string>();
                    //     for (int j = 0; j < ws.Length; j++)//第几个词
                    //     {
                    //         word += ws[j];
                    //         for (int k = 0; k < ws[j].Length; k++)//词的第几个字
                    //         {
                    //             char c = ws[j][k];
                    //             if (k == ws[j].Length - 1&&py.Count-1>=j)//最后一个字(多音字)
                    //             {
                    //                 pinyin.Add(py[j].Value);
                    //             }
                    //             else
                    //             {
                    //                 pinyin.Add(single.GetPinYinOfChar(c)[0]);
                    //             }
                    //         }
                    //     }
                    //     wl.Word = word;
                    //     wl.PinYin = pinyin;
                    //     wlList.Add(wl);
                    // }
                    #endregion
                    string word = words[i];
                    string hz = "";
                    List<string> py = new List<string>();
                    int j ;
                    for (j=0; j < word.Length - 1; j++)
                    {
                        hz += word[j];
                        if (word[j + 1] > 'z')//而且后面跟的不是拼音
                        {
                            py.Add(single.GetPinYinOfChar(word[j])[0]);
                        }
                        else//后面跟拼音
                        {
                            int k = 1;
                            string py1 = "";
                            while (j+k !=word.Length &&  word[j + k] <= 'z')
                            {                               
                                py1 += word[j + k];
                                k++;
                            }
                            py.Add(py1);
                            j += k-1;//减1是因为接下来会运行j++
                        }

                    }
                    if (j == word.Length-1)//最后一个字是汉字
                    {
                        hz += word[j];
                        py.Add(single.GetPinYinOfChar(word[j])[0]);
                    }
                    WordLibrary wl = new WordLibrary();
                    wl.PinYin = py.ToArray();
                    wl.Word = hz;
                    wlList.Add(wl);
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
