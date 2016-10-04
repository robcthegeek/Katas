using Katas.Model;
using NUnit.Framework;

namespace Katas.Tests
{
    [TestFixture]
    public class CheckerTests
    {
        [Test]
        public void MoveUpRight_InBottomLeft_EndsInR1C1()
        {
            var board = new Board();
            var piece = board.AddPiece(0, 0);

            piece.MoveUpRight();

            Assert.That(piece.Column, Is.EqualTo(1));
            Assert.That(piece.Row, Is.EqualTo(1));
        }

        [Test]
        public void MoveUpLeft_InBottomRight_EndsInR1C1()
        {
            var board = new Board();
            var piece = board.AddPiece(7, 0);

            piece.MoveUpLeft();

            Assert.That(piece.Column, Is.EqualTo(6));
            Assert.That(piece.Row, Is.EqualTo(1));
        }

        [Test]
        public void MoveUpLeft_ThrowsException_If_We_are_in_bottomleft()
        {
            var board = new Board();
            var piece = board.AddPiece(0, 0);

            piece.MoveUpLeft();

            Assert.Throws<IllegalMoveException>(() => piece.MoveUpLeft());

        }

    }
}
