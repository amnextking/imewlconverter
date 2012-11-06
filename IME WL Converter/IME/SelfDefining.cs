using System;
using System.Text;

namespace Studyzy.IMEWLConverter.IME
{
    public class SelfDefining : IWordLibraryTextImport,IWordLibraryExport
    {
        public ParsePattern UserDefiningPattern { get; set; }

        #region IWordLibraryImport 成员

        public Encoding Encoding
        {
            get { return Encoding.Default; }
        }

        public string Export(WordLibraryList wlList)
        {
            StringBuilder sb=new StringBuilder();
            foreach (WordLibrary wordLibrary in wlList)
            {
                try
                {
                    sb.Append(ExportLine(wordLibrary));
                    sb.Append("\r\n");
                }
                catch { }
            }
            return sb.ToString();
        }

        public string ExportLine(WordLibrary wl)
        {
          
            string line = UserDefiningPattern.BuildWLString(wl);

            return line;
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