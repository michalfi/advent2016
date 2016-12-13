using System.Linq;
using Advent2016.Bunny.Factory;
using Xunit;

namespace Bunny.UnitTests.Factory
{
    public class SwarmScriptCompilationTests
    {
        [Fact]
        public void OutputBinsHaveProperValuesForSampleScript()
        {
            var layout = new SwarmScript(this.SampleScript).Compile();

            Assert.Equal(5, layout.Outputs[0]);
            Assert.Equal(2, layout.Outputs[1]);
            Assert.Equal(3, layout.Outputs[2]);
        }

        [Fact]
        public void BotGetsInputValues()
        {
            var layout = new SwarmScript(this.SampleScript).Compile();

            Assert.Equal(2, layout.Bots[2].Values.Min());
            Assert.Equal(5, layout.Bots[2].Values.Max());
        }

        [Fact]
        public void ValuesPropagateToNextBot()
        {
            var layout = new SwarmScript(this.SampleScript).Compile();

            Assert.Equal(3, layout.Bots[0].Values.Min());
            Assert.Equal(5, layout.Bots[0].Values.Max());
        }

        private IBotInstruction[] SampleScript { get; } =
        {
                new InputInstruction(5, 2),
                new BotInstruction(2, new Destination(DestinationKind.bot, 1), new Destination(DestinationKind.bot, 0)),
                new InputInstruction(3, 1),
                new BotInstruction(1, new Destination(DestinationKind.output, 1),
                    new Destination(DestinationKind.bot, 0)),
                new BotInstruction(0, new Destination(DestinationKind.output, 2),
                    new Destination(DestinationKind.output, 0)),
                new InputInstruction(2, 2),
        };

    }
}
