using System;
using Katas;
using Xunit;

namespace KataTests
{
    public class Tests
    {
        private readonly Grid _sampleGrid;

        public Tests()
        {
            _sampleGrid = new Grid(
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
            Assert.Equal(6, _sampleGrid.Coordinates.Count);
        }

        [Fact]
        public void grid_sized_correctly()
        {
            Assert.Equal(8u, _sampleGrid.Width);
            Assert.Equal(9u, _sampleGrid.Height);
        }

        [Fact]
        public void closest_sample_data_works()
        {
            Assert.Contains("8, 3", _sampleGrid.ClosestAt("7, 0"));
        }
    }

    public class DistanceTests
    {
        [Fact]
        public void calculates_sample_distances_correctly()
        {
            Assert.Equal(4, Distance.Manahattan(new Coord(8,3), new Coord(9,0)));
            Assert.Equal(4, Distance.Manahattan(new Coord(8,9), new Coord(4,9)));
            Assert.Equal(1, Distance.Manahattan(new Coord(1,6), new Coord(1,5)));
        }
    }
}
