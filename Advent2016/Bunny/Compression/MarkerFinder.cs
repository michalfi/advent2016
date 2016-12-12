using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Advent2016.Bunny.Compression
{
    public class MarkerFinder
    {
        private static Regex MarkerRe = new Regex(@"\((\d+)x(\d+)\)");

        public Marker? FindFirst(string message, int startPosition)
        {
            var match = MarkerRe.Match(message, startPosition);
            if (!match.Success)
                return null;
            return ReadMarker(match);
        }

        private static Marker ReadMarker(Match match)
        {
            Debug.Assert(match.Success);

            int repCount, repLength;
            if (!int.TryParse(match.Groups[1].Value, out repLength) || !int.TryParse(match.Groups[2].Value, out repCount))
                throw new InvalidDataException();

            return new Marker(match.Index, repCount, match.Index + match.Length, repLength);
        }
    }
}
