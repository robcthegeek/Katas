using System;
using System.Collections.Generic;
using System.Linq;
using Katas;
using Xunit;

namespace KataTests
{
    public class TrigramTests
    {
        [Fact]
        public void single_trigram_returns_single()
        {
            var result = Trigrams.Find("I wish I");

            Assert.Single(result);
            Assert.Contains("I", result.Trigrams("I wish"));
        }

        [Fact]
        public void three_trigrams_returns_two_options()
        {
            var result = Trigrams.Find("I wish I wish I");

            Assert.Equal(2, result.Count);
            Assert.Equal(2, result.Trigrams("I wish").Count(x => x == "I"));
        }

        [Fact]
        public void sample_text_returns_expected()
        {
            var result = Trigrams.Find("I wish I may I wish I might");

            Assert.Equal(4, result.Count);
            Assert.Equal(new[] { "I", "I" }, result["I wish"]);
            Assert.Equal(new[] { "may", "might" }, result["wish I"]);
            Assert.Equal(new[] { "wish" }, result["may I"]);
            Assert.Equal(new[] { "I" }, result["I may"]);
        }
    }

    public class TrigramStoryWriterTests
    {
        [Fact]
        public void single_trigram_returns_single_option()
        {
            var trigrams = new TrigramCollection
            {
                {"I wish", new List<string> { "I" } }
            };

            var result = TrigramStory.From(trigrams);

            Assert.Equal("I", result);
        }
    }

    public class Samples
    {
        [Fact]
        public void specsample()
        {
            var trigrams = Trigrams.Find("I wish I may I wish I might");
            var result = TrigramStory.From(trigrams);
            Console.WriteLine(result);
            Assert.NotNull(result);
        }
    }
}
