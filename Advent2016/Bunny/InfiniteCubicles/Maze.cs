using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2016.Bunny.InfiniteCubicles
{
    public class Maze
    {
        public Maze(int favoriteNumber)
        {
            FavoriteNumber = favoriteNumber;
        }

        private static int[] Steps { get; } = {-1, 1};

        private int FavoriteNumber { get; }

        public IEnumerable<Cubicle> Neighbors(Cubicle cubicle)
        {
            return
                Steps.Select(xStep => new {x = cubicle.X + xStep, y = (int) cubicle.Y})
                    .Concat(Steps.Select(yStep => new {x = (int) cubicle.X, y = cubicle.Y + yStep}))
                    .Where(c => this.IsCubicle(c.x, c.y))
                    .Select(c => new Cubicle((ushort) c.x, (ushort) c.y));
        }

        public bool IsCubicle(int x, int y)
        {
            if (x < 0 || y < 0)
                return false;
            long n = x*x + 3*x + 2*x*y + y + y*y + this.FavoriteNumber;
            string binary = Convert.ToString(n, 2);
            int ones = binary.Count(c => c == '1');
            return ones%2 == 0;
        }
    }
}
