﻿using System;
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
            return machine.NextActions();
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
                    var links = _state.FactoryLinks[b.Id];
                    var f1 = _state.Factories.Single(f => f.Id == b.Id);

                    var possibles = links.Select(l =>
                    {
                        var f2 = _state.Factories.Single(f => f.Id == l.Factory2);
                        return new
                        {
                            Factory1 = l.Factory1,
                            Factory2 = l.Factory2,
                            Distance = l.Distance,
                            CaptureCost = f2.Cyborgs + (l.Distance * f2.Production) + 1
                        };
                    })
                    .OrderBy(p => p.CaptureCost)
                    .ThenBy(t => t.Distance)
                    .ToList();


                    if (!possibles.Any())
                        return new List<Action> { new WaitAction( )};

                    var availTroops = f1.Cyborgs;
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
}