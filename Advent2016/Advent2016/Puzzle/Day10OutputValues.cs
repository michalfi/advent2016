using System.Linq;
using Advent2016.Bunny.Factory;

namespace Advent2016.Puzzle
{
    public class Day10OutputValues : IPuzzle
    {
        public string Solve(string[] input)
        {
            var parser = new InstructionParser();
            var instructions = input.Select(line => parser.Parse(line));
            var layout = new SwarmScript(instructions).Compile();
            return (layout.Outputs[0]*layout.Outputs[1]*layout.Outputs[2]).ToString();
        }
    }
}
