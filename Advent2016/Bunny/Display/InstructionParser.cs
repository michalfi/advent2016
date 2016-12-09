using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2016.Bunny.Display
{
    public class InstructionParser
    {
        private Regex RectangleRe { get; } = new Regex(@"rect (\d+)x(\d+)");

        private Regex RowRotationRe { get; } = new Regex(@"rotate row y=(\d+) by (\d+)");

        private Regex ColRotationRe { get; } = new Regex(@"rotate column x=(\d+) by (\d+)");

        public Instruction Parse(string instructionText)
        {
            var choices = new[]
            {
                new {re = RectangleRe, op = Instruction.Operation.Rectangle},
                new {re = RowRotationRe, op = Instruction.Operation.RowRotation},
                new {re = ColRotationRe, op = Instruction.Operation.ColRotation}
            };
            var instruction = choices.Select(c => Match(c.re, c.op, instructionText)).FirstOrDefault(i => i != null);
            if (instruction != null)
                return instruction;
            throw new InvalidDataException($"Not an instruction: {instructionText}");
        }

        private static Instruction Match(Regex regex, Instruction.Operation op, string input)
        {
            var m = regex.Match(input);
            if (m.Success)
                return new Instruction(op, ParamA(m), ParamB(m));
            return null;
        }

        private static byte ParamA(Match m)
        {
            int value;
            if (!int.TryParse(m.Groups[1].Value, out value) || value > byte.MaxValue || value < byte.MinValue)
                throw new InvalidDataException($"Parameter A out of bounds: {m.Groups[1].Value}");
            return (byte) value;
        }

        private static byte ParamB(Match m)
        {
            int value;
            if (!int.TryParse(m.Groups[2].Value, out value) || value > byte.MaxValue || value < byte.MinValue)
                throw new InvalidDataException($"Parameter B out of bounds: {m.Groups[2].Value}");
            return (byte)value;
        }
    }
}
