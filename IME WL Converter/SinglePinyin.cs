using System;
using System.Collections.Generic;

namespace Studyzy.IMEWLConverter
{
    public class SinglePinyin : PinYinFactory
    {
        private readonly Dictionary<char, string> dic;

        public SinglePinyin()
        {
            dic = new Dictionary<char, string>();
            //string singlePinYin = FileOperationHelper.ReadFile("SinglePinYin.txt");

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
        /// 获得单个汉字的所有拼音
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public override List<string> GetPinYinOfChar(char str)
        {
            return new List<string> {dic[str]};
        }

        /// <summary>
        /// 获得一个词的所有拼音组合
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public override List<List<string>> GetPinYinListOfString(string str)
        {
            var pyList = new List<List<string>>();
            List<string> pys = MutiPinYinWord.GenerateMutiWordPinYin(str);

            for (int i = 0; i < str.Length; i++)
            {
                if (string.IsNullOrEmpty(pys[i]))
                {
                    try
                    {
                        pys[i] = dic[str[i]];
                    }
                    catch //给定的字没有对应的拼音
                    {
                        pys[i] = "";
                    }
                }
            }
            pyList.Add(pys);
            return pyList;
        }
    }
}