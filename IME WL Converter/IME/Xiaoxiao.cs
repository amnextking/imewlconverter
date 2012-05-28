using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter.IME
{
    public class Xiaoxiao : IWordLibraryExport
    {

        #region IWordLibraryExport 成员
        public string ExportLine(WordLibrary wl)
        {
            var sb = new StringBuilder();
            sb.Append(wl.GetPinYinString("", BuildType.None));
            sb.Append(" ");
            sb.Append(wl.Word);
            return sb.ToString();
        }
        public string Export(WordLibraryList wlList)
        {
            var sb = new StringBuilder();
            sb.Append(
                @"name=拼音
key=abcdefghijklmnopqrstuvwxyz
len=63
wildcard=?
pinyin=1
split='
hint=0
user=pinyin.usr
assist=mb/yong.txt 2
code_a1=p..
[DATA]
");
            IDictionary<string,string> xiaoxiaoDic=new Dictionary<string, string>();

            for (int i = 0; i < wlList.Count; i++)
            {
                string key = wlList[i].GetPinYinString("", BuildType.None);
                string value = wlList[i].Word;
                if (xiaoxiaoDic.ContainsKey(key))
                {
                    xiaoxiaoDic[key] += " " + value;
                }
                else
                {
                    xiaoxiaoDic.Add(key,value);
                }
            }
            foreach (KeyValuePair<string, string> keyValuePair in xiaoxiaoDic)
            {
                sb.Append(keyValuePair.Key + " " + keyValuePair.Value + "\n");
            }

            return sb.ToString();
        }

        public Encoding Encoding
        {
            get { return Encoding.GetEncoding("GB18030"); }
        }

        #endregion

    }
}
