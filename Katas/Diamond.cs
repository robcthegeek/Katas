using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Katas
{
    public class Diamond
    {
        string _asStr = "A";

        private Diamond(char letter)
        {
            if (letter == 'A') return;

            int index = letter - 'A';

            var sb = new StringBuilder();
            int current = letter;
            for (int i = index; i >= 0; i--)
            {
                if (i == 0)
                {
                    var pad = (index - 1) / 2;
                    sb.AppendLine($"{new string(' ', pad)}{(char)current}{new string(' ', pad)}");
                }
                else
                {
                    sb.AppendLine($"{(char)current}{new string(' ', i)}{(char)current}");
                }

                current--;
            }

            _asStr = sb.ToString();
        }

        public static Diamond For(char letter) => new Diamond(letter);

        public static implicit operator string(Diamond diamond) => diamond._asStr;
    }
}