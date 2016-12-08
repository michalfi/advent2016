using Advent2016.Bunny.Ip7;
using Xunit;

namespace Bunny.UnitTests.Ip7
{
    public class AddressParserTests
    {
        [Fact]
        public void ShortAddressIsParsed()
        {
            var parsed = new AddressParser().Parse("abba[mnop]qrst");
            var manual = new Ip7Address(new []{new Ip7Sequence("abba", false), new Ip7Sequence("mnop", true), new Ip7Sequence("qrst", false) });
            Assert.Equal(manual, parsed);
        }

        [Fact]
        public void LongAddressIsParsed()
        {
            var parsed =
                new AddressParser().Parse(
                    "kcnplnobxleghgdvuj[xmkpquawwovbgbki]ydrgjkuwsnowlxp[otgpeovujsfeshns]vqiwhcljdyfdrgpss[mbueikaehexofmdkxtz]mbgagruljphuhapf");
            var manual =
                new Ip7Address(new[]
                {
                    new Ip7Sequence("kcnplnobxleghgdvuj", false), new Ip7Sequence("xmkpquawwovbgbki", true),
                    new Ip7Sequence("ydrgjkuwsnowlxp", false), new Ip7Sequence("otgpeovujsfeshns", true),
                    new Ip7Sequence("vqiwhcljdyfdrgpss", false), new Ip7Sequence("mbueikaehexofmdkxtz", true),
                    new Ip7Sequence("mbgagruljphuhapf", false)
                });
            Assert.Equal(manual, parsed);
        }

        [Fact]
        public void AddressWithLeadingAndTrailingHypernetSequenceIsParsed()
        {
            var parsed = new AddressParser().Parse("[xxyy]abba[mnop]");
            var manual = new Ip7Address(new []{new Ip7Sequence("xxyy", true), new Ip7Sequence("abba", false), new Ip7Sequence("mnop", true) });
            Assert.Equal(manual, parsed);
        }
    }
}
