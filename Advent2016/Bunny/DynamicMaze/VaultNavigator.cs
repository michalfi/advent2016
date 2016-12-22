using System.Collections.Generic;

namespace Advent2016.Bunny.DynamicMaze
{
    public class VaultNavigator
    {
        public VaultNavigator(Maze maze)
        {
            Maze = maze;
        }

        private Maze Maze { get; }

        public string ShortestPath()
        {
            var paths = new Queue<Position>(new[] {Maze.Init});
            while (paths.Count > 0)
            {
                var pos = paths.Dequeue();
                foreach (var extended in Maze.ContinueFrom(pos))
                {
                    if (extended.X == Maze.Vault.X && extended.Y == Maze.Vault.Y)
                        return extended.Path;
                    paths.Enqueue(extended);
                }
            }
            return null;
        }

        public string LongestPath()
        {
            string longest = null;
            var paths = new Queue<Position>(new[] { Maze.Init });
            while (paths.Count > 0)
            {
                var pos = paths.Dequeue();
                foreach (var extended in Maze.ContinueFrom(pos))
                {
                    if (extended.X == Maze.Vault.X && extended.Y == Maze.Vault.Y)
                    {
                        if (extended.Path.Length > (longest?.Length ?? 0))
                            longest = extended.Path;
                        continue;
                    }
                    paths.Enqueue(extended);
                }
            }
            return longest;
        }
    }
}
