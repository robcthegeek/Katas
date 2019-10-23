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
        public void games_have_10_frames()
        {
            Assert.Throws<IncorrectNumberOfFramesException>(
                () => BowlingGame.Score("12"));
        }

        [Fact]
        public void ten_missed_frames_returns_0()
        {
            var score = BowlingGame.Score("-- -- -- -- -- -- -- -- -- --");

            Assert.Equal(0, score);
        }

        [Fact]
        public void twelve_strikes_returns_300()
        {
            var score = BowlingGame.Score("X X X X X X X X X X X X");

            Assert.Equal(300, score);
        }

        [Fact]
        public void ten_singles_returns_10()
        {
            var score = BowlingGame.Score("1- 1- 1- 1- 1- 1- 1- 1- 1- 1-");

            Assert.Equal(10, score);
        }

        [Fact]
        public void first_frame_spare_adds_next_roll()
        {
            var frames = "1/ 1- -- -- -- -- -- -- -- --"; // 1 + 9 + 1 = 11 + 1 = 12

            var score = BowlingGame.Score(frames);

            Assert.Equal(12, score);
        }

        [Fact]
        public void ten_twos_returns_20()
        {
            var score = BowlingGame.Score("2- 2- 2- 2- 2- 2- 2- 2- 2- 2-");

            Assert.Equal(20, score);
        }

        [Fact]
        public void ten_threes_returns_30()
        {
            var score = BowlingGame.Score("3- 3- 3- 3- 3- 3- 3- 3- 3- 3-");

            Assert.Equal(30, score);
        }

        [Theory]
        [InlineData("12 3/ 42 4/ X 12 45 1/ X 42", 110)]
        [InlineData("1- 2- 3- 4- 5- 6- 7- 8- 9- X 1/", 65)]
        public void various_games_return_expected_scores(string scoreCard, int expected)
        {
            // FANKS: https://www.bowlinggenius.com
            Assert.Equal(expected, BowlingGame.Score(scoreCard));
        }

        // TODO: Remove Total
        // TODO: Spare on last = 1 bonus roll
        // TODO: Strike on last = 2 bonus rolls
        // TODO: Easier if 11th frame is fatter?
    }
}
