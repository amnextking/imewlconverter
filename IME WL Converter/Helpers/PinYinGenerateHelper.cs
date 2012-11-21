using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Studyzy.IMEWLConverter.Helpers
{
    /// <summary>
    /// 这个类用于给含有多音字的词注音
    /// </summary>
    public class PinYinGenerateHelper
    {
        private static Dictionary<string, List<string>> mutiPinYinWord;

        private static void InitMutiPinYinWord()
        {
            if (mutiPinYinWord == null)
            {
                var wlList = new Dictionary<string, List<string>>();
                string[] lines = GetMutiPinyin().Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    string py = line.Split(' ')[0];
                    string word = line.Split(' ')[1];

                    var pinyin = new List<string>(py.Split(new[] {'\''}, StringSplitOptions.RemoveEmptyEntries));
                    wlList.Add(word, pinyin);
                }
                mutiPinYinWord = wlList;
            }
        }

        private static string GetMutiPinyin()
        {
            string path = ConstantString.PinyinLibPath;
            var sb = new StringBuilder();
            if (File.Exists(path))
            {
                string txt = FileOperationHelper.ReadFile(path);

                var reg = new Regex(@"^('[a-z]+)+\s[\u4E00-\u9FA5]+$");
                string[] lines = txt.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (reg.IsMatch(lines[i]))
                    {
                        sb.Append(lines[i] + "\r\n");
                    }
                }
            }
            sb.Append(Dictionaries.WordPinyin);
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
                if (word.Contains(key))
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
            var pinyin = new string[word.Length];
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