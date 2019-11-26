using System;
using System.Collections.Generic;
using System.Linq;

namespace Katas
{
    public class Grid
    {
        public HashSet<Coord> Coordinates { get; } = new HashSet<Coord>();
        public uint Width { get; }
        public uint Height { get; }

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

            // determine the coords that are "infinite" (on the edges) - these need to be immediately excluded from area calc.
        }

        public HashSet<Coord> ClosestAt(Coord point) =>
            new HashSet<Coord>(Coordinates
                .Select(coord => new { Coord = coord, Distance = Distance.Manahattan(point, coord) })
                .OrderBy(coord => coord.Distance)
                .GroupBy(g => g.Distance)
                .Take(1)
                .SelectMany(g => g.Select(_ => _.Coord))
                .ToList());
    }

    public static class Distance
    {
        public static int Manahattan(Coord a, Coord b) => Math.Abs((int)a.X - (int)b.X) + Math.Abs((int)a.Y - (int)b.Y);
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