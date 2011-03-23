using System;
using System.IO;

namespace Studyzy.IMEWLConverter
{
    public class WordLibraryStream
    {
        private readonly IWordLibraryExport export;
        private readonly IWordLibraryImport import;

        private readonly string[] lines;
        private readonly StreamWriter sw;


        public WordLibraryStream(IWordLibraryImport import, IWordLibraryExport export, string txt, StreamWriter sw)
        {
            this.import = import;
            this.export = export;
            this.sw = sw;
            lines = txt.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            import.CountWord = lines.Length;
        }

        public int Count
        {
            get { return lines.Length; }
        }

        public void ConvertWordLibrary(Predicate<WordLibrary> match)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    string line = lines[i];
                    WordLibraryList wll = import.ImportLine(line);
                    import.CurrentStatus = i;
                    foreach (WordLibrary wl in wll)
                    {
                        if (wl != null && match(wl))
                        {
                            sw.WriteLine(export.ExportLine(wl));
                        }
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    throw ex;
#endif
                }
            }
        }
    }
}