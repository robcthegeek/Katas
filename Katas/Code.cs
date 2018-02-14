using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        Func<int> readInt = () => int.Parse(Console.ReadLine());
        Func<int, IList<string>> readDeck = cardCount =>
            Enumerable.Range(0, cardCount)
                .Select(i => Console.ReadLine())
                .ToList();

        var p1Deck = readDeck(readInt());
        var p2Deck = readDeck(readInt());

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        var result = War.Play(p1Deck, p2Deck);

        Console.WriteLine(result);
    }
}

public static class War
{
    public static string Play(IList<string> p1Deck, IList<string> p2Deck)
    {
        var rounds = 0;
        var decks = new[] {
            new Queue<Card>(p1Deck.Select(s => new Card(s))),
            new Queue<Card>(p2Deck.Select(s => new Card(s)))
        };

        var p1Played = new Queue<Card>();
        var p2Played = new Queue<Card>();

        while (decks[0].Any() || decks[1].Any())
        {
            rounds++;

            // Battle
            var p1 = decks[0].Dequeue();
            p1Played.Enqueue(p1);
            var p2 = decks[1].Dequeue();
            p2Played.Enqueue(p2);

            if (p1 > p2)
            {
                AddCardsToDeck(decks[0], p1Played, p2Played);
            }

            if (p2 > p1)
            {
                AddCardsToDeck(decks[1], p1Played, p2Played);
            }

            if (p2 == p1)
            {
                // Dequeue WAR! Cards
                for (int i = 0; i < 3; i++)
                {
                    p1Played.Enqueue(decks[0].Dequeue());
                    p2Played.Enqueue(decks[1].Dequeue());

                    // TODO: If a player runs out of cards during a "war" (when giving up the three cards or when doing the battle), then the game ends and both players are placed equally first.
                    if (!decks[0].Any() || !decks[1].Any()) return "PAT";
                }

                rounds--; // Wars don't close the round
            }

            // Player Won?
            if (!decks[0].Any()) return $"2 {rounds}";
            if (!decks[1].Any()) return $"1 {rounds}";
        }

        throw new Exception("I BROKE!");
    }

    private static void AddCardsToDeck(Queue<Card> deck, Queue<Card> p1Cards, Queue<Card> p2Cards)
    {
        while (p1Cards.Count > 0)
        {
            deck.Enqueue(p1Cards.Dequeue());
        }

        while (p2Cards.Count > 0)
        {
            deck.Enqueue(p2Cards.Dequeue());
        }
    }
}

internal class Card
{
    private static Dictionary<string, int> _valueMap = new Dictionary<string, int>
    {
        { "2", 1 },
        { "3", 2 },
        { "4", 3 },
        { "5", 4 },
        { "6", 5 },
        { "7", 6 },
        { "8", 7 },
        { "9", 8 },
        { "10", 9 },
        { "J", 10 },
        { "Q", 11 },
        { "K", 12 },
        { "A", 13 },
    };

    public string Value { get; private set; }
    public string Suit { get; private set; }

    private int intValue;

    public Card(string value)
    {
        var match = Regex.Match(value, @"(?<value>(\d{1,2}|[JQKA]))(?<suit>[HSCD])");
        Value = match.Groups["value"].Value;
        intValue = _valueMap[Value];
        Suit = match.Groups["suit"].Value;
    }

    public static bool operator >(Card a, Card b)
    {
        return a.intValue > b.intValue;
    }

    public static bool operator <(Card a, Card b)
    {
        return a.intValue < b.intValue;
    }

    public static bool operator ==(Card a, Card b)
    {
        return a.intValue == b.intValue;
    }

    public static bool operator !=(Card a, Card b)
    {
        return a.intValue != b.intValue;
    }
}