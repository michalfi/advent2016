using Advent2016.Bunny.Scrambling;
using Xunit;

namespace Bunny.UnitTests.Scrambling
{
    public class ScramblerTests
    {
        [Fact]
        public void SwapPositionWorks()
        {
            var scrambler = new Scrambler();
            Assert.Equal("ebcda", scrambler.Scramble("abcde", new[] {SampleOps.Swap4With0}));
        }

        [Fact]
        public void SwapLetterWorks()
        {
            var scrambler = new Scrambler();
            Assert.Equal("edcba", scrambler.Scramble("ebcda", new[] { SampleOps.SwapDWithB }));
        }

        [Fact]
        public void ReverseWorks()
        {
            var scrambler = new Scrambler();
            Assert.Equal("abcde", scrambler.Scramble("edcba", new[] { SampleOps.Reverse0Through4 }));
        }

        [Fact]
        public void RotateWorks()
        {
            var scrambler = new Scrambler();
            Assert.Equal("bcdea", scrambler.Scramble("abcde", new[] { SampleOps.Rotate1Left }));
            Assert.Equal("cdeab", scrambler.Scramble("abcde", new[] { SampleOps.Rotate8Right }));
        }

        [Fact]
        public void MoveWorks()
        {
            var scrambler = new Scrambler();
            Assert.Equal("bdeac", scrambler.Scramble("bcdea", new[] { SampleOps.Move1To4 }));
            Assert.Equal("abdec", scrambler.Scramble("bdeac", new[] { SampleOps.Move3To0 }));
        }

        [Fact]
        public void RotateByLetterWorks()
        {
            var scrambler = new Scrambler();
            Assert.Equal("ecabd", scrambler.Scramble("abdec", new[] { SampleOps.RotateByB }));
            Assert.Equal("decab", scrambler.Scramble("ecabd", new[] { SampleOps.RotateByD }));
        }

        [Fact]
        public void ScramblingWithSequenceWorks()
        {
            var scrambler = new Scrambler();
            Assert.Equal("decab", scrambler.Scramble("abcde", SampleOps.Sequence));
        }
    }
}
