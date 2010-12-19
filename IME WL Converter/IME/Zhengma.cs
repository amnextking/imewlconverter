using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
   public class Zhengma:IWordLibraryImport
   {

        #region IWordLibraryImport 成员

        public WordLibraryList Import(string str)
        {
            WordLibraryList wlList = new WordLibraryList();
            var lines = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            CountWord = lines.Length;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                wlList.AddWordLibraryList(ImportLine(line));
                CurrentStatus = i;
            }
            return wlList;
        }


        private PinYinFactory pinyinFactory = new SinglePinyin();

        public WordLibraryList ImportLine(string line)
        {
            WordLibraryList wlList = new WordLibraryList();
            string[] strs=line.Split(' ');
            for (var i = 1; i < strs.Length; i++)
            {
                string word = strs[i].Replace("，","");//把汉字中带有逗号的都去掉逗号
                var list = pinyinFactory.GetPinYinListOfString(word);
                for (int j = 0; j < list.Count; j++)
                {
                    WordLibrary wl = new WordLibrary();
                    wl.Word = word;
                    wl.PinYin = list[j].ToArray();
                    wlList.Add(wl);
                }
            }
            return wlList;
        }

        public int CountWord { get; set; }
        public int CurrentStatus { get; set; }

        #endregion
   }
}
