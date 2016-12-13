using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2016.Bunny.Factory
{
    public class SwarmScript
    {
        public SwarmScript(IEnumerable<IBotInstruction> instructions)
        {
            Instructions = instructions.ToArray();
        }

        private IBotInstruction[] Instructions { get; }

        public SwarmLayout Compile()
        {
            var outputs = new Dictionary<int, int>();
            var bots = this.Instructions.OfType<BotInstruction>().Select(bi => new Bot(bi)).ToDictionary(b => b.Id);
            foreach (var input in this.Instructions.OfType<InputInstruction>())
                bots[input.Bot].AddValue(input.Value);
            var ready = new Queue<Bot>(bots.Values.Where(b => b.HasEnoughValues));

            while (ready.Count > 0)
            {
                var b = ready.Dequeue();
                var lowReady = this.PropagateValue(Math.Min(b.Values[0], b.Values[1]), b.Instruction.LowDestination, bots, outputs);
                var highReady = this.PropagateValue(Math.Max(b.Values[0], b.Values[1]), b.Instruction.HighDestination, bots, outputs);
                if (lowReady != null)
                    ready.Enqueue(lowReady);
                if (highReady != null)
                    ready.Enqueue(highReady);
            }

            return new SwarmLayout(bots, outputs);
        }

        private Bot PropagateValue(int value, Destination destination, Dictionary<int, Bot> bots, Dictionary<int, int> outputs)
        {
            switch (destination.Kind)
            {
                case DestinationKind.bot:
                    var target = bots[destination.Id];
                    target.AddValue(value);
                    return target.HasEnoughValues ? target : null;
                case DestinationKind.output:
                default:
                    outputs[destination.Id] = value;
                    return null;
            }
        }
    }
}
