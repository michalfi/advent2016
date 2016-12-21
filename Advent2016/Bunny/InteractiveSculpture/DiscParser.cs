using System.Text.RegularExpressions;

namespace Advent2016.Bunny.InteractiveSculpture
{
    public class DiscParser
    {
        private Regex DiscRe = new Regex(@"Disc #(\d+) has (\d+) positions; at time=0, it is at position (\d+)");

        public Disc Parse(string description)
        {
            var m = DiscRe.Match(description);
            return new Disc(Value(m, 1), Value(m, 2), Value(m, 3));
        }

        private int Value(Match m, int group)
        {
            return int.Parse(m.Groups[group].Value);
        }
    }
}
