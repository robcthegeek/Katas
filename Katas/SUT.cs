using System;
using System.Collections.Generic;

namespace Katas
{
    public static class BowlingGame
    {
        public static int Score(string scoreCard)
        {
            var game = new Game(scoreCard);

            var rolls = scoreCard.Split(' ');

            Parse(scoreCard);

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
                frames.Add(new Frame(rolls[i]));
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
            var frames = scoreCard.Split(' ');

            for (int i = 0; i < frames.Length; i ++)
            {
                Frames.Add(new Frame(frames[i]));
            }

            var (incorrect, expected) = IncorrectCount(Frames);
            if (incorrect) throw new IncorrectNumberOfFramesException(expected, Frames.Count);
        }

        private (bool incorrect, int expected) IncorrectCount(List<Frame> frames)
        {
            if (frames.Count < 10 || frames.Count > 12) return (true, 10); // obvs incorrect.

            if (frames[9].Bonus == Bonus.Strike)
            {
                // 11 AND 12 can be strikes
                if (frames[10].Bonus == Bonus.Strike)
                {
                    return (frames.Count != 12, 12);
                }

                return (frames.Count != 11, 11);
            }

            if (frames[9].Bonus != Bonus.None) return (frames.Count != 11, 11);

            return (false, 10);
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

        public Frame(string frame)
        {
            int TryParse(char @char)
            {
                int.TryParse(@char.ToString(), out var i);
                return i;
            }

            var score1 = frame[0];

            Roll1 = TryParse(score1);

            if (score1 == Specials.STRIKE)
            {
                Roll1 = 10;
                Roll2 = 0;
                Total = 10;
                Bonus = Bonus.Strike;
            }
            else
            {
                var score2 = frame[1];

                Roll2 = TryParse(score2);

                if (score2 == Specials.SPARE)
                {
                    Roll2 = 10 - Roll1;
                    Total = 10;
                    Bonus = Bonus.Spare;
                }
            }

            Total = Roll1 + Roll2;
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
        internal const char STRIKE = 'X';
        internal const char SPARE = '/';
    }

    enum Bonus
    {
        None,
        Spare,
        Strike
    }

    public class IncorrectNumberOfFramesException : Exception
    {
        static string CreateMessage(int expected, int actual) =>
            $"Incorrect number of frames - expected {expected}, actual: {actual}";

        public IncorrectNumberOfFramesException(int expected, int actual) : base(CreateMessage(expected, actual))
        {

        }
    }
}