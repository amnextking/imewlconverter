using System.Text;

namespace Studyzy.IMEWLConverter
{
    public interface IWordLibraryImport
    {
        int CountWord { get; set; }
        int CurrentStatus { get; set; }
        WordLibraryList Import(string path);
        WordLibraryList ImportLine(string str);
        bool IsText { get; }
    }

    public interface IWordLibraryTextImport : IWordLibraryImport
    {
        Encoding Encoding { get; }
        WordLibraryList ImportText(string text);
    }

    public interface IWordLibraryExport
    {
        Encoding Encoding { get; }
        string Export(WordLibraryList wlList);
        string ExportLine(WordLibrary wl);
    }
}