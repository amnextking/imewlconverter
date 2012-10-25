﻿using System;
using System.Text;

namespace Studyzy.IMEWLConverter.IME
{
	/// <summary>
	/// Google拼音输入法
	/// </summary>
    public class GooglePinyin : IWordLibraryExport, IWordLibraryTextImport
    {

        #region IWordLibraryExport 成员
        public string ExportLine(WordLibrary wl)
        {
            var sb = new StringBuilder();

            sb.Append(wl.Word);
            sb.Append("\t");
            sb.Append(wl.Count);
            sb.Append("\t");
            sb.Append(wl.GetPinYinString(" ", BuildType.None));


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
            get { return Encoding.Default; }
        }

        #endregion

        #region IWordLibraryImport 成员
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
        public int CountWord { get; set; }
        public int CurrentStatus { get; set; }


        public WordLibraryList ImportLine(string line)
        {
            string[] c = line.Split('\t');
            var wl = new WordLibrary();
            wl.Word = c[0];
            wl.Count = Convert.ToInt32(c[1]);
            wl.PinYin = c[2].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var wll = new WordLibraryList();
            wll.Add(wl);
            return wll;
        }
        #endregion
    }
}