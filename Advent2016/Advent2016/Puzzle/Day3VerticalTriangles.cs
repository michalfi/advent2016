using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2016.Puzzle
{
    using MoreLinq;

    class Day3VerticalTriangles : IPuzzle
    {
        public string Solve(string[] input)
        {
            var rowTriplets =
                input.Select(s => s.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries))
                    .Select(t => t.Select(int.Parse).ToArray()).ToArray();
            var sideTriplets =
                rowTriplets.Select(t => t[0])
                    .Concat(rowTriplets.Select(t => t[1]))
                    .Concat(rowTriplets.Select(t => t[2])).Batch(3);
            var possible =
                sideTriplets.Select(t => t.OrderByDescending(x => x).ToArray()).Where(t => t[0] < t[1] + t[2]);
            return possible.Count().ToString();
        }
    }
}
