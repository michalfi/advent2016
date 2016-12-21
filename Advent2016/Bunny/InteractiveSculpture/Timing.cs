using System;
using System.Linq;
using MoreLinq;

namespace Advent2016.Bunny.InteractiveSculpture
{
    public class Timing
    {
        public Timing(Disc[] discs)
        {
            var largest = discs.MaxBy(d => d.Size);
            var firstLargestChance = (largest.Size - (largest.InitialPosition + largest.Number)%largest.Size)%
                                     largest.Size;
            LaunchTime =
                Enumerable.Range(0, Int32.MaxValue)
                    .Select(t => t*largest.Size + firstLargestChance)
                    .First(time => discs.All(d => d.Position(time) == 0));
        }

        public int LaunchTime { get; }
    }
}
