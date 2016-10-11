using System.Linq;
using Katas.Model;
using NUnit.Framework;

namespace Katas.Tests.Models
{
    public class GameTests
    {
        [Test]
        public void Ctor_NoParams_CreatesWhiteAndBlackPlayers()
        {
            var game = new Game();

            Assert.That(game.WhitePlayer, Is.Not.Null);
            Assert.That(game.BlackPlayer, Is.Not.Null);
        }

        [Test]
        public void Ctor_NoParams_CreateBlackAndWhitePieces()
        {
            var game = new Game();

            Assert.That(game.Pieces.Count(x => x.Color == PieceColor.White), Is.EqualTo(12));
            Assert.That(game.Pieces.Count(x => x.Color == PieceColor.Black), Is.EqualTo(12));
        }

        [Test]
        public void Reset_NoParams_PutsBlackPieceInPlace1()
        {
            var game = new Game();

            game.Reset();

            Assert.That(game.Board.Square(1).Piece.Color, Is.EqualTo(PieceColor.Black));
        }
    }
}