using System.Text.RegularExpressions;

namespace Advent2016.Bunny.Firewall
{
    public class Range
    {
        private static Regex RangeRegex { get; } = new Regex(@"(\d+)-(\d+)");

        public Range(uint start, uint end)
        {
            Start = start;
            End = end;
        }

        public uint Start { get; }

        public uint End { get; }

        public override string ToString() => $"{Start}-{End}";

        public static Range FromString(string input)
        {
            var match = RangeRegex.Match(input);
            return new Range(Limit(match.Groups[1]), Limit(match.Groups[2]));
        }

        private static uint Limit(Group input) => uint.Parse(input.Value);
    }
}
