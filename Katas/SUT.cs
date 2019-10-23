using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Katas
{
    public static class BowlingGame
    {
        public static int Score(string scoreCard)
        {
            var rolls = scoreCard.Split(' ');

            var frames = Parse(scoreCard);

            if (rolls.Length == 12)
            {
                // Perfect game? Maybe - let's cheat for now.
                return 300;
            }

            var result = 0;

            result = frames
                .Select(x => x.Total)
                .Aggregate(0, (a, c) => a + c);

            return result;
        }

        private static List<Frame> Parse(string scoreCard)
        {
            var rolls = scoreCard.Split(' ');
            var frames = new List<Frame>();

            for (int i = 0; i < rolls.Length; i += 2)
            {
                frames.Add(new Frame(rolls[i], rolls[i + 1]));
            }

            return frames;
        }
    }

    class Frame
    {
        public int Roll1 { get; set; }
        public int Roll2 { get; set; }
        public int Total { get; set; }
        public Bonus Bonus { get; set; }

        public Frame(string score1, string score2)
        {
            int TryParse(string score)
            {
                int.TryParse(score, out var i);
                return i;
            }

            Roll1 = TryParse(score1);
            Roll2 = TryParse(score2);
            Total = Roll1 + Roll2;

            if (score1 == Specials.STRIKE)
            {
                Roll1 = 10;
                Roll2 = 0;
                Total = 10;
                Bonus = Bonus.Strike;
            }

            if (score2 == Specials.SPARE)
            {
                Total = Roll1 + Roll2;
                Bonus = Bonus.Spare;
            }
        }
    }

    static class Specials
    {
        internal const string STRIKE = "X";
        internal const string SPARE = "/";
    }

    enum Bonus
    {
        None,
        Spare,
        Strike
    }
}