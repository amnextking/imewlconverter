using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Studyzy.IMEWLConverter.IME;

namespace Studyzy.IMEWLConverter.Test
{
    [TestFixture]
    public class NoPinyinWordOnlyTest:BaseTest
    {
        [SetUp]
        public override void InitData()
        {
            importer = new NoPinyinWordOnly();
            exporter = new NoPinyinWordOnly();
        }
        protected override string StringData
        {
            get { return Resource4Test.NoPinyinWordOnly; }
        }
        [Test]
        public void TestExportLine()
        {
            string txt = exporter.ExportLine(WlData);
            Assert.AreEqual(txt,"深蓝测试");
        }
        [Test]
        public void TestExport()
        {
            string txt = exporter.Export(WlListData);
            Assert.AreEqual(txt, "深蓝测试\r\n词库转换\r\n");
        }
        [Test]
        public void TestImport()
        {
            var wl = ((IWordLibraryTextImport)importer).ImportText(StringData);
            Assert.AreEqual(wl.Count, 10);
        }
    }
}
