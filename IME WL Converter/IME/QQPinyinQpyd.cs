using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace Studyzy.IMEWLConverter.IME
{
    public class QQPinyinQpyd : IWordLibraryImport
    {
        private string ParseQpyd(string qqydFile)
        {
            var fs = new FileStream(qqydFile, FileMode.Open, FileAccess.Read);
            fs.Position = 0x38;
            byte[] startAddressByte = new byte[4];
            fs.Read(startAddressByte, 0, 4);
            var startAddress = BitConverter.ToInt32(startAddressByte, 0);
            fs.Position = 0x44;
            var wordCount = BinFileHelper.ReadInt32(fs);
            CountWord = wordCount;
            CurrentStatus = 0;

            fs.Position = startAddress;
            InflaterInputStream zipStream = new InflaterInputStream(fs);


            int bufferSize = 2048; //缓冲区大小
            int readCount = 0; //读入缓冲区的实际字节
            byte[] buffer = new byte[bufferSize];
            List<byte> byteList = new List<byte>();
            readCount = zipStream.Read(buffer, 0, bufferSize);
            while (readCount > 0)
            {
                for (var i = 0; i < readCount; i++)
                {
                    byteList.Add(buffer[i]);
                }
                readCount = zipStream.Read(buffer, 0, bufferSize);
            }
            zipStream.Close();
            zipStream.Dispose();
            fs.Close();

            byte[] byteArray = byteList.ToArray();

            int unzippedDictStartAddr = -1;
            int idx = 0;
            StringBuilder sb=new StringBuilder();
            while (unzippedDictStartAddr == -1 || idx < unzippedDictStartAddr)
            {
                // read word

                int pinyinStartAddr = BitConverter.ToInt32(byteArray, idx + 0x6);
                int pinyinLength = BitConverter.ToInt32(byteArray, idx + 0x0) & 0xff;
                int wordStartAddr = pinyinStartAddr + pinyinLength;
                int wordLength = BitConverter.ToInt32(byteArray, idx + 0x1) & 0xff;
                if (unzippedDictStartAddr == -1)
                {
                    unzippedDictStartAddr = pinyinStartAddr;
                    Debug.WriteLine("词库地址（解压后）：0x" + unzippedDictStartAddr.ToString("0x") + "\n");
                }

                string pinyin = Encoding.UTF8.GetString(byteArray, pinyinStartAddr, pinyinLength);
                string word = Encoding.Unicode.GetString(byteArray, wordStartAddr, wordLength);
                sb.Append(word + "\t" + pinyin+"\n");
                Debug.WriteLine(word + "\t" + pinyin);
                CurrentStatus++;
                // step up
                idx += 0xa;
            }
            return sb.ToString();

        }



        public int CountWord { get; set; }

        public int CurrentStatus { get; set; }

        public WordLibraryList Import(string path)
        {
            WordLibraryList wll = new WordLibraryList();
            string txt = ParseQpyd(path);
            foreach (var line in txt.Split('\n'))
            {
                if(line!="")
                {
                    wll.AddWordLibraryList(ImportLine(line));
                }
            }
            return wll;
        }

        public WordLibraryList ImportLine(string line)
        {
            
            string[] sp = line.Split('\t');
            string word = sp[0];
            string py = sp[1];
            int count = 1;
            var wl = new WordLibrary();
            wl.Word = word;
            wl.Count = count;
            wl.PinYin = py.Split(new[] { '\'' }, StringSplitOptions.RemoveEmptyEntries);
            var wll = new WordLibraryList();
            if (!string.IsNullOrEmpty(py))
            {
                wll.Add(wl);
            }
            return wll;
        }

        public bool IsText
        {
            get { return false; }
        }
    }
}
