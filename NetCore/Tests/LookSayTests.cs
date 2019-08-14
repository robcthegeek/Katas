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
        [Test]
        public void Test()
        {
            Assert.Null(LookSay.Next(0));
        }
    }
}
