namespace Studyzy.IMEWLConverter.Helpers
{
    internal class WubiHelper
    {
        public static string GetWubi86Code(char c)
        {
            return DictionaryHelper.GetCode(c).Wubi86;
        }

        public static string GetWubi98Code(char c)
        {
            return DictionaryHelper.GetCode(c).Wubi98;
        }

        public static string GetStringWubi86Code(string str)
        {
            return GetStringWubiCode(GetWubi86Code, str);
        }

        public static string GetStringWubi98Code(string str)
        {
            return GetStringWubiCode(GetWubi98Code, str);
        }

        private static string GetStringWubiCode(GetWubiCode getWubiCode, string str)
        {
            if (str.Length == 1)
            {
                return getWubiCode(str[0]);
            }
            if (str.Length == 2)
            {
                string code1 = getWubiCode(str[0]);
                string code2 = getWubiCode(str[1]);
                return code1.Substring(0, 2) + code2.Substring(0, 2);
            }
            if (str.Length == 3)
            {
                string code1 = getWubiCode(str[0]);
                string code2 = getWubiCode(str[1]);
                string code3 = getWubiCode(str[2]);
                return code1[0].ToString() + code2[0].ToString() + code3.Substring(0, 2);
            }
            else
            {
                string code1 = getWubiCode(str[0]);
                string code2 = getWubiCode(str[1]);
                string code3 = getWubiCode(str[2]);
                string code4 = getWubiCode(str[str.Length - 1]);
                return code1[0].ToString() + code2[0] + code3[0] + code4[0];
            }
        }

        #region Nested type: GetWubiCode

        private delegate string GetWubiCode(char c);

        #endregion
    }
}