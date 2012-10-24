using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter.IME
{
    class Rime : IWordLibraryTextImport, IWordLibraryExport
    {
        #region IWordLibraryExport 成员

        public string ExportLine(WordLibrary wl)
        {
            var sb = new StringBuilder();

            sb.Append(wl.Word);
            sb.Append("\t");
            sb.Append(wl.GetPinYinString(" ", BuildType.None));
            sb.Append("\t");
            sb.Append(wl.Count);

            return sb.ToString();
        }

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
            get { return new UTF8Encoding(false); }
        }

        #endregion

        #region IWordLibraryImport 成员

        public int CountWord { get; set; }
        public int CurrentStatus { get; set; }

        public bool IsText
        {
            get { return true; }
        }
        public WordLibraryList Import(string path)
        {
            var str = FileOperationHelper.ReadFile(path, Encoding);
            return ImportText(str);
        }
        public WordLibraryList ImportText(string str)
        {

            var wlList = new WordLibraryList();
            string[] lines = str.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            CountWord = lines.Length;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                CurrentStatus = i;
                if (line.StartsWith("#"))
                {
                    continue;
                }
                wlList.AddWordLibraryList(ImportLine(line));
            }
            return wlList;
        }

        public WordLibraryList ImportLine(string line)
        {
            var lineArray = line.Split('\t');
            string py = lineArray[1];
            string word = lineArray[0];
            var wl = new WordLibrary();
            wl.Word = word;
            wl.Count =Convert.ToInt32(lineArray[2]);
            wl.PinYin = py.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var wll = new WordLibraryList();
            wll.Add(wl);
            return wll;
        }

        #endregion
    }
}
