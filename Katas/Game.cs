using System.Collections.Generic;
using System.Linq;

namespace Katas
{
    public class Game
    {
        public int FactoryCount { get; private set; }
        public int LinkCount { get; private set; }
        public IEnumerable<FactoryLink> Links { get; private set; }

        public Game(int factoryCount, int linkCount, IEnumerable<FactoryLink> links)
        {
            FactoryCount = factoryCount;
            LinkCount = linkCount;
            Links = links;
        }

        public void SetState(IEnumerable<Entity> entities)
        {

        }

        public Action NextMove()
        {
            // TODO (RC): First Up - Limited Set of Moves I Can Make - i.e. Currently Owned

            // TODO (RC): Strategy? KILL vs. CAPTURE?
            // <= Troops than enemy? CAPTURE
            // > Troops than enemy? KILL

            // TODO (RC): Determine Possible Options for moves (Player Owned > Links > Bases)
            // Each Option will have:
            // Source / Target Factories
            // Turns Required
            // Troop Count on Arrival (Turns Required * Target Production)
            // Expected Losses (Availability - (Troops on Arrival + Troops Inbound)

            // TODO (RC): Risk - Ability for Enemy Player to Attack

            // TODO (RC): Balance of Power - Power Shift over Turns >> Move Results
            // Want maximum return over shortest turns

            // TODO (RC): Growth ability - capturing nodes with more links = better growth.


            // TODO (RC): Find Quickest Move that Captures Most Production w/ Least Losses

            // TODO (RC): Also need to think defense - if there are troops incoming, then we need to weigh up production losses

            return new MoveAction(1, 2, 10);
        }
    }

    public class GameState
    {
        public IList<FactoryLink> Links { get; private set; }
        public IList<Entity> Entities { get; private set; }
        public IList<Factory> Factories { get; private set; }
        public IList<Troop> Troops { get; private set; }

        public GameState(IList<FactoryLink> links, IList<Entity> entities)
        {
            Links = links;
            Entities = entities;
            Factories = entities.OfType<Factory>().ToList();
            Troops = entities.OfType<Troop>().ToList();
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

            return null;
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
            : this (id, (int)owner, cyborgs, production)
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