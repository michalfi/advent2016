namespace Advent2016.Bunny.Scrambling
{
    public class RotateByLetter : IOperation
    {
        public RotateByLetter(char letter)
        {
            Letter = letter;
        }

        public char Letter { get; }

        public override string ToString() => $"rotate based on position of letter {Letter}";
    }
}
