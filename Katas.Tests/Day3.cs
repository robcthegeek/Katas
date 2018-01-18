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
                return new Point(X, Y- 1);

            throw new NotImplementedException("Where the fuck are you going?");
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

        internal List<MemoryAddress> Addresses { get; private set; }

        public Map(int maxAddress)
        {
            _maxAddress = maxAddress;

            Addresses = new List<MemoryAddress>();

            // Draw this thing somehow :)
            var currentPoint = new Point(0, 0);
            var direction = Direction.Right;

            for (uint i = 1; i <= maxAddress; i++)
            {
                Addresses.Add(new MemoryAddress(i, currentPoint));
                direction = GoSpirograph(currentPoint, direction);
                currentPoint = currentPoint.Move(direction);
            }
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
            return (Addresses.Any(ma => ma.Point.X == check.X && ma.Point.Y == check.Y))
                ? currentDirection
                : checkDirection;
        }

        public static implicit operator string(Map map)
        {
            var sb = new StringBuilder();
            const string Title = "CRAZY FUCKING MAP CHALLENGE";
            sb.AppendLine(Title);
            sb.AppendLine(new string('=', Title.Length));

            var xMin = map.Addresses.Min(address => address.Point.X);
            var xMax = map.Addresses.Max(address => address.Point.X);
            var yMin = map.Addresses.Min(address => address.Point.Y);
            var yMax = map.Addresses.Max(address => address.Point.Y);

            int maxDigits = map.Addresses.Max(address => address.Address).ToString().Length;

            sb.AppendLine($"Map Range:: X: {xMin}-{xMax} / Y: {yMin}-{yMax}");

            for (int y = yMax; y > yMin; y--)
            {
                for (int x = xMin; x < xMax; x++)
                {
                    sb.AppendFormat("{0,-" + (maxDigits + 1) + "}", map.Addresses.Single(ma => ma.Point.X == x && ma.Point.Y == y).Address);
                }

                sb.AppendLine();
            }

            sb.AppendLine("TODO");


            return sb.ToString();
        }
    }

    [TestFixture]
    public class Day3
    {
        private int Solve(int address)
        {
            var map = new Map(address);

            Console.WriteLine(map);
            return -1;
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
            var solution = Solve(123);

            Console.WriteLine($"Solution is... (drum roll): {solution}");
        }
    }
}