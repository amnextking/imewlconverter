using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
   public class QQShouji:IWordLibraryImport,IWordLibraryExport
   {
       public int CountWord { get; set; }
       public int CurrentStatus { get; set; }
        #region IWordLibraryExport 成员

        public string Export(WordLibraryList wlList)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < wlList.Count; i++)
            {
               ExportLine(wlList[i]);

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
            WordLibraryList wlList = new WordLibraryList();
            var lines = str.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                wlList.Add(ImportLine(line));

            }
            return wlList;
        }

       #endregion

        public string ExportLine(WordLibrary wl)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(wl.Word);
            sb.Append(" ");
            sb.Append(wl.GetPinYinString("'", BuildType.None));

            return sb.ToString();
        }


        public WordLibrary ImportLine(string line)
        {
            if (line.IndexOf("'") == 0)
            {
                string py = line.Split(' ')[1];
                string word = line.Split(' ')[0];
                WordLibrary wl = new WordLibrary();
                wl.Word = word;
                wl.Count = 1;
                wl.PinYin = py.Split(new char[] { '\'' }, StringSplitOptions.RemoveEmptyEntries);
                return wl;
            }
            return null;
        }
    }
}
