using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Studyzy.IMEWLConverter.Test
{
    [TestFixture]
    class QQPinyinTest : BaseTest
    {
        [SetUp]
        public override void InitData()
        {
            exporter = new QQPinyin();
            importer = new QQPinyin();
        }
        protected override string StringData
        {
            get { throw new NotImplementedException(); }
        }
    }
}