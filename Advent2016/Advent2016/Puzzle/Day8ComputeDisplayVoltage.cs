using System.Linq;
using Advent2016.Bunny.Display;

namespace Advent2016.Puzzle
{
    public class Day8ComputeDisplayVoltage : IPuzzle
    {
        public string Solve(string[] input)
        {
            var parser = new InstructionParser();
            var instructions = input.Select(parser.Parse);
            var simulator = new ScreenSimulator(50, 6);
            var endState = instructions.Aggregate(simulator.InitialState(),
                (state, instruction) => simulator.Simulate(instruction, state));
            int pixelsLit = endState.PixelState.Cast<bool>().Count(pixel => pixel);
            return pixelsLit.ToString();
        }
    }
}
