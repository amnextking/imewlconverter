using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    static class BinFileHelper
    {
        public static short ReadInt16(Stream fs)
        {
            var temp = new byte[2];
            fs.Read(temp, 0, 2);
            var s = BitConverter.ToInt16(temp, 0);
            return s;
        }
        public static int ReadInt32(Stream fs)
        {
            var temp = new byte[4];
            fs.Read(temp, 0, 4);
            var s = BitConverter.ToInt32(temp, 0);
            return s;
        }
        public static long ReadInt64(Stream fs)
        {
            var temp = new byte[8];
            fs.Read(temp, 0, 8);
            var s = BitConverter.ToInt64(temp, 0);
            return s;
        }
        public static byte[] ReadArray(Stream fs, int count)
        {
            byte[] bytes = new byte[count];
            fs.Read(bytes, 0, count);
            return bytes;
        }
        public static byte[] ReadArray(byte[] fs, int position, int count)
        {
            byte[] bytes = new byte[count];
            for (var i = 0; i < count; i++)
            {
                bytes[i] = fs[position + i];
            }

            return bytes;
        }
    }
}
