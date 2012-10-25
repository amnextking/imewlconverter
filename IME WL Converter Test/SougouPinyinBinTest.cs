using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Studyzy.IMEWLConverter.IME;

namespace Studyzy.IMEWLConverter.Test
{
    class SougouPinyinTest:BaseTest
    {
        [SetUp]
        public override void InitData()
        {
            importer = new SougouPinyin();
            exporter = new SougouPinyin();
        }
        protected override string StringData
        {
            get { throw new NotImplementedException(); }
        }
    }
}
