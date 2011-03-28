using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    /// <summary>
    /// 一个词，是由n个字组成，除此之外还有词频4字节，未知1字节，汉字Unicode编码2*n字节，所以一个词占有26*n+4+1+2*n =   28*n+5个字节
    /// </summary>
    class TouchPalWord
    {
        private int count;
        private string chineseWord;
        public TouchPalChar[] Chars
        {
            get; set; }

        /// <summary>
        /// Load词频和中文词
        /// </summary>
        /// <param name="wordLength"></param>
        /// <param name="fs"></param>
        public static TouchPalWord LoadCountAndWord(int wordLength, FileStream fs,int position)
        {
            if (position > 0)
            {
                fs.Position = position;
            }
            if (GlobalCache.WordList.ContainsKey(position))
            {
                return GlobalCache.WordList[position];
            }
            TouchPalWord w = new TouchPalWord();
            byte[] temp = new byte[4];
            fs.Read(temp, 0, 4);
            w.Count = BitConverter.ToInt32(temp, 0);
            byte[] unkonwByte = new byte[1];
            fs.Read(unkonwByte, 0, 1);//这里一个字节不知道干什么的
            temp = new byte[wordLength*2];
            fs.Read(temp, 0, wordLength*2);
            w.ChineseWord = Encoding.Unicode.GetString(temp);
            GlobalCache.WordList.Add(position, w);
            return w;
        }
        /// <summary>
        /// 这个词的开始位置
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// 词频
        /// </summary>
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        /// <summary>
        /// 中文词语
        /// </summary>
        public string ChineseWord
        {
            get { return chineseWord; }
            set { chineseWord = value; }
        }
        public List<string> PinYin
        {
            get
            {
                List<string> py = new List<string>();
                for (int i = Chars.Length-1; i >= 0;i-- )
                {
                    TouchPalChar c = Chars[i];
                    py.Add(c.PinyinString);
                }
                return py;
            }
        }
    }
}
