using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter.Language
{
    public interface IChineseConverter
    {
        string ToChs(string cht);
        string ToCht(string chs);
        //void Init();
    }
}
