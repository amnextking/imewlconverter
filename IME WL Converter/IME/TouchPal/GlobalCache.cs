using System;
using System.Collections.Generic;



namespace Studyzy.IMEWLConverter
{

    class GlobalCache
    {
        public static Dictionary<int, TouchPalChar> CharList = new Dictionary<int, TouchPalChar>();
        public static Dictionary<int, TouchPalWord> WordList = new Dictionary<int, TouchPalWord>();
        public static Stack<TouchPalChar> Stackes = new Stack<TouchPalChar>();
        public static Stack<TouchPalChar> ExportStackes = new Stack<TouchPalChar>();
        private static Dictionary<int, string> pinyinMapping = new Dictionary<int, string>();
        public static TouchPalChar JumpChar;
        public static Dictionary<int, string> PinyinMapping
        {
            get
            {
                if (pinyinMapping.Count == 0)
                {
                    var lines = PinyinDic.TouchPalPinyinDic.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in lines)
                    {
                         var pycd=line.Split(',');
                        var id = Convert.ToInt32(pycd[0]);
                        var py = pycd[1];
                        pinyinMapping.Add(id, py);
                    }
                    
                }
                return pinyinMapping;
            }
        }

        private static Dictionary<string,int> pinyinIndexMapping = new Dictionary< string,int>();
        public static Dictionary< string,int> PinyinIndexMapping
        {
            get
            {
                if (pinyinIndexMapping.Count == 0)
                {
                    var lines = PinyinDic.TouchPalPinyinDic.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in lines)
                    {
                        var pycd = line.Split(',');
                        var id = Convert.ToInt32(pycd[0]);
                        var py = pycd[1];
                        pinyinIndexMapping.Add(py, id);
                    }

                }
                return pinyinIndexMapping;
            }
        }

    }
}
