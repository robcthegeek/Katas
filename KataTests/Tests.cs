using System;
using System.Collections.Generic;
using System.Linq;
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

            var t1 = _sampleGrid.ClosestAt("5, 8");
            Assert.Single(t1);
            Assert.Contains("5, 5", t1);
            var t2 = _sampleGrid.ClosestAt("6, 8");
            Assert.Contains("8, 9", t2);
            Assert.Single(t2);
        }

        [Fact]
        public void can_get_infinites()
        {
            var infinites = _sampleGrid.Infinites;

            Assert.Contains("1, 1", infinites);
            Assert.Contains("1, 6", infinites);
            Assert.Contains("8, 3", infinites);
            Assert.DoesNotContain("3, 4", infinites);
            Assert.DoesNotContain("5, 5", infinites);
            Assert.Contains("8, 9", infinites);
            Assert.Equal(4, infinites.Count);
        }

        [Fact]
        public void sample_d_closest_correct()
        {
            var expected = new[]
            {
                "3, 2",
                "4, 2",
                "2, 3",
                "3, 3",
                "4, 3",
                "2, 4",
                "3, 4",
                "4, 4",
                "3, 5",
            };

            expected.ToList().ForEach(_ =>
            {
                var closestAt = _sampleGrid.ClosestAt(_);
                Assert.Contains("3,4",closestAt);
                Assert.Single(closestAt);
            });

            Assert.Equal((uint)expected.Length, _sampleGrid.Areas["3,4"]);
        }

        [Fact]
        public void can_solve_sample()
        {
            Assert.Equal(17u, _sampleGrid.Answer);
        }

        [Fact]
        public void can_solve_step1()
        {
            var grid = new Grid(
                "242, 164",
                "275, 358",
                "244, 318",
                "301, 335",
                "310, 234",
                "159, 270",
                "82, 142",
                "229, 286",
                "339, 256",
                "305, 358",
                "224, 339",
                "266, 253",
                "67, 53",
                "100, 143",
                "64, 294",
                "336, 303",
                "261, 267",
                "202, 86",
                "273, 43",
                "115, 256",
                "78, 356",
                "91, 234",
                "114, 146",
                "114, 260",
                "353, 346",
                "336, 283",
                "312, 341",
                "234, 119",
                "281, 232",
                "65, 203",
                "95, 85",
                "328, 72",
                "285, 279",
                "61, 123",
                "225, 179",
                "97, 140",
                "329, 305",
                "236, 337",
                "277, 110",
                "321, 335",
                "261, 258",
                "304, 190",
                "41, 95",
                "348, 53",
                "226, 298",
                "263, 187",
                "106, 338",
                "166, 169",
                "310, 295",
                "236, 191"
            );

            Assert.True(grid.Answer < 9220);

            Console.WriteLine(grid.Answer);
        }
    }

    public class DistanceTests
    {
        [Fact]
        public void calculates_sample_distances_correctly()
        {
            Assert.Equal(4, Manhattan.Distance(new Coord(8,3), new Coord(9,0)));
            Assert.Equal(4, Manhattan.Distance(new Coord(8,9), new Coord(4,9)));
            Assert.Equal(1, Manhattan.Distance(new Coord(1,6), new Coord(1,5)));
        }
    }
}
