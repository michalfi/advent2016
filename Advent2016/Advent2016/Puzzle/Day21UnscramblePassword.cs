using System.Linq;
using Advent2016.Bunny.Scrambling;

namespace Advent2016.Puzzle
{
    public class Day21UnscramblePassword : IPuzzle
    {
        public Day21UnscramblePassword(string password)
        {
            Password = password;
        }

        public string Password { get; }

        public string Solve(string[] input)
        {
            var parser = new OperationParser();
            var steps = input.Select(line => parser.Parse(line));
            var unscrambler = new Unscrambler();
            return unscrambler.Unscramble(Password, steps);
        }
    }
}
