using System.Linq;
using Advent2016.Bunny.RoomList;
using Xunit;

namespace Bunny.UnitTests.RoomList
{
    public class ChecksumComputerTests
    {
        [Fact]
        public void ChecksumIsCorrectForSamples()
        {
            var cc = new ChecksumComputer();
            Assert.Equal("abxyz", cc.Compute("aaaaa-bbb-z-y-x"));
            Assert.Equal("oarel", cc.Compute("not-a-real-room"));
        }

        [Fact]
        public void TiesAreBrokenAlphabetically()
        {
            var cc = new ChecksumComputer();
            Assert.Equal("abcde", cc.Compute("a-b-c-d-e-f-g-h"));
        }
    }
}
