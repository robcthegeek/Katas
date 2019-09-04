using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Katas
{
    public class PokerFace
    {

    }

    public class Deck
    {
        public int Count => _cards.Count;

        private readonly List<Card> _cards = new List<Card>(52);

        public IReadOnlyList<Card> Cards => new ReadOnlyCollection<Card>(_cards);

        public Deck()
        {
            // EW EW EW EW EW EW EW EW!
            var suits = new[] { "H", "C", "D", "S" };
            var numbers = new[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

            foreach (var suit in suits)
            {
                foreach (var number in numbers)
                {
                    _cards.Add(new Card($"{number}{suit}"));
                }
            }
        }

        public Card Draw()
        {
            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }
        public Hand DrawHand()
        {
            var cards = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                cards.Add(Draw());
            }
            return new Hand(cards.ToArray());
        }
    }

    public class Hand : Collection<Card>
    {
        public int Rank { get; } = 1;

        public Hand(Card[] cards)
        {
            foreach (var card in cards)
            {
                Add(card);
            }
        }
    }

    public struct Card
    {
        static readonly Regex Matcher = new Regex(@"(?<number>([AJQK]|\d{1,2}))(?<suit>[SCDH])");
        public string Suit { get; }
        public string Number { get; }
        public string Value { get; }

        public Card(string value)
        {
            var match = Matcher.Match(value);
            Suit = match.Groups["suit"].Value;
            Number = match.Groups["number"].Value;
            Value = value;
        }
    }
}