using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
namespace Studyzy.IMEWLConverter
{
   public static  class FileOperationHelper
    {
        /// <summary>
        /// 根据词库的格式或内容判断源词库的类型
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string AutoMatchSourceWLType(string filePath)
        {
            if (Path.GetExtension(filePath) == ".scel")
            {
                return ConstantString.SOUGOU_XIBAO_SCEL;
            }
            string example = "";
            using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
            {
                for (int i = 0; i < 5; i++)
                {
                    example = sr.ReadLine();
                }
                sr.Close();
            }
            if(example==null)
            {
                example = "";
            }
            Regex reg = new Regex(@"^('[a-z]+)+\s[\u4E00-\u9FA5]+$");
            if (reg.IsMatch(example))
            {
                return ConstantString.SOUGOU_PINYIN;
            }
            reg = new Regex(@"^[a-z']+\s[\u4E00-\u9FA5]+\s\d+$");
            if (reg.IsMatch(example))
            {
                return ConstantString.QQ_PINYIN;
            }
            //reg = new Regex(@"^[\u4E00-\u9FA5]+$");
            //if (reg.IsMatch(example))
            //{
            //    return ConstantString.WORD_ONLY;
            //}//用户“不再梦想”建议删除该功能，因为加加词库也可能是纯汉字，会形成误判。
            reg = new Regex(@"^[a-z\u4E00-\u9FA5]+$");
            if (reg.IsMatch(example))
            {
                return ConstantString.PINYIN_JIAJIA;
            }
            reg = new Regex(@"^[\u4E00-\u9FA5]+\t[a-z']+\t\d+$");
            if (reg.IsMatch(example))
            {
                return ConstantString.ZIGUANG_PINYIN;
            }
            reg = new Regex(@"^[\u4E00-\u9FA5]+\t\d+[a-z\s]+$");
            if (reg.IsMatch(example))
            {
                return ConstantString.GOOGLE_PINYIN;
            }
            reg = new Regex(@"^[\u4E00-\u9FA5]+\s[a-z\|]+\s\d+$");
            if (reg.IsMatch(example))
            {
                return ConstantString.BAIDU_SHOUJI;
            }
            reg = new Regex(@"^[a-z']+\s[\u4E00-\u9FA5]+$");
            if (reg.IsMatch(example))
            {
                return ConstantString.SINA_PINYIN;
            }
            return "";

        }
        public static string ReadFile(string path)
        {
            string ext = Path.GetExtension(path);
            if (ext == ".scel")//搜狗细胞词库
            {
                return SougouPinyinScel.ReadScel(path);
            }
            else//文本
            {
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        public static bool WriteFile(string path, Encoding coding, string content)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, coding))
                {
                    sw.Write(content);
                    sw.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
