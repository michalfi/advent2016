using Advent2016.Bunny.InfiniteCubicles;
using Xunit;

namespace Bunny.UnitTests.InfiniteCubicles
{
    public class CubicleTestTests
    {
        [Theory]
        [InlineData(10, 0, 0)]
        [InlineData(10, 1, 1)]
        [InlineData(10, 9, 3)]
        public void SampleCubicleIsEvaluatedAsCubicle(int favoriteNumber, int x, int y)
        {
            var maze = new Maze(favoriteNumber);
            Assert.True(maze.IsCubicle(x, y));
        }

        [Theory]
        [InlineData(10, 5, 0)]
        [InlineData(10, 5, 1)]
        [InlineData(10, 6, 3)]
        public void SampleWallIsEvaluatedAsWall(int favoriteNumber, int x, int y)
        {
            var maze = new Maze(favoriteNumber);
            Assert.False(maze.IsCubicle(x, y));
        }
    }
}
