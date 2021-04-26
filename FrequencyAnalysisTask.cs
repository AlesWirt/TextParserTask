using System;
using System.Text;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            if (text == null)
                return null;
            var result = new Dictionary<string, string>();
            Dictionary<string, int> ngrams = CountBiGrams(text);
            Dictionary<string, int> trigrams = CountTriGrams(text);
            result = FillDictionary(result, ngrams);
            result = FillDictionary(result, trigrams);
            return result;
        }
        public static Dictionary<string, string> FillDictionary(Dictionary<string, string> container, Dictionary<string, int> ngramsFrequency)
        {
            Dictionary<string, string> result = container;
            foreach (KeyValuePair<string, int> pair in ngramsFrequency)
                result = GetMostFrequentNgram(result, ngramsFrequency, pair);
            return result;
        }

        public static Dictionary<string, string> GetMostFrequentNgram(Dictionary<string, string> ngrams, Dictionary<string, int> source, KeyValuePair<string, int> kvp)
        {
            Dictionary<string, string> result = ngrams;
            int lastWhiteSpaceIndex = kvp.Key.LastIndexOf(' ');
            if (lastWhiteSpaceIndex < 0)
                return result;
            string ngram = kvp.Key.Substring(0, lastWhiteSpaceIndex);
            string continuation = kvp.Key.Substring(lastWhiteSpaceIndex + 1);
            string previousKey;

            if (!result.ContainsKey(ngram))
            {
                result[ngram] = continuation;
            }
            else if (result.ContainsKey(ngram))
            {
                previousKey = ngram + " " + result[ngram];
                if (kvp.Value > source[previousKey])
                {
                    result[ngram] = continuation;
                }
                else if (source[previousKey] == kvp.Value)
                {
                    if (string.CompareOrdinal(continuation, result[ngram]) < 0)
                    {
                        result[ngram] = continuation;
                    }
                }
            }
            return result;
        }

        public static Dictionary<string, int> CountBiGrams(List<List<string>> text)
        {
            if (text == null)
                return null;
            StringBuilder sb = new StringBuilder();
            Dictionary<string, int> ngrams = new Dictionary<string, int>();
            for (int i = 0; i < text.Count; i++)
            {
                for (int j = 0; j < text[i].Count - 1; j++)
                {
                    sb.Append(text[i][j] + " " + text[i][j + 1]);
                    AddNgrams(sb, ngrams);
                }
            }
            return ngrams;
        }
        public static Dictionary<string, int> CountTriGrams(List<List<string>> text)
        {
            if (text == null)
                return null;
            StringBuilder sb = new StringBuilder();
            Dictionary<string, int> ngrams = new Dictionary<string, int>();
            for (int i = 0; i < text.Count; i++)
            {
                for (int j = 0; j < text[i].Count - 2; j++)
                {
                    sb.Append(text[i][j] + " " + text[i][j + 1] + " " + text[i][j + 2]);
                    AddNgrams(sb, ngrams);
                }
            }
            return ngrams;
        }

        public static void AddNgrams(StringBuilder sb, Dictionary<string, int> dictionary)
        {
            if (sb.Length > 0)
            {
                string ngram = sb.ToString().Trim(' ');
                if (dictionary.ContainsKey(ngram))
                {
                    dictionary[ngram] += 1;
                }
                else
                {
                    dictionary[ngram] = 1;
                }
                sb.Clear();
            }
        }

    }
}