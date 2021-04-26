using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            if(text == null)
            {
                return null;
            }
			text = text.ToLower();
            string[] splitText = text.Split(new char[] { '.', '!', '?', ';', ':', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            List<List<string>> sequences = new List<List<string>>();
			StringBuilder sb = new StringBuilder();
            foreach(string sequence in splitText)
            {
                List<string> wordsInSequence = new List<string>();
                foreach(char symb in sequence)
                    if (char.IsLetter(symb) || symb == '\'')
                        sb.Append(symb);
                    else
                        AddElements(sb, wordsInSequence);
                AddElements(sb, wordsInSequence);
				if(wordsInSequence.Count > 0)
                	sequences.Add(wordsInSequence);
            }
            return sequences;
        }
        public static void AddElements(StringBuilder sb, List<string> list)
        {
			if(sb.Length > 0)
			{
            	list.Add(sb.ToString());
            	sb.Clear();
			}
        }
	}
}

