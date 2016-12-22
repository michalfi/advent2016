using System.Collections;
using System.Linq;

namespace Advent2016.Bunny.TrapCorridor
{
    public class RowGenerator
    {
        public TileRow FromString(string input)
        {
            var bits = input.Select(c => c == '^');
            return new TileRow(new BitArray(bits.ToArray()));
        }

        public TileRow Following(TileRow previous)
        {
            var bits = new BitArray(previous.Width);
            for (int i = 0; i < previous.Width; i++)
            {
                var left = i > 0 && previous.TileTrapped[i - 1];
                var right = i < previous.Width - 1 && previous.TileTrapped[i + 1];
                bits.Set(i, left ^ right);
            }
            return new TileRow(bits);
        }
    }
}
