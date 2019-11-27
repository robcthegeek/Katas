﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Katas
{
    public class Grid
    {
        public HashSet<Coord> Coordinates { get; }
        public HashSet<Coord> Infinites { get; }
        public HashSet<Coord> Finites { get; }
        public Dictionary<Coord, uint> Areas { get; }

        public uint Width { get; }
        public uint Height { get; }

        public Grid(params string[] coords)
        {
            // determine bounds (needed to find size of map)
            Coordinates = new HashSet<Coord>(coords.Select(s => new Coord(s)));
            Infinites = new HashSet<Coord>();
            Finites = new HashSet<Coord>(Coordinates);
            Areas = new Dictionary<Coord, uint>(Coordinates.Select(_ => new KeyValuePair<Coord, uint>(_, 0)));
            Width = Coordinates.Max(x => x.X);
            Height = Coordinates.Max(x => x.Y);

            bool OnEdge(uint x, uint y) => (x == 0 || x == Width) || (y == 0 || y == Height);

            for (uint y = 0; y <= Height; y++)
            {
                for (uint x = 0; x <= Width; x++)
                {
                    // any Coordinates on the edges, **with only a single 'closest'** are 'infinite', others need to be summed.
                    var closest = ClosestAt(new Coord(x, y));
                    if (OnEdge(x, y) && closest.Count == 1)
                    {
                        Infinites.UnionWith(closest);
                    }
                    else if (closest.Count == 1)
                    {
                        closest
                            .ToList()
                            .ForEach(coord => Areas[coord] += 1);
                    }
                }
            }

            Finites.RemoveWhere(Infinites.Contains);
        }

        public uint Answer => Areas
            .OrderByDescending(kvp => kvp.Value)
            .First()
            .Value;

        public HashSet<Coord> ClosestAt(Coord point) =>
            new HashSet<Coord>(Coordinates
                    .Select(coord => new {Coord = coord, Distance = Manhattan.Distance(point, coord)})
                    .OrderBy(coord => coord.Distance)
                    .GroupBy(g => g.Distance)
                    .Take(1)
                    .SelectMany(g => g.Select(_ => _.Coord))
                    .ToList());
    }

    public static class Manhattan
    {
        public static int Distance(Coord a, Coord b) => Math.Abs((int)a.X - (int)b.X) + Math.Abs((int)a.Y - (int)b.Y);
    }

    [DebuggerDisplay("[{X},{Y}]")]
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