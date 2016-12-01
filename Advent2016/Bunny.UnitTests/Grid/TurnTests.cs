using Advent2016.Bunny.Grid;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Bunny.UnitTests.Grid
{
    public class TurnTests
    {
        [Fact]
        public void TurnLeftWorks()
        {
            var left = new Turn(Turn.Dir.L);
            Assert.Equal(Direction.West, left.ResultingDirection(Direction.North));
            Assert.Equal(Direction.North, left.ResultingDirection(Direction.East));
            Assert.Equal(Direction.East, left.ResultingDirection(Direction.South));
            Assert.Equal(Direction.South, left.ResultingDirection(Direction.West));
        }

        [Fact]
        public void TurnRightWorks()
        {
            var left = new Turn(Turn.Dir.R);
            Assert.Equal(Direction.West, left.ResultingDirection(Direction.South));
            Assert.Equal(Direction.North, left.ResultingDirection(Direction.West));
            Assert.Equal(Direction.East, left.ResultingDirection(Direction.North));
            Assert.Equal(Direction.South, left.ResultingDirection(Direction.East));
        }

        [Property]
        public Property TurnRoundMaintainsDirection()
        {
            return Prop.ForAll<Direction, Turn.Dir>((direction, dir) => TurnRound(direction, dir) == direction);
        }

        private static Direction TurnRound(Direction original, Turn.Dir turnDir)
        {
            var turn = new Turn(turnDir);
            var dir = original;
            for (int i = 0; i < 4; i++)
                dir = turn.ResultingDirection(dir);
            return dir;
        }
    }
}
