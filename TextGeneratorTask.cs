using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        static StringSplitOptions arg = StringSplitOptions.RemoveEmptyEntries;

        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            StringBuilder result = CleanText(phraseBeginning);

            if (result.Length >= 1)
                result = GetFromDict(nextWords, result, wordsCount);

            return result.ToString();
        }

        static StringBuilder CleanText(string text)
        {
            StringBuilder cleanedText = new StringBuilder();

            foreach (char ch in text)
            {
                if (char.IsLetter(ch) || ch == '\'')
                    cleanedText.Append(ch);
                else
                    cleanedText.Append(' ');
            }

            return cleanedText;
        }

        static StringBuilder GetFromDict(Dictionary<string, string> dict, StringBuilder result, int counter)
        {
            for (; counter > 0; counter--)
            {
                string[] splitKey = result.ToString().Split(new char[] { ' ' }, arg);
                int keyLen = splitKey.Length;

                if (keyLen >= 2 && dict.ContainsKey(splitKey[keyLen - 2] + ' ' + splitKey[keyLen - 1]))
                {
                    result.Append(' ');
                    result.Append(dict[splitKey[keyLen - 2] + ' ' + splitKey[keyLen - 1]]);
                }
                else if (keyLen >= 1 && dict.ContainsKey(splitKey[keyLen - 1]))
                {
                    result.Append(' ');
                    result.Append(dict[splitKey[keyLen - 1]]);
                }
                else break;
            }

            return result;
        }
    }
}