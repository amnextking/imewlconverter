using System;
using System.Collections.Generic;
using System.Text;
using Studyzy.IMEWLConverter;
using NUnit.Framework;
namespace Studyzy.IMEWLConverter.Test
{
    [TestFixture]
    public class BaiduShoujiTest : BaseTest
    {
        [SetUp]
        public override void InitData()
        {
            exporter = new BaiduShouji();
            importer = new BaiduShouji();
        }
        protected override string StringData
        {
            get { throw new NotImplementedException(); }
        }

        [Test]
        public void TestExport()
        {

        }
    }
}
