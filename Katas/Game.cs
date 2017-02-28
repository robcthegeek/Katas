using System;
using System.Collections.Generic;
using System.Linq;

namespace Katas
{
    public class Game
    {
        private GameState _state;

        public int FactoryCount { get; private set; }
        public int LinkCount { get; private set; }
        public IList<FactoryLink> Links { get; private set; }

        public Game(int factoryCount, int linkCount)
        {
            FactoryCount = factoryCount;
            LinkCount = linkCount;
            Links = new List<FactoryLink>();
        }

        public void AddLink(int factory1, int factory2, int distance)
        {
            Links.Add(new FactoryLink(factory1, factory2, distance));
        }

        public void StartTurn()
        {
            _state = new GameState(Links);
        }

        public void AddEntity(int id, string type, int arg1, int arg2, int arg3, int arg4, int arg5)
        {
            _state.Entities.Add(Factory.Create(id, type, arg1, arg2, arg3, arg4, arg5));
        }

        public Action NextMove()
        {
            var machine = new WarMachine(_state);
            return machine.NextMove();
        }
    }

    public class WarMachine
    {
        private readonly GameState _state;

        public WarMachine(GameState state)
        {
            _state = state;
        }

        public Action NextMove()
        {
            // Get Possible Moves
            var actions = _state.PossibleActions();

            // Iterate Moves - Determine Move w/ Least Casulties
            var results = actions
                .Where(x => x.Action is MoveAction)
                .Select(x => x.Action as MoveAction)
                .Select(x =>
                {
                    var playerCyborgs = x.CyborgCount;
                    var enemyCyborgs = _state.Factories.Single(y => y.Id == x.Destination).Cyborgs;
                    var casulties = playerCyborgs - enemyCyborgs;

                    return new
                    {
                        Casulties = casulties,
                        Source = x.Source,
                        Destination = x.Destination,
                        Cyborgs = x.CyborgCount,
                        Outgunned = casulties < 0
                    };
                })
                .Where(x => !x.Outgunned)
                .OrderBy(x => x.Casulties)
                .First();

            return new MoveAction(results.Source, results.Destination, results.Cyborgs);
        }
    }

    public class GameState
    {
        public IList<FactoryLink> Links { get; private set; }
        public List<Entity> Entities { get; private set; }
        public IList<Factory> Factories => Entities.OfType<Factory>().ToList();
        public IList<Troop> Troops => Entities.OfType<Troop>().ToList();

        public GameState(IList<FactoryLink> links)
        {
            Links = links;
            Entities = new List<Entity>();
        }

        public void AddEntities(params Entity[] entities)
        {
            Entities.AddRange(entities);
        }

        public List<PossibleAction> PossibleActions()
        {
            // Get All Player-Owned Factories
            var playerOwned = Factories
                .Where(x => x.Owner == (int)Owner.Player)
                .ToDictionary(f => f.Id, f => f);

            // Determine Links to/from Factories
            var playerLinked = Links
                .Where(x => playerOwned.ContainsKey(x.Factory1) || playerOwned.ContainsKey(x.Factory2))
                .ToList();

            // Return Possible Move Actions
            var moveActions = playerLinked
                .Select(link =>
                {
                    var playerFactoryId = playerOwned.ContainsKey(link.Factory1)
                        ? link.Factory1
                        : link.Factory2;
                    var opponentFactoryId = playerOwned.ContainsKey(link.Factory1)
                        ? link.Factory2
                        : link.Factory1;
                    var factory = playerOwned[playerFactoryId];

                    var move = new MoveAction(playerFactoryId, opponentFactoryId, factory.Cyborgs);

                    return new PossibleAction(move);
                })
                .ToList();

            var result = new List<PossibleAction>
            {
                new PossibleAction(new WaitAction())
            };

            result.AddRange(moveActions);

            return result;
        }
    }

    public abstract class Entity
    {
        public int Id { get; private set; }
        public int Owner { get; private set; }

        protected Entity(int id, int owner)
        {
            Id = id;
            Owner = owner;
        }

        protected Entity(int id, Owner owner) : this(id, (int)owner)
        {
        }

        public static Entity Create(int id, string type, int arg1, int arg2, int arg3, int arg4, int arg5)
        {
            if (type == "FACTORY")
            {
                return new Factory(id, arg1, arg2, arg3);
            }

            if (type == "TROOP")
            {
                return new Troop(id, arg1, arg2, arg3, arg4, arg5);
            }

            throw new ArgumentOutOfRangeException($"Unexpected entity type '{type}'.");
        }
    }

    public class Troop : Entity
    {
        public int SourceFactoryId { get; private set; }
        public int TargetFactoryId { get; private set; }
        public int Headcount { get; private set; }
        public int TurnsRemaining { get; private set; }

        public Troop(int id, int owner, int sourceFactoryId, int targetFactoryId, int headcount, int turnsRemaining) : base(id, owner)
        {
            SourceFactoryId = sourceFactoryId;
            TargetFactoryId = targetFactoryId;
            Headcount = headcount;
            TurnsRemaining = turnsRemaining;
        }

        public Troop(int id, Owner owner, int sourceFactoryId, int targetFactoryId, int headcount, int turnsRemaining)
            : this(id, (int)owner, sourceFactoryId, targetFactoryId, headcount, turnsRemaining)
        {
        }
    }
    public class Factory : Entity
    {
        public int Cyborgs { get; private set; }
        public int Production { get; private set; }

        public Factory(int id, int owner, int cyborgs, int production)
            : base(id, owner)
        {
            Cyborgs = cyborgs;
            Production = production;
        }

        public Factory(int id, Owner owner, int cyborgs, int production)
            : this(id, (int)owner, cyborgs, production)
        {

        }
    }

    public enum Owner
    {
        Player = 1,
        Neutral = 0,
        Enemy = -1
    }

    public class FactoryLink
    {
        public int Factory1 { get; private set; }
        public int Factory2 { get; private set; }
        public int Distance { get; private set; }

        public FactoryLink(int factory1, int factory2, int distance)
        {
            Factory1 = factory1;
            Factory2 = factory2;
            Distance = distance;
        }
    }

    public class PossibleAction
    {
        public Action Action { get; protected set; }

        public PossibleAction(Action action)
        {
            Action = action;
        }
    }

    public abstract class Action
    {
        public new abstract string ToString();
    }

    public class WaitAction : Action
    {
        public override string ToString()
        {
            return "WAIT";
        }
    }

    public class MoveAction : Action
    {
        public int Source { get; private set; }
        public int Destination { get; private set; }
        public int CyborgCount { get; private set; }


        public MoveAction(int source, int destination, int cyborgCount)
        {
            Source = source;
            Destination = destination;
            CyborgCount = cyborgCount;
        }

        public override string ToString()
        {
            return $"MOVE {Source} {Destination} {CyborgCount}";
        }
    }
}