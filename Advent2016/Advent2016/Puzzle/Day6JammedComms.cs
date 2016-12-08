using Advent2016.Bunny.Comms;

namespace Advent2016.Puzzle
{
    public class Day6JammedComms : IPuzzle
    {
        public Day6JammedComms(RepeatedMessageCleaner.Protocol protocol)
        {
            Protocol = protocol;
        }

        private RepeatedMessageCleaner.Protocol Protocol { get; }

        public string Solve(string[] input)
        {
            var cleaner = new RepeatedMessageCleaner(this.Protocol);
            return cleaner.CleanMessage(input);
        }
    }
}
