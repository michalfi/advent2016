using System.Text.RegularExpressions;
using Advent2016.Bunny.Compression;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Bunny.UnitTests.Compression
{
    public class RepetitiveExplosionCounterTests
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
        [InlineData("(3x3)XYZ", 9)]
        [InlineData("X(8x2)(3x3)ABCY", 20)]
        [InlineData("(27x12)(20x12)(13x14)(7x10)(1x12)A", 241920)]
        [InlineData("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN", 445)]
        public void MessageWithMarkersIsDecompressed(string message, int decompressedLength)
        {
            var reader = new RepetitiveExplosionCounter();
            Assert.Equal(decompressedLength, reader.DecompressedLength(message));
        }
    }
}
