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
            if (direction == Direction.UpLeft)
                return new Point(X - 1, Y + 1);
            if (direction == Direction.UpRight)
                return new Point(X + 1, Y + 1);
            if (direction == Direction.DownLeft)
                return new Point(X - 1, Y - 1);
            if (direction == Direction.DownRight)
                return new Point(X + 1, Y - 1);

            throw new NotImplementedException("Where the fuck are you going?");
        }

        public override string ToString()
        {
            return $"({X},{Y})";
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
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownRight,
        DownLeft
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

        public uint Seeking { get; private set; }
        public uint FirstValueLargerThanSeeking { get; private set; }

        public Map(uint seeking)
        {
            Seeking = seeking;
            Addresses = new Dictionary<Point, MemoryAddress>();

            var currentPoint = new Point(0, 0);
            var direction = Direction.Right;
            uint sumNeighbours = 0;

            while (sumNeighbours <= seeking)
            {
                if (currentPoint.X == 0 && currentPoint.Y == 0)
                    sumNeighbours = 1;
                else
                    sumNeighbours = SumNeighbours(currentPoint);

                Addresses.Add(currentPoint, new MemoryAddress(sumNeighbours, currentPoint));

                if (sumNeighbours > seeking)
                {
                    FirstValueLargerThanSeeking = sumNeighbours;
                    break;
                }

                direction = GoSpirograph(currentPoint, direction);
                currentPoint = currentPoint.Move(direction);
            }
        }

        private uint SumNeighbours(Point point)
        {
            var neighbourPoints = new[]
            {
                point.Move(Direction.Up),
                point.Move(Direction.Down),
                point.Move(Direction.Left),
                point.Move(Direction.Right),
                point.Move(Direction.UpLeft),
                point.Move(Direction.UpRight),
                point.Move(Direction.DownLeft),
                point.Move(Direction.DownRight)
            };

            Console.WriteLine($"Neighbour Points to {point}: {string.Join(", ", neighbourPoints.Select(p => p))}");


            var neighbours = Addresses
                .Where(kvp => neighbourPoints.Contains(kvp.Key));

            Console.WriteLine($"Placed Neighbours to {point}: {string.Join(", ", neighbours.Select(kvp => kvp.Key))}");

            var sum = neighbours
                .Sum(kvp => kvp.Value.Address);

            Console.WriteLine($"Sum of Neighbours of {point}: {sum}");

            return (uint)sum;
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
            const string title = "CRAZY FUCKING MAP CHALLENGE";
            sb.AppendLine(title);
            sb.AppendLine(new string('=', title.Length));

            var allPoints = map.Addresses.Keys;

            var xMin = allPoints.Min(p => p.X);
            var xMax = allPoints.Max(p => p.X);
            var yMin = allPoints.Min(p => p.Y);
            var yMax = allPoints.Max(p => p.Y);

            int maxDigits = map.Addresses.Values.Max(address => address.Address).ToString().Length;

            sb.AppendLine($"Seeking: {map.Seeking} || Grid: {xMin}-{xMax} / {yMin}-{yMax} || {allPoints.Count} Point(s) Mapped");

            for (int y = yMax; y >= yMin; y--)
            {
                for (int x = xMin; x <= xMax; x++)
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
            //return "Disabled";
        }
    }

    [TestFixture]
    public class Day3
    {
        private uint Solve(uint seeking)
        {
            var map = new Map(seeking);

            Console.WriteLine(map);

            return map.FirstValueLargerThanSeeking;
        }

        [TestCase(1, 2)]
        [TestCase(3, 4)]
        [TestCase(24, 25)]
        [TestCase(55, 57)]
        [TestCase(332, 351)]
        public void Solve_SampleSpreadsheet_Produces_Expected(int seeking, int expectedValue)
        {
            var solution = Solve((uint)seeking);

            Assert.That(solution, Is.EqualTo((uint)expectedValue));
        }

        [Test]
        //[Ignore("WIP")]
        public void Solve_ChallengeInput_Produces_WinningResult()
        {
            var solution = Solve(289326);

            Console.WriteLine($"Solution is... (drum roll): {solution}");
        }
    }
}