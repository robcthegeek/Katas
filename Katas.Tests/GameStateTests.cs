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