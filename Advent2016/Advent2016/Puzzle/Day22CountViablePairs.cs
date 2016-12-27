using System.Linq;
using Advent2016.Bunny.Cluster;

namespace Advent2016.Puzzle
{
    public class Day22CountViablePairs : IPuzzle
    {
        public string Solve(string[] input)
        {
            var nodeLines = input.Skip(2).Where(line => !string.IsNullOrWhiteSpace(line));
            var nodes = nodeLines.Select(Node.FromString).ToArray();
            var pairs =
                nodes.SelectMany(n => nodes.Where(other => !other.Equals(n)).Select(other => new {fst = n, snd = other}));
            var viable = pairs.Where(p => p.fst.UsedSpace > 0 && p.fst.UsedSpace <= p.snd.AvailableSpace);
            return viable.Count().ToString();
        }
    }
}
