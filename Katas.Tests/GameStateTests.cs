using System.Collections.Generic;
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

            var state = new GameState(
                new List<FactoryLink> {l1},
                new List<Entity> {f1, f2, t1, t2});

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

            var state = new GameState(
                new List<FactoryLink> {l1},
                new List<Entity> {f1, f2, t1, t2});

            var troops = state.Troops;

            Assert.That(troops.Count, Is.EqualTo(2));
            Assert.That(troops, Contains.Item(t1));
            Assert.That(troops, Contains.Item(t2));
        }

        // TODO (RC): GameState - Players Possible Moves
        // TODO (RC): GameState - Enemies Possible Moves
        // TODO (RC): GameState - Balance of Power

        // TODO (RC): Projection - Move vs. GameState = Power Shift
    }
}