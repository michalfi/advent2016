using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent2016.Bunny.Compression
{
    public class ExplosiveReader
    {
        private MarkerFinder Finder = new MarkerFinder();

        public string Decompress(string message)
        {
            var decompressed = new StringBuilder();
            int position = 0;
            while (position < message.Length)
            {
                var marker = Finder.FindFirst(message, position);
                int unchangedLen = (marker.HasValue ? marker.Value.Position : message.Length) - position;
                decompressed.Append(message, position, unchangedLen);
                if (!marker.HasValue)
                    break;

                string repeated = message.Substring(marker.Value.RepetitionStart, marker.Value.RepetitionLength);
                for (int i = 0; i < marker.Value.RepetitionCount; i++)
                    decompressed.Append(repeated);
                position = marker.Value.RepetitionStart + marker.Value.RepetitionLength;
            }
            return decompressed.ToString();
        }

    }
}
