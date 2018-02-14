using NUnit.Framework;

namespace Katas.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test()
        {
            Assert.True(Defib.Solve());
        }
    }
}