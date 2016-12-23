using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent2016.Bunny.Scrambling
{
    public class Unscrambler
    {
        private const int InputLength = 8;

        private int[] Unrotate =
        {
            1, 1, 6, 2, 7, 3, 0, 4
        };

        public string Unscramble(string input, IEnumerable<IOperation> steps)
        {
            return steps.Reverse().Aggregate(input, (acc, step) => Apply(step as dynamic, acc));
        }

        private string Apply(IOperation op, string input) => input;

        private string Apply(SwapPosition op, string input)
        {
            var chars = input.ToCharArray();
            char x = chars[op.X];
            chars[op.X] = chars[op.Y];
            chars[op.Y] = x;
            return new string(chars);
        }

        private string Apply(SwapLetter op, string input)
        {
            var chars = input.ToCharArray();
            int x = Array.IndexOf(chars, op.X);
            chars[Array.IndexOf(chars, op.Y)] = op.X;
            chars[x] = op.Y;
            return new string(chars);
        }

        private string Apply(Reverse op, string input)
        {
            var changed = input.ToCharArray(op.Start, op.End - op.Start + 1);
            Array.Reverse(changed);
            return input.Substring(0, op.Start) + new string(changed) +
                   input.Substring(op.End + 1, input.Length - op.End - 1);
        }

        private string Apply(Rotate op, string input)
        {
            int len = input.Length;
            int dir = op.Direction == Rotate.RotateDirection.left ? -1 : 1;
            int chop = (dir * (op.Count % len) + len) % len;
            return input.Substring(chop) + input.Substring(0, chop);
        }

        private string Apply(Move op, string input)
        {
            char moved = input[op.To];
            string without = input.Substring(0, op.To) + input.Substring(op.To + 1);
            return without.Substring(0, op.From) + moved + without.Substring(op.From);
        }

        private string Apply(RotateByLetter op, string input)
        {
            Debug.Assert(input.Length == InputLength);
            int index = input.IndexOf(op.Letter);
            int chop = Unrotate[index];
            //int original = index == 0 ? 7 : (index%2 == 0 ? (index + InputLength)/2 - 1 : (index - 1)/2);
            return input.Substring(chop) + input.Substring(0, chop);
        }
    }
}
