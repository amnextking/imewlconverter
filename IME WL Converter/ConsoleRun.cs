using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    class ConsoleRun
    {
        private enum CommandType
        {
            Import,
            Export,
            Help,
            Null,
            Other
        }
        
        IWordLibraryImport wordLibraryImport = null;
        IWordLibraryExport wordLibraryExport = null;
        public ConsoleRun(string[] args)
        {
            this.Args = args;

        }
        public string[] Args { get; set; }
        CommandType type = CommandType.Null;
        List<string> importPaths = new List<string>();
        private string exportPath = "";
        public void Run()
        {
            for (var i = 0; i < Args.Length; i++)
            {
                string arg = Args[i];
                type= RunCommand(arg);
            }
            if (importPaths.Count > 0 && exportPath != "")
            {
                WordLibraryList wordLibraryList=new WordLibraryList();
                Console.WriteLine("转换开始...");
                foreach (var importPath in importPaths)
                {
                    wordLibraryList.AddWordLibraryList(wordLibraryImport.Import(importPath));
                }
                string str = wordLibraryExport.Export(wordLibraryList);
                FileOperationHelper.WriteFile(exportPath, wordLibraryExport.Encoding, str);
                Console.WriteLine("转换完成,共转换"+wordLibraryList.Count+"个");
                return;
            }
            else
            {
                Console.WriteLine("输入 -? 可获取帮助");
            }
        }
        private CommandType RunCommand(string command)
        {
         
            if (command == "-help" || command == "-?")
            {
                Help();
                return CommandType.Help;
            }
            if (command.StartsWith("-i:"))
            {
                wordLibraryImport = GetImportInterface(command.Substring(3));
                return CommandType.Import;
            }
            if (command.StartsWith("-o:"))
            {
                wordLibraryExport = GetExportInterface(command.Substring(3));
                return CommandType.Export;
            }
            if (type == CommandType.Import)
            {
                importPaths.Add(command);
            }
            if (type == CommandType.Export)
            {
                exportPath = command;
            }
            return CommandType.Other;

        }

        private IWordLibraryExport GetExportInterface(string str)
        {
            switch (str)
            {
                case ConstantString.BAIDU_SHOUJI_C:
                    return new BaiduShouji();
                case ConstantString.QQ_SHOUJI_C:
                    return new QQShouji();
                case ConstantString.SOUGOU_PINYIN_C:
                    return new SougouPinyin();
                case ConstantString.SOUGOU_WUBI_C:
                    return new SougouWubi();
                case ConstantString.QQ_PINYIN_C:
                    return new QQPinyin();
                case ConstantString.GOOGLE_PINYIN_C:
                    return new GooglePinyin();
                case ConstantString.WORD_ONLY_C:
                    return new NoPinyinWordOnly();
                case ConstantString.ZIGUANG_PINYIN_C:
                    return new ZiGuangPinyin();
                case ConstantString.PINYIN_JIAJIA_C:
                    return new PinyinJiaJia();
                case ConstantString.SINA_PINYIN_C:
                    return new SinaPinyin();
                case ConstantString.TOUCH_PAL_C:
                    return new TouchPal();
                default:
                    throw new ArgumentException("导出词库的输入法错误");
            }
        }

        private IWordLibraryImport GetImportInterface(string str)
        {
            switch (str)
            {
                case ConstantString.BAIDU_SHOUJI_C:
                    return new BaiduShouji();
                case ConstantString.BAIDU_BDICT_C:
                    return new BaiduPinyinBdict();
                case ConstantString.QQ_SHOUJI_C:
                    return new QQShouji();
                case ConstantString.SOUGOU_PINYIN_C:
                    return new SougouPinyin();
                case ConstantString.SOUGOU_WUBI_C:
                    return new SougouWubi();
                case ConstantString.QQ_PINYIN_C:
                    return new QQPinyin();
                case ConstantString.GOOGLE_PINYIN_C:
                    return new GooglePinyin();
                case ConstantString.ZIGUANG_PINYIN_C:
                    return new ZiGuangPinyin();
                case ConstantString.PINYIN_JIAJIA_C:
                    return new PinyinJiaJia();
                case ConstantString.WORD_ONLY_C:
                    return new NoPinyinWordOnly();
                case ConstantString.SINA_PINYIN_C:
                    return new SinaPinyin();
                case ConstantString.SOUGOU_XIBAO_SCEL_C:
                    return new SougouPinyinScel();
                case ConstantString.ZHENGMA_C:
                    return new Zhengma();
                case ConstantString.SELF_DEFINING_C:
                    return new SelfDefining();
                case ConstantString.TOUCH_PAL_C:
                    return new TouchPal();
                default:
                    throw new ArgumentException("导入词库的输入法错误");
            }
        }
        private void Help()
        {
            Console.WriteLine("-i:输入的词库类型 词库路径1 词库路径2 词库路径3 -o:输出的词库类型 输出词库路径");
            Console.WriteLine("输入和输出的词库类型如下：");
            Console.WriteLine(ConstantString.GOOGLE_PINYIN_C+ "\t"+ConstantString.GOOGLE_PINYIN);
            Console.WriteLine(ConstantString.BAIDU_SHOUJI_C + "\t" + ConstantString.BAIDU_SHOUJI);
            Console.WriteLine(ConstantString.BAIDU_BDICT_C + "\t" + ConstantString.BAIDU_BDICT);
            Console.WriteLine(ConstantString.SOUGOU_XIBAO_SCEL_C + "\t" + ConstantString.SOUGOU_XIBAO_SCEL);
            Console.WriteLine(ConstantString.PINYIN_JIAJIA_C + "\t" + ConstantString.PINYIN_JIAJIA);
            Console.WriteLine(ConstantString.ZIGUANG_PINYIN_C + "\t" + ConstantString.ZIGUANG_PINYIN);
            Console.WriteLine(ConstantString.SINA_PINYIN_C + "\t" + ConstantString.SINA_PINYIN);
            Console.WriteLine(ConstantString.TOUCH_PAL_C + "\t" + ConstantString.TOUCH_PAL);
            Console.WriteLine(ConstantString.ZHENGMA_C + "\t" + ConstantString.ZHENGMA);
            Console.WriteLine(ConstantString.QQ_PINYIN_C + "\t" + ConstantString.QQ_PINYIN);
            Console.WriteLine(ConstantString.QQ_SHOUJI_C + "\t" + ConstantString.QQ_SHOUJI);
            Console.WriteLine("");
            Console.WriteLine("例如要将C:\\test.scel的搜狗细胞词库转换为D:\\gg.txt的谷歌拼音词库，命令为：");
            Console.WriteLine("深蓝词库转换.exe -i:"+ConstantString.SOUGOU_XIBAO_SCEL_C+" C:\\test.scel -o:"+ConstantString.GOOGLE_PINYIN_C+" D:\\gg.txt");
        }
    }
}
