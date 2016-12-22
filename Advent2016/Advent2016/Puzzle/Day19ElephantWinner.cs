namespace Advent2016.Puzzle
{
    public class Day19ElephantWinner : IPuzzle
    {
        public string Solve(string[] input)
        {
            int i = int.Parse(input[0]);
            int winner = 0;
            int power = 2;
            while (i > 1)
            {
                winner += (i%2)*power;
                i /= 2;
                power *= 2;
            }
            return (winner + 1).ToString();
        }
    }
}
