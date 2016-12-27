using Dnum;
using System.Text.RegularExpressions;
using Advent2016.Bunny.Utils;

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
                return new SwapPosition(match.Int(1), match.Int(2));

            match = SwapLetterRe.Match(description);
            if (match.Success)
                return new SwapLetter(match.Char(1), match.Char(2));

            match = RotateRe.Match(description);
            if (match.Success)
                return new Rotate(Direction(match, 1), match.Int(2));

            match = RotateByRe.Match(description);
            if (match.Success)
                return new RotateByLetter(match.Char(1));

            match = ReverseRe.Match(description);
            if (match.Success)
                return new Reverse(match.Int(1), match.Int(2));

            match = MoveRe.Match(description);
            if (match.Success)
                return new Move(match.Int(1), match.Int(2));

            return null;
        }

        private Rotate.RotateDirection Direction(Match match, int group)
            => Dnum<Rotate.RotateDirection>.Parse(match.Groups[group].Value);
    }
}
