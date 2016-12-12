using Advent2016.Bunny.Compression;

namespace Advent2016.Puzzle
{
    public class Day9DecompressedFileLength : IPuzzle
    {
        public string Solve(string[] input)
        {
            string message = string.Join("", input);
            var reader = new ExplosiveReader();
            var decompressed = reader.Decompress(message);
            return decompressed.Length.ToString();
        }
    }
}
