using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
public class Solution
{
    static void Main(string[] args)
    {
        Console.ReadLine(); // Don't care about the array size.
        var result = Solve(Console.ReadLine().ToIntArray());
        Console.WriteLine($"{result}");
    }

    public static int Solve(int[] values)
    {
        var highestPrice = -1;
        var worstLoss = -1;

        values.Aggregate()

        for (int i = 0; i < values.Length; i++)
        {
            int v = values[i];

            if (v >= highestPrice)
            {
                highestPrice = v;
            }
            else
            {
                if (highestPrice - v > worstLoss) worstLoss = highestPrice - v;
            }
        }

        var result = worstLoss > 0 ? 0 - worstLoss : 0;
        return result;
    }
}

public static class Extensions
{
    public static int[] ToIntArray(this string input)
    {
        return input.Split(' ').Select(int.Parse).ToArray();
    }
}