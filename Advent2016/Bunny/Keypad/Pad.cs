using System.Collections.Generic;
using System.Linq;

namespace Advent2016.Bunny.Keypad
{
    public class Pad
    {
        public Pad(IEnumerable<PadKey> keys)
        {
            this.Keys = keys.ToDictionary(k => k.Key);
        }

        private Dictionary<char, PadKey> Keys { get; }

        public PadKey this[char key] => this.Keys[key];
    }
}
