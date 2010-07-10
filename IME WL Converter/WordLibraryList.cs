using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    public class WordLibraryList : List<WordLibrary>
    {
        /// <summary>
        /// 将词库中重复出现的单词合并成一个词，多词库合并时使用(词重复就算)
        /// </summary>
        public void MergeSameWord()
        {
            Dictionary<string, WordLibrary> dic = new Dictionary<string, WordLibrary>();
            foreach (WordLibrary wl in this)
            {
                if (!dic.ContainsKey(wl.Word))
                {
                    dic.Add(wl.Word, wl);
                }
            }
            this.Clear();
            foreach (WordLibrary wl in dic.Values)
            {
                this.Add(wl);
            }
        }
        public void AddWordLibraryList(WordLibraryList wll)
        {
            this.AddRange(wll);
        }
    }
}
