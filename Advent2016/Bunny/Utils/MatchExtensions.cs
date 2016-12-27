using System.Text.RegularExpressions;

namespace Advent2016.Bunny.Utils
{
    public static class MatchExtensions
    {
        public static int Int(this Match match, int groupNumber)
        {
            return int.Parse(match.Groups[groupNumber].Value);
        }

        public static char Char(this Match match, int groupNumber)
        {
            return match.Groups[groupNumber].Value[0];
        }

        public static short Short(this Match match, int groupNumber)
        {
            return short.Parse(match.Groups[groupNumber].Value);
        }
    }
}
