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
        Func<int> readInt = () => int.Parse(Console.ReadLine());
        var horses = new int[readInt()].Select(idx => readInt());
        Console.WriteLine(HorseStrengths.DifferenceBetweenClosest(horses));
    }
}

public static class HorseStrengths
{
    public static int DifferenceBetweenClosest(IEnumerable<int> horses)
    {
        int closest = -1;
        int previous = -1;
        foreach (var horse in new List<int>(horses).OrderBy(x => x))
        {
            var difference = horse - previous;
            closest = closest == -1 ? difference : Math.Min(difference, closest);
            previous = horse;
        }

        return closest;
    }
}