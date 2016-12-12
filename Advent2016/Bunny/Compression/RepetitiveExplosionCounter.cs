namespace Advent2016.Bunny.Compression
{
    public class RepetitiveExplosionCounter
    {
        private MarkerFinder Finder = new MarkerFinder();

        public long DecompressedLength(string message)
        {
            long len = 0;
            int position = 0;
            while (position < message.Length)
            {
                var marker = Finder.FindFirst(message, position);
                int unchangedLen = (marker.HasValue ? marker.Value.Position : message.Length) - position;
                len += unchangedLen;
                if (!marker.HasValue)
                    break;

                string repeated = message.Substring(marker.Value.RepetitionStart, marker.Value.RepetitionLength);
                len += this.DecompressedLength(repeated)*marker.Value.RepetitionCount;
                position = marker.Value.RepetitionStart + marker.Value.RepetitionLength;
            }

            return len;
        }
    }
}
