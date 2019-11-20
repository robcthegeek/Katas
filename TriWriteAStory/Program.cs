using System;
using System.IO;
using Katas;

namespace TriWriteAStory
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "";
            var inputSource = "";

            // path?
            if (Path.HasExtension(args[0]))
            {
                input = File.Exists(args[0]) ? File.ReadAllText(args[0]) : "";
                inputSource = $"file '{args[0]}'";
            }
            else
            {
                input = args[0];
                inputSource = $"text: '{args[0]}'";
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ApplicationException("No text given or file not found.");
            }

            var story = TrigramStory.From(Trigrams.Find(input));

            const string outputFile = "MyTriStory.txt";
            File.WriteAllText(outputFile, story);

            Console.WriteLine($"Trigram story created from {inputSource}.");
            Console.WriteLine($"Saved to '{outputFile}'");
        }
    }
}
