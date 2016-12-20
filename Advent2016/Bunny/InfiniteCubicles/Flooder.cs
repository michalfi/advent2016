using System.Collections.Generic;
using System.Linq;

namespace Advent2016.Bunny.InfiniteCubicles
{
    public class Flooder
    {
        public ISet<Cubicle> Flood(Maze maze, Cubicle start, int rounds)
        {
            var flooded = new HashSet<Cubicle> {start};
            var wave = new Queue<Cubicle>(new[] {start});

            for (int i = 0; i < rounds; i++)
            {
                var nextWave = new Queue<Cubicle>();
                while (wave.Count > 0)
                {
                    var c = wave.Dequeue();
                    foreach (var neighbor in maze.Neighbors(c).Where(n => !flooded.Contains(n)))
                    {
                        flooded.Add(neighbor);
                        nextWave.Enqueue(neighbor);
                    }
                }
                wave = nextWave;
            }

            return flooded;
        }
    }
}
