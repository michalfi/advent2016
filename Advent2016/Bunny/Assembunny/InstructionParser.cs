using Dnum;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Advent2016.Bunny.Assembunny
{
    public class InstructionParser
    {
        private enum InstructionCode
        {
            cpy,
            inc,
            dec,
            jnz
        }

        private static Regex InstructionRe = new Regex(@"(\w+) (-?\w+) ?(-?\w*)");

        public InstructionParser(RegisterMap registers)
        {
            Registers = registers;
        }

        private RegisterMap Registers { get; }

        public IInstruction Parse(string instruction)
        {
            var match = InstructionRe.Match(instruction);
            Debug.Assert(match.Success);
            var code = Dnum<InstructionCode>.Parse(match.Groups[1].Value);
            switch (code)
            {
                case InstructionCode.inc:
                    return new Inc(Register(match.Groups[2].Value));
                case InstructionCode.dec:
                    return new Dec(Register(match.Groups[2].Value));
                case InstructionCode.cpy:
                    return new Cpy(Source(match.Groups[2].Value), Register(match.Groups[3].Value));
                case InstructionCode.jnz:
                default:
                    return new Jnz(Source(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
            }
        }

        private ISource Source(string description)
        {
            int regIndex;
            if (description.Length == 1 && Registers.RegisterIndex.TryGetValue(description[0], out regIndex))
                return new Register(description[0], regIndex);
            return new Literal(int.Parse(description));
        }

        private Register Register(string description)
        {
            return new Register(description[0], Registers.RegisterIndex[description[0]]);
        }
    }
}
