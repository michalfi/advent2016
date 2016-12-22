using Advent2016.Bunny.DynamicMaze;
using Kts.AStar;

namespace Advent2016.Puzzle
{
    public class Day17PathToVault : IPuzzle
    {
        public string Solve(string[] input)
        {
            var maze = new Maze(input[0], new Dynamizer());
            return new VaultNavigator(maze).ShortestPath();
        }
    }
}
