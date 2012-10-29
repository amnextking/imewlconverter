﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Studyzy.IMEWLConverter.IME
{
    /// <summary>
    /// 搜狗二进制备份词库
    /// </summary>
    public class SougouPinyinBin : IWordLibraryImport
    {
        private List<string> PinYinDic = new List<string>() {"a","ai","an","ang","ao","ba","bai","ban","bang","bao","bei","ben","beng","bi","bian","biao","bie","bin","bing","bo","bu","ca","cai","can","cang","cao","ce","cen","ceng","cha","chai","chan","chang","chao","che","chen","cheng","chi","chong","chou","chu","chua","chuai","chuan","chuang","chui","chun","chuo","ci","cong","cou","cu","cuan","cui","cun","cuo","da","dai","dan","dang","dao","de","dei","den","deng","di","dia","dian","diao","die","ding","diu","dong","dou","du","duan","dui","dun","duo","e","ei","en","eng","er","fa","fan","fang","fei","fen","feng","fiao","fo","fou","fu","ga","gai","gan","gang","gao","ge","gei","gen","geng","gong","gou","gu","gua","guai","guan","guang","gui","gun","guo","ha","hai","han","hang","hao","he","hei","hen","heng","hong","hou","hu","hua","huai","huan","huang","hui","hun","huo","ji","jia","jian","jiang","jiao","jie","jin","jing","jiong","jiu","ju","juan","jue","jun","ka","kai","kan","kang","kao","ke","kei","ken","keng","kong","kou","ku","kua","kuai","kuan","kuang","kui","kun","kuo","la","lai","lan","lang","lao","le","lei","leng","li","lia","lian","liang","liao","lie","lin","ling","liu","lo","long","lou","lu","luan","lue","lun","luo","lv","ma","mai","man","mang","mao","me","mei","men","meng","mi","mian","miao","mie","min","ming","miu","mo","mou","mu","na","nai","nan","nang","nao","ne","nei","nen","neng","ni","nian","niang","niao","nie","nin","ning","niu","nong","nou","nu","nun","nuan","nue","nuo","nv","o","ou","pa","pai","pan","pang","pao","pei","pen","peng","pi","pian","piao","pie","pin","ping","po","pou","pu","qi","qia","qian","qiang","qiao","qie","qin","qing","qiong","qiu","qu","quan","que","qun","ran","rang","rao","re","ren","reng","ri","rong","rou","ru","rua","ruan","rui","run","ruo","sa","sai","san","sang","sao","se","sen","seng","sha","shai","shan","shang","shao","she","shei","shen","sheng","shi","shou","shu","shua","shuai","shuan","shuang","shui","shun","shuo","si","song","sou","su","suan","sui","sun","suo","ta","tai","tan","tang","tao","te","tei","teng","ti","tian","tiao","tie","ting","tong","tou","tu","tuan","tui","tun","tuo","wa","wai","wan","wang","wei","wen","weng","wo","wu","xi","xia","xian","xiang","xiao","xie","xin","xing","xiong","xiu","xu","xuan","xue","xun","ya","yan","yang","yao","ye","yi","yin","ying","yo","yong","you","yu","yuan","yue","yun","za","zai","zan","zang","zao","ze","zei","zen","zeng","zha","zhai","zhan","zhang","zhao","zhe","zhei","zhen","zheng","zhi","zhong","zhou","zhu","zhua","zhuai","zhuan","zhuang","zhui","zhun","zhuo","zi","zong","zou","zu","zuan","zui","zun","zuo",
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
        
        //4字节使用同一个拼音的词条数x，2字节拼音长度n，n字节拼音的编号，（2字节汉字的长度y，y*2字节汉字的内容Unicode编码，2字节词频，2字节未知，4字节未知）*x
        public WordLibraryList Import(string path)
        {
            var pyAndWord = new WordLibraryList();
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            fs.Position = 0x18;
            CountWord = BinFileHelper.ReadInt32(fs);
            CurrentStatus = 0;
            fs.Position = 0x30;

            while (CurrentStatus < CountWord)
            {
                int samePyCount = BinFileHelper.ReadInt16(fs);
                int unkown1 = BinFileHelper.ReadInt16(fs);
                var pyLength = BinFileHelper.ReadInt16(fs);
                var pyArray = new string[pyLength/2];
                for (var i = 0; i < pyLength/2; i++)
                {
                    var idx =BinFileHelper.ReadInt16(fs);
                    try
                    {
                        pyArray[i] = PinYinDic[idx];
                    }
                    catch
                    {
                        pyArray[i] = "--";
                    }
                }
                for (var i = 0; i < samePyCount; i++)
                {
                    var wordByteLength = BinFileHelper.ReadInt16(fs);
                    var wordArray = new byte[wordByteLength];
                    fs.Read(wordArray, 0, wordByteLength);
                    string word = Encoding.Unicode.GetString(wordArray);
                    var count = BinFileHelper.ReadInt16(fs);
                    var count2 = BinFileHelper.ReadInt16(fs);
                    var unknown = BinFileHelper.ReadInt32(fs); //不知道干啥的
                    WordLibrary wl = new WordLibrary() { Count = count, Word = word, PinYin = pyArray };
                    pyAndWord.Add(wl);
                    CurrentStatus++;
                }
            }
            return pyAndWord;
        }

        public int CountWord { get; set; }
        public int CurrentStatus { get; set; }

        public bool IsText
        {
            get { return false; }
        }

        public WordLibraryList ImportLine(string line)
        {
           throw new NotImplementedException("搜狗Bin文件为二进制文件，不支持");
        }


    }
}