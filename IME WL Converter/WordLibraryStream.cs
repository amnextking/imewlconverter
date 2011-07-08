﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    public class WordLibraryStream
    {
        private readonly IWordLibraryExport export;
        private readonly IWordLibraryImport import;

        private readonly string path;
        private readonly StreamWriter sw;
        private Encoding encoding;

        public WordLibraryStream(IWordLibraryImport import, IWordLibraryExport export, string path, Encoding encoding, StreamWriter sw)
        {
            this.import = import;
            this.export = export;
            this.sw = sw;
            this.path = path;
            this.encoding = encoding;
        }

        public int Count
        {
            get { return 0; }
        }

        public void ConvertWordLibrary(Predicate<WordLibrary> match)
        {
            int i = 0;
            using (StreamReader sr = new StreamReader(path, encoding))
            {

                try
                {
                    while (sr.Peek() != -1)
                    {
                        string line = sr.ReadLine();
                        WordLibraryList wll = import.ImportLine(line);
                        import.CurrentStatus = i++;
                        foreach (WordLibrary wl in wll)
                        {
                            if (wl != null && match(wl))
                            {
                                sw.WriteLine(export.ExportLine(wl));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
#if DEBUG
                    throw ex;
#endif
                }
            }
        }
    }
}