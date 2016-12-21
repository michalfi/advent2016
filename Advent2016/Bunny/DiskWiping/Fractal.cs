using System.Collections;
using System.Linq;
using Advent2016.Bunny.ChipAssembly;
using MoreLinq;

namespace Advent2016.Bunny.DiskWiping
{
    public class Fractal
    {
        public Fractal(BitArray initial, int minSize)
        {
            var value = initial;
            while (value.Length < minSize)
            {
                var len = value.Length;
                var reversed = value.MakeNot().Cast<bool>().Reverse();
                value.Length = len*2 + 1;
                value[len] = false;
                reversed.ForEach((b, i) => value[len + i + 1] = b);
            }
            Value = value;
        }

        public BitArray Value { get; }
    }
}
