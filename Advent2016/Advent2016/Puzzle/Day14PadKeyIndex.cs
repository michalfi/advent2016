using System.Linq;
using Advent2016.Bunny.Comms;

namespace Advent2016.Puzzle
{
    public class Day14PadKeyIndex : IPuzzle
    {
        public Day14PadKeyIndex(int stretchCount = 0)
        {
            StretchCount = stretchCount;
        }

        private int StretchCount { get; }

        public string Solve(string[] input)
        {
            var gen = new DataGenerator(input[0], this.StretchCount);
            var finder = new KeyFinder(gen);
            var index = finder.KeyIndices().Skip(63).First();
            return index.ToString();
        }
    }
}
