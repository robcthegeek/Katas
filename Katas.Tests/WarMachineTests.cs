using System.Linq;
using NUnit.Framework;

namespace Katas.Tests
{
    public class WarMachineTests
    {
        [Test]
        public void NextActions_OneEnemyWithLessCyborgs_MovesMinimalUnitsToEnemyFactory()
        {
            var state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 3)
                .EnemyFactory(2, 1, 0)
                .Link(1, 2, 1);

            var machine = new WarMachine(state);

            var move = machine.NextActions().Single() as MoveAction;

            Assert.That(move.Source, Is.EqualTo(1));
            Assert.That(move.Destination, Is.EqualTo(2));
            Assert.That(move.CyborgCount, Is.EqualTo(2));
        }

        [Test]
        public void NextActions_TwoEnemyOneWithLessCyborgs_MovesUnitsToWeakestEnemyFactory()
        {
            var state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 0)
                .EnemyFactory(2, 6, 0)
                .EnemyFactory(3, 1, 0)
                .Link(1, 2, 1)
                .Link(1, 3, 1);

            var machine = new WarMachine(state);

            var move = machine.NextActions().Single() as MoveAction;

            Assert.That(move.Source, Is.EqualTo(1));
            Assert.That(move.Destination, Is.EqualTo(3));
            Assert.That(move.CyborgCount, Is.EqualTo(2));
        }

        [Test]
        public void NextActions_TwoEnemyWithEqualCyborgs_MovesUnitsToQuickestEnemyFactory()
        {
            var state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 0)
                .EnemyFactory(2, 4, 0)
                .EnemyFactory(3, 4, 0)
                .Link(1, 2, 5)
                .Link(1, 3, 1); // Shortest Walk ;)

            var machine = new WarMachine(state);

            var move = machine.NextActions().Single() as MoveAction;

            Assert.That(move.Source, Is.EqualTo(1));
            Assert.That(move.Destination, Is.EqualTo(3));
            Assert.That(move.CyborgCount, Is.EqualTo(5));
        }

        [Test]
        public void NextMoves_OneBaseLinkedToTwoEnemySteamRoller_MovesMinimalUnitsToBothEnemyFactories()
        {
            var state = GameStateBuilder
                .With
                .PlayerFactory(1, 100, 3)
                .EnemyFactory(2, 1, 3)
                .EnemyFactory(3, 1, 3)
                .Link(1, 2, 1)
                .Link(1, 3, 1);

            var machine = new WarMachine(state);

            var moves = machine.NextActions().OfType<MoveAction>().ToList();

            var t2 = moves.Single(x => x.Destination == 2);
            Assert.That(t2.CyborgCount, Is.EqualTo(2));
            var t3 = moves.Single(x => x.Destination == 3);
            Assert.That(t3.CyborgCount, Is.EqualTo(2));
        }

        // TODO (RC): Take neutral bases!
        // TODO (RC): Factor in Travel Time * Production
        // TODO (RC): Take into account troops en route.
        // TODO (RC): Move troops from isolated player base (not connected to enemy or neutral - move to the front to SMASH THE EN
        // TODO (RC): Ensure NextActions calc < 50ms (First can be 100ms)
        // TODO (RC): Take into account troops en route.
        // TODO (RC): Add INC Action to increase production
        // TODO (RC): Wait if Outnumbered
        // TODO (RC): Add Bomb Action
        // TODO (RC): Bombs can be really useful for taking neutral targets etc.
    }
}
