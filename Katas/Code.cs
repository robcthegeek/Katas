using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
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

        while (decks[0].Any() || decks[1].Any())
        {
            rounds++;

            // Battle
            var p1 = decks[0].Dequeue();
            var p2 = decks[1].Dequeue();

            if (p1 > p2)
            {
                decks[0].Enqueue(p1);
                decks[0].Enqueue(p2);
            }

            if (p2 > p1)
            {
                decks[1].Enqueue(p1);
                decks[1].Enqueue(p2);
            }

            if (p2 == p1)
            {
                throw new NotImplementedException("WAR!");

                // First, both players place the three next cards of their pile face down.
                for (int i = 0; i < 3; i++)
                {
                    var wp1 = decks[0].Dequeue();
                    var w2 = decks[1].Dequeue();
                }

                // Then they go back to step 1 to decide who is going to win the war (several "wars" can be chained).

                // As soon as a player wins a "war", the winner adds all the cards from the "war" to their deck.
            }

            // Player Won?
            if (!decks[0].Any()) return $"2 {rounds}";
            if (!decks[1].Any()) return $"1 {rounds}";
        }

        return "PAT";
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