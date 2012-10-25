using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Studyzy.IMEWLConverter.IME;

namespace Studyzy.IMEWLConverter.Test
{
    [TestFixture]
    class RimeTest : BaseTest
    {
        [TestFixtureSetUp]
        public override void InitData()
        {
            exporter = new Rime();
            importer = new Rime();
        }
        protected override string StringData
        {
            get { throw new NotImplementedException(); }
        }
        [TestCase("luna_pinyin_export.txt")]
        public void TestImport(string path)
        {
            var wl = importer.Import(path);
            Assert.Greater(wl.Count,0);
        }
    }
}