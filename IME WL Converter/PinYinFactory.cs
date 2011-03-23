using System.Collections.Generic;

namespace Studyzy.IMEWLConverter
{
    public abstract class PinYinFactory
    {
        /// <summary>
        /// 获得单个汉字的所有拼音
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public abstract List<string> GetPinYinOfChar(char str);

        /// <summary>
        /// 获得一个词的所有拼音组合
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public abstract List<List<string>> GetPinYinListOfString(string str);
    }
}