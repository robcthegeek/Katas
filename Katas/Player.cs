using System;

namespace Katas
{
    public class Player
    {
        public string Name { get; private set; }

        public Player(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument is null or whitespace", nameof(name));

            Name = name;
        }
    }
}