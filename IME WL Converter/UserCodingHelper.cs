using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    using System.IO;

    public static class UserCodingHelper
    {
        private static string filePath = "";

        public static string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                dictionary = GetCodingDict(FileOperationHelper.ReadFile(filePath));
            }
        }

        private static IDictionary<char, string> dictionary = new Dictionary<char, string>();

        public static string GetCharCoding(char c, string codingFilePath = null)
        {
            if (codingFilePath != null && codingFilePath != filePath)
            {
                dictionary = GetCodingDict(FileOperationHelper.ReadFile(codingFilePath));
                filePath = codingFilePath;
            }
            if (dictionary.ContainsKey(c))
            {
                return dictionary[c];
            }
            else
            {
                throw new ArgumentOutOfRangeException("从编码文件中找不到字[" + c + "]对应的编码");
            }
        }

        private static IDictionary<char, string> GetCodingDict(string codingContent)
        {
            Dictionary<char, string> dic = new Dictionary<char, string>();
            foreach (string line in codingContent.Split(new char[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries))
            {
                var l = line.Split(',');
                var c = l[0][0];
                var code = l[1];
                if (!dic.ContainsKey(c))
                {
                    dic.Add(c, code);
                }
            }
            return dic;
        }

    }
}
