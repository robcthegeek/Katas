using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace Katas.Tests
{
    [TestFixture]
    public class DayOne
    {
        private int Solve(params int[] digits)
        {
            var list = digits.ToList();
            list.Add(digits.Last());

            var sum = 0;
            var previous = digits.Last(); // Rem it's Circular

            foreach (var digit in digits)
            {
                Console.WriteLine($"Current Digit: '{digit}', Previous: '{previous}', Sum: '{sum}'");

                if (digit == previous)
                {
                    sum += digit;
                }

                previous = digit;
            }

            return sum;
        }

        private int Solve(string digits)
        {
            var split = digits
                .ToCharArray()
                .Select(c => int.Parse(c.ToString()))
                .ToArray();

            return Solve(split);
        }

        [Test]
        public void Solve_1122_Produces_3()
        {
            var solution = Solve(1, 1, 2, 2);

            Assert.That(solution, Is.EqualTo(3));
        }

        [Test]
        public void Solve_1111_Produces_4()
        {
            var solution = Solve(1, 1, 1, 1);

            Assert.That(solution, Is.EqualTo(4));
        }

        [Test]
        public void Solve_91212129_Produces_9()
        {
            var solution = Solve("91212129");

            Assert.That(solution, Is.EqualTo(9));
        }

        [Test]
        public void Solve_ChallengeInput_Produces_WinningResult()
        {
            var solution = Solve("91212129");

            Assert.That(solution, Is.EqualTo(9));
        }
    }
}