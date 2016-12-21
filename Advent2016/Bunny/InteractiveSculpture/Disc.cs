namespace Advent2016.Bunny.InteractiveSculpture
{
    public class Disc
    {
        public Disc(int number, int size, int initialPosition)
        {
            Number = number;
            Size = size;
            InitialPosition = initialPosition;
        }

        public int Number { get; }
        public int Size { get; }
        public int InitialPosition { get; }

        public int Position(int launchTime)
        {
            return (Number + InitialPosition + launchTime)%Size;
        }
    }
}
