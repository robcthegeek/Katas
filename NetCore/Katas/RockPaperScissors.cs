using System.Collections;
using System.Collections.Generic;

namespace Katas
{
    public enum Hand
    {
        Rock,
        Paper,
        Scissors
    }

    public static class RockPaperScissors
    {
        private static readonly Dictionary<Hand, Hand> _beats = new Dictionary<Hand, Hand>
        {
            { Hand.Rock, Hand.Scissors },
            { Hand.Paper, Hand.Rock },
            { Hand.Scissors, Hand.Paper },
        };

        private static bool Beats(this Hand a, Hand b) => _beats[a] == b;

        public static Hand Play(Hand p1, Hand p2)
        {
            return p1.Beats(p2) ? p1 : p2;
        }
    }
}