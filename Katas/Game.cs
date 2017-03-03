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

        public List<Action> NextActions()
        {
            var machine = new WarMachine(_state);
            var actions = machine.NextActions();

            return !actions.Any()
                ? new List<Action> { new WaitAction() }
                : actions;
        }
    }

    public class WarMachine
    {
        private readonly GameState _state;

        public WarMachine(GameState state)
        {
            _state = state;
        }

        public List<Action> NextActions()
        {
            var bases = _state.Factories.Where(x => x.Owner == (int)Owner.Player);

            var moves = bases
                .SelectMany(b =>
                {
                    var factory = _state.Factories.Single(f => f.Id == b.Id);
                    var meta = _state.Factory(b.Id);
                    var availTroops = factory.Cyborgs;
                    var result = new List<Action>();


                    if (meta.IsRearGuard && meta.CanIncreaseProd)
                    {
                        result.Add(new IncreaseProductionAction(b.Id));
                        availTroops -= 10;
                    }

                    var links = _state.FactoryLinks[b.Id];

                    var possibles = links.Select(l =>
                    {
                        var f2Meta = _state.Factory(l.Factory2);

                        return new
                        {
                            Factory1 = l.Factory1,
                            Factory2 = l.Factory2,
                            Distance = l.Distance,
                            CaptureCost = f2Meta.ForceRequiredToCapture
                        };
                    })
                    .OrderBy(p => p.CaptureCost)
                    .ThenBy(t => t.Distance)
                    .ToList();

                    if (!possibles.Any())
                        return new List<Action> { new WaitAction( )};

                    return possibles
                        .TakeWhile(p => (availTroops -= p.CaptureCost) > 0)
                        .Select(p => new MoveAction(p.Factory1, p.Factory2, p.CaptureCost))
                        .Cast<Action>()
                        .ToList();
                })
                .ToList();

            return moves;
        }
    }

    public class GameState
    {
        public IList<FactoryLink> Links { get; private set; }
        public List<Entity> Entities { get; private set; }
        public IList<Factory> Factories => Entities.OfType<Factory>().ToList();
        public IList<Troop> Troops => Entities.OfType<Troop>().ToList();
        public Dictionary<int, List<FactoryLink>> FactoryLinks { get; private set; }

        public GameState(IList<FactoryLink> links)
        {
            Links = links;
            Entities = new List<Entity>();
            FactoryLinks = MapFactories(links);
        }

        private Dictionary<int, List<FactoryLink>> MapFactories(IList<FactoryLink> links)
        {
            var result = new Dictionary<int, List<FactoryLink>>();

            foreach (var link in links)
            {
                var f1 = result.ContainsKey(link.Factory1)
                    ? result[link.Factory1]
                    : new List<FactoryLink>();
                result[link.Factory1] = f1;

                f1.Add(new FactoryLink(link.Factory1, link.Factory2, link.Distance));

                var f2 = result.ContainsKey(link.Factory2)
                    ? result[link.Factory2]
                    : new List<FactoryLink>();

                f2.Add(new FactoryLink(link.Factory2, link.Factory1, link.Distance));

                result[link.Factory2] = f2;
            }

            return result;
        }

        public void AddEntities(params Entity[] entities)
        {
            Entities.AddRange(entities);
        }

        public FactoryMetadata Factory(int id)
        {
            var factory = Factories.Single(x => x.Id == id);
            var links = FactoryLinks.ContainsKey(id)
                ? FactoryLinks[id].Select(x => x.Factory2).ToList()
                : new List<int>();

            var rearGuard = FactoryLinks.ContainsKey(id) && FactoryLinks[id].All(link =>
            {
                var linked = Factories.Single(f => f.Id == link.Factory2);
                return linked.Owner == (int) Owner.Player;
            });

            var playerEnRoute = Entities
                .OfType<Troop>()
                .Where(t => t.TargetFactoryId == id && t.Owner == (int)Owner.Player)
                .Sum(x => x.Cyborgs);

            var enemyEnRoute = Entities
                .OfType<Troop>()
                .Where(t => t.TargetFactoryId == id && t.Owner == (int)Owner.Enemy)
                .Sum(x => x.Cyborgs);

            var forceRequiredToCapture = factory.Owner == (int) Owner.Player
                ? 0
                : (factory.Cyborgs + 1 + enemyEnRoute) - playerEnRoute;

            var capturing = false;
            if (forceRequiredToCapture <= 0 && playerEnRoute > 0)
            {
                forceRequiredToCapture = 0;
                capturing = true;
            }

            return new FactoryMetadata
            {
                IsRearGuard = rearGuard,
                CanIncreaseProd = factory.Cyborgs >= 10,
                Capturing = capturing,
                PlayerEnRoute = playerEnRoute,
                EnemyEnRoute = enemyEnRoute,
                ForceRequiredToCapture = forceRequiredToCapture,
                LinkedFactories = links
            };
        }
    }

    public class FactoryMetadata
    {
        public bool IsRearGuard { get; set; }
        public bool CanIncreaseProd { get; set; }
        public bool Capturing { get; set; }
        public int PlayerEnRoute { get; set; }
        public int EnemyEnRoute { get; set; }
        public int ForceRequiredToCapture { get; set; }
        public List<int> LinkedFactories { get; set; }

        public FactoryMetadata()
        {
            LinkedFactories = new List<int>();
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

            if (type == "BOMB")
            {
                return new Bomb(id, arg1, arg2, arg3, arg4);
            }

            throw new ArgumentOutOfRangeException($"Unexpected entity type '{type}'.");
        }
    }

    public class Bomb : Entity
    {
        public int Id { get; private set; }
        public bool PlayerOwned { get; private set; }
        public bool OpponentOwned { get; private set; }
        public int Source { get; private set; }
        public int Target { get; private set; }
        public int TurnsRemaining { get; private set; }

        public Bomb(int id, int owner, int source, int target, int turnsRemaining) : base(id, owner)
        {
            Id = id;
            PlayerOwned = owner == 1;
            OpponentOwned = owner == -1;
            Source = source;
            Target = target;
            TurnsRemaining = turnsRemaining;
        }

        public Bomb(int id, Owner owner, int source, int target, int turnsRemaining) :
            this(id, (int)owner, source, target, turnsRemaining)
        {
        }
    }

    public class Troop : Entity
    {
        public int SourceFactoryId { get; private set; }
        public int TargetFactoryId { get; private set; }
        public int Cyborgs { get; private set; }
        public int TurnsRemaining { get; private set; }

        public Troop(int id, int owner, int sourceFactoryId, int targetFactoryId, int cyborgs, int turnsRemaining) : base(id, owner)
        {
            SourceFactoryId = sourceFactoryId;
            TargetFactoryId = targetFactoryId;
            Cyborgs = cyborgs;
            TurnsRemaining = turnsRemaining;
        }

        public Troop(int id, Owner owner, int sourceFactoryId, int targetFactoryId, int cyborgs, int turnsRemaining)
            : this(id, (int)owner, sourceFactoryId, targetFactoryId, cyborgs, turnsRemaining)
        {
        }
    }
    public class Factory : Entity
    {
        public int Cyborgs { get; private set; }
        public int Production { get; private set; }
        public int TurnsBeforeProducing { get; private set; }
        public bool ProductionStopped => TurnsBeforeProducing > 0;

        public Factory(int id, int owner, int cyborgs, int production, int turnsBeforeProducing = 0)
            : base(id, owner)
        {
            Cyborgs = cyborgs;
            Production = production;
            TurnsBeforeProducing = turnsBeforeProducing;
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

    public class MessageAction : Action
    {
        public string Message { get; private set; }

        public MessageAction(string message)
        {
            Message = message;
        }

        public override string ToString()
        {
            return $"MSG {Message}";
        }
    }

    public class IncreaseProductionAction : Action
    {
        public int Factory { get; private set; }

        public IncreaseProductionAction(int factoryId)
        {
            Factory = factoryId;
        }

        public override string ToString()
        {
            return $"INC {Factory}";
        }
    }
}