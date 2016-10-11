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
    }
}