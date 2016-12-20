using System.Linq;
using Advent2016.Bunny.InfiniteCubicles;

namespace Advent2016.Puzzle
{
    public class Day13StepsToCubicle : IPuzzle
    {
        public string Solve(string[] input)
        {
            var waitingRoom = new Cubicle(1, 1);
            var destination = new Cubicle(31, 39);
            var favoriteNumber = int.Parse(input[0]);
            var maze = new Maze(favoriteNumber);
            var navigator = new Navigator(maze);
            var path = navigator.ShortestPath(waitingRoom, destination);
            return (path.Count() - 1).ToString();
        }
    }
}
