using System;
using System.Collections.Generic;
using System.Text;
using Katas;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Determines_Correct_Player()
        {
            Assert.That(SUT.IsThere);
        }
    }
}
