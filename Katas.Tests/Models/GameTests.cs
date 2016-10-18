using System;
using Katas.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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

        [Test]
        public void Reset_NoParams_PutswhitePieceInPlace32()
        {
            var game = new Game();

            game.Reset();

            Assert.That(game.Board.Square(32).Piece.Color, Is.EqualTo(PieceColor.White));
        }

        [Test]
        public void Reset_CheckAll32Locations_Returns24Pieces()
        {
            var game = new Game();

            game.Reset();

            var pieces = new List<Piece>();

            for (int i = 1; i < 33; i++)
            {
                var square = game.Board.Square(i);
                if (square.Piece != null)
                {
                    pieces.Add(square.Piece);
                }
            }

            Assert.That(pieces.Count, Is.EqualTo(24));
        }

        [Test]
        public void Reset_RequestSameLocationTwice_ReturnsSamePiece()
        {
            var game = new Game();

            game.Reset();

            var square1 = game.Board.Square(1);
            var square1Again = game.Board.Square(1);

            Assert.That(square1, Is.SameAs(square1Again));
        }

        [Test]
        public void Start_PlayerOneIsNull_ThrowsArgumentNullException()
        {
            var game = new Game();

            Assert.Throws<ArgumentNullException>(
                () => game.Start(null, new Player()));
        }
    }
}