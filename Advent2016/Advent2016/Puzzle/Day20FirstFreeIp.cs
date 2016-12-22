using System.Linq;
using Advent2016.Bunny.Firewall;

namespace Advent2016.Puzzle
{
    public class Day20FirstFreeIp : IPuzzle
    {
        public string Solve(string[] input)
        {
            var ranges = input.Select(Range.FromString).OrderBy(r => r.Start);
            long end = -1;
            foreach (var range in ranges)
            {
                if (range.Start > end + 1)
                    break;
                if (range.End > end)
                    end = range.End;
            }
            return (end + 1).ToString();
        }
    }
}
