using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    interface IWordLibraryImport
    {
        int CountWord { get; set; }
        int CurrentStatus { get; set; }
        WordLibraryList Import(string str);
    }
    interface IWordLibraryExport
    {
        string Export(WordLibraryList wlList);
        Encoding Encoding { get; }
       
    }
}
