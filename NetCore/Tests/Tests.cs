using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Katas;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void new_deck_has_52_cards()
        {
            var deck = new Deck();
            Assert.AreEqual(52, deck.Count);
        }

        [Test]
        public void draw_returns_a_card_from_deck()
        {
            var deck = new Deck();
            var card = deck.Draw();

            Assert.IsNotNull(card);
            Assert.AreEqual(51, deck.Count);
        }

        [Test]
        public void draw_hand_returns_5_cards_from_deck()
        {
            var deck = new Deck();
            var cards = deck.DrawHand();

            Assert.AreEqual(5, cards.Count);
            Assert.AreEqual(47, deck.Count);
        }

        [TestCase]
        public void card_only_accepts_valid_values()
        {
            // should be TestCaseSource but THERE'S NO TIME DAMMIT
            var suits = new[] {"H", "C", "D", "S"};
            var numbers = new[] {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};

            foreach (var suit in suits)
            {
                foreach (var number in numbers)
                {
                    var card = new Card($"{number}{suit}");
                    Assert.AreEqual(suit, card.Suit);
                    Assert.AreEqual(number, card.Number);
                    Assert.AreEqual($"{number}{suit}", card.Value);
                }
            }
        }

        [Test]
        public void deck_cant_value_duplicates()
        {
            var deck = new Deck();
            var distinct = deck.Cards.Distinct().ToList();
            Assert.AreEqual(52, distinct.Count);
        }

        [TestCase("AD", "KD", "QD", "JD", "10D", 1)]
        [TestCase("AH", "KH", "QH", "JH", "10H", 1)]
        [TestCase("AS", "KS", "QS", "JS", "10S", 1)]
        [TestCase("AC", "KC", "QC", "JC", "10C", 1)]
        [TestCase("AC", "KC", "QC", "JC", "10C", 1)]
        public void hand_is_ranked_correctly(string c1, string c2, string c3, string c4, string c5, int rank)
        {
            var hand = new Hand(new[] { new Card(c1), new Card(c2), new Card(c3), new Card(c4), new Card(c5) });
            Assert.AreEqual(rank, hand.Rank);
        }
    }
}
