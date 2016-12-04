using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2016.Puzzle
{
    class Day3PossibleTriangles : IPuzzle
    {
        public string Solve(string[] input)
        {
            var sideTriplets =
                input.Select(s => s.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries))
                    .Select(t => t.Select(int.Parse));
            var possible =
                sideTriplets.Select(t => t.OrderByDescending(x => x).ToArray()).Where(t => t[0] < t[1] + t[2]);
            return possible.Count().ToString();
        }
    }
}
