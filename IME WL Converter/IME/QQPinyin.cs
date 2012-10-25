using System;
using System.Text;

namespace Studyzy.IMEWLConverter.IME
{
    public class QQPinyin : IWordLibraryTextImport, IWordLibraryExport
    {


       

        #region IWordLibraryExport 成员
        public string ExportLine(WordLibrary wl)
        {
            var sb = new StringBuilder();

            sb.Append(wl.GetPinYinString("'", BuildType.None));
            sb.Append(" ");
            sb.Append(wl.Word);
            sb.Append(" ");
            sb.Append(wl.Count);
            return sb.ToString();
        }
        public string Export(WordLibraryList wlList)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < wlList.Count-1; i++)
            {
                sb.Append(ExportLine(wlList[i]));
                sb.Append("\r\n");
            }
            var last = wlList[wlList.Count - 1];
            sb.Append(ExportLine(last));
            sb.Append(", ");
            sb.Append(last.GetPinYinString("'", BuildType.None));
            sb.Append(" ");
            sb.Append(last.Count);
            sb.Append("\r\n");
            return sb.ToString();
        }

        public Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }

        #endregion
        #region IWordLibraryImport Members

        public int CountWord { get; set; }
        public int CurrentStatus { get; set; }

        public bool IsText
        {
            get { return true; }
        }
        public WordLibraryList ImportLine(string line)
        {
            line = line.Split(',')[0];//如果有逗号，就只取第一个
            string[] sp = line.Split(' ');
            string py = sp[0];
            string word = sp[1];
            int count = Convert.ToInt32(sp[2]);
            var wl = new WordLibrary();
            wl.Word = word;
            wl.Count = count;
            wl.PinYin = py.Split(new[] { '\'' }, StringSplitOptions.RemoveEmptyEntries);
            var wll = new WordLibraryList();
            wll.Add(wl);
            return wll;
        }

        public WordLibraryList Import(string path)
        {
            var str = FileOperationHelper.ReadFile(path, Encoding);
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
                CurrentStatus = i;
                wlList.AddWordLibraryList(ImportLine(line));
            }
            return wlList;
        }

        #endregion
    }
}