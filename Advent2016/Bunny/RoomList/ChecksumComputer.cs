using System.Linq;

namespace Advent2016.Bunny.RoomList
{
    public class ChecksumComputer
    {
        public static char[] Alphabet = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (char) i).ToArray();

        private const int ChecksumLength = 5;

        public string Compute(string encryptedName)
        {
            var charCounts = encryptedName.Where(c => char.IsLower(c) && char.IsLetter(c)).GroupBy(c => c);
            var sortedByAppearance =
                charCounts.OrderByDescending(g => g.Count()).ThenBy(g => g.Key).Select(g => g.Key).ToArray();
            var checksum = sortedByAppearance.Concat(Alphabet.Except(sortedByAppearance)).Take(ChecksumLength);
            return string.Join("", checksum);
        }
    }
}
