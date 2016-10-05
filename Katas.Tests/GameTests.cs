using System;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Katas.Tests
{
    public class GameTests : AutoTestFixture
    {
        private ICoinFlipper _coinFlipper;
        private Player _player1;
        private Player _player2;
        private Game _game;

        [SetUp]
        public void SetUp()
        {
            _coinFlipper = Fixture.Freeze<ICoinFlipper>();
            _game = Fixture.Create<Game>();
            _player1 = _game.Player1;
            _player2 = _game.Player2;

        }

        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void Ctor_NullPlayer_ThrowsMissingPlayerException(bool includeP1, bool includeP2)
        {
            var player1 = includeP1 ? Fixture.Create<Player>() : null;
            var player2 = includeP2 ? Fixture.Create<Player>() : null;

            Assert.Throws<MissingPlayerException>(
                () => new Game(player1, player2, _coinFlipper));
        }

        [Test]
        public void Ctor_NullFlipper_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => new Game(_player1, _player2, null));
        }

        [Test]
        public void Ctor_PlayersGiven_CreatesBoard()
        {
            var board = _game.Board;

            Assert.That(board, Is.Not.Null);
        }

        [Test]
        public void Ctor_PlayersGiven_SetsStatusToWaiting()
        {
            Assert.That(_game.Status, Is.EqualTo(GameStatus.Waiting));
        }

        [Test]
        public void Ctor_PlayersGiven_ColoursUnassigned()
        {
            Assert.That(_game.WhitePlayer, Is.Null);
            Assert.That(_game.BlackPlayer, Is.Null);
        }

        [TestCase(CoinSide.Heads, true)]
        [TestCase(CoinSide.Tails, false)]
        public void Start_CoinFlips_SetsPlayersBasedOnHeadsTails(CoinSide flip, bool player1Wins)
        {
            // Heads - Player 1 Wins, Tails - Player 2
            _coinFlipper.Flip().Returns(flip);

            var expectedBlack = player1Wins ? _player1 : _player2;
            var expectedWhite = player1Wins ? _player2 : _player1;

            _game.Start();

            Assert.That(_game.BlackPlayer, Is.SameAs(expectedBlack));
            Assert.That(_game.WhitePlayer, Is.SameAs(expectedWhite));
        }

        [Test]
        public void Start_CoinFlipped_SetsStatusToPlaying()
        {
            _game.Start();

            Assert.That(_game.Status, Is.EqualTo(GameStatus.Playing));
        }

        [Test]
        public void Start_CoinFlipped_SetsNextPlayerToBlackPlayer()
        {
            _game.Start();

            var expected = _game.BlackPlayer;

            Assert.That(_game.NextPlayer, Is.SameAs(expected));
        }
    }
}