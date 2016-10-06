using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Katas.Tests
{
    public class BoardTests : AutoTestFixture
    {
        private Board _board;

        [SetUp]
        public void SetUp()
        {
            _board = Fixture.Create<Board>();
        }

        [Test]
        public void Ctor_DefaultParams_Creates8Rows8Columns()
        {
            Assert.That(_board.Rows, Is.EqualTo(8));
            Assert.That(_board.Columns, Is.EqualTo(8));
        }

        [Test]
        public void Ctor_DefaultParams_Creates32Places()
        {
            // We only need to remember 32 Places as you can't use White squares in Checkers.
            Assert.That(_board.Places.Count, Is.EqualTo(32));
        }

        [Test]
        public void Ctor_NoPiecesPlaced_HasNoPieces()
        {
            Assert.That(_board.Pieces.Count, Is.EqualTo(0));
        }

        [Test]
        public void Reset_AfterCtor_Places24Pieces()
        {
            _board.Reset();

            Assert.That(_board.Pieces.Count, Is.EqualTo(24));
        }

        [Test]
        public void Reset_AfterCtor_Places12WhitePieces()
        {
            _board.Reset();

            var whitePieces = _board.Pieces.Where(x => x.Color == PieceColor.White);

            Assert.That(whitePieces.Count(), Is.EqualTo(12));
        }

        [Test]
        public void Reset_AfterCtor_Places12BlackPieces()
        {
            _board.Reset();

            var blackPieces = _board.Pieces.Where(x => x.Color == PieceColor.Black);

            Assert.That(blackPieces.Count(), Is.EqualTo(12));
        }
    }
}