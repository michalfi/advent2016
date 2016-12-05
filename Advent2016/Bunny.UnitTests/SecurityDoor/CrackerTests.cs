using Advent2016.Bunny.SecurityDoor;
using Xunit;

namespace Bunny.UnitTests.SecurityDoor
{
    public class CrackerTests
    {
        [Fact]
        public void CrackerFindsPassword()
        {
            var cracker = new PasswordCracker();
            Assert.Equal("18f47a30", cracker.Crack("abc"));
        }
    }
}
