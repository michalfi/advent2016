using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Advent2016.Bunny.DynamicMaze
{
    public class Dynamizer
    {
        private MD5 Hasher { get; } = MD5.Create();

        public bool[] DoorsState(string passcode, Position position)
        {
            var data = passcode + position.Path;
            var hashBytes = this.Hasher.ComputeHash(Encoding.ASCII.GetBytes(data));
            return hashBytes.Take(2).SelectMany(b => new[] {b/16, b%16}).Select(x => x > 10).ToArray();
        }
    }
}
