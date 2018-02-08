using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Katas;
using NUnit.Framework;

namespace Katas.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void AllHorsesTie()
        {
            var actual = HorseStrengths.DifferenceBetweenClosest(new[] { 2, 2, 2, 2 });
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void SimpleCase()
        {
            var actual = HorseStrengths.DifferenceBetweenClosest(new[] { 3, 5, 8, 9 });
            Assert.AreEqual(1, actual);
        }

        [Test]
        public void HorsesInAnyOrder()
        {
            var actual = HorseStrengths.DifferenceBetweenClosest(new[] { 10, 5, 15, 17, 3, 8, 11, 28, 6, 55, 7 });
            Assert.AreEqual(1, actual);
        }
    }
}
