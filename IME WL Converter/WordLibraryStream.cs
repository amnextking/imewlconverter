using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Studyzy.IMEWLConverter
{
   public class WordLibraryStream
   {
       private IWordLibraryImport import;
       private IWordLibraryExport export;

       private string[] lines;
       private StreamWriter sw;


       public WordLibraryStream(IWordLibraryImport import, IWordLibraryExport export, string txt, StreamWriter sw)
       {
           this.import = import;
           this.export = export;
           this.sw = sw;
           lines = txt.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
           import.CountWord = lines.Length;
       }
       public int Count
       {
           get { return lines.Length; }
       }
    
       public void ConvertWordLibrary(Predicate<WordLibrary> match)
       {
           for(int i=0;i<lines.Length;i++)
           {
               try
               {
                   string line = lines[i];
                   var wll = import.ImportLine(line);
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
