using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Katas.Tests
{
    public class GameStateTests
    {
        [Test]
        public void Factories_TwoFactoriesTwoTroops_ReturnsOnlyFactories()
        {
            var l1 = new FactoryLink(1, 2, 1);
            var f1 = new Factory(1, Owner.Player, 5, 3);
            var f2 = new Factory(2, Owner.Enemy, 5, 3);
            var t1 = new Troop(3, Owner.Player, 1, 2, 1, 1);
            var t2 = new Troop(4, Owner.Enemy, 2, 1, 1, 1);

            var state = new GameState(new List<FactoryLink> { l1 });
            state.AddEntities(f1, f2, t1, t2);

            var factories = state.Factories;

            Assert.That(factories.Count, Is.EqualTo(2));
            Assert.That(factories, Contains.Item(f1));
            Assert.That(factories, Contains.Item(f2));
        }

        [Test]
        public void Troops_TwoFactoriesTwoTroops_ReturnsOnlyTroops()
        {
            var l1 = new FactoryLink(1, 2, 1);
            var f1 = new Factory(1, Owner.Player, 5, 3);
            var f2 = new Factory(2, Owner.Enemy, 5, 3);
            var t1 = new Troop(3, Owner.Player, 1, 2, 1, 1);
            var t2 = new Troop(4, Owner.Enemy, 2, 1, 1, 1);

            var state = new GameState(new List<FactoryLink> { l1 });
            state.AddEntities(f1, f2, t1, t2);

            var troops = state.Troops;

            Assert.That(troops.Count, Is.EqualTo(2));
            Assert.That(troops, Contains.Item(t1));
            Assert.That(troops, Contains.Item(t2));
        }

        [Test]
        public void PossibleActions_TwoFactories_ReturnsPossibleWaitMove()
        {
            // You can always wait :)
            var l1 = new FactoryLink(1, 2, 1);
            var f1 = new Factory(1, Owner.Player, 5, 3);
            var f2 = new Factory(2, Owner.Enemy, 5, 3);

            var state = new GameState(new List<FactoryLink> { l1 });
            state.AddEntities(f1, f2);

            var actions = state.PossibleActions();

            Assert.That(actions.Single(x => x.Action is WaitAction), Is.Not.Null);
        }

        [Test]
        public void PossibleActions_TwoFactories_ReturnsPossibleMoveAction()
        {
            var l1 = new FactoryLink(1, 2, 1);
            var f1 = new Factory(1, Owner.Player, 5, 3);
            var f2 = new Factory(2, Owner.Enemy, 5, 3);

            var state = new GameState(new List<FactoryLink> { l1 });
            state.AddEntities(f1, f2);

            var actions = state.PossibleActions();

            var move = actions.Single(x => x.Action is MoveAction).Action as MoveAction;
            Assert.That(move.Source, Is.EqualTo(f1.Id));
            Assert.That(move.Destination, Is.EqualTo(f2.Id));
            Assert.That(move.CyborgCount, Is.EqualTo(f1.Cyborgs));
        }

        [Test]
        public void PossibleActions_TwoFactoriesLinkedToPlayer_ReturnsPossibleMoveAction()
        {
            var l1 = new FactoryLink(1, 2, 1);
            var l2 = new FactoryLink(1, 3, 1);
            var f1 = new Factory(1, Owner.Player, 5, 3);
            var f2 = new Factory(2, Owner.Enemy, 5, 3);
            var f3 = new Factory(3, Owner.Enemy, 5, 3);

            var state = new GameState(new List<FactoryLink> { l1, l2 });
            state.AddEntities(f1, f2, f3);

            var actions = state.PossibleActions();

            var moves = actions
                .Where(x => x.Action is MoveAction)
                .Select(x => x.Action as MoveAction)
                .ToList();

            Assert.That(moves.Count(), Is.EqualTo(2));
            Assert.That(moves.SingleOrDefault(x => x.Source == 1 && x.Destination == 2), Is.Not.Null);
            Assert.That(moves.SingleOrDefault(x => x.Source == 1 && x.Destination == 3), Is.Not.Null);
        }
    }
}