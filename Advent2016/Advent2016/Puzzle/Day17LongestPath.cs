using Advent2016.Bunny.DynamicMaze;

namespace Advent2016.Puzzle
{
    public class Day17LongestPath : IPuzzle
    {
        public string Solve(string[] input)
        {
            var maze = new Maze(input[0], new Dynamizer());
            return new VaultNavigator(maze).LongestPath().Length.ToString();
        }
    }
}
