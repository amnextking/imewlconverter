using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Studyzy.IMEWLConverter.Test
{

    [TestFixture]
    internal class QQPinyinQpydTest : BaseTest
    {
        protected override string StringData
        {
            get { throw new NotImplementedException(); }
        }

        [SetUp]
        public override void InitData()
        {
            importer = new Studyzy.IMEWLConverter.IME.QQPinyinQpyd();
        }
        public  void TestParseQypd()
        {
            
        }
    }
}
