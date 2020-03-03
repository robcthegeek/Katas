using System;
using Katas;
using Xunit;

namespace KataTests
{
    public class CoinFlipperTests
    {
        [Fact]
        public void Tests_Are_Working()
        {
            Assert.NotEqual("pending", CoinFlipper.Flip().winner);
        }
    }
}
