using System;
using System.Collections.Generic;
using Kts.AStar;

namespace Advent2016.Bunny.InfiniteCubicles
{
    public class Navigator
    {
        public Navigator(Maze maze)
        {
            Maze = maze;
        }

        public Maze Maze { get; }

        public IEnumerable<Cubicle> ShortestPath(Cubicle start, Cubicle destination)
        {
            return AStarUtilities.FindMinimalPath(start, destination, cubicle => this.Maze.Neighbors(cubicle),
                (c1, c2) => 1, c => c.ManhattanDistance(destination)).Result;
        }
    }
}
