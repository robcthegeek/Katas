using System;
using System.Collections.Generic;
using System.Text;
using Katas;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RockPaperScissorsLizardSpockTests
    {
        [TestCase(LizardSpockHand.Rock, LizardSpockHand.Scissors)]
        [TestCase(LizardSpockHand.Rock, LizardSpockHand.Lizard)]
        [TestCase(LizardSpockHand.Paper, LizardSpockHand.Rock)]
        [TestCase(LizardSpockHand.Paper, LizardSpockHand.Spock)]
        [TestCase(LizardSpockHand.Scissors, LizardSpockHand.Paper)]
        [TestCase(LizardSpockHand.Scissors, LizardSpockHand.Lizard)]
        [TestCase(LizardSpockHand.Lizard, LizardSpockHand.Paper)]
        [TestCase(LizardSpockHand.Lizard, LizardSpockHand.Spock)]
        [TestCase(LizardSpockHand.Spock, LizardSpockHand.Scissors)]
        [TestCase(LizardSpockHand.Spock, LizardSpockHand.Rock)]
        public void Correct_Winner_Returned(LizardSpockHand winner, LizardSpockHand loser)
        {
            Assert.AreEqual(winner, RockPaperScissorsLizardSpock.Play(winner, loser));
        }

        [Test]
        public void Determines_Correct_Player()
        {
            var p1 = RockPaperScissorsLizardSpock.Play(LizardSpockHand.Scissors, LizardSpockHand.Paper);
            var p2 = RockPaperScissorsLizardSpock.Play(LizardSpockHand.Paper, LizardSpockHand.Scissors);

            Assert.That(p1 == LizardSpockHand.Scissors && p2 == LizardSpockHand.Scissors, "Correct winner is not both sides.");
        }

        [TestCase(LizardSpockHand.Rock)]
        [TestCase(LizardSpockHand.Paper)]
        [TestCase(LizardSpockHand.Scissors)]
        public void Draw_Returns_Same_Hand(LizardSpockHand hand)
        {
            Assert.AreEqual(hand, RockPaperScissorsLizardSpock.Play(hand, hand));
        }

        [TestCase(LizardSpockHand.Rock, LizardSpockHand.Scissors, "rock blunts scissors")]
        [TestCase(LizardSpockHand.Rock, LizardSpockHand.Lizard, "rock crushes lizard")]
        [TestCase(LizardSpockHand.Paper, LizardSpockHand.Rock, "paper covers rock")]
        [TestCase(LizardSpockHand.Paper, LizardSpockHand.Spock, "paper disproves spock")]
        [TestCase(LizardSpockHand.Scissors, LizardSpockHand.Paper, "scissors cut paper")]
        [TestCase(LizardSpockHand.Scissors, LizardSpockHand.Lizard, "scissors decapitates lizard")]
        [TestCase(LizardSpockHand.Lizard, LizardSpockHand.Paper, "lizard eats paper")]
        [TestCase(LizardSpockHand.Lizard, LizardSpockHand.Spock, "lizard poisons spock")]
        [TestCase(LizardSpockHand.Spock, LizardSpockHand.Rock, "spock vaporises rock")]
        [TestCase(LizardSpockHand.Spock, LizardSpockHand.Scissors, "spock smashes scissors")]
        public void Outputs_Text_Answer(LizardSpockHand a, LizardSpockHand b, string expected)
        {
            Assert.AreEqual(expected, RockPaperScissorsLizardSpock.Text(a, b));
        }

        [TestCase(LizardSpockHand.Rock, LizardSpockHand.Scissors, "🗿 blunts ✂")]
        [TestCase(LizardSpockHand.Rock, LizardSpockHand.Lizard, "🗿 crushes 🦎")]
        [TestCase(LizardSpockHand.Paper, LizardSpockHand.Rock, "📄 covers 🗿")]
        [TestCase(LizardSpockHand.Paper, LizardSpockHand.Spock, "📄 disproves 🖖")]
        [TestCase(LizardSpockHand.Scissors, LizardSpockHand.Paper, "✂ cut 📄")]
        [TestCase(LizardSpockHand.Scissors, LizardSpockHand.Lizard, "✂ decapitates 🦎")]
        [TestCase(LizardSpockHand.Lizard, LizardSpockHand.Paper, "🦎 eats 📄")]
        [TestCase(LizardSpockHand.Lizard, LizardSpockHand.Spock, "🦎 poisons 🖖")]
        [TestCase(LizardSpockHand.Spock, LizardSpockHand.Rock, "🖖 vaporises 🗿")]
        [TestCase(LizardSpockHand.Spock, LizardSpockHand.Scissors, "🖖 smashes ✂")]
        public void Outputs_Emoji_Answer(LizardSpockHand a, LizardSpockHand b, string expected)
        {
            Assert.AreEqual(expected, RockPaperScissorsLizardSpock.Emoji(a, b));
        }
    }
}
