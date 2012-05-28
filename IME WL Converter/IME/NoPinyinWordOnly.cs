using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    public class NoPinyinWordOnly : IWordLibraryTextImport, IWordLibraryExport
    {
        private PinYinFactory pinyinFactory;

        #region IWordLibraryImport 成员
        public int CountWord { get; set; }
        public int CurrentStatus { get; set; }

        /// <summary>
        /// 将一行纯文本转换为对象
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public virtual WordLibraryList ImportLine(string line)
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
        public virtual WordLibraryList Import(string path)
        {
            var str = FileOperationHelper.ReadFile(path);
            return ImportText(str);
        }
        public virtual WordLibraryList ImportText(string str)
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
                    if (word != string.Empty)
                    {
                        wlList.AddWordLibraryList(ImportLine(word));
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
        public virtual string ExportLine(WordLibrary wl)
        {
            return wl.Word;
        }
        public virtual string Export(WordLibraryList wlList)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < wlList.Count; i++)
            {
                sb.Append(wlList[i].Word);
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

        public virtual Encoding Encoding
        {
            get { return Encoding.Default; }
        }

        #endregion
        public bool IsText
        {
            get { return true; }
        }
    }
}