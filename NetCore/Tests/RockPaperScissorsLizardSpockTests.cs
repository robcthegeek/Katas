using System;
using System.Collections.Generic;
using System.Text;
using Katas;
using NUnit.Framework;
using Hand = Katas.RPSLSHand;

namespace Tests
{
    [TestFixture]
    public class RockPaperScissorsLizardSpockTests
    {
        [TestCase(Hand.Rock, Hand.Scissors)]
        [TestCase(Hand.Rock, Hand.Lizard)]
        [TestCase(Hand.Paper, Hand.Rock)]
        [TestCase(Hand.Paper, Hand.Spock)]
        [TestCase(Hand.Scissors, Hand.Paper)]
        [TestCase(Hand.Scissors, Hand.Lizard)]
        [TestCase(Hand.Lizard, Hand.Paper)]
        [TestCase(Hand.Lizard, Hand.Spock)]
        [TestCase(Hand.Spock, Hand.Scissors)]
        [TestCase(Hand.Spock, Hand.Rock)]
        public void Correct_Winner_Returned(Hand winner, Hand loser)
        {
            Assert.AreEqual(winner, RockPaperScissorsLizardSpock.Play(winner, loser));
        }

        [Test]
        public void Determines_Correct_Player()
        {
            var p1 = RockPaperScissorsLizardSpock.Play(Hand.Scissors, Hand.Paper);
            var p2 = RockPaperScissorsLizardSpock.Play(Hand.Paper, Hand.Scissors);

            Assert.That(p1 == Hand.Scissors && p2 == Hand.Scissors, "Correct winner is not both sides.");
        }

        [TestCase(Hand.Rock)]
        [TestCase(Hand.Paper)]
        [TestCase(Hand.Scissors)]
        public void Draw_Returns_Same_Hand(Hand hand)
        {
            Assert.AreEqual(hand, RockPaperScissorsLizardSpock.Play(hand, hand));
        }

        [TestCase(Hand.Rock, Hand.Scissors, "rock blunts scissors")]
        [TestCase(Hand.Rock, Hand.Lizard, "rock crushes lizard")]
        [TestCase(Hand.Paper, Hand.Rock, "paper covers rock")]
        [TestCase(Hand.Paper, Hand.Spock, "paper disproves spock")]
        [TestCase(Hand.Scissors, Hand.Paper, "scissors cut paper")]
        [TestCase(Hand.Scissors, Hand.Lizard, "scissors decapitates lizard")]
        [TestCase(Hand.Lizard, Hand.Paper, "lizard eats paper")]
        [TestCase(Hand.Lizard, Hand.Spock, "lizard poisons spock")]
        [TestCase(Hand.Spock, Hand.Rock, "spock vaporises rock")]
        [TestCase(Hand.Spock, Hand.Scissors, "spock smashes scissors")]
        public void Outputs_Text_Answer(Hand a, Hand b, string expected)
        {
            Assert.AreEqual(expected, RockPaperScissorsLizardSpock.Text(a, b));
        }

        [TestCase(Hand.Rock, Hand.Scissors, "🗿 blunts ✂")]
        [TestCase(Hand.Rock, Hand.Lizard, "🗿 crushes 🦎")]
        [TestCase(Hand.Paper, Hand.Rock, "📄 covers 🗿")]
        [TestCase(Hand.Paper, Hand.Spock, "📄 disproves 🖖")]
        [TestCase(Hand.Scissors, Hand.Paper, "✂ cut 📄")]
        [TestCase(Hand.Scissors, Hand.Lizard, "✂ decapitates 🦎")]
        [TestCase(Hand.Lizard, Hand.Paper, "🦎 eats 📄")]
        [TestCase(Hand.Lizard, Hand.Spock, "🦎 poisons 🖖")]
        [TestCase(Hand.Spock, Hand.Rock, "🖖 vaporises 🗿")]
        [TestCase(Hand.Spock, Hand.Scissors, "🖖 smashes ✂")]
        public void Outputs_Emoji_Answer(RPSLSHand a, RPSLSHand b, string expected)
        {
            Assert.AreEqual(expected, RockPaperScissorsLizardSpock.Emoji(a, b));
        }
    }
}
