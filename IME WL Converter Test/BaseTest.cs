using System;
using System.Collections.Generic;
using System.Text;
using Studyzy.IMEWLConverter;
namespace Studyzy.IMEWLConverter.Test
{
    public abstract class BaseTest
    {
        protected IWordLibraryExport exporter;
        protected IWordLibraryImport importer;
        protected abstract string StringData { get; }
        /// <summary>
        /// 深蓝测试
        /// 词库转换
        /// </summary>
        protected WordLibraryList WlListData
        {
            get
            {
                WordLibrary wordLibrary=new WordLibrary(){ Count = 80,PinYin = new string[]{"ci","ku","zhuan","huan"},Word = "词库转换"};
                return new WordLibraryList() {WlData, wordLibrary};
            }
        }
        /// <summary>
        /// 深蓝测试
        /// </summary>
        protected WordLibrary WlData=new WordLibrary(){ Count = 10,PinYin = new string[]{"shen","lan","ce","shi"},Word = "深蓝测试"};
        public abstract void InitData();
    }
}
