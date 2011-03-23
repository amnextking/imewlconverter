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

    public interface IWordLibraryTextImport : IWordLibraryImport
    {
        Encoding Encoding { get; }
    }

    public interface IWordLibraryExport
    {
        Encoding Encoding { get; }
        string Export(WordLibraryList wlList);
        string ExportLine(WordLibrary wl);
    }
}