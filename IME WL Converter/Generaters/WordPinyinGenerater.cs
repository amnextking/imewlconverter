using System.Collections.Generic;
using Studyzy.IMEWLConverter.Helpers;

namespace Studyzy.IMEWLConverter.Generaters
{
    internal class WordPinyinGenerater : IWordCodeGenerater
    {
        #region IWordCodeGenerater Members

        public string GetCodeOfChar(char str)
        {
            return PinyinHelper.GetDefaultPinyin(str);
        }

        /// <summary>
        /// 获得一个词的拼音
        /// 如果这个词不包含多音字，那么直接使用其拼音
        /// 如果包含多音字，则找对应的注音词，根据注音词进行注音
        /// 没有找到注音词的，使用默认拼音
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<string> GetCodeOfString(string str)
        {
            return PinYinGenerateHelper.GenerateMutiWordPinYin(str);
        }

        #endregion
    }
}