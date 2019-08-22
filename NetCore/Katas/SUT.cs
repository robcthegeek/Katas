using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Katas
{
    public class LowercaseString
    {
        private readonly string _value;

        public LowercaseString(string value)
        {
            /* People may find this strange, but makes the code cleaner - and also
             * prevents people from doing things like:
             *
             *  var lower = "I LIED"
             */
            _value = value?.ToLowerInvariant();
        }

        public static implicit operator string(LowercaseString lcs) => lcs._value;
        public static implicit operator LowercaseString(string value) => new LowercaseString(value);
    }

    public class Animalia
    {
        private readonly List<string> _said = new List<string>();
        private string _previous;
        private readonly IEnumerable<string> _animalList;

        public Animalia(IEnumerable<string> animalList)
        {
            _animalList = animalList?
                .Select(x => x.ToLowerInvariant())
                .ToArray();
        }

        public bool Say(LowercaseString input) {
            string animal = input;

            if (!_animalList.Contains(animal) || _said.Contains(animal)) return false;

            if (_previous.IsNotSet() || animal.FirstLetter() == _previous.LastLetter())
            {
                _said.Add(animal);
                _previous = input;
                return true;
            };

            return false;
        }
    }

    internal static class Extensions
    {
        internal static char FirstLetter(this string value) => value[0];
        internal static char LastLetter(this string value) => string.IsNullOrWhiteSpace(value) ? default : value[value.Length - 1];
        internal static bool IsNotSet(this string value) => string.IsNullOrWhiteSpace(value);
    }
}