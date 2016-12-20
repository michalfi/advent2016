using Advent2016.Bunny.InfiniteCubicles;

namespace Advent2016.Puzzle
{
    public class Day13CountReachable : IPuzzle
    {
        public string Solve(string[] input)
        {
            var waitingRoom = new Cubicle(1, 1);
            var favoriteNumber = int.Parse(input[0]);
            var maze = new Maze(favoriteNumber);
            var flooder = new Flooder();
            var flooded = flooder.Flood(maze, waitingRoom, 50);
            return flooded.Count.ToString();
        }
    }
}
