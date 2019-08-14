using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Katas
{
    public static class LookSay
    {
        public static string Digits(uint input)
        {
            var ins = input.ToString();
            var currDigit = ins[0];
            var count = 0;
            var sb = new StringBuilder();

            foreach (var c in ins)
            {
                if (c == currDigit)
                {
                    count++;
                }
                else
                {
                    sb.Append($"{count}{currDigit}");
                    currDigit = c;
                    count = 1;
                }
            }

            sb.Append($"{count}{currDigit}");

            return sb.ToString();
        }
    }
}