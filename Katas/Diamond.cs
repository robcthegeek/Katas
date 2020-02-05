using System.Collections;
using System.Collections.Generic;

namespace Katas
{
    public class Diamond
    {
        public static Diamond For(char letter)
        {
            return new Diamond();
        }

        public static implicit operator string(Diamond diamond) => "A";
    }
}