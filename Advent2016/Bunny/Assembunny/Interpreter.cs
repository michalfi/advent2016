using System;
using System.Diagnostics;

namespace Advent2016.Bunny.Assembunny
{
    public class Interpreter
    {
        public Interpreter(RegisterMap registers)
        {
            this.Registers = new int[registers.RegisterIndex.Count];
        }

        public int[] Registers { get; }

        public void Execute(IInstruction[] program)
        {
            for (int ip = 0; ip < program.Length; ip++)
            {
                Debug.Assert(ip >= 0);
                Execute(program[ip] as dynamic, ref ip);
            }
        }

        private void Execute(Inc instruction, ref int ip)
        {
            this.Registers[instruction.Register.Index] += 1;
        }

        private void Execute(Dec instruction, ref int ip)
        {
            this.Registers[instruction.Register.Index] -= 1;
        }

        private void Execute(Cpy instruction, ref int ip)
        {
            int value = Value(instruction.Source as dynamic);
            this.Registers[instruction.Target.Index] = value;
        }

        private void Execute(Jnz instruction, ref int ip)
        {
            int value = Value(instruction.Source as dynamic);
            if (value != 0)
                ip += instruction.Count - 1;
        }

        private int Value(Register source) => this.Registers[source.Index];

        private int Value(Literal source) => source.Value;
    }
}
