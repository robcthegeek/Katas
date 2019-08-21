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

        private static readonly Dictionary<Hand, string> _winText = new Dictionary<Hand, string>
        {
            {Hand.Rock, "blunts"},
            {Hand.Paper, "covers"},
            {Hand.Scissors, "cut"}
        };

        private static readonly Dictionary<Hand, string> _emoji = new Dictionary<Hand, string>
        {
            {Hand.Rock, "🗿"},
            {Hand.Paper, "📄"},
            {Hand.Scissors, "✂"}
        };

        public static Hand Play(Hand a, Hand b) => WinnerAndLoser(a, b).winner;

        public static string Text(Hand a, Hand b)
        {
            var (winner, loser) = WinnerAndLoser(a, b);
            return $"{winner} {_winText[winner]} {loser}".ToLowerInvariant();
        }
        public static string Emoji(Hand a, Hand b)
        {
            var (winner, loser) = WinnerAndLoser(a, b);
            return $"{_emoji[winner]} {_winText[winner]} {_emoji[loser]}".ToLowerInvariant();
        }

        private static (Hand winner, Hand loser) WinnerAndLoser(Hand a, Hand b) => _beats[a] == b ? (a, b) : (b, a);
    }
}