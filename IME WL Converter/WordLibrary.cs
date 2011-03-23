using System.Text;

namespace Studyzy.IMEWLConverter
{
    /// <summary>
    /// 词条类
    /// </summary>
    public class WordLibrary
    {
        private int count = 1;
        private string[] pinYin;
        private string pinYinString = "";
        private string word;

        /// <summary>
        /// 词语
        /// </summary>
        public string Word
        {
            get { return word; }
            set { word = value; }
        }

        /// <summary>
        /// 词频，打字出现次数
        /// </summary>
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        /// <summary>
        /// 词中每个字的拼音
        /// </summary>
        public string[] PinYin
        {
            get { return pinYin; }
            set { pinYin = value; }
        }

        /// <summary>
        /// 词的拼音字符串，可以单独设置的一个属性，如果没有设置该属性，而获取该属性，则返回PinYin属性和“'”组合的字符串
        /// </summary>
        public string PinYinString
        {
            get
            {
                if (pinYinString == "")
                {
                    pinYinString = string.Join("'", pinYin);
                }
                return pinYinString;
            }
            set { pinYinString = value; }
        }

        /// <summary>
        /// 获得拼音字符串
        /// </summary>
        /// <param name="split">每个拼音之间的分隔符</param>
        /// <param name="buildType">组装拼音字符串的方式</param>
        /// <returns></returns>
        public string GetPinYinString(string split, BuildType buildType)
        {
            var sb = new StringBuilder();
            foreach (string s in pinYin)
            {
                sb.Append(s + split);
            }
            if (buildType == BuildType.RightContain)
            {
                return sb.ToString();
            }
            if (buildType == BuildType.FullContain)
            {
                return split + sb;
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

        public string ToDisplayString()
        {
            return "汉字：" + word + "；拼音：" + PinYinString + "；词频：" + count;
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
        None,
        /// <summary>
        /// 字符串两侧都有分隔符
        /// </summary>
        FullContain
    }
}