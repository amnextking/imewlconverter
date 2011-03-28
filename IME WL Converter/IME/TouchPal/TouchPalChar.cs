using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    /// <summary>
    /// 一个字，包含拼音本字的顺序和拼音2字节，词频4，下字4，跳转4，上字4，真实上字4，未知4，共占有空间26字节
    /// </summary>
    class TouchPalChar
    {
        public TouchPalChar()
        {
            
        }

        private short pinYinIndex;
        private int countPosition;
        private int nextCharPosition;
        private int jumpToPosition;
        private int prevCharPosition;
        private int prevValidCharPosition;
        private int unknown;
        private int beginPosition;

        public int Unknown
        {
            get { return unknown; }
        }

        public int PrevValidCharPosition
        {
            get { return prevValidCharPosition; }
            set { prevValidCharPosition = value; }
        }

        public int PrevCharPosition
        {
            get { return prevCharPosition; }
        }

        public int JumpToPosition
        {
            get { return jumpToPosition; }
        }

        public int NextCharPosition
        {
            get { return nextCharPosition; }
        }

        public int CountPosition
        {
            get { return countPosition; }
        }

        public short PinYinIndex
        {
            get { return pinYinIndex; }
        }
        public int BeginPosition
        {
            get { return beginPosition; }
            set { beginPosition = value; }
        }
        public static TouchPalChar Load(FileStream fs,int position=0)
        {
            if (position > 0)
            {
                fs.Position = position;
            }
            if (GlobalCache.CharList.ContainsKey((int)fs.Position))
            {
                return GlobalCache.CharList[(int) fs.Position];
            }

            TouchPalChar c = new TouchPalChar();
            c.beginPosition = (int)fs.Position;
            byte[] temp=new byte[2];
            fs.Read(temp, 0, 2);
            c.pinYinIndex = BitConverter.ToInt16(temp, 0);
            temp = new byte[4];
            fs.Read(temp, 0, 4);
            c.countPosition = BitConverter.ToInt32(temp, 0);
            fs.Read(temp, 0, 4);
            c.nextCharPosition = BitConverter.ToInt32(temp, 0);
            fs.Read(temp, 0, 4);
            c.jumpToPosition = BitConverter.ToInt32(temp, 0);
            fs.Read(temp, 0, 4);
            c.prevCharPosition = BitConverter.ToInt32(temp, 0);
            fs.Read(temp, 0, 4);
            c.prevValidCharPosition = BitConverter.ToInt32(temp, 0);
            fs.Read(temp, 0, 4);
            c.unknown = BitConverter.ToInt32(temp, 0);
            GlobalCache.CharList.Add(c.beginPosition, c);
            return c;
        }
        /// <summary>
        /// 是否是词中的最后一个字
        /// </summary>
        /// <returns></returns>
        public bool IsLastChar()
        {
            return countPosition > 0;
        }

        public TouchPalChar NextChar { get; set; }
        public TouchPalChar JumpToChar { get; set; }
        public TouchPalChar PrevChar { get; set; }
        public TouchPalChar PrevValidChar { get; set; }
        public TouchPalWord Word { get; set; }
        public int PinyinSortIndex
        {
            get { return pinYinIndex >> 11; }
        }
        public int PinyinCode
        {
            get { return pinYinIndex&2047 ; }
        }
        public string PinyinString
        {
            get { return GlobalCache.PinyinMapping[ PinyinCode]; }
        }
        /// <summary>
        /// 真实对应的汉字，在导出时使用
        /// </summary>
        public char Char
        {
            get; set; }
    }
}
