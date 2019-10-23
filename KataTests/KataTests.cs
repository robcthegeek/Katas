using System;
using Katas;
using Xunit;

namespace KataTests
{
    public class KataTests
    {
        /* Bowling Scoring
            https://www.liveabout.com/bowling-scoring-420895

            - Two rolls per frame.
            - 1 game = 10 frames per person
            - X strike - nailed it in one :)
            - num / - hit num of pins, then finished remained for the 10
            - F = foul
            - Strike = 10 points for that frame, PLUS the score from the next frame (both rolls)
            - Spare = 10 points for that frame, PLUS pins knocked from next roll
            - Strike on 10th frame = gets another frame. Cannot repeat the process and bowl forever :)
        */

        [Fact]
        public void ten_misses_returns_0()
        {
            var frames = "- - - - - - - - - -";

            var score = BowlingGame.Score(frames);

            Assert.Equal(0, score);
        }

        [Fact]
        public void twelve_strikes_returns_300()
        {
            var frames = "X X X X X X X X X X X X";

            var score = BowlingGame.Score(frames);

            Assert.Equal(300, score);
        }

        [Fact]
        public void ten_singles_returns_10()
        {
            var frames = "1 1 1 1 1 1 1 1 1 1";

            var score = BowlingGame.Score(frames);

            Assert.Equal(10, score);
        }

        [Fact]
        public void first_frame_spare_adds_next_roll()
        {
            var frames = "1 / 1 - - - - - - -"; // 1 + 9 + 1 = 11
                                                // 1

            var score = BowlingGame.Score(frames);

            Assert.Equal(12, score);
        }

        [Fact]
        public void ten_twos_returns_20()
        {
            var frames = "2 2 2 2 2 2 2 2 2 2";

            var score = BowlingGame.Score(frames);

            Assert.Equal(20, score);
        }

        [Fact]
        public void ten_threes_returns_30()
        {
            var frames = "3 3 3 3 3 3 3 3 3 3";

            var score = BowlingGame.Score(frames);

            Assert.Equal(30, score);
        }

        // Remove Total
        // TODO: Spare '/' parsing - remainder
        // TODO: Lookahead to next frame for spare / strike
    }
}
