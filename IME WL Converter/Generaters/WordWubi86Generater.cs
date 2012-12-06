using System.Collections.Generic;
using Studyzy.IMEWLConverter.Helpers;

namespace Studyzy.IMEWLConverter.Generaters
{
    internal class WordWubi86Generater : IWordCodeGenerater
    {
        #region IWordCodeGenerater Members

        public string GetCodeOfChar(char str)
        {
            return WubiHelper.GetWubi86Code(str);
        }

        public IList<string> GetCodeOfString(string str)
        {
            return new List<string> {WubiHelper.GetStringWubi86Code(str)};
        }

        #endregion
    }
}