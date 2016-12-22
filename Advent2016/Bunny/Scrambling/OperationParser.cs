using Dnum;
using System.Text.RegularExpressions;

namespace Advent2016.Bunny.Scrambling
{
    public class OperationParser
    {
        private static Regex SwapPositionRe = new Regex(@"swap position (\d+) with position (\d+)");
        private static Regex SwapLetterRe = new Regex(@"swap letter (\w) with letter (\w)");
        private static Regex RotateRe = new Regex(@"rotate (left|right) (\d+) steps?");
        private static Regex RotateByRe = new Regex(@"rotate based on position of letter (\w)");
        private static Regex ReverseRe = new Regex(@"reverse positions (\d+) through (\d+)");
        private static Regex MoveRe = new Regex(@"move position (\d+) to position (\d+)");

        public IOperation Parse(string description)
        {
            var match = SwapPositionRe.Match(description);
            if (match.Success)
                return new SwapPosition(Int(match, 1), Int(match, 2));

            match = SwapLetterRe.Match(description);
            if (match.Success)
                return new SwapLetter(Char(match, 1), Char(match, 2));

            match = RotateRe.Match(description);
            if (match.Success)
                return new Rotate(Direction(match, 1), Int(match, 2));

            match = RotateByRe.Match(description);
            if (match.Success)
                return new RotateByLetter(Char(match, 1));

            match = ReverseRe.Match(description);
            if (match.Success)
                return new Reverse(Int(match, 1), Int(match, 2));

            match = MoveRe.Match(description);
            if (match.Success)
                return new Move(Int(match, 1), Int(match, 2));

            return null;
        }

        private int Int(Match match, int group) => int.Parse(match.Groups[group].Value);

        private char Char(Match match, int group) => match.Groups[group].Value[0];

        private Rotate.RotateDirection Direction(Match match, int group)
            => Dnum<Rotate.RotateDirection>.Parse(match.Groups[group].Value);
    }
}
