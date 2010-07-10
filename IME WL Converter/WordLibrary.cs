using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    public class WordLibrary
    {
        private string word;

        /// <summary>
        /// 汉字
        /// </summary>
        public string Word
        {
            get { return word; }
            set { word = value; }
        }
        private int count=1;
        /// <summary>
        /// 权值，打字出现次数
        /// </summary>
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private List<string> pinYin;
        /// <summary>
        /// 词中每个字的拼音
        /// </summary>
        public List<string> PinYin
        {
            get { return pinYin; }
            set { pinYin = value; }
        }
        /// <summary>
        /// 词的拼音字符串，单独的一个属性，与PinYin属性无关联
        /// </summary>
        public string PinYinString
        {
            get;
            set;
        }

        /// <summary>
        /// 获得拼音字符串
        /// </summary>
        /// <param name="split">每个拼音之间的分隔符</param>
        /// <param name="buildType">组装拼音字符串的方式</param>
        /// <returns></returns>
        public string GetPinYinString(string split,BuildType buildType)
        {
            StringBuilder sb=new StringBuilder();
            pinYin.ForEach(delegate(string s)
            {
                sb.Append(s + split);
            });
            if (buildType == BuildType.RightContain)
            {
                return sb.ToString();
            }
            string str = sb.ToString().Remove(sb.Length - 1);
            if (buildType == BuildType.None)
            {
                return str;
            }
            else
            {
                return split + str;
            }
        }
       
    }
    public enum BuildType
    {
        /// <summary>
        /// 字符串左边包含分隔符
        /// </summary>
        LeftContain,
        /// <summary>
        /// 字符串右边包含分隔符
        /// </summary>
        RightContain,
        /// <summary>
        /// 字符串两侧都不包含分隔符
        /// </summary>
        None
    }
}
