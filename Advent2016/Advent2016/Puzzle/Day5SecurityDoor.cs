using Advent2016.Bunny.SecurityDoor;

namespace Advent2016.Puzzle
{
    public class Day5SecurityDoor : IPuzzle
    {
        public Day5SecurityDoor(PasswordCracker cracker)
        {
            Cracker = cracker;
        }

        private PasswordCracker Cracker { get; }

        public string Solve(string[] input)
        {
            string doorId = input[0];
            var pass = this.Cracker.Crack(doorId);
            return pass;
        }
    }
}
