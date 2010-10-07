using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
namespace Studyzy.IMEWLConverter
{
   public  class MutiPinYinWord
   {
       private static Dictionary<string,List<string>> mutiPinYinWord = null;
       private static void InitMutiPinYinWord()
       {
           if (mutiPinYinWord == null)
           {
               Dictionary<string, List<string>> wlList = new Dictionary<string, List<string>>();
               var lines = GetMutiPinyin().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
               for (int i = 0; i < lines.Length; i++)
               {
                   string line = lines[i];

                   string py = line.Split(' ')[0];
                   string word = line.Split(' ')[1];

                   List<string> pinyin = new List<string>(py.Split(new char[] { '\'' }, StringSplitOptions.RemoveEmptyEntries));
                   wlList.Add(word,pinyin);

               }
               mutiPinYinWord = wlList;
           }
       }
       private static string GetMutiPinyin()
       {
           string path = "pinyin.txt";
           StringBuilder sb = new StringBuilder();
           if (File.Exists(path))
           {
               using (StreamReader sr = new StreamReader(path, Encoding.Default))
               {
                   string txt = sr.ReadToEnd();
                   sr.Close();
                   Regex reg = new Regex(@"^('[a-z]+)+\s[\u4E00-\u9FA5]+$");
                   var lines = txt.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                   for (int i = 0; i < lines.Length; i++)
                   {
                       if (reg.IsMatch(lines[i]))
                       {
                           sb.Append(lines[i] + "\r\n");
                       }
                   }
               }
           }
           sb.Append(PinyinDic.WordPinyin);
           return sb.ToString();
       }
       /// <summary>
       /// 一个词中是否有多音字注音
       /// </summary>
       /// <param name="word"></param>
       /// <returns></returns>
       public static bool IsInWordPinYin(string word)
       {
           InitMutiPinYinWord();
           foreach (string key in mutiPinYinWord.Keys)
           {
               if(word.Contains(key))
               {
                   return true;
               }
           }
           return false;
       }
       /// <summary>
       /// 产生一个词中多音字的拼音,没有的就空着
       /// </summary>
       /// <param name="word"></param>
       /// <returns></returns>
       public static List<string> GenerateMutiWordPinYin(string word)
       {
           InitMutiPinYinWord();
           string[] pinyin = new string[word.Length];
           foreach (string key in mutiPinYinWord.Keys)
           {
               if (word.Contains(key))
               {
                   int index = word.IndexOf(key);
                   for (int i = 0; i < mutiPinYinWord[key].Count; i++)
                   {
                       pinyin[index + i] = mutiPinYinWord[key][i];
                   }
               }
           }
           return new List<string>(pinyin);
       }
    }
    
}
