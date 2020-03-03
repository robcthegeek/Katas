using System;
using System.Linq;
using Katas;
using Xunit;

namespace KataTests
{
    public class CoinFlipperTests
    {
        [Fact]
        public void flipper_outputs_101_results()
        {
            var result = CoinFlipper.Flip();
            Assert.Equal(101, result.results.Length);
        }

        [Fact]
        public void flipper_outputs_some_heads_and_tails()
        {
            // WARN: In theory - this could return all 'Heads', but YOLOOOO!
            var result = CoinFlipper.Flip();
            Assert.Contains('H', result.results);
            Assert.Contains('T', result.results);
        }

        [Fact]
        public void flipper_outputs_winner()
        {
            var result = CoinFlipper.Flip();
            var winner = result.results.Count(x => x == 'H') > 50 ? "Heads" : "Tails";
            Assert.Equal($"{winner} Wins!", result.winner);
        }
    }
}
