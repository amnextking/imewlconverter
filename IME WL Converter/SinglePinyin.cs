using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    public class SinglePinyin:PinYinFactory
    {
       private Dictionary<char, string> dic;
       public SinglePinyin()
       {
           dic = new Dictionary<char, string>();
           string[] pyList = PinyinDic.SinglePinYin.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

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

           return new List<string>() { dic[str] };

       }
        /// <summary>
       /// 获得一个词的所有拼音组合
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public override List<List<string>> GetPinYinListOfString(string str)
        {
            List<List<string>> pyList = new List<List<string>>();
            List<string> pys = new List<string>();
            for (int i = 0; i < str.Length; i++)
            {
                pys.Add(dic[str[i]]);
            }
            pyList.Add(pys);
            return pyList;
        }
    }
}
