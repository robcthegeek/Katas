using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Katas.Tests
{
    public class WarMachineTests
    {
 [Test]
        public void NextMove_OneEnemyWithLessCyborgs_MovesUnitsToEnemyFactory()
        {
            var l1 = new FactoryLink(1, 2, 1);
            var f1 = new Factory(1, Owner.Player, 5, 3);
            var f2 = new Factory(2, Owner.Enemy, 1, 3);

            var state = new GameState(new List<FactoryLink> { l1 });
            state.AddEntities(f1, f2);

            var machine = new WarMachine(state);

            var move = machine.NextMove() as MoveAction;

            Assert.That(move.Source, Is.EqualTo(f1.Id));
            Assert.That(move.Destination, Is.EqualTo(f2.Id));
            Assert.That(move.CyborgCount, Is.EqualTo(f1.Cyborgs));
        }

        [Test]
        public void NextMove_TwoEnemyOneWithLessCyborgs_MovesUnitsToWeakestEnemyFactory()
        {
            var l1 = new FactoryLink(1, 2, 1);
            var l2 = new FactoryLink(1, 3, 1);
            var f1 = new Factory(1, Owner.Player, 5, 3);
            var f2 = new Factory(2, Owner.Enemy, 6, 3);
            var f3 = new Factory(3, Owner.Enemy, 1, 3);

            var state = new GameState(new List<FactoryLink> { l1, l2 });
            state.AddEntities(f1, f2, f3);

            var machine = new WarMachine(state);

            var move = machine.NextMove() as MoveAction;

            Assert.That(move.Source, Is.EqualTo(f1.Id));
            Assert.That(move.Destination, Is.EqualTo(f3.Id));
            Assert.That(move.CyborgCount, Is.EqualTo(f1.Cyborgs));
        }

        // TODO (RC): Wait if Outnumbered
    }
}