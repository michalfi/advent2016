using System.Text.RegularExpressions;
using Advent2016.Bunny.Compression;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Bunny.UnitTests.Compression
{
    public class ExplosiveReaderTests
    {
        [Property]
        public Property MessageWithoutMarkerStaysIntact()
        {
            var markerRe = new Regex(@"\(\d+x\d+\)");
            var messagesWithoutMarker = Arb.Generate<string>().Where(msg => !string.IsNullOrEmpty(msg) && !markerRe.IsMatch(msg));
            var reader = new ExplosiveReader();
            return Prop.ForAll(messagesWithoutMarker.ToArbitrary(), msg => msg == reader.Decompress(msg));
        }

        [Theory]
        [InlineData("A(1x5)BC", "ABBBBBC")]
        [InlineData("(3x3)XYZ", "XYZXYZXYZ")]
        [InlineData("A(2x2)BCD(2x2)EFG", "ABCBCDEFEFG")]
        public void MessageWithMarkerIsDecompressed(string original, string decompressed)
        {
            var reader = new ExplosiveReader();
            Assert.Equal(decompressed, reader.Decompress(original));
        }

        [Theory]
        [InlineData("(6x1)(1x3)A", "(1x3)A")]
        [InlineData("X(8x2)(3x3)ABCY", "X(3x3)ABC(3x3)ABCY")]
        public void MarkerInRepeatedSequenceIsNotExploded(string original, string decompressed)
        {
            var reader = new ExplosiveReader();
            Assert.Equal(decompressed, reader.Decompress(original));
        }
    }
}
