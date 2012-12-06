﻿using System;
using System.Text;
using Studyzy.IMEWLConverter.Generaters;
using Studyzy.IMEWLConverter.Helpers;

namespace Studyzy.IMEWLConverter.IME
{
    /// <summary>
    /// 搜狗五笔的词库格式为“五笔编码 词语”\r\n
    /// </summary>
    [ComboBoxShow(ConstantString.WUBI86, ConstantString.WUBI86_C, 210)]
    public class Wubi86 : BaseImport, IWordLibraryTextImport, IWordLibraryExport
    {
        #region IWordLibraryExport 成员



        public string ExportLine(WordLibrary wl)
        {
            var sb = new StringBuilder();

            sb.Append(WubiHelper.GetStringWubi86Code(wl.Word));
            sb.Append(" ");
            sb.Append(wl.Word);

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
            get { return Encoding.Unicode; }
        }



        #endregion
        private readonly IWordCodeGenerater pinyinFactory = new WordPinyinGenerater();
        #region IWordLibraryImport 成员

     
        public WordLibraryList ImportLine(string line)
        {
            string code = line.Split(' ')[0];
            string word = line.Split(' ')[1];
            var wl = new WordLibrary();
            wl.Word = word;
            wl.Count = DefaultRank;
            wl.PinYin = ToArray(pinyinFactory.GetCodeOfString(word));
            var wll = new WordLibraryList();
            if(wl.PinYin.Length>0)
            {
                wll.Add(wl);
            }
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