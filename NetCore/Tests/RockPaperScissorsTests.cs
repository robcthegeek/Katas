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
        [TestCase(RockPaperScissorsHand.Rock, RockPaperScissorsHand.Scissors)]
        [TestCase(RockPaperScissorsHand.Paper, RockPaperScissorsHand.Rock)]
        [TestCase(RockPaperScissorsHand.Scissors, RockPaperScissorsHand.Paper)]
        public void Correct_Winner_Returned(RockPaperScissorsHand winner, RockPaperScissorsHand loser)
        {
            Assert.AreEqual(winner, RockPaperScissors.Play(winner, loser));
        }

        [Test]
        public void Determines_Correct_Player()
        {
            var p1 = RockPaperScissors.Play(RockPaperScissorsHand.Scissors, RockPaperScissorsHand.Paper);
            var p2 = RockPaperScissors.Play(RockPaperScissorsHand.Paper, RockPaperScissorsHand.Scissors);

            Assert.That(p1 == RockPaperScissorsHand.Scissors && p2 == RockPaperScissorsHand.Scissors, "Correct winner is not both sides.");
        }

        [TestCase(RockPaperScissorsHand.Rock)]
        [TestCase(RockPaperScissorsHand.Paper)]
        [TestCase(RockPaperScissorsHand.Scissors)]
        public void Draw_Returns_Same_Hand(RockPaperScissorsHand hand)
        {
            Assert.AreEqual(hand, RockPaperScissors.Play(hand, hand));
        }

        [TestCase(RockPaperScissorsHand.Rock, RockPaperScissorsHand.Scissors, "rock blunts scissors")]
        [TestCase(RockPaperScissorsHand.Paper, RockPaperScissorsHand.Rock, "paper covers rock")]
        [TestCase(RockPaperScissorsHand.Scissors, RockPaperScissorsHand.Paper, "scissors cut paper")]
        public void Outputs_Text_Answer(RockPaperScissorsHand a, RockPaperScissorsHand b, string expected)
        {
            Assert.AreEqual(expected, RockPaperScissors.Text(a, b));
        }

        [TestCase(RockPaperScissorsHand.Rock, RockPaperScissorsHand.Scissors, "🗿 blunts ✂")]
        [TestCase(RockPaperScissorsHand.Paper, RockPaperScissorsHand.Rock, "📄 covers 🗿")]
        [TestCase(RockPaperScissorsHand.Scissors, RockPaperScissorsHand.Paper, "✂ cut 📄")]
        public void Outputs_Emoji_Answer(RockPaperScissorsHand a, RockPaperScissorsHand b, string expected)
        {
            Assert.AreEqual(expected, RockPaperScissors.Emoji(a, b));
        }
    }
}
