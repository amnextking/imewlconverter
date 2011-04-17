using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    public class NoPinyinWordOnly : IWordLibraryImport, IWordLibraryExport
    {
        private PinYinFactory pinyinFactory;

        #region IWordLibraryImport 成员
        public int CountWord { get; set; }
        public int CurrentStatus { get; set; }

        /// <summary>
        /// 好像不对
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public WordLibraryList ImportLine(string line)
        {
            List<List<string>> list = pinyinFactory.GetPinYinListOfString(line);
            var wl = new WordLibrary();
            wl.Word = line;
            wl.PinYin = list[0].ToArray();
            var wll = new WordLibraryList();
            wll.Add(wl);
            return wll;
        }
        /// <summary>
        /// 通过搜狗细胞词库txt内容构造词库对象
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public WordLibraryList Import(string str)
        {
            //if (OnlySinglePinyin)
            //{
            pinyinFactory = new SinglePinyin();
            //}
            //else
            //{
            //    pinyinFactory = new AllPinyin();
            //}
            var wlList = new WordLibraryList();
            string[] words = str.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                try
                {
                    string word = words[i].Trim();
                    List<List<string>> list = pinyinFactory.GetPinYinListOfString(word);
                    for (int j = 0; j < list.Count; j++)
                    {
                        var wl = new WordLibrary();
                        wl.Word = word;
                        wl.PinYin = list[j].ToArray();
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

        #region IWordLibraryExport 成员
        public string ExportLine(WordLibrary wl)
        {
            return wl.Word;
        }
        public string Export(WordLibraryList wlList)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < wlList.Count; i++)
            {
                sb.Append(wlList[i].Word);
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

        public Encoding Encoding
        {
            get { return Encoding.Default; }
        }

        #endregion

    }
}