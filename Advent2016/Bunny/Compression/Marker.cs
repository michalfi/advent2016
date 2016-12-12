namespace Advent2016.Bunny.Compression
{
    public struct Marker
    {
        public int RepetitionCount;
        public int RepetitionLength;
        public int RepetitionStart;
        public int Position;

        public Marker(int position, int repetitionCount, int repetitionStart, int repetitionLength)
        {
            Position = position;
            RepetitionCount = repetitionCount;
            RepetitionStart = repetitionStart;
            RepetitionLength = repetitionLength;
        }
    }
}
