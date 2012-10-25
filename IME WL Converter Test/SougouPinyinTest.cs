using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Studyzy.IMEWLConverter.IME;

namespace Studyzy.IMEWLConverter.Test
{
    [TestFixture]
    class SougouPinyinBinTest:BaseTest
    {
        [TestFixtureSetUp]
        public override void InitData()
        {
            importer = new SougouPinyinBin();
        }
        protected override string StringData
        {
            get { throw new NotImplementedException(); }
        }
        [TestCase("sougoubak.bin")]
        public void TestParseBinFile(string filePath)
        {
            var lib = importer.Import(filePath);
            Assert.Greater(lib.Count,0);
        }
    }
}
