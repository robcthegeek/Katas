using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Katas.Tests
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

    internal Point StepTowards(Point b)
        {
            // Can only go along single axis
            if (X != b.X && Y != b.Y)
                throw new Exception("Cannot step diagonally.");

            if (X == b.X && Y == b.Y)
                throw new Exception("You're already there.");

            var result = b;

            if (X == b.X)
            {
                result = (b.Y > Y)
                    ? new Point(X, Y + 1)
                    : new Point(X, Y - 1);
            }
            else
            {

                result = (b.X > X)
                    ? new Point(X + 1, Y)
                    : new Point(X - 1, Y);
            }

            Console.WriteLine($"Stepping from {this} to {result}.");
            return result;
        }
    }

    public struct MemoryAddress
    {
        public Point Point { get; set; }
        public uint Address { get; set; }

        public MemoryAddress(uint address, int x, int y) : this(address, new Point(x, y))
        {
        }

        public MemoryAddress(uint address, Point point)
        {
            Address = address;
            Point = point;
        }
    }

    public enum Direction
    {
        Up,     // Moving X=, Y+
        Down,   // Moving X=, Y-
        Left,   // Moving X-, Y=
        Right   // Moving X+, Y=
    }

    public class Map
    {
        private static readonly Dictionary<Direction, Direction> GoingToTryDirectionMap = new Dictionary<Direction, Direction>()
        {
            { Direction.Right, Direction.Up },
            { Direction.Up, Direction.Left },
            { Direction.Left, Direction.Down },
            { Direction.Down, Direction.Right },
        };

        internal Dictionary<Point, MemoryAddress> Addresses { get; private set; }

        public Map(int maxAddress)
        {
            var currentAddress = new MemoryAddress(1, 0, 0);
            var direction = Direction.Right;

            Addresses = new Dictionary<Point, MemoryAddress>
            {
                { currentAddress.Point, currentAddress }
            };

            while (currentAddress.Address < maxAddress)
            {
                var turningPoint = GetTurningPoint(currentAddress.Point, direction);
                Console.WriteLine($"Address: {currentAddress.Address} - Current Position: {currentAddress.Point}, Turning Point: {turningPoint}, Direction: {direction}");
                currentAddress = Addresses.MoveFrom(currentAddress, turningPoint);
                Console.WriteLine($"New Position: {currentAddress.Point}");
                direction = GoingToTryDirectionMap[direction];
                Console.WriteLine($"New Direction: {direction}");
            }
        }

        private Point GetTurningPoint(Point currentPoint, Direction direction)
        {
            var trying = GoingToTryDirectionMap[direction];
            var mappedPoints = Addresses.Keys;

            if (currentPoint.X == 0 && currentPoint.Y == 0)
                return new Point(1, 0);

            var result = new Point(0, 0);

            if (trying == Direction.Up)
            {
                var maxX = mappedPoints.Max(p => p.X);
                result = new Point(maxX +  1, currentPoint.Y);
            }

            if (trying == Direction.Down)
            {
                var minX = mappedPoints.Min(p => p.X);
                result = new Point(minX - 1, currentPoint.Y);
            }

            if (trying == Direction.Left)
            {
                var maxY = mappedPoints.Max(p => p.Y);
                result = new Point(currentPoint.X, maxY + 1);
            }

            if (trying == Direction.Right)
            {
                var minY = mappedPoints.Min(p => p.Y);
                result = new Point(currentPoint.X, minY - 1);
            }

            // Make sure I've not screwed up
            if (mappedPoints.Contains(result) || (result.X == 0 && result.Y == 0))
                throw new Exception($"Turning Point {result} Already Exists");

            return result;
        }

        public static implicit operator string(Map map)
        {
            var sb = new StringBuilder();
            const string Title = "CRAZY FUCKING MAP CHALLENGE";
            sb.AppendLine(Title);
            sb.AppendLine(new string('=', Title.Length));

            var allPoints = map.Addresses.Keys;
            Console.WriteLine($"Point Count: {allPoints.Count}");

            var xMin = allPoints.Min(p => p.X);
            var xMax = allPoints.Max(p => p.X);
            var yMin = allPoints.Min(p => p.Y);
            var yMax = allPoints.Max(p => p.Y);

            int maxDigits = map.Addresses.Values.Max(address => address.Address).ToString().Length;

            sb.AppendLine($"Map Range:: X: {xMin}-{xMax} / Y: {yMin}-{yMax}");

            for (int y = yMax; y >= yMin; y--)
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    var key = new Point(x, y);
                    if (!map.Addresses.ContainsKey(key))
                    {
                        Console.WriteLine($"WARN: Key Not Found for {key.X}/{key.Y}");
                        sb.AppendFormat("{0,-" + (maxDigits + 1) + "}", new string('*', maxDigits));
                    }
                    else
                    {
                        Console.WriteLine($"Rendering {map.Addresses[key].Address} at {key}");
                        sb.AppendFormat("{0,-" + (maxDigits + 1) + "}", map.Addresses[key].Address);
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    internal static class DictionaryExtensions
    {
        internal static MemoryAddress MoveFrom(this Dictionary<Point, MemoryAddress> addresses, MemoryAddress current, Point destination)
        {
            Console.WriteLine($"Moving From {current.Point} to {destination}.");
            var last = current;
            var there = false;

            while (!there)
            {
                var step = last.Point.StepTowards(destination);
                last = new MemoryAddress
                {
                    Address = last.Address + 1,
                    Point = step
                };

                Console.WriteLine($"Adding Address {last.Address} at {last.Point}.");
                addresses.Add(last.Point, last);

                if (last.Point.X == destination.X && last.Point.Y == destination.Y)
                    there = true;
            }

            return last;
        }
    }

    [TestFixture]
    public class Day3
    {
        private int Solve(int address, bool output = true)
        {
            var map = new Map(address);

            if (output)
                Console.WriteLine(map);

            // Get Vector for Address
            var vector = map.Addresses.Values.Single(ma => ma.Address == address).Point;

            // Taxicab Geometry - Get to Zero!
            var result = Math.Abs(vector.X) + Math.Abs(vector.Y);
            Console.WriteLine($"Taxicab Distance from {vector} to {new Point(0,0)}: {result}");

            return result;
        }

        [TestCase(1, 0)]
        [TestCase(12, 3)]
        [TestCase(23, 2)]
        [TestCase(1024, 31)]
        public void Solve_SampleSpreadsheet_Produces_Expected(int address, int expectedSteps)
        {
            var solution = Solve(address);

            Assert.That(solution, Is.EqualTo(expectedSteps));
        }

        [Test]
        [Ignore("WIP")]
        public void Solve_ChallengeInput_Produces_WinningResult()
        {
            var solution = Solve(289326, output: false);

            Console.WriteLine($"Solution is... (drum roll): {solution}");

            Assert.That(solution, Is.EqualTo(419));
        }
    }
}