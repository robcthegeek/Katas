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
            var game = CreateGame();

            Assert.That(game.WhitePlayer, Is.Not.Null);
            Assert.That(game.BlackPlayer, Is.Not.Null);
        }

        [Test]
        public void Ctor_NoParams_CreateBlackAndWhitePieces()
        {
            var game = CreateGame();

            Assert.That(game.Pieces.Count(x => x.Color == PieceColor.White), Is.EqualTo(12));
            Assert.That(game.Pieces.Count(x => x.Color == PieceColor.Black), Is.EqualTo(12));
        }

        [Test]
        public void Reset_NoParams_PutsBlackPieceInPlace1()
        {
            var game = CreateGame();

            game.Reset();

            Assert.That(game.Board.Square(1).Piece.Color, Is.EqualTo(PieceColor.Black));
        }

        [Test]
        public void Reset_NoParams_PutswhitePieceInPlace32()
        {
            var game = CreateGame();

            game.Reset();

            Assert.That(game.Board.Square(32).Piece.Color, Is.EqualTo(PieceColor.White));
        }

        [Test]
        public void Reset_CheckAll32Locations_Returns24Pieces()
        {
            var game = CreateGame();

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
            var game = CreateGame();

            game.Reset();

            var square1 = game.Board.Square(1);
            var square1Again = game.Board.Square(1);

            Assert.That(square1, Is.SameAs(square1Again));
        }

        [Test]
        public void Start_PlayerOneIsNull_ThrowsArgumentNullException()
        {
            var game = CreateGame();

            Assert.Throws<ArgumentNullException>(
                () => game.Start(null, new Player()));
        }

        [Test]
        public void Start_PlayerTwoIsNull_ThrowsArgumentNullException()
        {
            var game = CreateGame();

            Assert.Throws<ArgumentNullException>(
                () => game.Start(new Player(), null));
        }

        [Test]
        public void Start_CoinFlipPicksBlackPlayer_Player1IsBlackPlayer2IsWhite()
        {
            var flipper = new StaticCoinFlipper(PlayerType.Black);

            var game = new Game(flipper);

            var player1 = new Player();
            var player2 = new Player();

            game.Start(player1, player2);

            Assert.That(game.BlackPlayer, Is.SameAs(player1));
            Assert.That(game.WhitePlayer, Is.SameAs(player2));
        }

        [Test]
        public void Start_CoinFlipPicksWhitePlayer_Player1IsWhitePlayer2IsBlack()
        {
            var flipper = new StaticCoinFlipper(PlayerType.White);

            var game = new Game(flipper);

            var player1 = new Player();
            var player2 = new Player();

            game.Start(player1, player2);

            Assert.That(game.BlackPlayer, Is.SameAs(player2));
            Assert.That(game.WhitePlayer, Is.SameAs(player1));
        }

        [Test]
        public void Start_CoinFlipped_NextPlayerIsBlackPlayer()
        {
            var flipper = new StaticCoinFlipper(PlayerType.Black);

            var game = new Game(flipper);

            game.Start(new Player(), new Player());

            Assert.That(game.NextPlayer, Is.SameAs(game.BlackPlayer));
        }

        private static Game CreateGame()
        {
            return new Game(new StaticCoinFlipper(PlayerType.Black));
        }
    }

    public class StaticCoinFlipper : ICoinFlipper
    {
        private readonly PlayerType _result;

        public StaticCoinFlipper(PlayerType result)
        {
            _result = result;
        }

        public PlayerType PickPlayer()
        {
            return _result;
        }
    }
}