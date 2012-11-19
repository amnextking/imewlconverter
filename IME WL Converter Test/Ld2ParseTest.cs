using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
using Studyzy.IMEWLConverter.IME;

namespace Studyzy.IMEWLConverter.Test
{
    class Ld2ParseTest
    {
        [Test]
       public void TestParseLd2()
       {
           var ld2File = AppDomain.CurrentDomain.BaseDirectory + "\\i.ld2";
           IWordLibraryImport import=new LingoesLd2();
           var reult= import.Import(ld2File);
            
           Assert.IsNotNull(reult);
            foreach (WordLibrary wordLibrary in reult)
            {
                Debug.WriteLine(wordLibrary);
            }
       }
    }
}
