using System.Linq;
using Advent2016.Bunny.Firewall;

namespace Advent2016.Puzzle
{
    public class Day20CountFreeIps : IPuzzle
    {
        public string Solve(string[] input)
        {
            var ranges = input.Select(Range.FromString).OrderBy(r => r.Start);
            long count = 0;
            long end = -1;
            foreach (var range in ranges)
            {
                if (range.Start > end + 1)
                    count += range.Start - end - 1;
                if (range.End > end)
                    end = range.End;
            }
            count += uint.MaxValue - end;
            return count.ToString();
        }
    }
}
