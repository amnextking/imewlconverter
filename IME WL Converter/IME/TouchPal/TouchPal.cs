﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    /// <summary>
    /// 触宝输入法
    /// </summary>
    class TouchPal : IWordLibraryImport, IWordLibraryExport
    {

        private int beginCharPosition;
        private void ParseHeader(FileStream fs)
        {
            byte[] temp = new byte[4];
            fs.Read(temp, 0, 4);
            int totalLength = BitConverter.ToInt32(temp, 0);
            byte[] unknown = new byte[6];
            fs.Read(unknown, 0, 6);//这6个字节不知道什么用，好像都是0
            fs.Read(temp, 0, 4);
            beginCharPosition = BitConverter.ToInt32(temp, 0);//值为1E 
            fs.Position = beginCharPosition;
        }

        private TouchPalChar FindBeginPosition(string word, out int charIndex)
        {
            var stack = new List<TouchPalChar>(GlobalCache.ExportStackes.ToArray());
            stack.Reverse();
            int len = Math.Min(stack.Count, word.Length);
            for (int i = 0; i < len; i++)
            {
                char c = word[i];
                if (stack[i].Char != c)
                {
                    charIndex = i;

                    for (int j = i; j < stack.Count; j++) //把剩下的字出栈
                    {
                        GlobalCache.ExportStackes.Pop();
                    }
                    if (i == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return stack[i - 1];
                    }
                }
            }
            if (stack.Count > 0)
            {
                charIndex = stack.Count;
                return stack[stack.Count - 1];
            }
            else
            {
                charIndex = 0;
                return null;
            }
        }

        /// <summary>
        /// 把一个词条一个字一个字的写入词库文件中
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="wl"></param>
        /// <param name="isLastWord"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public int WriteWord(FileStream fs, WordLibrary wl, bool isLastWord)
        {
            int beginPosition = (int)fs.Position;
            int wordLength = wl.Word.Length;
            int charIndex;
            var stackChar = FindBeginPosition(wl.Word, out charIndex);

            for (int i = charIndex; i < wordLength; i++)
            {
                TouchPalChar item = new TouchPalChar();
                item.Char = wl.Word[i];
                item.BeginPosition = (int)fs.Position;
                string py = wl.PinYin[i];
                int pyIndex = GlobalCache.PinyinIndexMapping[py];
                short code = (short)(((i + 1) << 11) + pyIndex);
                fs.Write(BitConverter.GetBytes(code), 0, 2);
                int p1 = 0;//词频位置
                if (i == wordLength - 1)//最后一个字
                {
                    p1 = beginPosition + wordLength * 26;

                }
                fs.Write(BitConverter.GetBytes(p1), 0, 4);
                int p2 = 0;//下个字位置
                if (i != wordLength - 1)
                {
                    p2 = beginPosition + (i + 1) * 26;
                }
                fs.Write(BitConverter.GetBytes(p2), 0, 4);
                int p3 = 0;//跳转位置
                if (!isLastWord && i == 0)
                {
                    p3 = beginPosition + wordLength * 28 + 5;
                }
                fs.Write(BitConverter.GetBytes(p3), 0, 4);
                int p4 = 0;//上个字位置
                if (charIndex == 0)
                {
                    if (i == 0)
                    {
                        p4 = GlobalCache.JumpChar.BeginPosition;
                        GlobalCache.JumpChar = item;
                    }
                    else
                    {
                        p4 = beginPosition + (i - 1) * 26;
                    }
                }
                else
                {
                    p4 = stackChar.BeginPosition;
                }
                fs.Write(BitConverter.GetBytes(p4), 0, 4);
                int p5 = 4;
                if (charIndex == 0)
                {
                    if (i != 0)
                    {
                        p5 = p4;
                    }
                }
                else
                {
                    p5 = stackChar.PrevValidCharPosition;
                }
                item.PrevValidCharPosition = p5;
                fs.Write(BitConverter.GetBytes(p5), 0, 4);
                int p6 = 0;
                fs.Write(BitConverter.GetBytes(p6), 0, 4);
                GlobalCache.ExportStackes.Push(item);
            }
            int count = 96;// wl.Count;
            fs.Write(BitConverter.GetBytes(count), 0, 4);
            fs.WriteByte(0);//这个字节不知道干什么的
            byte[] wordByte = Encoding.Unicode.GetBytes(wl.Word);
            fs.Write(wordByte, 0, wordByte.Length);
            return beginPosition;
        }


        #region IWordLibraryExport Members

        public Encoding Encoding
        {
            get { return Encoding.Default; }
        }
        /// <summary>
        /// 将词库写入一个二进制文件，然后返回二进制文件的路径
        /// </summary>
        /// <param name="wlList"></param>
        /// <returns></returns>
        public string Export(WordLibraryList wlList)
        {
            TouchPalChar rootChar = BuildTree(wlList);
            string s = rootChar.ToString();


            GlobalCache.ExportStackes.Clear();
            //创建一个临时文件
            string tempPath = System.Windows.Forms.Application.StartupPath + "\\temp" +
                              DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
            var fs = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.Write);
            //int totalLength = 30;
            //foreach (WordLibrary wl in wlList)
            //{
            //    totalLength += wl.Word.Length * 28 + 5;
            //}
            //fs.Write(BitConverter.GetBytes(totalLength), 0, 4);
            //byte[] head = new byte[] { 0, 0, 0, 0, 0, 0, 0x1E, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //fs.Write(head, 0, 26);
            //int from = 4;
            //GlobalCache.JumpChar = new TouchPalChar() {BeginPosition = 4};
            //for (int i = 0; i < wlList.Count; i++)
            //{
            //    WordLibrary wl = wlList[i];
            //    from = WriteWord(fs, wl, i == wlList.Count - 1);
            //}
            fs.Close();
            return tempPath;
        }

        #region Build Word Lib Tree
       
        /// <summary>
        /// 构造一棵词库的树
        /// </summary>
        /// <param name="wlList"></param>
        /// <returns></returns>
        private TouchPalChar BuildTree(WordLibraryList wlList)
        {
            //先对词库进行排序再生成词库树
            wlList.Sort((a, b) => a.Word.CompareTo(b.Word));
            WordLibrary rootWL = wlList[0];
            TouchPalChar rootChar = new TouchPalChar();//这个只是一个根Char，没有实际的字
            TouchPalChar lastChar = AddWordLink2Char(rootChar, rootWL, 0);


            for (int i = 1; i < wlList.Count; i++)
            {
                WordLibrary wl = wlList[i];
                string a = lastChar.Word.ChineseWord;
                string b = wl.Word;
                int len = FindSameWordLen(a, b);
                if (len == 0)
                {
                    lastChar = AddWordLink2Char(rootChar, wl, 0);
                }
                else
                {
                    lastChar = AddWordLink2Char(lastChar, wl, len);
                }
            }
            return rootChar;
        }
        

        /// <summary>
        /// 将一个词加入到最后一个节点
        /// </summary>
        /// <param name="rootChar"></param>
        /// <param name="wl"></param>
        /// <param name="begin"></param>
        /// <returns></returns>
        private TouchPalChar AddWordLink2Char(TouchPalChar rootChar, WordLibrary wl, int begin)
        {
            TouchPalWord rootWord = new TouchPalWord {ChineseWord = wl.Word, Count = wl.Count};
            TouchPalChar lastChar = rootChar;
            if (begin > 0)
            {
                lastChar = lastChar.Word.Chars[begin - 1];
            }
            TouchPalChar[] chars = new TouchPalChar[wl.Word.Length];
            rootWord.Chars = chars;
            for (int i = 0; i < begin; i++)
            {
                chars[i] = rootChar.Word.Chars[i];
            }
            for (int i = begin; i < wl.Word.Length; i++)
            {
                char c = wl.Word[i];
                TouchPalChar tpc = new TouchPalChar();
                tpc.Char = c;
                AddChar2Next(lastChar, tpc);
                lastChar = tpc;
                chars[i] = lastChar;
            }
            lastChar.Word = rootWord;
            return lastChar;
        }

        /// <summary>
        /// 找到两个字符串中前面相同的字符的长度
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private int FindSameWordLen(string a, string b)
        {
            int len = Math.Min(a.Length, b.Length);
            for (int i = 0; i < len; i++)
            {
                if (a[i] != b[i])
                {
                    return i;
                }
            }
            return len;
        }
        /// <summary>
        /// 把字b加到字a的后面
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private void AddChar2Next(TouchPalChar a, TouchPalChar b)
        {
            if (a.NextChar == null)
            {
                a.NextChar = b;
                b.PrevChar = a;
                b.PrevValidChar = a;
            }
            else
            {
                TouchPalChar p = GetTouchPalChar2Link(a.NextChar);
                p.JumpToChar = b;
                b.PrevChar = p;
                b.PrevValidChar = a;
            }
        }

        /// <summary>
        /// 找到字p能够跳转的字并返回
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private TouchPalChar GetTouchPalChar2Link(TouchPalChar p)
        {
            if (p.JumpToChar == null)
            {
                return p;
            }
            else
            {
                return GetTouchPalChar2Link(p.JumpToChar);
            }
        }
        #endregion

        public string ExportLine(WordLibrary wl)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IWordLibraryImport Members

        public int CountWord
        {
            get { return 0; }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int CurrentStatus
        {
            get { return 0; }
            set
            {
                throw new NotImplementedException();
            }
        }

        public WordLibraryList Import(string str)
        {
            GlobalCache.CharList.Clear();
            GlobalCache.Stackes.Clear();
            GlobalCache.WordList.Clear();
            var fs = new FileStream(str, FileMode.Open, FileAccess.Read);
            ParseHeader(fs);
            TouchPalChar rootChar = TouchPalChar.Load(fs);//载入第一个字
            LoadTree(fs, rootChar);
            fs.Close();
            WordLibraryList wwl = new WordLibraryList();
            foreach (int i in GlobalCache.WordList.Keys)
            {
                var w = GlobalCache.WordList[i];
                WordLibrary wl = new WordLibrary();
                wl.Count = w.Count;
                wl.PinYin = w.PinYin.ToArray();
                wl.Word = w.ChineseWord;
                //sb.AppendLine(py + "\t" + GlobalCache.WordList[i].ChineseWord + "\t" + GlobalCache.WordList[i].Count);
                wwl.Add(wl);
            }
            return wwl;
        }
        private void LoadTree(FileStream fs, TouchPalChar root)
        {

            if (root.CountPosition > 0)
            {
                GlobalCache.Stackes.Push(root);
                int wordLength = GlobalCache.Stackes.Count;
                root.Word = TouchPalWord.LoadCountAndWord(wordLength, fs, root.CountPosition);
                root.Word.Chars = GlobalCache.Stackes.ToArray();
                GlobalCache.Stackes.Pop();
            }
            if (root.NextCharPosition > 0)
            {
                GlobalCache.Stackes.Push(root);
                root.NextChar = TouchPalChar.Load(fs, root.NextCharPosition);
                LoadTree(fs, root.NextChar);
            }
            if (root.NextCharPosition > 0 && root.JumpToPosition > 0)
            {
                GlobalCache.Stackes.Pop();
            }
            if (root.JumpToPosition > 0)
            {
                root.JumpToChar = TouchPalChar.Load(fs, root.JumpToPosition);

                LoadTree(fs, root.JumpToChar);
            }
            if (root.NextCharPosition > 0 && root.JumpToPosition == 0)
            {
                GlobalCache.Stackes.Pop();
            }

            //if (root.PrevCharPosition > 0)
            //{
            //    root.PrevChar = TouchPalChar.Load(fs, root.PrevCharPosition);
            //}
            //if (root.PrevValidCharPosition > 0)
            //{
            //    root.PrevValidChar = TouchPalChar.Load(fs, root.PrevValidCharPosition);
            //}
        }
        public WordLibraryList ImportLine(string str)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
