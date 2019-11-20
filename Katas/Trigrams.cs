using System;
using System.Collections;
using System.Collections.Generic;

namespace Katas
{
    public static class Trigrams
    {
        public static TrigramCollection Find(string text)
        {
            var result = new TrigramCollection();

            var words = text.Split(' ');
            Console.WriteLine($"Words: {words.Length}");

            for (int i = 0; i < words.Length - 2; i++)
            {
                Console.WriteLine($"i: {i}");
                var ngram = string.Join(' ', words[i], words[i + 1]);
                var list = (result.GetValueOrDefault(ngram) ?? new List<string>());
                list.Add(words[i + 2]);
                result[ngram] = list;
            }

            Console.WriteLine(string.Join(Environment.NewLine, result["I wish"]));

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
}