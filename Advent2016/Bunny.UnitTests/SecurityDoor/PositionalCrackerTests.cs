using Advent2016.Bunny.SecurityDoor;
using Xunit;

namespace Bunny.UnitTests.SecurityDoor
{
    public class PositionalCrackerTests
    {
        [Fact]
        public void CrackerFindsPassword()
        {
            var cracker = new PositionalCracker();
            Assert.Equal("05ace8e3", cracker.Crack("abc"));
        }
    }
}
