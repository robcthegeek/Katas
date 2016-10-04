using Katas.Model;
using NUnit.Framework;

namespace Katas.Tests
{
    [TestFixture]
    public class CheckerTests
    {
        [Test]
        public void CanMoveOneSteps()
        {
            var board = new Board();
            var piece = board.AddPiece(0, 0);

            piece.MoveUp();

            Assert.That(piece.Column, Is.EqualTo(1));
            Assert.That(piece.Row, Is.EqualTo(1));

        }

    }
}
