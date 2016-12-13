using System.Linq;
using Advent2016.Bunny.Factory;

namespace Advent2016.Puzzle
{
    public class Day10IdentifyBot : IPuzzle
    {
        public string Solve(string[] input)
        {
            var parser = new InstructionParser();
            var instructions = input.Select(line => parser.Parse(line));
            var layout = new SwarmScript(instructions).Compile();
            var bot = layout.Bots.Values.Single(b => b.Values.Max() == 61 && b.Values.Min() == 17);
            return bot.Id.ToString();
        }
    }
}
