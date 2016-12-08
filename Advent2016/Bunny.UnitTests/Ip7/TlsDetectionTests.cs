using Advent2016.Bunny.Ip7;
using Xunit;

namespace Bunny.UnitTests.Ip7
{
    public class TlsDetectionTests
    {
        [Fact]
        public void NoAbbaInNormalMeansNoTls()
        {
            var address = new Ip7Address(new[] { new Ip7Sequence("abca", false) });
            var inspector = new AddressInspector();
            Assert.False(inspector.DetectTls(address));
        }

        [Fact]
        public void AaaaInNormalMeansNoTls()
        {
            var address = new Ip7Address(new[] { new Ip7Sequence("aaaa", false) });
            var inspector = new AddressInspector();
            Assert.False(inspector.DetectTls(address));
        }

        [Fact]
        public void AbbaInNormalMeansTls()
        {
            var address = new Ip7Address(new[] {new Ip7Sequence("abba", false)});
            var inspector = new AddressInspector();
            Assert.True(inspector.DetectTls(address));
        }

        [Fact]
        public void AbbaInLongerIsDetected()
        {
            var address = new Ip7Address(new[] { new Ip7Sequence("kjhabbaer", false) });
            var inspector = new AddressInspector();
            Assert.True(inspector.DetectTls(address));
        }

        [Fact]
        public void AbbaInHypernetMeansNoTls()
        {
            var address = new Ip7Address(new[] { new Ip7Sequence("abba", false), new Ip7Sequence("xyyx", true),  });
            var inspector = new AddressInspector();
            Assert.False(inspector.DetectTls(address));
        }

    }
}
