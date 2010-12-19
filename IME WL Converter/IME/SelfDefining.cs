using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    public class SelfDefining : IWordLibraryImport
   {
        public ParsePattern UserDefiningPattern { get; set; }
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
            var wl= UserDefiningPattern.BuildWordLibrary(line);
            wlList.Add(wl);
            return wlList;
        }

        public int CountWord { get; set; }
        public int CurrentStatus { get; set; }

        #endregion
   }
}
