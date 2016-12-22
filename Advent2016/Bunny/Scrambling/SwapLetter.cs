namespace Advent2016.Bunny.Scrambling
{
    public class SwapLetter : IOperation
    {
        public SwapLetter(char x, char y)
        {
            X = x;
            Y = y;
        }

        public char X { get; }

        public char Y { get; }

        public override string ToString() => $"swap letter {X} with letter {Y}";
    }
}
