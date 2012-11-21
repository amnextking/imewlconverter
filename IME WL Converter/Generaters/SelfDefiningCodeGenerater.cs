using System.Collections.Generic;
using Studyzy.IMEWLConverter.Helpers;

namespace Studyzy.IMEWLConverter.Generaters
{
    internal class SelfDefiningCodeGenerater : IWordCodeGenerater
    {
        #region IWordCodeGenerater Members

        public string GetCodeOfChar(char str)
        {
            string s = UserCodingHelper.GetCharCoding(str);
            return s;
        }

        public List<string> GetCodeOfString(string str)
        {
            var list = new List<string>();
            foreach (char c in str)
            {
                list.Add(GetCodeOfChar(c));
            }
            return list;
        }

        #endregion
    }
}