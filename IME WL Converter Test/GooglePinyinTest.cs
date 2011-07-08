using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Studyzy.IMEWLConverter.Test
{
    [TestFixture]
    class GooglePinyinTest:BaseTest
    {
        [SetUp]
        public override void InitData()
        {
            exporter = new GooglePinyin();
            importer = new GooglePinyin();

        }
        protected override string StringData
        {
            get { return Resource4Test.GooglePinyin; }
        }
        [Test]
        public void TestExportLine()
        {
            string txt = exporter.ExportLine(WlData);
            Assert.AreEqual(txt, "深蓝测试\t10\tshen lan ce shi");
        }
        [Test]
        public void TestExport()
        {
            string txt= exporter.Export(WlListData);
            Assert.IsTrue(txt.Split(new string[]{"\r\n"},StringSplitOptions.RemoveEmptyEntries).Length==2);
        }
        [Test]
        public void TestImport()
        {
            var list = ((IWordLibraryTextImport)importer).ImportText(StringData);
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count,10);
        }
    }
}
