using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Katas
{
    public enum RPSLSHand
    {
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spock
    }

    public static class RockPaperScissorsLizardSpock
    {
        private static readonly Dictionary<RPSLSHand, RPSLSHand[]> _beats = new Dictionary<RPSLSHand, RPSLSHand[]>
        {
            { RPSLSHand.Rock, new [] { RPSLSHand.Scissors, RPSLSHand.Lizard } },
            { RPSLSHand.Paper, new [] { RPSLSHand.Rock, RPSLSHand.Spock } },
            { RPSLSHand.Scissors, new [] { RPSLSHand.Paper, RPSLSHand.Lizard } },
            { RPSLSHand.Lizard, new [] { RPSLSHand.Paper, RPSLSHand.Spock } },
            { RPSLSHand.Spock, new [] { RPSLSHand.Scissors, RPSLSHand.Rock } },
        };

        private static readonly Dictionary<(RPSLSHand, RPSLSHand), string> _winText = new Dictionary<(RPSLSHand, RPSLSHand), string>
        {
            {(RPSLSHand.Rock, RPSLSHand.Scissors), "blunts"},
            {(RPSLSHand.Rock, RPSLSHand.Lizard), "crushes"},
            {(RPSLSHand.Paper, RPSLSHand.Rock), "covers"},
            {(RPSLSHand.Paper, RPSLSHand.Spock), "disproves"},
            {(RPSLSHand.Scissors, RPSLSHand.Paper), "cut"},
            {(RPSLSHand.Scissors, RPSLSHand.Lizard), "decapitates"},
            {(RPSLSHand.Lizard, RPSLSHand.Paper), "eats"},
            {(RPSLSHand.Lizard, RPSLSHand.Spock), "poisons"},
            {(RPSLSHand.Spock, RPSLSHand.Rock), "vaporises"},
            {(RPSLSHand.Spock, RPSLSHand.Scissors), "smashes"},
        };

        private static readonly Dictionary<RPSLSHand, string> _emoji = new Dictionary<RPSLSHand, string>
        {
            {RPSLSHand.Rock, "🗿"},
            {RPSLSHand.Paper, "📄"},
            {RPSLSHand.Scissors, "✂"},
            {RPSLSHand.Lizard, "🦎"},
            {RPSLSHand.Spock, "🖖"}
    };

        public static RPSLSHand Play(RPSLSHand a, RPSLSHand b) => WinnerAndLoser(a, b).winner;

        public static string Text(RPSLSHand a, RPSLSHand b)
        {
            var (winner, loser) = WinnerAndLoser(a, b);
            return $"{winner} {_winText[(winner, loser)]} {loser}".ToLowerInvariant();
        }
        public static string Emoji(RPSLSHand a, RPSLSHand b)
        {
            var (winner, loser) = WinnerAndLoser(a, b);
            return $"{_emoji[winner]} {_winText[(winner, loser)]} {_emoji[loser]}".ToLowerInvariant();
        }

        private static (RPSLSHand winner, RPSLSHand loser) WinnerAndLoser(RPSLSHand a, RPSLSHand b) => _beats[a].Contains(b) ? (a, b) : (b, a);
    }
}