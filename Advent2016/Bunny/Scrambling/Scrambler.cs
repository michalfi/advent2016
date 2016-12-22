using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2016.Bunny.Scrambling
{
    public class Scrambler
    {
        public string Scramble(string input, IEnumerable<IOperation> steps)
        {
            return steps.Aggregate(input, (acc, step) => Apply(step as dynamic, acc));
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
            int dir = op.Direction == Rotate.RotateDirection.left ? 1 : -1;
            int chop = (dir*(op.Count%len) + len)%len;
            return input.Substring(chop) + input.Substring(0, chop);
        }

        private string Apply(Move op, string input)
        {
            char moved = input[op.From];
            string without = input.Substring(0, op.From) + input.Substring(op.From + 1);
            return without.Substring(0, op.To) + moved + without.Substring(op.To);
        }

        private string Apply(RotateByLetter op, string input)
        {
            int index = input.IndexOf(op.Letter);
            int chop = input.Length - (index + 1 + (index >= 4 ? 1 : 0))%input.Length;
            return input.Substring(chop) + input.Substring(0, chop);
        }
    }
}
