using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Katas.Tests
{
    public class BoardTests : AutoTestFixture
    {
        [Test]
        public void Ctor_DefaultParams_Creates8Rows8Columns()
        {
            var board = Fixture.Create<Board>();

            Assert.That(board.Rows, Is.EqualTo(8));
            Assert.That(board.Columns, Is.EqualTo(8));
        }
    }
}