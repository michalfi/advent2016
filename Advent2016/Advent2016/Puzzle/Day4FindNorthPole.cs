using System.Linq;
using Advent2016.Bunny.RoomList;

namespace Advent2016.Puzzle
{
    public class Day4FindNorthPole : IPuzzle
    {
        public string Solve(string[] input)
        {
            var parser = new EntryParser();
            var list = input.Select(parser.Parse);
            var cc = new ChecksumComputer();
            var realRooms = list.Where(r => r.DeclaredChecksum == cc.Compute(r.EncryptedName));
            var shifter = new Shifter();
            var decrypted = realRooms.Select(r => new {name = shifter.Shift(r.EncryptedName, r.SectorId), r.SectorId});
            var northPole = decrypted.FirstOrDefault(x => x.name.Contains("northpole"));
            return northPole?.SectorId.ToString() ?? "Not found";
        }
    }
}
