using System.Linq;
using Advent2016.Bunny.Ip7;

namespace Advent2016.Puzzle
{
    public class Day7CountSslIps : IPuzzle
    {
        public string Solve(string[] input)
        {
            var parser = new AddressParser();
            var ips = input.Select(line => parser.Parse(line));
            var inspector = new AddressInspector();
            return ips.Count(ip => inspector.DetectSsl(ip)).ToString();
        }
    }
}
