using System.Collections.Generic;
using Advent2016.Bunny.Keypad;
using Dnum;

namespace Advent2016.Puzzle
{
    public class Day2BathroomCode : IPuzzle
    {
        public Day2BathroomCode(Pad usedPad)
        {
            UsedPad = usedPad;
        }

        private Pad UsedPad { get; }

        public string Solve(string[] input)
        {
            var pad = this.UsedPad;
            List<char> code = new List<char>();
            char initialDigit = '5';
            var key = pad[initialDigit];
            foreach (string keyInstructions in input)
            {
                foreach (char instruction in keyInstructions)
                {
                    var dir = Dnum<Direction>.Parse(instruction.ToString());
                    key = pad[key.Neighbor(dir)];
                }
                code.Add(key.Key);
            }
            return string.Join("", code);
        }
    }
}
