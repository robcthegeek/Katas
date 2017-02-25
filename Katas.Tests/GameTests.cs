using NUnit.Framework;

namespace Katas.Tests
{
    public class GameTests
    {
        // TODO (RC): Build Picture of "Game" (pieces in play)..

        // TODO (RC): Ensure First Turn < 100ms

        // TODO (RC): Ensure Other Turns < 50ms

        [Test]
        public void NextMove_OnlyTwoFactoriesAndPlayerHasMoreTroops_ReturnsMoveEnemyFactory()
        {
            // 2 Factories, 1 Link, 2 Different Players
            var f1 = new Factory(1, Owner.Player, 10, 5);
            var f2 = new Factory(2, Owner.Enemy, 1, 5);

            var game = new Game(2, 1, new []
            {
                new FactoryLink(1, 2, 5),
            });

            game.SetState(new [] { f1, f2 });

            var move = (MoveAction)game.NextMove();

            Assert.That(move.Source, Is.EqualTo(1));
            Assert.That(move.Destination, Is.EqualTo(2));
            Assert.That(move.CyborgCount, Is.EqualTo(10));
        }
    }
}