using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Katas
{
    public static class Trigrams
    {
        public static TrigramCollection Find(string text)
        {
            var result = new TrigramCollection();

            var words = text.Split(' ');

            for (int i = 0; i < words.Length - 2; i++)
            {
                var ngram = string.Join(' ', words[i], words[i + 1]);
                var list = (result.GetValueOrDefault(ngram) ?? new List<string>());
                list.Add(words[i + 2]);
                result[ngram] = list;
            }

            return result;
        }
    }

    public class TrigramCollection : Dictionary<string, List<string>>
    {
        public List<string> Trigrams(string ngram)
        {
            return this[ngram];
        }
    }

    public class TrigramStory
    {
        private static readonly Random Rng = new Random();

        public static string From(TrigramCollection trigrams)
        {
            var result = new StringBuilder();

            // pick start
            var bigram = trigrams.Keys.ElementAt(Rng.Next(trigrams.Keys.Count));
            while (trigrams.ContainsKey(bigram) && trigrams[bigram].Count > 0)
            {
                var options = trigrams[bigram];

                // pick possible options
                var pick = Rng.Next(options.Count);
                var picked = options[pick];
                options.RemoveAt(pick);

                // take option, append
                result.Append($" {picked}");

                bigram = $"{bigram.Split(' ')[1]} {picked}";
                // next bigram
            }

            return result.ToString().Trim();
        }
    }
}