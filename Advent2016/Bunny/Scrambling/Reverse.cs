namespace Advent2016.Bunny.Scrambling
{
    public class Reverse : IOperation
    {
        public Reverse(int start, int end)
        {
            Start = start;
            End = end;
        }

        public int Start { get; }

        public int End { get; }

        public override string ToString() => $"reverse positions {Start} through {End}";
    }
}
