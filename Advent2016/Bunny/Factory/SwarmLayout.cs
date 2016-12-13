using System.Collections.Generic;

namespace Advent2016.Bunny.Factory
{
    public class SwarmLayout
    {
        public SwarmLayout(Dictionary<int, Bot> bots, Dictionary<int, int> outputs)
        {
            Bots = bots;
            Outputs = outputs;
        }

        public Dictionary<int, int> Outputs { get; }

        public Dictionary<int, Bot> Bots { get; }
    }
}
