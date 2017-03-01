using System.Collections.Generic;

namespace Katas.Tests
{
    internal class GameStateBuilder
    {
        public static GameStateBuilder With => new GameStateBuilder();

        private readonly List<Entity> _entities;
        private readonly List<FactoryLink> _links;

        public GameStateBuilder()
        {
            _entities = new List<Entity>();
            _links = new List<FactoryLink>();
        }

        public GameStateBuilder NeutralFactory(int id, int cyborgs, int production)
        {
            _entities.Add(new Factory(id, Owner.Neutral, cyborgs, production));
            return this;
        }

        public GameStateBuilder PlayerFactory(int id, int cyborgs, int production)
        {
            _entities.Add(new Factory(id, Owner.Player, cyborgs, production));
            return this;
        }

        public GameStateBuilder PlayerTroop(int id, int sourceFactory, int targetFactory, int cyborgs, int turnsRemaining)
        {
            _entities.Add(new Troop(id, Owner.Player, sourceFactory, targetFactory, cyborgs, turnsRemaining));
            return this;
        }

        public GameStateBuilder EnemyFactory(int id, int cyborgs, int production)
        {
            _entities.Add(new Factory(id, Owner.Enemy, cyborgs, production));
            return this;
        }

        public GameStateBuilder EnemyTroop(int id, int sourceFactory, int targetFactory, int cyborgs, int turnsRemaining)
        {
            _entities.Add(new Troop(id, Owner.Enemy, sourceFactory, targetFactory, cyborgs, turnsRemaining));
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