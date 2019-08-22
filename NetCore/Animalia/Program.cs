using System;
using System.Collections;
using System.Collections.Generic;
using Katas;

namespace Animalia
{
    class Program
    {
        private static readonly string[] NiceThings = {
            "Nice!",
            "Awesome!",
            "Yes!",
            "Great!",
            "Well done!",
            "Nailing it!",
            "Amazing!"
        };

        static void Main(string[] args)
        {
            "Welcome to Animalia!".Output(ConsoleColor.Yellow);

            while (true)
            {
                Asciimals.Random.Output(ConsoleColor.Green);
                Console.WriteLine();
                "Please name your first animal:".Output(ConsoleColor.Magenta);

                var animalia = new Katas.Animalia("Animals.txt".ResourceText());

                int answers = 0;
                string input;
                while ((input = Console.ReadLine()) != string.Empty)
                {
                    if ((LowercaseString)input == "q")
                    {
                        "Bye!".Output(ConsoleColor.Cyan);
                        return;
                    }

                    if (!animalia.Say(input))
                    {
                        $"Nope! You've lost after {answers} animal{(answers != 1 ? "s" : "")}!".Output(ConsoleColor.Red);
                        break;
                    }

                    answers++;
                    NiceThings.Random().Output(ConsoleColor.Green);
                }
            }
        }
    }
}
