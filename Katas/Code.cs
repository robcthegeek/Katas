using System;
using System.Linq;
using System.Collections.Generic;

class Player
{
    static void Main(string[] args)
    {
        var input = Console.ReadLine();
        var initState = new InitState(input);

        Enumerable.Range(0, initState.NumElevators).ToList()
            .ForEach(i => initState.AddElevator(Console.ReadLine()));

        var panic = new DontPanic(initState);

        while (true)
        {
            var inputs = Console.ReadLine().Split(' ');
            var cloneFloor = inputs.AsInt(0);
            var clonePos = inputs.AsInt(1);
            var direction = inputs[2];

            Console.WriteLine(panic.Next(cloneFloor, clonePos, direction));
        }
    }

    public class DontPanic
    {
        private readonly InitState _state;

        public DontPanic(InitState state)
        {
            _state = state;
        }

        public string Next(int cloneFloor, int clonePos, string direction)
        {
            if (direction == Direction.None)
                return Action.Wait;

            var targetPos = _state.ExitFloor == cloneFloor
                ? _state.ExitPosition
                : _state.Elevators.Single(x => x.Floor == cloneFloor).Position;

            if (GoingRightWay(direction, clonePos, targetPos))
                return Action.Wait;

            if (GoingWrongWay(direction, clonePos, targetPos))
                return Action.Block;

            return Action.Wait;
        }

        private bool GoingRightWay(string direction, int clonePos, int targetPos)
        {
            return direction == Direction.Left && targetPos < clonePos ||
                   direction == Direction.Right && targetPos > clonePos;
        }

        private bool GoingWrongWay(string direction, int clonePos, int targetPos)
        {
            return direction == Direction.Left && targetPos > clonePos ||
                   direction == Direction.Right && targetPos < clonePos;
        }
    }
}

public class InitState
{
    public int Floors { get; set; }
    public int Width { get; set; }
    public int Rounds { get; set; }
    public int ExitFloor { get; set; }
    public int ExitPosition { get; set; }
    public int TotalClones { get; set; }
    public int NumElevators { get; set; }
    public int AdditionalElevators { get; set; }
    public List<Elevator> Elevators { get; set; }

    public InitState(string input)
    {
        var inputs = input.Split(' ');
        Floors = inputs.AsInt(0);
        Width = inputs.AsInt(1);
        Rounds = inputs.AsInt(2);
        ExitFloor = inputs.AsInt(3);
        ExitPosition = inputs.AsInt(4);
        TotalClones = inputs.AsInt(5);
        AdditionalElevators = inputs.AsInt(6);
        NumElevators = inputs.AsInt(7);
        Elevators = new List<Elevator>();
    }

    public void AddElevator(string input)
    {
        var inputs = input.Split(' ');
        Elevators.Add(new Elevator
        {
            Floor = inputs.AsInt(0),
            Position = inputs.AsInt(1)
        });
    }
}

public class Elevator
{
    public int Floor { get; set; }
    public int Position { get; set; }
}

public static class Direction
{
    internal const String Left = "LEFT";
    internal const String Right = "RIGHT";
    internal const String None = "NONE";
}

public static class Action
{
    internal const string Wait = "WAIT";
    internal const string Block = "BLOCK";
}

public static class Extensions
{
    internal static int AsInt(this string[] array, int index)
    {
        return int.Parse(array[index]);
    }
}