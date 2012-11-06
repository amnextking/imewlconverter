using System;
using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.Text;

namespace Studyzy.IMEWLConverter.Language
{
    class SystemKernel : IChineseConverter
    {
        [DllImport("kernel32.dll", EntryPoint = "LCMapStringA")]
        public static extern int LCMapString(int Locale, int dwMapFlags, byte[] lpSrcStr, int cchSrc, byte[] lpDestStr, int cchDest);

        const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
        const int LCMAP_TRADITIONAL_CHINESE = 0x04000000;

        public string ToChs(string cht)
        {
            Encoding gb2312 = Encoding.GetEncoding(936);
            byte[] src = gb2312.GetBytes(cht);
            byte[] dest = new byte[src.Length];
            LCMapString(0x0804, LCMAP_SIMPLIFIED_CHINESE, src, -1, dest, src.Length);

            //LCMapString(0x0804, LCMAP_TRADITIONAL_CHINESE, src, -1, dest, src.Length);
            return gb2312.GetString(dest);
        }

        public string ToCht(string chs)
        {
            Encoding gb2312 = Encoding.GetEncoding(936);
            byte[] src = gb2312.GetBytes(chs);
            byte[] dest = new byte[src.Length];
            //LCMapString(0x0804, LCMAP_SIMPLIFIED_CHINESE, src, -1, dest, src.Length);

            LCMapString(0x0804, LCMAP_TRADITIONAL_CHINESE, src, -1, dest, src.Length);
            return gb2312.GetString(dest);
        }

        public void Init()
        {
            
        }
    }
}
