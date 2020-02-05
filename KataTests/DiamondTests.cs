using System;
using Katas;
using Xunit;

namespace KataTests
{
    public class DiamondTests
    {
        [Fact]
        public void A_returns_just_a()
        {
            Assert.Equal(
                "A",
                Diamond.For('A'));
        }
    }
}
