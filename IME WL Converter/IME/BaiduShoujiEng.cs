﻿using System;
using System.Text;

namespace Studyzy.IMEWLConverter.IME
{
    /// <summary>
    /// 百度手机输入法支持单独的英语词库，格式“单词Tab词频”
    /// </summary>
    public class BaiduShoujiEng : IWordLibraryTextImport, IWordLibraryExport
    {
        #region IWordLibraryExport 成员

        public string ExportLine(WordLibrary wl)
        {
            return wl.Word + "\t" + (54999 + wl.Count);
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
            get { return Encoding.ASCII; }
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
            string[] lines = str.Split(new[] { "\r","\n" }, StringSplitOptions.RemoveEmptyEntries);
            CountWord = lines.Length;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                CurrentStatus = i;

                wlList.AddWordLibraryList(ImportLine(line));
            }
            return wlList;
        }

        public WordLibraryList ImportLine(string line)
        {
            var wp = line.Split('\t');
            
            string word = wp[0];
            var wl = new WordLibrary();
            wl.Word = word;
            wl.Count =Convert.ToInt32( wp[1]);
            wl.PinYin = new string[]{};
            var wll = new WordLibraryList();
            wll.Add(wl);
            return wll;
        }

        #endregion
    }
}