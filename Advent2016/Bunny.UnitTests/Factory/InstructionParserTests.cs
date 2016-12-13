using Advent2016.Bunny.Factory;
using FsCheck;
using FsCheck.Experimental;
using FsCheck.Xunit;
using Xunit;

namespace Bunny.UnitTests.Factory
{
    public class InstructionParserTests
    {
        [Property]
        public Property InputInstructionGetsParsed()
        {
            var inputInstructions = from value in Arb.Generate<byte>()
                from bot in Arb.Generate<byte>()
                select new { value, bot, instruction = $"value {value} goes to bot {bot}"};
            var parser = new InstructionParser();
            var parsing = inputInstructions.Select(i => new {original = i, parsed = parser.Parse(i.instruction) as InputInstruction});
            return Prop.ForAll(parsing.ToArbitrary(),
                p => p.parsed != null && p.parsed.Value == p.original.value && p.parsed.Bot == p.original.bot);
        }

        [Theory]
        [MemberData(nameof(SampleInstructions))]
        public void SampleInstructionsGetParsed(string instruction, IBotInstruction parsed)
        {
            var parser = new InstructionParser();
            Assert.Equal(parsed, parser.Parse(instruction));
        }

        public static object[][] SampleInstructions { get; } =
        {
            new object[] {"value 5 goes to bot 2", new InputInstruction(5, 2)},
            new object[]
            {
                "bot 2 gives low to bot 1 and high to bot 0",
                new BotInstruction(2, new Destination(DestinationKind.bot, 1), new Destination(DestinationKind.bot, 0))
            },
            new object[]
            {
                "bot 1 gives low to output 1 and high to bot 0",
                new BotInstruction(1, new Destination(DestinationKind.output, 1),
                    new Destination(DestinationKind.bot, 0))
            },
            new object[]
            {
                "bot 0 gives low to output 2 and high to output 0",
                new BotInstruction(0, new Destination(DestinationKind.output, 2),
                    new Destination(DestinationKind.output, 0))
            }
        };
    }
}
