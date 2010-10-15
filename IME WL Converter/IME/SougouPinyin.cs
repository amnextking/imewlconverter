using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
   public class SougouPinyin:IWordLibraryExport,IWordLibraryImport
   {
       public int CountWord { get; set; }
       public int CurrentStatus { get; set; }
        #region IWordLibraryExport 成员

        public string Export(WordLibraryList wlList)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < wlList.Count; i++)
            {

                sb.Append(ExportLine(wlList[i]));
                //sb.Append(" ");
                //sb.Append(wlList[i].Word);

                sb.Append("\r\n");
            }
            return sb.ToString();
        }

        public Encoding Encoding
        {
            get { return Encoding.Default; }
        }

        #endregion

        #region IWordLibraryImport 成员

        public WordLibraryList Import(string str)
        {
            WordLibraryList wlList = new WordLibraryList();
            var lines = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.IndexOf("'") == 0)
                {
                    wlList.Add(ImportLine(line));
                }
            }
            return wlList;
        }
       

        #endregion

        public string ExportLine(WordLibrary wl)
        {
            //StringBuilder sb = new StringBuilder();

            string str = wl.GetPinYinString("'", BuildType.LeftContain) + " " + wl.Word ;

            return str;
        }


        public WordLibrary ImportLine(string line)
        {
            if (line.IndexOf("'") == 0)
            {
                string py = line.Split(' ')[0];
                string word = line.Split(' ')[1];
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
