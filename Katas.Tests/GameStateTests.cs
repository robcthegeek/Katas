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
            GameState state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 3)
                .EnemyFactory(2, 5, 3)
                .Link(1, 2, 1)
                .PlayerTroop(3, 1, 2, 1, 1)
                .EnemyTroop(4, 1, 2, 1, 1);

            var factories = state.Factories;

            Assert.That(factories.Count, Is.EqualTo(2));
        }

        [Test]
        public void Troops_TwoFactoriesTwoTroops_ReturnsOnlyTroops()
        {
            GameState state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 3)
                .EnemyFactory(2, 5, 3)
                .Link(1, 2, 1)
                .PlayerTroop(3, 1, 2, 1, 1)
                .EnemyTroop(4, 1, 2, 1, 1);

            var troops = state.Troops;

            Assert.That(troops.Count, Is.EqualTo(2));
        }

        [Test]
        public void PossibleActions_TwoFactories_ReturnsPossibleWaitMove()
        {
            GameState state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 3)
                .EnemyFactory(2, 5, 3)
                .Link(1, 2, 1);

            var actions = state.PossibleActions();

            Assert.That(actions.Single(x => x.Action is WaitAction), Is.Not.Null);
        }

        [Test]
        public void PossibleActions_TwoFactories_ReturnsPossibleMoveAction()
        {
            GameState state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 3)
                .EnemyFactory(2, 5, 3)
                .Link(1, 2, 1);

            var actions = state.PossibleActions();

            var move = actions.Single(x => x.Action is MoveAction).Action as MoveAction;
            Assert.That(move.Source, Is.EqualTo(1));
            Assert.That(move.Destination, Is.EqualTo(2));
            Assert.That(move.CyborgCount, Is.EqualTo(5));
        }

        [Test]
        public void PossibleActions_TwoFactoriesLinkedToPlayer_ReturnsPossibleMoveAction()
        {
            GameState state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 3)
                .EnemyFactory(2, 5, 3)
                .EnemyFactory(3, 5, 3)
                .Link(1, 2, 1)
                .Link(1, 3, 1);

            var actions = state.PossibleActions();

            var moves = actions
                .Where(x => x.Action is MoveAction)
                .Select(x => x.Action as MoveAction)
                .ToList();

            Assert.That(moves.Count(), Is.EqualTo(2));
            Assert.That(moves.SingleOrDefault(x => x.Source == 1 && x.Destination == 2), Is.Not.Null);
            Assert.That(moves.SingleOrDefault(x => x.Source == 1 && x.Destination == 3), Is.Not.Null);
        }

        [Test]
        public void FactoryLinks_OneLink_LinksBothSides()
        {
            GameState state = GameStateBuilder
                .With
                .PlayerFactory(1, 1, 3)
                .EnemyFactory(1, 1, 3)
                .Link(1, 2, 3);

            var f1 = state.FactoryLinks[1];
            Assert.That(f1[0].Factory2, Is.EqualTo(2));

            var f2 = state.FactoryLinks[2];
            Assert.That(f2[0].Factory2, Is.EqualTo(1));
        }
    }
}