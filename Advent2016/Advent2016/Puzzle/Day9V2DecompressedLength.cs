using Advent2016.Bunny.Compression;

namespace Advent2016.Puzzle
{
    public class Day9V2DecompressedLength : IPuzzle
    {
        public string Solve(string[] input)
        {
            string message = string.Join("", input);
            var reader = new RepetitiveExplosionCounter();
            var len = reader.DecompressedLength(message);
            return len.ToString();
        }
    }
}
