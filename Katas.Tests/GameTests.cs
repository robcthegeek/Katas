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
                () => new Game(player1, player2));
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

        [Test]
        public void Start_CoinFlipsHeads_SetsPlayer1ToBlack()
        {
            _coinFlipper.Flip().Returns(CoinSide.Heads);

            _game.Start();

            Assert.That(_game.BlackPlayer, Is.SameAs(_player1));
        }
    }
}