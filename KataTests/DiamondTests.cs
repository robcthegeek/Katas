using System;
using System.Collections.Generic;
using Katas;
using Xunit;
using Xunit.Sdk;

namespace KataTests
{
    public class DiamondTests
    {
        [Fact]
        public void A_returns_just_a()
        {
            AssertDiamond.Equal(
                'A',
                "A");
        }

        [Fact]
        public void B_returns_expected_diamond()
        {
            AssertDiamond.Equal(
                'B',
                " A ",
                "B B",
                " A ");
        }
    }

    public static class AssertDiamond
    {
        static string Lined(IEnumerable<string> lines) => string.Join(Environment.NewLine, lines);

        public static void Equal(char diamond, params string[] expected)
        {
            var exp = Lined(expected);
            string actual = Diamond.For(diamond);

            if (exp != actual)
            {
                throw new EqualException($"{Environment.NewLine}{exp.Replace(' ', '.')}", $"{Environment.NewLine}{actual.Replace(' ', '.')}");
            }
        }
    }
}
