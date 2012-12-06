﻿using System;
using System.Collections.Generic;
using System.Text;
using Studyzy.IMEWLConverter.Generaters;
using Studyzy.IMEWLConverter.Helpers;

namespace Studyzy.IMEWLConverter.IME
{
    [ComboBoxShow(ConstantString.WORD_ONLY, ConstantString.WORD_ONLY_C, 2010)]
    public class NoPinyinWordOnly : BaseImport, IWordLibraryTextImport, IWordLibraryExport
    {
        private IWordCodeGenerater pinyinFactory;

        #region IWordLibraryImport 成员

     

        /// <summary>
        /// 将一行纯文本转换为对象
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public virtual WordLibraryList ImportLine(string line)
        {
            var py = pinyinFactory.GetCodeOfString(line);
            var wl = new WordLibrary();
            wl.Word = line;
            wl.PinYin = ToArray(py);
            var wll = new WordLibraryList();
            wll.Add(wl);
            return wll;
        }

        /// <summary>
        /// 通过搜狗细胞词库txt内容构造词库对象
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual WordLibraryList Import(string path)
        {
            string str = FileOperationHelper.ReadFile(path);
            return ImportText(str);
        }

        public virtual WordLibraryList ImportText(string str)
        {
            pinyinFactory = new WordPinyinGenerater();

            var wlList = new WordLibraryList();
            string[] words = str.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                try
                {
                    string word = words[i].Trim();
                    if (word != string.Empty)
                    {
                        wlList.AddWordLibraryList(ImportLine(word));
                    }
                }
                catch
                {
                }
            }
            return wlList;
        }

        #endregion

        #region IWordLibraryExport 成员

        #region IWordLibraryExport Members

        public virtual string ExportLine(WordLibrary wl)
        {
            return wl.Word;
        }

        public virtual string Export(WordLibraryList wlList)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < wlList.Count; i++)
            {
                sb.Append(wlList[i].Word);
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

        #endregion

        #region IWordLibraryTextImport Members

        public virtual Encoding Encoding
        {
            get { return Encoding.Default; }
        }

        #endregion

        #endregion

    
    }
}