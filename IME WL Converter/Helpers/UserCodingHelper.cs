﻿using System;
using System.Collections.Generic;

namespace Studyzy.IMEWLConverter.Helpers
{
    public static class UserCodingHelper
    {
        private static string filePath = "";

        private static IDictionary<char, string> dictionary = new Dictionary<char, string>();

        public static string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                dictionary = GetCodingDict(FileOperationHelper.ReadFile(filePath));
            }
        }

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
            var dic = new Dictionary<char, string>();
            foreach (string line in codingContent.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] l = line.Split(',');
                char c = l[0][0];
                string code = l[1];
                if (!dic.ContainsKey(c))
                {
                    dic.Add(c, code);
                }
            }
            return dic;
        }
    }
}