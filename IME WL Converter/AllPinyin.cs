using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Collections;

namespace Studyzy.IMEWLConverter
{
    public class AllPinyin : PinYinFactory
    {
        public AllPinyin()
        {
            dictionary = new Dictionary<char, List<string>>();
        }

       /// <summary>
       /// 获得单个字的拼音
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public override List<string> GetPinYinOfChar(char str)
       {                   
           return PinYinDict[str];
       }
       /// <summary>
       /// 获得一个词的拼音
       /// </summary>
       /// <param name="str"></param>
       /// <param name="split">拼音间的分隔符</param>
       /// <returns></returns>       
       public List<string> GetPinYinOfString(string str, string split)
       {
           List<string> pinyinList = new List<string>();
           List<string> pinyin = MutiPinYinWord.GenerateMutiWordPinYin(str);
           for (int i = 0; i < str.Length; i++)
           {
               List<string> py=new List<string>();
               if (string.IsNullOrEmpty(pinyin[i]))
               {
                   py = GetPinYinOfChar(str[i]);
               }
               else
               {
                   py.Add(pinyin[i]);
               }
               if (pinyinList.Count == 0)
               {
                   py.ForEach(delegate(string p)
                   {
                       pinyinList.Add(p + split);
                   });

               }
               else//不是第一个字
               {
                   List<string> newString = new List<string>();
                   foreach (string p in py)
                   {
                       string charPy = p + split;

                       
                       pinyinList.ForEach(delegate(string s)
                       {
                           newString.Add(s + charPy);
                       });                      
                   }
                   pinyinList = newString;
               }

           }
           return pinyinList;
       }

       /// <summary>
       /// 获得一个词的所有拼音
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public override  List<List<string>> GetPinYinListOfString(string str)
       {
           var pyList= GetPinYinOfString(str, "'");
           List<List<string>> pyArray = new List<List<string>>();
           pyList.ForEach(delegate(string pyString)
           {
               List<string> py = new List<string>(pyString.Split('\''));
               pyArray.Add(py);
           });
           return pyArray;
       }

       /// <summary>
       /// 获得一个词中的每个字的音
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public  List<List<string>> GetPinYinOfStringEveryChar(string str)
       {
           List<List<string>> pyList = new List<List<string>>();
           for (int i = 0; i < str.Length; i++)
           {
               pyList.Add(GetPinYinOfChar(str[i]));
           }
           return pyList;
       }

       private Dictionary<char, List<string>> dictionary;
       private Dictionary<char, List<string>> PinYinDict
       {
           get
           {
               if (dictionary.Count == 0)
               {
                   //string allPinYin = FileOperationHelper.ReadFile("AllPinYin.txt");
                   string allPinYin = PinyinDic.AllPinYin;
                   string[] pyList = allPinYin.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                   for (int i = 0; i < pyList.Length; i++)
                   {
                       string[] hzpy = pyList[i].Split(',');
                       char hz =Convert.ToChar( hzpy[0]);
                       string py = hzpy[1];
                       if (dictionary.ContainsKey(hz))
                       {
                           dictionary[hz].Add(py);
                       }
                       else
                       {
                           dictionary.Add(hz,new List<string>(){ py});
                       }
                   }
               }
               return dictionary;
           }
       }



    }
}
