using System.Collections.Generic;

namespace Studyzy.IMEWLConverter.Generaters
{
    /// <summary>
    /// 根据汉字输出其Code的接口,其假设是一个汉字只有一个Code对应
    /// </summary>
    public interface IWordCodeGenerater
    {
        string GetCodeOfChar(char str);
        List<string> GetCodeOfString(string str);
    }
}