using System;
using System.Collections.Generic;
using System.Text;
using Katas;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RockPaperScissorsTests
    {
        [TestCase(Hand.Rock, Hand.Scissors)]
        [TestCase(Hand.Paper, Hand.Rock)]
        [TestCase(Hand.Scissors, Hand.Paper)]
        public void Correct_Winner_Returned(Hand winner, Hand loser)
        {
            Assert.AreEqual(winner, RockPaperScissors.Play(winner, loser));
        }

        [Test]
        public void Determines_Correct_Player()
        {
            var p1 = RockPaperScissors.Play(Hand.Scissors, Hand.Paper);
            var p2 = RockPaperScissors.Play(Hand.Paper, Hand.Scissors);

            Assert.That(p1 == Hand.Scissors && p2 == Hand.Scissors, "Correct winner is not both sides.");
        }

        [TestCase(Hand.Rock)]
        [TestCase(Hand.Paper)]
        [TestCase(Hand.Scissors)]
        public void Draw_Returns_Same_Hand(Hand hand)
        {
            Assert.AreEqual(hand, RockPaperScissors.Play(hand, hand));
        }

        [TestCase(Hand.Rock, Hand.Scissors, "rock blunts scissors")]
        [TestCase(Hand.Paper, Hand.Rock, "paper covers rock")]
        [TestCase(Hand.Scissors, Hand.Paper, "scissors cut paper")]
        public void Outputs_Text_Answer(Hand a, Hand b, string expected)
        {
            Assert.AreEqual(expected, RockPaperScissors.Text(a, b));
        }

        [TestCase(Hand.Rock, Hand.Scissors, "🗿 blunts ✂")]
        [TestCase(Hand.Paper, Hand.Rock, "📄 covers 🗿")]
        [TestCase(Hand.Scissors, Hand.Paper, "✂ cut 📄")]
        public void Outputs_Emoji_Answer(Hand a, Hand b, string expected)
        {
            Assert.AreEqual(expected, RockPaperScissors.Emoji(a, b));
        }
    }
}
