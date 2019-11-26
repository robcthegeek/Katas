using System;
using Katas;
using Xunit;

namespace KataTests
{
    public class Tests
    {
        private readonly Grid sampleGrid;

        public Tests()
        {
            sampleGrid = new Grid(
                "1, 1",
                "1, 6",
                "8, 3",
                "3, 4",
                "5, 5",
                "8, 9"
            );
        }

        [Fact]
        public void fromcoords_creates_grid()
        {
            Assert.Equal(6, sampleGrid.Coordinates.Count);
        }

        [Fact]
        public void grid_sized_correctly()
        {
            Assert.Equal(8u, sampleGrid.Width);
            Assert.Equal(9u, sampleGrid.Height);
        }

        [Fact]
        public void closest_sample_data_works()
        {
            Assert.Equal("1, 1", sampleGrid.ClosestAt("0, 0"));
        }
    }
}
