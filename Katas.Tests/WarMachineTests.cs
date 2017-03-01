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
            var state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 3)
                .EnemyFactory(2, 1, 3)
                .Link(1, 2, 1);

            var machine = new WarMachine(state);

            var move = machine.NextMove() as MoveAction;

            Assert.That(move.Source, Is.EqualTo(1));
            Assert.That(move.Destination, Is.EqualTo(2));
            Assert.That(move.CyborgCount, Is.EqualTo(5));
        }

        [Test]
        public void NextMove_TwoEnemyOneWithLessCyborgs_MovesUnitsToWeakestEnemyFactory()
        {
            var state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 3)
                .EnemyFactory(2, 6, 3)
                .EnemyFactory(3, 1, 3)
                .Link(1, 2, 1)
                .Link(1, 3, 1);

            var machine = new WarMachine(state);

            var move = machine.NextMove() as MoveAction;

            Assert.That(move.Source, Is.EqualTo(1));
            Assert.That(move.Destination, Is.EqualTo(3));
            Assert.That(move.CyborgCount, Is.EqualTo(5));
        }

        [Test]
        public void NextMove_TwoEnemyWithEqualCyborgs_MovesUnitsToQuickestEnemyFactory()
        {
            var state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 3)
                .EnemyFactory(2, 3, 3)
                .EnemyFactory(3, 3, 1)
                .Link(1, 2, 1)
                .Link(1, 3, 1);

            var machine = new WarMachine(state);

            var move = machine.NextMove() as MoveAction;

            Assert.That(move.Source, Is.EqualTo(1));
            Assert.That(move.Destination, Is.EqualTo(3));
            Assert.That(move.CyborgCount, Is.EqualTo(5));
        }

        // TODO (RC): Take into account troops en route.

        // TODO (RC): Wait if Outnumbered
    }

    public class GameStateBuilder
    {
        public static GameStateBuilder With => new GameStateBuilder();

        private readonly List<Entity> _entities;
        private readonly List<FactoryLink> _links;

        public GameStateBuilder()
        {
            _entities = new List<Entity>();
            _links = new List<FactoryLink>();
        }

        public GameStateBuilder PlayerFactory(int id, int cyborgs, int production)
        {
            _entities.Add(new Factory(id, Owner.Player, cyborgs, production));
            return this;
        }

        public GameStateBuilder EnemyFactory(int id, int cyborgs, int production)
        {
            _entities.Add(new Factory(id, Owner.Enemy, cyborgs, production));
            return this;
        }

        public GameStateBuilder Link(int factory1, int factory2, int distance)
        {
            _links.Add(new FactoryLink(factory1, factory2, distance));
            return this;
        }

        public static implicit operator GameState(GameStateBuilder factory)
        {
            var state = new GameState(factory._links);
            state.AddEntities(factory._entities.ToArray());
            return state;
        }
    }
}