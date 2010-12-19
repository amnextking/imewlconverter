using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    public interface IWordLibraryImport
    {
        int CountWord { get; set; }
        int CurrentStatus { get; set; }
        WordLibraryList Import(string str);
        WordLibraryList ImportLine(string str);
    }
    public interface IWordLibraryExport
    {
        string Export(WordLibraryList wlList);
        string ExportLine(WordLibrary wl);
        Encoding Encoding { get; }
       
    }
}
