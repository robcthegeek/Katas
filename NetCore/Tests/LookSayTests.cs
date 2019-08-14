using System;
using System.Collections.Generic;
using System.Text;
using Katas;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class LookSayTests
    {
        [TestCase(1U, "11")]
        [TestCase(11U, "21")]
        [TestCase(21U, "1211")]
        [TestCase(1211U, "111221")]
        [TestCase(111221U, "312211")]
        public void ReturnsDigitOutput(uint input, string expected)
        {
            Assert.AreEqual(expected, LookSay.Digits(input));
        }
    }
}
