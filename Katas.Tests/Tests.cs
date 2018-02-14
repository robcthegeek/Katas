using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Katas;
using NUnit.Framework;

namespace Katas.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ThreeCards()
        {
            var actual = War.Play(new[] { "AD", "KC", "QC" }, new[] { "KH", "QS", "JC" });
            Assert.AreEqual("1 3", actual);
        }

        [Test]
        public void TwentySixCards()
        {
            var actual = War.Play(
                new[] { "5C", "3D", "2C", "7D", "8C", "7S", "5D", "5H", "6D", "5S", "4D", "6H", "6S", "3C", "3S", "7C", "4S", "4H", "7H", "4C", "2H", "6C", "8D", "3H", "2D", "2S" },
                new[] { "AC", "9H", "KH", "KC", "KD", "KS", "10S", "10D", "9S", "QD", "JS", "10H", "8S", "QH", "JD", "AD", "JC", "AS", "QS", "AH", "JH", "10C", "9C", "8H", "QC", "9D" });
            Assert.AreEqual("2 26", actual);
        }

        [Test]
        public void TwentySixCardsMediumLength()
        {
            var actual = War.Play(
                new[] { "6H", "7H", "6C", "QS", "7S", "8D", "6D", "5S", "6S", "QH", "4D", "3S", "7C", "3C", "4S", "5H", "QD", "5C", "3H", "3D", "8C", "4H", "4C", "QC", "5D", "7D" },
                new[] { "JH", "AH", "KD", "AD", "9C", "2D", "2H", "JC", "10C", "KC", "10D", "JS", "JD", "9D", "9S", "KS", "AS", "KH", "10S", "8S", "2S", "10H", "8H", "AC", "2C", "9H" });
            Assert.AreEqual("2 56", actual);
        }

        [Test]
        public void Battle()
        {
            var actual = War.Play(
                new[] { "8C", "KD", "AH", "QH", "2S" },
                new[] { "8D", "2D", "3H", "4D", "3S" });
            Assert.AreEqual("2 1", actual);
        }
    }
}
