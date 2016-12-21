using Advent2016.Bunny.ChipAssembly;
using Advent2016.Bunny.DiskWiping;

namespace Advent2016.Puzzle
{
    public class Day16ComputeChecksum : IPuzzle
    {
        public Day16ComputeChecksum(int diskLen)
        {
            DiskLen = diskLen;
        }

        private int DiskLen { get; }

        public string Solve(string[] input)
        {
            var initial = BitArrayExtensions.FromDigitString(input[0]);
            var data = new Fractal(initial, DiskLen);
            var truncated = data.Value.ToDigitString().Substring(0, DiskLen);
            var checksum = new Checksum(truncated);
            return checksum.Value;
        }
    }
}
