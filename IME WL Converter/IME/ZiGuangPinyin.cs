using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    public class ZiGuangPinyin : IWordLibraryImport, IWordLibraryExport
    {
        #region IWordLibraryImport 成员

        public WordLibraryList Import(string str)
        {
            WordLibraryList wlList = new WordLibraryList();
            var lines = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 3; i < lines.Length; i++)
            {
                string line = lines[i];

                wlList.Add(ImportLine(line));

            }
            return wlList;
        }

        #endregion
        public int CountWord { get; set; }
        public int CurrentStatus { get; set; }
        #region IWordLibraryExport 成员

        public string Export(WordLibraryList wlList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("名称=用户词库\r\n");
            sb.Append("作者=深蓝词库转换\r\n");
            sb.Append("编辑=1\r\n\r\n");
            for (int i = 0; i < wlList.Count; i++)
            {
                sb.Append(ExportLine(wlList[i]));
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

        public Encoding Encoding
        {
            get
            {
                return Encoding.Unicode;
            }
        }

        #endregion

        public string ExportLine(WordLibrary wl)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(wl.Word);
            sb.Append("\t");
            sb.Append(wl.GetPinYinString("'", BuildType.None));
            sb.Append("\t100000");

            return sb.ToString();
        }


        public WordLibrary ImportLine(string line)
        {
            string py = line.Split('\t')[0];
            string word = line.Split('\t')[1];
            WordLibrary wl = new WordLibrary();
            wl.Word = word;
            wl.Count = 1;
            wl.PinYin = py.Split(new char[] { '\'' }, StringSplitOptions.RemoveEmptyEntries);
            return wl;

        }
    }
}
