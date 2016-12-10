using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2016.Puzzle
{
    using Bunny.Display;

    class Day8DisplayCode : IPuzzle
    {
        public string Solve(string[] input)
        {
            var parser = new InstructionParser();
            var instructions = input.Select(parser.Parse);
            var simulator = new ScreenSimulator(50, 6);
            var endState = instructions.Aggregate(simulator.InitialState(),
                (state, instruction) => simulator.Simulate(instruction, state));
            return endState.ToString();
        }
    }
}
