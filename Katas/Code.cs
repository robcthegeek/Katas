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
class Solution
{
    static void Main(string[] args)
    {
        var values = new List<int>();

        int n = int.Parse(Console.ReadLine());
        string[] inputs = Console.ReadLine().Split(' ');

        var best = -1;
        var losses = -1;

        for (int i = 0; i < n; i++)
        {
            int v = int.Parse(inputs[i]);

            if (v >= best)
            {
                best = v;
                //losses = 0;
            }
            else
            {
                losses = best - v;
            }
        }

        var result = losses > 0 ? 0 - losses : 0;
        Console.WriteLine($"{result}");
    }
}