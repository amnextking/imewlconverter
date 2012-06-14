using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    class SelfDefiningCode:PinYinFactory
    {
        public override List<string> GetPinYinOfChar(char str)
        {
            var s = UserCodingHelper.GetCharCoding(str);
            return new List<string>(){s};
        }

        public override List<List<string>> GetPinYinListOfString(string str)
        {
            throw new NotImplementedException();
        }
    }
}
