using System.Diagnostics;
using System.Linq;

namespace Advent2016.Bunny.Factory
{
    public class Bot
    {
        public Bot(BotInstruction instruction)
        {
            this.Instruction = instruction;
            this.Id = instruction.Bot;
        }

        public int Id { get; }

        public BotInstruction Instruction { get; }

        public int[] Values { get; } = new int[2];

        private int ValueCount { get; set; } = 0;

        public bool HasEnoughValues => this.ValueCount >= 2;

        public void AddValue(int value)
        {
            Debug.Assert(this.ValueCount < 2);
            this.Values[this.ValueCount] = value;
            this.ValueCount += 1;
        }
    }
}
