using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Katas
{
    public enum RockPaperScissorsHand
    {
        Rock = 0,
        Paper = 1,
        Scissors = 2
    }

    public enum LizardSpockHand
    {
        Rock = 0,
        Paper = 1,
        Scissors = 2,
        Lizard = 3,
        Spock = 4
    }

    internal static class RockPaperScissorLizardSpockGame
    {
        private static readonly Dictionary<LizardSpockHand, LizardSpockHand[]> _beats = new Dictionary<LizardSpockHand, LizardSpockHand[]>
        {
            { LizardSpockHand.Rock, new [] { LizardSpockHand.Scissors, LizardSpockHand.Lizard } },
            { LizardSpockHand.Paper, new [] { LizardSpockHand.Rock, LizardSpockHand.Spock } },
            { LizardSpockHand.Scissors, new [] { LizardSpockHand.Paper, LizardSpockHand.Lizard } },
            { LizardSpockHand.Lizard, new [] { LizardSpockHand.Paper, LizardSpockHand.Spock } },
            { LizardSpockHand.Spock, new [] { LizardSpockHand.Scissors, LizardSpockHand.Rock } },
        };

        private static readonly Dictionary<(LizardSpockHand, LizardSpockHand), string> _winText = new Dictionary<(LizardSpockHand, LizardSpockHand), string>
        {
            {(LizardSpockHand.Rock, LizardSpockHand.Scissors), "blunts"},
            {(LizardSpockHand.Rock, LizardSpockHand.Lizard), "crushes"},
            {(LizardSpockHand.Paper, LizardSpockHand.Rock), "covers"},
            {(LizardSpockHand.Paper, LizardSpockHand.Spock), "disproves"},
            {(LizardSpockHand.Scissors, LizardSpockHand.Paper), "cut"},
            {(LizardSpockHand.Scissors, LizardSpockHand.Lizard), "decapitates"},
            {(LizardSpockHand.Lizard, LizardSpockHand.Paper), "eats"},
            {(LizardSpockHand.Lizard, LizardSpockHand.Spock), "poisons"},
            {(LizardSpockHand.Spock, LizardSpockHand.Rock), "vaporises"},
            {(LizardSpockHand.Spock, LizardSpockHand.Scissors), "smashes"},
        };

        private static readonly Dictionary<LizardSpockHand, string> _emoji = new Dictionary<LizardSpockHand, string>
        {
            {LizardSpockHand.Rock, "🗿"},
            {LizardSpockHand.Paper, "📄"},
            {LizardSpockHand.Scissors, "✂"},
            {LizardSpockHand.Lizard, "🦎"},
            {LizardSpockHand.Spock, "🖖"}
    };

        internal static LizardSpockHand Play(LizardSpockHand a, LizardSpockHand b) => WinnerAndLoser(a, b).winner;

        internal static string Text(LizardSpockHand a, LizardSpockHand b)
        {
            var (winner, loser) = WinnerAndLoser(a, b);
            return $"{winner} {_winText[(winner, loser)]} {loser}".ToLowerInvariant();
        }
        internal static string Emoji(LizardSpockHand a, LizardSpockHand b)
        {
            var (winner, loser) = WinnerAndLoser(a, b);
            return $"{_emoji[winner]} {_winText[(winner, loser)]} {_emoji[loser]}".ToLowerInvariant();
        }

        internal static (LizardSpockHand winner, LizardSpockHand loser) WinnerAndLoser(LizardSpockHand a, LizardSpockHand b) => _beats[a].Contains(b) ? (a, b) : (b, a);
    }

    public static class RockPaperScissors
    {
        public static RockPaperScissorsHand Play(RockPaperScissorsHand a, RockPaperScissorsHand b) =>
            (RockPaperScissorsHand)RockPaperScissorLizardSpockGame.WinnerAndLoser((LizardSpockHand)a, (LizardSpockHand)b).winner;

        public static string Text(RockPaperScissorsHand a, RockPaperScissorsHand b) =>
            RockPaperScissorLizardSpockGame.Text((LizardSpockHand) a, (LizardSpockHand) b);

        public static string Emoji(RockPaperScissorsHand a, RockPaperScissorsHand b) =>
            RockPaperScissorLizardSpockGame.Emoji((LizardSpockHand) a, (LizardSpockHand) b);
    }

    public static class RockPaperScissorsLizardSpock
    {
        public static LizardSpockHand Play(LizardSpockHand a, LizardSpockHand b) => RockPaperScissorLizardSpockGame.WinnerAndLoser(a, b).winner;

        public static string Text(LizardSpockHand a, LizardSpockHand b) => RockPaperScissorLizardSpockGame.Text(a, b);

        public static string Emoji(LizardSpockHand a, LizardSpockHand b) => RockPaperScissorLizardSpockGame.Emoji(a, b);
    }
}