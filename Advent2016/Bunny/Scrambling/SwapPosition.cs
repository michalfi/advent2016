namespace Advent2016.Bunny.Scrambling
{
    public class SwapPosition : IOperation
    {
        public SwapPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public override string ToString() => $"swap position {X} with position {Y}";
    }
}
