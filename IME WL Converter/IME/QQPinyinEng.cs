﻿using System;
using System.Text;
using Studyzy.IMEWLConverter.Helpers;

namespace Studyzy.IMEWLConverter.IME
{
    /// <summary>
    /// QQ拼音支持单独的英语词库，使用“英文单词,词频”的格式
    /// </summary>
    [ComboBoxShow(ConstantString.QQ_PINYIN_ENG, ConstantString.QQ_PINYIN_ENG_C, 80)]
    public class QQPinyinEng : IWordLibraryTextImport, IWordLibraryExport
    {
        #region IWordLibraryExport 成员

        #region IWordLibraryExport Members

        public string ExportLine(WordLibrary wl)
        {
            return wl.Word + "," + wl.Count;
        }

        public string Export(WordLibraryList wlList)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < wlList.Count - 1; i++)
            {
                sb.Append(ExportLine(wlList[i]));
                sb.Append("\r\n");
            }

            return sb.ToString();
        }

        #endregion

        #region IWordLibraryTextImport Members

        public Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }

        #endregion

        #endregion

        #region IWordLibraryTextImport Members

        public int CountWord { get; set; }
        public int CurrentStatus { get; set; }

        public bool IsText
        {
            get { return true; }
        }

        public WordLibraryList ImportLine(string line)
        {
            string[] sp = line.Split(',');

            string word = sp[0];
            int count = Convert.ToInt32(sp[1]);
            var wl = new WordLibrary();
            wl.Word = word;
            wl.Count = count;
            wl.PinYin = new string[] {};
            var wll = new WordLibraryList();
            wll.Add(wl);
            return wll;
        }

        public WordLibraryList Import(string path)
        {
            string str = FileOperationHelper.ReadFile(path, Encoding);
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