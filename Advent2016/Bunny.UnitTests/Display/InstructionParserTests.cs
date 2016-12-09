using Advent2016.Bunny.Display;
using Xunit;

namespace Bunny.UnitTests.Display
{
    public class InstructionParserTests
    {
        [Fact]
        public void RectangleGetsParsed()
        {
            var parser = new InstructionParser();
            var parsed = parser.Parse("rect 3x2");
            var proper = new Instruction(Instruction.Operation.Rectangle, 3, 2);
            Assert.Equal(proper, parsed);
        }

        [Fact]
        public void LargerRectangleGetsParsed()
        {
            var parser = new InstructionParser();
            var parsed = parser.Parse("rect 13x22");
            var proper = new Instruction(Instruction.Operation.Rectangle, 13, 22);
            Assert.Equal(proper, parsed);
        }

        [Fact]
        public void RowRotationGetsParsed()
        {
            var parser = new InstructionParser();
            var parsed = parser.Parse("rotate row y=0 by 4");
            var proper = new Instruction(Instruction.Operation.RowRotation, 0, 4);
            Assert.Equal(proper, parsed);
        }

        [Fact]
        public void ColumnRotationGetsParsed()
        {
            var parser = new InstructionParser();
            var parsed = parser.Parse("rotate column x=14 by 31");
            var proper = new Instruction(Instruction.Operation.ColRotation, 14, 31);
            Assert.Equal(proper, parsed);
        }
    }
}
