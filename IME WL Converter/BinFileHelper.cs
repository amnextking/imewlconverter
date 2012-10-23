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
    }
}
