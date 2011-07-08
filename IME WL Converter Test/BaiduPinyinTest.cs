using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Studyzy.IMEWLConverter.Test
{
     [TestFixture]
    class BaiduPinyinTest : BaseTest
    {
        protected override string StringData
        {
            get { throw new NotImplementedException(); }
        }
          [SetUp]
        public override void InitData()
        {
            importer = new BaiduPinyinBdict();
        }
         [TestCase("movie.bdict")]
         public void TestImport(string file)
         {
             var wlList = importer.Import(file);
             Assert.IsNotNull(wlList);
             Assert.Greater(wlList.Count,0);
         }
    }
}
