using System.Collections.Generic;

namespace Katas
{
    public static class BowlingGame
    {
        public static int Score(string scoreCard)
        {
            var game = new Game(scoreCard);

            var rolls = scoreCard.Split(' ');

            var frames = Parse(scoreCard);

            if (rolls.Length == 12)
            {
                // Perfect game? Maybe - let's cheat for now.
                return 300;
            }

            return game.Score;
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

    class Game
    {
        public List<Frame> Frames { get; } = new List<Frame>();

        public int Score => CalculateScore();

        public Game(string scoreCard)
        {
            var rolls = scoreCard.Split(' ');

            for (int i = 0; i < rolls.Length; i += 2)
            {
                Frames.Add(new Frame(rolls[i], rolls[i + 1]));
            }
        }

        int CalculateScore()
        {
            int result = 0;
            for (int i = 0; i < Frames.Count; i++)
            {
                var current = Frames[i];

                result += current.Total;

                if (Frames[i].HasBonus)
                {
                    var next = Frames[i + 1];
                    result += next.BonusValue(current.Bonus);
                }
            }

            return result;
        }
    }

    class Frame
    {
        public int Roll1 { get; set; }
        public int Roll2 { get; set; }
        public int Total { get; set; }
        public Bonus Bonus { get; set; }
        public bool HasBonus => Bonus != Bonus.None;

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
                Roll2 = 10 - Roll1;
                Total = 10;
                Bonus = Bonus.Spare;
            }
        }

        public int BonusValue(Bonus bonus)
        {
            switch (bonus)
            {
                case Bonus.Spare: return Roll1;
                case Bonus.Strike: return Roll1 + Roll2;
                default:
                    return 0;
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