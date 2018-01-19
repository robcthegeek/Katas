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

        public Point Move(Direction direction)
        {
            if (direction == Direction.Left)
                return new Point(X - 1, Y);
            if (direction == Direction.Right)
                return new Point(X + 1, Y);
            if (direction == Direction.Up)
                return new Point(X, Y + 1);
            if (direction == Direction.Down)
                return new Point(X, Y - 1);

            throw new NotImplementedException("Where the fuck are you going?");
        }

        internal Point StepTowards(Point b)
        {
            // Can only go along single axis
            if (X != b.X && Y != b.Y)
                throw new Exception("Cannot step diagonally.");

            if (X == b.X && Y == b.Y)
                throw new Exception("You're already there.");

            if (X == b.X)
            {
                return (b.Y > Y)
                    ? new Point(X, Y + 1)
                    : new Point(X, Y - 1);
            }

            if (Y == b.Y)
            {
                return (b.X > X)
                    ? new Point(X + 1, Y)
                    : new Point(X - 1, Y);
            }

            throw new Exception("I tried to Step and fell over, are my shoe laces tied?");
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
        private readonly int _maxAddress;

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
            _maxAddress = maxAddress;

            var currentAddress = new MemoryAddress(0, 0, 0);
            var direction = Direction.Right;

            Addresses = new Dictionary<Point, MemoryAddress>
            {
                { currentAddress.Point, currentAddress }
            };

            while (currentAddress.Address < maxAddress)
            {
                var turningPoint = GetTurningPoint(currentAddress.Point, direction);
                currentAddress = Addresses.MoveFrom(currentAddress, turningPoint);
            }
        }

        private Point GetTurningPoint(Point currentPoint, Direction direction)
        {
            // e.g. If going up, trying to go left - I can just find the highest 'Y' on the current 'X' and add 1
            var trying = GoingToTryDirectionMap[direction];
            var mappedPoints = Addresses.Keys;

            if (!mappedPoints.Any())
                return new Point(1, 0);

            Point result = new Point(0, 0);

            if (trying == Direction.Up)
            {
                var maxX = mappedPoints.Max(p => p.X);
                result = new Point(maxX + 1, currentPoint.Y);
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
            if (mappedPoints.Contains(result))
                throw new Exception($"Turning Point {result.X}/{result.Y} Already Exists");

            return result;
        }

        private Direction GoSpirograph(Point location, Direction currentDirection)
        {
            // Always start with 'Right'
            if (location.X == 0 && location.Y == 0)
                return Direction.Right;

            var tryDirection = GoingToTryDirectionMap[currentDirection];
            return FindDirection(location, currentDirection, tryDirection);
        }

        private Direction FindDirection(Point location, Direction currentDirection, Direction checkDirection)
        {
            var check = location.Move(checkDirection);
            return (Addresses.ContainsKey(check))
                ? currentDirection
                : checkDirection;
        }

        public static implicit operator string(Map map)
        {
            var sb = new StringBuilder();
            const string Title = "CRAZY FUCKING MAP CHALLENGE";
            sb.AppendLine(Title);
            sb.AppendLine(new string('=', Title.Length));

            var allPoints = map.Addresses.Keys;

            var xMin = allPoints.Min(p => p.X);
            var xMax = allPoints.Max(p => p.X);
            var yMin = allPoints.Min(p => p.Y);
            var yMax = allPoints.Max(p => p.Y);

            int maxDigits = map.Addresses.Values.Max(address => address.Address).ToString().Length;

            sb.AppendLine($"Map Range:: X: {xMin}-{xMax} / Y: {yMin}-{yMax}");

            for (int y = yMax; y > yMin; y--)
            {
                for (int x = xMin; x < xMax; x++)
                {
                    var key = new Point(x, y);
                    if (!map.Addresses.ContainsKey(key))
                    {
                        Console.WriteLine($"WARN: Key Not Found for {key.X}/{key.Y}");
                    }
                    else
                    {
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
        internal static MemoryAddress MoveFrom(this Dictionary<Point, MemoryAddress> addresses, MemoryAddress current, Point b)
        {
            // TODO: Add to addresses as we go...
            var step = current.Point.StepTowards(b);

            var result =  new MemoryAddress
            {
                Address = current.Address + 1,
                Point = step
            };

            addresses.Add(result.Point, result);

            return result;
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
            Console.WriteLine($"Maths: {result}");

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