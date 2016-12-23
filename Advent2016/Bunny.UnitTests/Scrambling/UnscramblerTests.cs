using System.Linq;
using Advent2016.Bunny.Scrambling;
using Xunit;

namespace Bunny.UnitTests.Scrambling
{
    public class UnscramblerTests
    {
        [Fact]
        public void SwapPositionWorks()
        {
            var unscrambler = new Unscrambler();
            Assert.Equal("abcde", unscrambler.Unscramble("ebcda", new[] { SampleOps.Swap4With0 }));
        }

        [Fact]
        public void SwapLetterWorks()
        {
            var unscrambler = new Unscrambler();
            Assert.Equal("ebcda", unscrambler.Unscramble("edcba", new[] { SampleOps.SwapDWithB }));
        }

        [Fact]
        public void ReverseWorks()
        {
            var unscrambler = new Unscrambler();
            Assert.Equal("edcba", unscrambler.Unscramble("abcde", new[] { SampleOps.Reverse0Through4 }));
        }

        [Fact]
        public void RotateWorks()
        {
            var unscrambler = new Unscrambler();
            Assert.Equal("abcde", unscrambler.Unscramble("bcdea", new[] { SampleOps.Rotate1Left }));
            Assert.Equal("abcde", unscrambler.Unscramble("cdeab", new[] { SampleOps.Rotate8Right }));
        }

        [Fact]
        public void MoveWorks()
        {
            var unscrambler = new Unscrambler();
            Assert.Equal("bcdea", unscrambler.Unscramble("bdeac", new[] { SampleOps.Move1To4 }));
            Assert.Equal("bdeac", unscrambler.Unscramble("abdec", new[] { SampleOps.Move3To0 }));
        }

        [Fact]
        public void RotateByLetterWorks()
        {
            var unscrambler = new Unscrambler();
            Assert.Equal("abcdefgh", unscrambler.Unscramble("ghabcdef", new[] { SampleOps.RotateByB }));
            Assert.Equal("abcdefgh", unscrambler.Unscramble("efghabcd", new[] { SampleOps.RotateByD }));
            Assert.Equal("abcdefgh", unscrambler.Unscramble("habcdefg", new[] { SampleOps.RotateByH }));
        }

        [Fact]
        public void ScramblingWithSequenceWorks()
        {
            var unscrambler = new Unscrambler();
            Assert.Equal("abcde", unscrambler.Unscramble("abdec", SampleOps.Sequence.Where(op => !(op is RotateByLetter))));
        }
    }
}
