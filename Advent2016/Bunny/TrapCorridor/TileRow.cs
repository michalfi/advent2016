using System.Collections;
using System.Linq;
using Advent2016.Bunny.ChipAssembly;

namespace Advent2016.Bunny.TrapCorridor
{
    public class TileRow
    {
        public TileRow(BitArray tileTrapped)
        {
            TileTrapped = tileTrapped;
            Width = tileTrapped.Length;
        }

        public BitArray TileTrapped { get; }

        public int Width { get; }

        public int SafeTileCount => Width - TileTrapped.Cardinality();

        public override string ToString()
        {
            return string.Join("", TileTrapped.Cast<bool>().Select(b => b ? '^' : '.'));
        }
    }
}
