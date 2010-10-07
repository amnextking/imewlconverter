using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    interface IWordLibraryImport
    {
        //bool OnlySinglePinyin { get; set; }
        WordLibraryList Import(string str);
    }
    interface IWordLibraryExport
    {
        string Export(WordLibraryList wlList);
        Encoding Encoding { get; }
       
    }
}
