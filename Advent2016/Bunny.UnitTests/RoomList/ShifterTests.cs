using Advent2016.Bunny.RoomList;
using Xunit;

namespace Bunny.UnitTests.RoomList
{
    public class ShifterTests
    {
        [Fact]
        public void ShiftWorksOnSamples()
        {
            var shifter = new Shifter();
            var shifted = shifter.Shift("qzmt-zixmtkozy-ivhz", 343);
            Assert.Equal("very encrypted name", shifted);
        }
    }
}
