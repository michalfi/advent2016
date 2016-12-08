using Advent2016.Bunny.Ip7;
using Xunit;

namespace Bunny.UnitTests.Ip7
{
    public class SslDetectionTests
    {
        [Fact]
        public void AbaAndBabMeansSsl()
        {
            var address = new Ip7Address(new []{new Ip7Sequence("aba", false), new Ip7Sequence("bab", true), new Ip7Sequence("xyz", false) });
            var inspector = new AddressInspector();
            Assert.True(inspector.DetectSsl(address));
        }

        [Fact]
        public void AbaAndAbaMeansNoSsl()
        {
            var address = new Ip7Address(new[] { new Ip7Sequence("xyx", false), new Ip7Sequence("xyx", true), new Ip7Sequence("xyx", false) });
            var inspector = new AddressInspector();
            Assert.False(inspector.DetectSsl(address));
        }

        [Fact]
        public void SecondAbaIsDetected()
        {
            var address = new Ip7Address(new[] { new Ip7Sequence("zazbz", false), new Ip7Sequence("bzb", true), new Ip7Sequence("cdb", false) });
            var inspector = new AddressInspector();
            Assert.True(inspector.DetectSsl(address));
        }
    }
}
