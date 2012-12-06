using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter.IME
{
    public class BaseImport
    {
        public BaseImport()
        {
            DefaultRank = 1;
        }

        public virtual int DefaultRank { get; set; }
        public virtual int CountWord { get; set; }
        public virtual int CurrentStatus { get; set; }

        public virtual bool IsText
        {
            get { return true; }
        }
        protected string[] ToArray(IList<string> str)
        {
            string[] result=new string[str.Count];
            for (int i = 0; i < str.Count; i++)
            {
                result[i] = str[i];
            }
            return result;
        }
    }
}
