using System.Linq;
using Advent2016.Bunny.ChipAssembly;

namespace Advent2016.Puzzle
{
    public class Day11MinimalPlanLength : IPuzzle
    {
        public Day11MinimalPlanLength(string extraFloor1Items)
        {
            ExtraFloor1Items = extraFloor1Items;
        }

        public string ExtraFloor1Items { get; }

        public string Solve(string[] input)
        {
            input[0] = input[0] + this.ExtraFloor1Items;
            var initialState = new InitialStateParser().Parse(input);
            var plan = new ManipulationPlanner().PlanAssembly(initialState);
            return (plan.Count() - 1).ToString();
        }
    }
}
