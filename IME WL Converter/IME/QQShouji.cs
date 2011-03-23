using System;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    public class QQShouji : IWordLibraryImport, IWordLibraryExport
    {
        #region IWordLibraryExport Members

        public string ExportLine(WordLibrary wl)
        {
            var sb = new StringBuilder();

            sb.Append(wl.Word);
            sb.Append(" ");
            sb.Append(wl.GetPinYinString("'", BuildType.None));

            return sb.ToString();
        }

        #endregion

        #region IWordLibraryImport Members

        public int CountWord { get; set; }
        public int CurrentStatus { get; set; }


        public WordLibraryList ImportLine(string line)
        {
            if (line.IndexOf("'") == 0)
            {
                string py = line.Split(' ')[1];
                string word = line.Split(' ')[0];
                var wl = new WordLibrary();
                wl.Word = word;
                wl.Count = 1;
                wl.PinYin = py.Split(new[] {'\''}, StringSplitOptions.RemoveEmptyEntries);
                var wll = new WordLibraryList();
                wll.Add(wl);
                return wll;
            }
            return null;
        }

        #endregion

        #region IWordLibraryExport 成员

        public string Export(WordLibraryList wlList)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < wlList.Count; i++)
            {
                sb.Append(ExportLine(wlList[i]));
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

        public WordLibraryList Import(string str)
        {
            var wlList = new WordLibraryList();
            string[] lines = str.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                wlList.AddWordLibraryList(ImportLine(line));
            }
            return wlList;
        }

        #endregion
    }
}