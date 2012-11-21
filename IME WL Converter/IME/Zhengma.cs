using System;
using System.Collections.Generic;
using System.Text;
using Studyzy.IMEWLConverter.Generaters;
using Studyzy.IMEWLConverter.Helpers;

namespace Studyzy.IMEWLConverter.IME
{
    [ComboBoxShow(ConstantString.ZHENGMA, ConstantString.ZHENGMA_C, 190)]
    public class Zhengma : IWordLibraryTextImport
    {
        #region IWordLibraryImport 成员

        private readonly IWordCodeGenerater pinyinFactory = new WordPinyinGenerater();

        public WordLibraryList Import(string path)
        {
            string str = FileOperationHelper.ReadFile(path, Encoding);
            return ImportText(str);
        }

        public WordLibraryList ImportText(string str)
        {
            var wlList = new WordLibraryList();
            string[] lines = str.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            CountWord = lines.Length;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                wlList.AddWordLibraryList(ImportLine(line));
                CurrentStatus = i;
            }
            return wlList;
        }

        public virtual WordLibraryList ImportLine(string line)
        {
            var wlList = new WordLibraryList();
            string[] strs = line.Split(' ');
            for (int i = 1; i < strs.Length; i++)
            {
                string word = strs[i].Replace("，", ""); //把汉字中带有逗号的都去掉逗号
                List<string> list = pinyinFactory.GetCodeOfString(word);
                for (int j = 0; j < list.Count; j++)
                {
                    var wl = new WordLibrary();
                    wl.Word = word;
                    wl.PinYin = list.ToArray();
                    wlList.Add(wl);
                }
            }
            return wlList;
        }

        public int CountWord { get; set; }
        public int CurrentStatus { get; set; }

        public bool IsText
        {
            get { return true; }
        }

        #endregion

        #region IWordLibraryTextImport Members

        public Encoding Encoding
        {
            get { return Encoding.Default; }
        }

        #endregion
    }
}