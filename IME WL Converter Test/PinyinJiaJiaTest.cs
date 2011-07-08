﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Studyzy.IMEWLConverter.Test
{
    [TestFixture]
    class PinyinJiaJiaTest:BaseTest
    {
        [SetUp]
        public override void InitData()
        {
            importer = new PinyinJiaJia();
            exporter = new PinyinJiaJia();
        }
        protected override string StringData
        {
            get { return Resource4Test.PinyinJiajia; }
        }
        [Test]
        public void ImportNoPinyin()
        {
            var wl = importer.ImportLine("深蓝测试");
            Assert.AreEqual(wl.Count,1);
            Assert.AreEqual(wl[0].PinYinString,"shen'lan'ce'shi");
        }
        [Test]
        public void ImportWithPinyinFull()
        {
            var wl = importer.ImportLine("深shen蓝lan居ju");
            Assert.AreEqual(wl.Count, 1);
            Assert.AreEqual(wl[0].PinYinString, "shen'lan'ju");
            Assert.AreEqual(wl[0].Word, "深蓝居");
        }
        [Test]
        public void ImportWithPinyinPart()
        {
            var wl = ((IWordLibraryTextImport)importer).ImportText(StringData);
            Assert.AreEqual(wl.Count, 10);
            Assert.AreEqual(wl[0].PinYinString, "ren'min'hen'xing");
            Assert.AreEqual(wl[0].Word, "人民很行");
            Assert.AreEqual(wl[1].PinYinString, "ren'min'yin'hang");
            Assert.AreEqual(wl[1].Word, "人民银行");
            Assert.AreEqual(wl[2].PinYinString, "dong'li'wu'xian");
            Assert.AreEqual(wl[2].Word, "栋力无限");
        }
        [Test]
        public void ExportLine()
        {
            var txt = exporter.ExportLine(WlData);
            Assert.AreEqual(txt,"深shen蓝lan测ce试shi");
        }
    }
}
