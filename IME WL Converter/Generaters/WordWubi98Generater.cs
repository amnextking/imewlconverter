using System.Collections.Generic;
using Studyzy.IMEWLConverter.Helpers;

namespace Studyzy.IMEWLConverter.Generaters
{
    internal class WordWubi98Generater : IWordCodeGenerater
    {
        #region IWordCodeGenerater Members

        public string GetCodeOfChar(char str)
        {
            return WubiHelper.GetWubi98Code(str);
        }

        public IList<string> GetCodeOfString(string str)
        {
            return new List<string> {WubiHelper.GetStringWubi98Code(str)};
        }

        #endregion
    }
}