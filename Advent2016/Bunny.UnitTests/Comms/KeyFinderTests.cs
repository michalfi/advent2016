using System.Linq;
using Advent2016.Bunny.Comms;
using Xunit;

namespace Bunny.UnitTests.Comms
{
    public class KeyFinderTests
    {
        [Fact]
        public void IndicesAreCorrectForSampleSalt()
        {
            var gen = new DataGenerator("abc");
            var finder= new KeyFinder(gen);
            var indices = finder.KeyIndices().Take(64).ToArray();

            Assert.Equal(39, indices[0]);
            Assert.Equal(92, indices[1]);
            Assert.Equal(22728, indices[63]);
        }

        [Fact]
        public void IndicesAreCorrectWithStreching()
        {
            var gen = new DataGenerator("abc", 2016);
            var finder = new KeyFinder(gen);

            Assert.Equal(10, finder.KeyIndices().First());
        }
    }
}
