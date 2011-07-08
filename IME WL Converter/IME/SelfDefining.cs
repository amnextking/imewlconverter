using System;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    public class SelfDefining : IWordLibraryTextImport
    {
        public ParsePattern UserDefiningPattern { get; set; }

        #region IWordLibraryImport 成员

        private PinYinFactory pinyinFactory = new SinglePinyin();

        public Encoding Encoding
        {
            get { return Encoding.Default; }
        }
        public bool IsText
        {
            get { return true; }
        }
        public WordLibraryList Import(string path)
        {
            var str = FileOperationHelper.ReadFile(path);
            return ImportText(str);
        }
        public WordLibraryList ImportText(string str)
        {
            var wlList = new WordLibraryList();
            string[] lines = str.Split(new[] {"\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            CountWord = lines.Length;
            CountWord = lines.Length;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                CurrentStatus = i;
                wlList.AddWordLibraryList(ImportLine(line));
                CurrentStatus = i;
            }
            return wlList;
        }

        public WordLibraryList ImportLine(string line)
        {
            var wlList = new WordLibraryList();
            WordLibrary wl = UserDefiningPattern.BuildWordLibrary(line);
            wlList.Add(wl);
            return wlList;
        }

        public int CountWord { get; set; }
        public int CurrentStatus { get; set; }

        #endregion
    }
}