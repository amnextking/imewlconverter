﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Studyzy.IMEWLConverter
{
   public class BaiduPinyinBdict: IWordLibraryImport
    {
       
       private List<string> Shengmu = new List<string>() { "c", "d", "b", "f", "g", "h", "ch", "j", "k", "l", "m", "n", "", "p", "q", "r", "s", "t", "sh", "zh", "w", "x", "y", "z" };
       private List<string> Yunmu = new List<string>() { "uang", "iang", "iong", "ang", "eng", "ian", "iao", "ing", "ong", "uai", "uan", "ai", "an", "ao", "ei", "en", "er", "ua", "ie", "in", "iu", "ou", "ia", "ue", "ui", "un", "uo", "a", "e", "i", "o", "u", "v" };
        #region IWordLibraryImport Members

       public int CountWord { get; set; }
       public int CurrentStatus { get; set; }
       public bool IsText
       {
           get { return false; }
       }
        public WordLibraryList Import(string path)
        {
            int endPosition = 0;
            WordLibraryList wordLibraryList=new WordLibraryList();
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            fs.Position = 0x60;
            endPosition = BinFileHelper.ReadInt32(fs);
            fs.Position = 0x350;
            CurrentStatus = 0;
            do
            {
                //CurrentStatus++;
                try
                {
                    var wl = ImportWord(fs);
                    if (wl == null)
                    {
                        break;
                    }
                    if(wl.Word!=""&&wl.PinYin.Length>0)
                    {
                        wordLibraryList.Add(wl);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            while (fs.Position!=endPosition);//< fs.Length
            fs.Close();
            //StreamWriter sw=new StreamWriter("D:\\py.txt",true,Encoding.Unicode);
            //SinglePinyin singlePinyin=new SinglePinyin();
               
            //foreach (var cpy in CharAndPinyin)
            //{
            //    var py = "";
            //    try
            //    {
            //        py = singlePinyin.GetPinYinOfChar(cpy.Key)[0];
            //    }
            //    catch
            //    {
            //        Debug.Write(cpy.Key);
            //    }
            //    sw.WriteLine(cpy.Key+"\t"+ py+"\t"+cpy.Value);
            //}
            //sw.Close();

            //wordLibraryList.ForEach(delegate(WordLibrary wl) { if(wl.Word==""||wl.PinYin.Length==0)
            //{
            //    Debug.WriteLine(wl.ToDisplayString());
            //}
            //});

            return wordLibraryList;
        }
       //public Dictionary<char,string > CharAndPinyin=new Dictionary<char, string>();
       //private void AddWordAndPinyin(char word,string pinyin)
       //{
       //    if (!CharAndPinyin.ContainsKey(word))
       //    {
       //        CharAndPinyin.Add(word,pinyin);
       //    }
       //}
        private WordLibrary ImportWord(FileStream fs)
        {
            int show = 0;
            WordLibrary wordLibrary=new WordLibrary();
            var temp = new byte[4];
            fs.Read(temp, 0, 4);
            var len = BitConverter.ToInt32(temp, 0);
            if (len == 0)
            {
                Debug.WriteLine(fs.Position);
                return null;
                return SpecialWord(fs);
            }
            List<string> pinyinList=new List<string>();
            for (var i = 0; i < len; i++)
            {
                temp = new byte[2];
                fs.Read(temp, 0, 2);
                try
                {
                    var sm = Shengmu[temp[0]];
                    var ym = Yunmu[temp[1]];

                    pinyinList.Add(sm + ym);
                }
                catch (Exception e)
                {
                    show = temp[0];
                }
            }
            wordLibrary.PinYin = pinyinList.ToArray();
            temp = new byte[2*len];
            fs.Read(temp, 0, 2*len);
            wordLibrary.Word = Encoding.Unicode.GetString(temp);
            //for (var i = 0; i < wordLibrary.Word.Length;i++ )
            //{
            //    AddWordAndPinyin(wordLibrary.Word[i], wordLibrary.PinYin[i]);
            //}
            if (show>0)
            {
                Debug.WriteLine(show+"  "+ wordLibrary.Word+"----"+wordLibrary.PinYinString);
            }
            return wordLibrary;
        }

        private WordLibrary SpecialWord(FileStream fs)
        {
            var temp = new byte[2];
            fs.Read(temp, 0, 2);
            var pinyinLen = BitConverter.ToInt16(temp, 0);
            fs.Read(temp, 0, 2);
            var wordLen = BitConverter.ToInt16(temp, 0);
            temp = new byte[pinyinLen*2];
            fs.Read(temp, 0, pinyinLen*2);
            var pinyinString = Encoding.Unicode.GetString(temp);
            temp = new byte[wordLen*2];
            fs.Read(temp, 0, wordLen*2);
            var word = Encoding.Unicode.GetString(temp);
            WordLibrary wordLibrary = new WordLibrary();
            wordLibrary.Word = word;
            wordLibrary.PinYin = pinyinString.Split('\'');
            return wordLibrary;
        }

       public WordLibraryList ImportLine(string str)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
