using System.Linq;
using Advent2016.Bunny.InteractiveSculpture;

namespace Advent2016.Puzzle
{
    public class Day15Timer : IPuzzle
    {
        public Day15Timer(string[] extraDiscs = null)
        {
            ExtraDiscs = extraDiscs ?? new string[] {};
        }

        private string[] ExtraDiscs { get; }

        public string Solve(string[] input)
        {
            var parser = new DiscParser();
            var discs = input.Concat(ExtraDiscs).Select(row => parser.Parse(row)).ToArray();
            var timing = new Timing(discs);
            return timing.LaunchTime.ToString();
        }
    }
}
