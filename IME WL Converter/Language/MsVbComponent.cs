using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;
namespace Studyzy.IMEWLConverter.Language
{
    class MsVbComponent : IChineseConverter
    {
        public string ToChs(string cht)
        {
            return Microsoft.VisualBasic.Strings.StrConv(cht, VbStrConv.SimplifiedChinese, 0);
        }

        public string ToCht(string chs)
        {
            return Microsoft.VisualBasic.Strings.StrConv(chs, VbStrConv.TraditionalChinese, 0);
        }

        public void Init()
        {
            
        }
    }
}
