using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Katas
{
    public class Grid
    {
        public HashSet<Coord> Coordinates { get; private set; } = new HashSet<Coord>();
        public uint Width { get; private set; }
        public uint Height { get; private set; }

        private readonly Dictionary<Coord, HashSet<Coord>> _closest = new Dictionary<Coord, HashSet<Coord>>();
        
        public Grid(params string[] coords)
        {
            uint maxX = 0;
            uint maxY = 0;

            // determine bounds (needed to find size of map)
            foreach (var s in coords)
            {
                var coord = new Coord(s);
                Coordinates.Add(coord);

                if (coord.X > maxX) maxX = coord.X;
                if (coord.Y > maxY) maxY = coord.Y;
            }

            Width = maxX;
            Height = maxY;

            // determine closest coord for each point on map (there may be multiple)

            for (uint y = 0; y < maxY; y++)
            {
                for (uint x = 0; x < maxX; x++)
                {
                    var closest = coords
                        .Select(coord => new { Coord = coord, Distance = Distance.Manahattan(new Coord(x, y), coord)})
                        .OrderBy(coord => coord.Distance)
                        .ToList();
                }
            }

            // every one that has a single item, add the total up to determine area (if on the edges, it's infinite)
        }

        public Coord ClosestAt(Coord coord)
        {
            return new Coord("0,0");
        }
    }

    internal static class Distance
    {
        internal static uint Manahattan(Coord a, Coord b)
        {
            return 0;
        }
    }

    public struct Coord
    {
        public Coord(string coord)
        {
            var split = coord.Split(',');
            X = uint.Parse(split[0].Trim());
            Y = uint.Parse(split[1].Trim());
        }

        public Coord(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        public uint X { get; set; }
        public uint Y { get; set; }

        public static implicit operator Coord(string value) => new Coord(value);
        public override string ToString() => $"{X},{Y}";
    }
}