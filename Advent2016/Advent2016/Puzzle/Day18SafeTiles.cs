using System.Collections.Generic;
using System.Linq;
using Advent2016.Bunny.TrapCorridor;

namespace Advent2016.Puzzle
{
    public class Day18SafeTiles : IPuzzle
    {
        public Day18SafeTiles(int corridorLength)
        {
            CorridorLength = corridorLength;
        }

        private int CorridorLength { get; }

        public string Solve(string[] input)
        {
            var gen = new RowGenerator();

            var corridor = Enumerable.Range(0, CorridorLength - 1)
                .Aggregate(new {row = gen.FromString(input[0]), safe = 0L},
                    (acc, _) => new {row = gen.Following(acc.row), safe = acc.safe + acc.row.SafeTileCount});

            return (corridor.row.SafeTileCount + corridor.safe).ToString();
        }
    }
}
