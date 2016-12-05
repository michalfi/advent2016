using System.Linq;
using Advent2016.Bunny.RoomList;

namespace Advent2016.Puzzle
{
    public class Day4EncryptedRooms : IPuzzle
    {
        public string Solve(string[] input)
        {
            var parser = new EntryParser();
            var list = input.Select(parser.Parse);
            var cc = new ChecksumComputer();
            var realRooms = list.Where(r => r.DeclaredChecksum == cc.Compute(r.EncryptedName));
            return realRooms.Sum(r => r.SectorId).ToString();
        }
    }
}
