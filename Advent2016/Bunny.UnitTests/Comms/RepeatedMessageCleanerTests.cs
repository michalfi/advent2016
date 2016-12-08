using System.Linq;
using Advent2016.Bunny.Comms;
using Fare;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Bunny.UnitTests.Comms
{
    public class RepeatedMessageCleanerTests
    {
        private static Gen<byte> LengthGen => Arb.Generate<byte>().Where(len => len > 0);

        private static Gen<short> CountGen => Arb.Generate<short>().Where(c => c > 0);

        private static Gen<string[]> AlphaStringSetGen(int length, int count)
        {
            var regex = $"[a-z]{{{length}}}";
            var xeger = new Xeger(regex);
            return Gen.ArrayOf(count, Arb.Generate<bool>().Select(_ => xeger.Generate()));
        }

        [Property]
        public Property CleanMessageHasProperLength()
        {
            var msgSetGen = LengthGen.SelectMany(len => CountGen, (len, count) => new {len, count})
                .SelectMany(
                    x =>
                        Gen.ArrayOf(x.count, Gen.ArrayOf(x.len, Arb.Generate<char>()).Select(msg => new string(msg)))
                            .Select(messages => new {x.len, messages}));

            // alternative way to write it
            var msgSetGen2 = from len in LengthGen
                from count in CountGen
                from messages in
                Gen.ArrayOf(count, Gen.ArrayOf(len, Arb.Generate<char>()).Select(msg => new string(msg)))
                select new {len, messages};

            var cleaner = new RepeatedMessageCleaner(RepeatedMessageCleaner.Protocol.Simple);
            var modified = new RepeatedMessageCleaner(RepeatedMessageCleaner.Protocol.Modified);
            return Prop.ForAll(msgSetGen.ToArbitrary(),
                msgset =>
                    cleaner.CleanMessage(msgset.messages).Length == msgset.len &&
                    modified.CleanMessage(msgset.messages).Length == msgset.len);
        }

        [Property]
        public Property CleanMessageContainsMostFrequentChars()
        {
            var msgSetGen = LengthGen.SelectMany(len => CountGen, (len, count) => new {len, count})
                .SelectMany(x => AlphaStringSetGen(x.len, x.count).Select(messages => new {x.len, messages}));
            var cleaner = new RepeatedMessageCleaner(RepeatedMessageCleaner.Protocol.Simple);
            return Prop.ForAll(msgSetGen.ToArbitrary(),
                msgset =>
                    cleaner.CleanMessage(msgset.messages)
                        .Select(
                            (c, i) =>
                                msgset.messages.Select(msg => msg[i]).GroupBy(ch => ch).Max(g => g.Count()) ==
                                msgset.messages.Select(msg => msg[i]).Count(ch => ch == c))
                        .All(mostFrequent => mostFrequent));
        }

        [Property]
        public Property CleanMessageContainsLeastFrequentWhenUsingModifiedProtocol()
        {
            var msgSetGen = LengthGen.SelectMany(len => CountGen, (len, count) => new { len, count })
                .SelectMany(x => AlphaStringSetGen(x.len, x.count).Select(messages => new { x.len, messages }));
            var cleaner = new RepeatedMessageCleaner(RepeatedMessageCleaner.Protocol.Modified);
            return Prop.ForAll(msgSetGen.ToArbitrary(),
                msgset =>
                    cleaner.CleanMessage(msgset.messages)
                        .Select(
                            (c, i) =>
                                msgset.messages.Select(msg => msg[i]).GroupBy(ch => ch).Min(g => g.Count()) ==
                                msgset.messages.Select(msg => msg[i]).Count(ch => ch == c))
                        .All(mostFrequent => mostFrequent));
        }

        [Fact]
        public void SampleMessageIsDecoded()
        {
            var sampleMsgSet = new[]
            {
                "eedadn", "drvtee", "eandsr", "raavrd", "atevrs", "tsrnev", "sdttsa", "rasrtv", "nssdts", "ntnada",
                "svetve", "tesnvt", "vntsnd", "vrdear", "dvrsen", "enarar"
            };
            var cleaner = new RepeatedMessageCleaner(RepeatedMessageCleaner.Protocol.Simple);
            Assert.Equal("easter", cleaner.CleanMessage(sampleMsgSet));
            var modified = new RepeatedMessageCleaner(RepeatedMessageCleaner.Protocol.Modified);
            Assert.Equal("advent", modified.CleanMessage(sampleMsgSet));
        }
    }
}
