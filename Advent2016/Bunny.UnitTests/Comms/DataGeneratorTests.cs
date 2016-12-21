using Advent2016.Bunny.Comms;
using Xunit;

namespace Bunny.UnitTests.Comms
{
    public class DataGeneratorTests
    {
        [Theory]
        [InlineData("abc", 123, "e99a18c428cb38d5f260853678922e03")]
        [InlineData("def", 0, "afa4808e96dafc87d7ca7d4069f39312")]
        public void GeneratedDataIsHashOfSaltAndIndex(string salt, int index, string hash)
        {
            var gen = new DataGenerator(salt);
            Assert.Equal(hash, gen.Get(index));
        }

        [Theory]
        [InlineData("abc", 1, 0, "eec80a0c92dc8a0777c619d9bb51e910")]
        [InlineData("abc", 2016, 0, "a107ff634856bb300138cac6568c0f24")]
        public void StretchingIsApplied(string salt, int stretch, int index, string hash)
        {
            var gen = new DataGenerator(salt, stretch);
            Assert.Equal(hash, gen.Get(index));
        }
    }
}
