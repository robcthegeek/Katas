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
        public void Tests_Are_Working()
        {
            Assert.True(SUT.IsThere);
        }
    }
}
