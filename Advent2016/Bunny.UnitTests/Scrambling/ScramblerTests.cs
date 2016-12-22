using Advent2016.Bunny.Scrambling;
using Xunit;

namespace Bunny.UnitTests.Scrambling
{
    public class ScramblerTests
    {
        private static IOperation SampleSwapPosition = new SwapPosition(4, 0);
        private static IOperation SampleSwapLetter = new SwapLetter('d', 'b');
        private static IOperation SampleReverse = new Reverse(0, 4);
        private static IOperation SampleRotate1 = new Rotate(Rotate.RotateDirection.left, 1);
        private static IOperation SampleRotate2 = new Rotate(Rotate.RotateDirection.right, 8);
        private static IOperation SampleMove1 = new Move(1, 4);
        private static IOperation SampleMove2 = new Move(3, 0);
        private static IOperation SampleRotateBy1 = new RotateByLetter('b');
        private static IOperation SampleRotateBy2 = new RotateByLetter('d');

        [Fact]
        public void SwapPositionWorks()
        {
            var scrambler = new Scrambler();
            Assert.Equal("ebcda", scrambler.Scramble("abcde", new[] {SampleSwapPosition}));
        }

        [Fact]
        public void SwapLetterWorks()
        {
            var scrambler = new Scrambler();
            Assert.Equal("edcba", scrambler.Scramble("ebcda", new[] { SampleSwapLetter }));
        }

        [Fact]
        public void ReverseWorks()
        {
            var scrambler = new Scrambler();
            Assert.Equal("abcde", scrambler.Scramble("edcba", new[] { SampleReverse }));
        }

        [Fact]
        public void RotateWorks()
        {
            var scrambler = new Scrambler();
            Assert.Equal("bcdea", scrambler.Scramble("abcde", new[] { SampleRotate1 }));
            Assert.Equal("cdeab", scrambler.Scramble("abcde", new[] { SampleRotate2 }));
        }

        [Fact]
        public void MoveWorks()
        {
            var scrambler = new Scrambler();
            Assert.Equal("bdeac", scrambler.Scramble("bcdea", new[] { SampleMove1 }));
            Assert.Equal("abdec", scrambler.Scramble("bdeac", new[] { SampleMove2 }));
        }

        [Fact]
        public void RotateByLetterWorks()
        {
            var scrambler = new Scrambler();
            Assert.Equal("ecabd", scrambler.Scramble("abdec", new[] { SampleRotateBy1 }));
            Assert.Equal("decab", scrambler.Scramble("ecabd", new[] { SampleRotateBy2 }));
        }

        [Fact]
        public void ScramblingWithSequenceWorks()
        {
            var scrambler = new Scrambler();
            var sequence = new[]
            {
                SampleSwapPosition, SampleSwapLetter, SampleReverse, SampleRotate1, SampleMove1, SampleMove2,
                SampleRotateBy1, SampleRotateBy2
            };
            Assert.Equal("decab", scrambler.Scramble("abcde", sequence));
        }
    }
}
