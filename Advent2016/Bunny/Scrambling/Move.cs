namespace Advent2016.Bunny.Scrambling
{
    public class Move : IOperation
    {
        public Move(int @from, int to)
        {
            From = @from;
            To = to;
        }

        public int From { get; }

        public int To { get; }

        public override string ToString() => $"move position {From} to position {To}";
    }
}
