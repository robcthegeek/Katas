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

        [Test]
        public void FactoryMeta_RearGuardAndFrontLinePlayers_SetsIsRearGuardCorrectly()
        {
            GameState state = GameStateBuilder
                .With
                .PlayerFactory(1, 10, 0) // Rear Guard
                .PlayerFactory(2, 5, 0)
                .EnemyFactory(3, 10, 0)
                .Link(1, 2, 5)
                .Link(2, 3, 10);

            Assert.That(state.Factory(1).IsRearGuard, Is.True);
            Assert.That(state.Factory(2).IsRearGuard, Is.False);
        }

        [Test]
        public void FactoryMeta_MoreThan10Cyborgs_CanIncreaseProd()
        {
            GameState state = GameStateBuilder
                .With
                .PlayerFactory(1, 11, 0)
                .EnemyFactory(2, 5, 0)
                .Link(1, 2, 5);

            Assert.That(state.Factory(1).CanIncreaseProd, Is.True);
            Assert.That(state.Factory(2).CanIncreaseProd, Is.False);
        }

        [Test]
        public void FactoryMeta_WithPlayerTroopEnRoute_SetsPlayerEnRoute()
        {
            GameState state = GameStateBuilder
                .With
                .PlayerFactory(1, 11, 0)
                .EnemyFactory(2, 5, 0)
                .Link(1, 2, 5)
                .PlayerTroop(1, 1, 2, 10, 1)
                .PlayerTroop(2, 1, 2, 10, 1);

            Assert.That(state.Factory(2).PlayerEnRoute, Is.EqualTo(20));
        }

        [Test]
        public void FactoryMeta_WithEnemyTroopEnRoute_SetsEnemyEnRoute()
        {
            GameState state = GameStateBuilder
                .With
                .PlayerFactory(1, 11, 0)
                .EnemyFactory(2, 5, 0)
                .Link(1, 2, 5)
                .EnemyTroop(1, 2, 1, 10, 1)
                .EnemyTroop(2, 2, 1, 10, 1);

            Assert.That(state.Factory(1).EnemyEnRoute, Is.EqualTo(20));
        }

        [Test]
        public void FactoryMeta_WithBothTroopsEnRoute_CalculatesForceRequiredToCapture()
        {
            GameState state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 0)
                .NeutralFactory(2, 0, 0) // << Target
                .EnemyFactory(3, 5, 0)
                .Link(1, 2, 5)
                .Link(2, 3, 5)
                .PlayerTroop(1, 1, 2, 10, 1)
                .EnemyTroop(2, 3, 2, 10, 1);
            //  5 in base +
            //  1 to capture +
            // 10 Enemy en-route -
            // 10 Player en-route =
            //  6 to win overall

            Assert.That(state.Factory(1).ForceRequiredToCapture, Is.EqualTo(0));
            Assert.That(state.Factory(2).ForceRequiredToCapture, Is.EqualTo(1));
            Assert.That(state.Factory(3).ForceRequiredToCapture, Is.EqualTo(6));
        }

        [Test]
        public void FactoryMeta_WhenLinked_SetsLinkedFactoryIds()
        {
            GameState state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 0)
                .EnemyFactory(2, 5, 0)
                .NeutralFactory(3, 5, 0) // Forever Alone
                .Link(1, 2, 5);

            Assert.That(state.Factory(1).LinkedFactories, Contains.Item(2));
            Assert.That(state.Factory(2).LinkedFactories, Contains.Item(1));
            Assert.That(state.Factory(3).LinkedFactories, Is.Empty);
        }

        [Test]
        public void FactoryMeta_WhenCaptureImminent_SetsCapturing()
        {
            GameState state = GameStateBuilder
                .With
                .PlayerFactory(1, 5, 0)
                .EnemyFactory(2, 5, 0)
                .EnemyFactory(3, 5, 0)
                .PlayerTroop(1, 1, 2, 6, 1)
                .Link(1, 2, 5)
                .Link(2, 3, 5);

            Assert.That(state.Factory(2).Capturing, Is.True);
            Assert.That(state.Factory(3).Capturing, Is.False);
        }

        // TODO (RC): Add RequiredDefense - Enemy Neighbours
        // TODO (RC): Add Cyborgs to Meta for Ease of Use
        // TODO (RC): Remove Factory Links - Use Meta Instead
    }
}