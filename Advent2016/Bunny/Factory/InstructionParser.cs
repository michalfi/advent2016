using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Dnum;

namespace Advent2016.Bunny.Factory
{
    public class InstructionParser
    {
        private static Regex InputRegex = new Regex(@"^value (\d+) goes to bot (\d+)$");

        private static Regex BotRegex = new Regex(@"^bot (\d+) gives low to (.*) and high to (.*)$");

        private static Regex DestinationRegex = new Regex(@"(bot|output) (\d+)");

        public IBotInstruction Parse(string instructionLine)
        {
            var inputMatch = InputRegex.Match(instructionLine);
            if (inputMatch.Success)
                return ReadInputInstruction(inputMatch);

            var botMatch = BotRegex.Match(instructionLine);
            if (botMatch.Success)
                return ReadBotInstruction(botMatch);
            
            throw new InvalidDataException();
        }

        private BotInstruction ReadBotInstruction(Match botMatch)
        {
            int bot;
            if (!int.TryParse(botMatch.Groups[1].Value, out bot))
                throw new InvalidDataException();
            var low = ReadDestination(botMatch.Groups[2].Value);
            var high = ReadDestination(botMatch.Groups[3].Value);
            return new BotInstruction(bot, low, high);
        }

        private Destination ReadDestination(string instructionPart)
        {
            var match = DestinationRegex.Match(instructionPart);
            int destId;
            DestinationKind? kind;
            if (!match.Success || !int.TryParse(match.Groups[2].Value, out destId) ||
                !Dnum<DestinationKind>.TryParse(match.Groups[1].Value, out kind))
                throw new InvalidDataException();
            return new Destination(kind.Value, destId);
        }

        private InputInstruction ReadInputInstruction(Match inputMatch)
        {
            int value, bot;
            if (!int.TryParse(inputMatch.Groups[1].Value, out value) ||
                !int.TryParse(inputMatch.Groups[2].Value, out bot))
                throw new InvalidDataException();
            return new InputInstruction(value, bot);
        }
    }
}
