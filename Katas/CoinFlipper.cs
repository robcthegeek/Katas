using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Katas
{
    public static class CoinFlipper
    {
        public static (string results, string winner) Flip()
        {
            var rng = new Random();
            var results = new List<char>();

            for (int i = 0; i < 101; i++)
            {
                results.Add(rng.Next(0, 2) == 1 ? 'H' : 'T');
            }

            var winner = results.Count(x => x == 'H') > 50 ? "Heads" : "Tails";

            return (string.Join("", results), $"{winner} Wins!");
        }
    }
}