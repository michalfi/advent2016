namespace Advent2016.Bunny.Scrambling
{
    public class Rotate : IOperation
    {
        public Rotate(RotateDirection direction, int count)
        {
            Direction = direction;
            Count = count;
        }

        public enum RotateDirection
        {
            left,
            right
        }

        public RotateDirection Direction { get; }

        public int Count { get; }

        public override string ToString() => $"rotate {Direction} {Count} steps";
    }
}
