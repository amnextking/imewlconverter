using System;
using System.Collections.Generic;

namespace Studyzy.IMEWLConverter
{
    public static class PinyinHelper
    {
        private static readonly Dictionary<char, List<string>> dictionary = new Dictionary<char, List<string>>();
        private static readonly Dictionary<char, List<string>> dictionaryWithTone = new Dictionary<char, List<string>>();
        /// <summary>
        /// 字的拼音
        /// </summary>
        private static Dictionary<char, List<string>> PinYinDict
        {
            get
            {
                if (dictionary.Count == 0)
                {
                    //string allPinYin = FileOperationHelper.ReadFile("AllPinYin.txt");
                    string allPinYin = PinyinDic.AllPinYin;
                    string[] pyList = allPinYin.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < pyList.Length; i++)
                    {
                        string[] hzpy = pyList[i].Split(',');
                        char hz = Convert.ToChar(hzpy[0]);
                        string py = hzpy[1];
                        py = py.Remove(py.Length - 1);//去掉了声调，因为大多数输入法不支持声调

                        if (dictionary.ContainsKey(hz))
                        {
                            dictionary[hz].Add(py);
                        }
                        else
                        {
                            dictionary.Add(hz, new List<string> {py});
                        }
                    }
                }
                return dictionary;
            }
        }

        private static Dictionary<char, List<string>> PinYinWithToneDict
        {
            get
            {
                if (dictionaryWithTone.Count == 0)
                {
                    //string allPinYin = FileOperationHelper.ReadFile("AllPinYin.txt");
                    string allPinYin = PinyinDic.AllPinYin;
                    string[] pyList = allPinYin.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < pyList.Length; i++)
                    {
                        string[] hzpy = pyList[i].Split(',');
                        char hz = Convert.ToChar(hzpy[0]);
                        string py = hzpy[1];
                        if (dictionaryWithTone.ContainsKey(hz))
                        {
                            dictionaryWithTone[hz].Add(py);
                        }
                        else
                        {
                            dictionaryWithTone.Add(hz, new List<string> { py });
                        }
                    }
                }
                return dictionaryWithTone;
            }
        }

        #region 默认拼音

        private static readonly Dictionary<char, string> dic = new Dictionary<char, string>();

        private static void InitSinglePinyin()
        {
            string singlePinYin = PinyinDic.SinglePinYin;
            string[] pyList = singlePinYin.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < pyList.Length; i++)
            {
                string[] hzpy = pyList[i].Split(',');
                char hz = Convert.ToChar(hzpy[0]);
                string py = hzpy[1];
                dic.Add(hz, py);
            }
        }

        /// <summary>
        /// 获得一个字的默认拼音
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string GetDefaultPinyin(char c)
        {
            if (dic.Count == 0)
            {
                InitSinglePinyin();
            }
            return dic[c];
        }

        #endregion

        /// <summary>
        /// 获得单个字的拼音,不包括声调
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> GetPinYinOfChar(char str)
        {
            return PinYinDict[str];
        }

        public static List<string> GetPinYinWithToneOfChar(char str)
        {
            return PinYinWithToneDict[str];
        }
        public static string GetPinyinWithToneOfChar(char str,string py)
        {
            var list = PinYinWithToneDict[str];
            foreach (var pinyin in list)
            {
                if (pinyin == py + "0" || pinyin == py + "1" || pinyin == py + "2" || pinyin == py + "3" || pinyin == py + "4")
                {
                    return pinyin;
                }
            }
            return null;
        }

        /// <summary>
        /// 判断给出的词和拼音是否有效
        /// </summary>
        /// <param name="word"></param>
        /// <param name="pinyin"></param>
        /// <returns></returns>
        public static bool ValidatePinyin(string word, List<string> pinyin)
        {
            List<string> pinyinList = pinyin;
            if (word.Length != pinyinList.Count)
            {
                return false;
            }
            for (int i = 0; i < word.Length; i++)
            {
                List<string> charPinyinList = GetPinYinOfChar(word[i]);
                if (!charPinyinList.Contains(pinyinList[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获得一个词中的每个字的音
        /// </summary>
        /// <param name="str">一个词</param>
        /// <returns></returns>
        public static List<List<string>> GetPinYinOfStringEveryChar(string str)
        {
            var pyList = new List<List<string>>();
            for (int i = 0; i < str.Length; i++)
            {
                pyList.Add(GetPinYinOfChar(str[i]));
            }
            return pyList;
        }
    }
}