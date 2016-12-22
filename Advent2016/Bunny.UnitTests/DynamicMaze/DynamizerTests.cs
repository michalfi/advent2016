using Advent2016.Bunny.DynamicMaze;
using Xunit;

namespace Bunny.UnitTests.DynamicMaze
{
    public class DynamizerTests
    {
        [Theory]
        [MemberData(nameof(SampleData))]
        public void ProperDoorsAreOpenForSample(Position position, bool[] doors)
        {
            var dynamizer = new Dynamizer();
            Assert.Equal(doors, dynamizer.DoorsState("hijkl", position));
        }

        public static object[][] SampleData =
        {
            new object[] {new Position(0, 0, ""), new[] {true, true, true, false}},
            new object[] {new Position(0, 1, "D"), new[] {true, false, true, true}},
            new object[] {new Position(0, 0, "DU"), new[] {false, false, false, true}},
        };
    }
}
