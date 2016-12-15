using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2016.Bunny.Comms;
using Advent2016.Bunny.Keypad;
using Advent2016.Bunny.SecurityDoor;
using Advent2016.Puzzle;

namespace Advent2016
{
    class Program
    {
        private static readonly Dictionary<string, IPuzzle> Puzzles = new Dictionary<string, IPuzzle>
        {
            {"01", new Day1DistanceToHq()},
            {"01.2", new Day1DistanceToActualHq()},
            {"02", new Day2BathroomCode(PadCatalogue.Numeric)},
            {"02.2", new Day2BathroomCode(PadCatalogue.Bathroom)},
            {"03", new Day3PossibleTriangles()},
            {"03.2", new Day3VerticalTriangles()},
            {"04", new Day4EncryptedRooms()},
            {"04.2", new Day4FindNorthPole()},
            {"05", new Day5SecurityDoor(new PasswordCracker())},
            {"05.2", new Day5SecurityDoor(new PositionalCracker())},
            {"06", new Day6JammedComms(RepeatedMessageCleaner.Protocol.Simple)},
            {"06.2", new Day6JammedComms(RepeatedMessageCleaner.Protocol.Modified)},
            {"07", new Day7CountTlsIps()},
            {"07.2", new Day7CountSslIps()},
            {"08", new Day8ComputeDisplayVoltage()},
            {"08.2", new Day8DisplayCode()},
            {"09", new Day9DecompressedFileLength()},
            {"09.2", new Day9V2DecompressedLength()},
            {"10", new Day10IdentifyBot()},
            {"10.2", new Day10OutputValues()},
            {"11", new Day11MinimalPlanLength("")},
            {
                "11.2",
                new Day11MinimalPlanLength(
                    "elerium generator, elerium-compatible microchip, dilithium generator, dilithium-compatible microchip")
            },
            {"12", new Day12InterpretCode()},
            {"12.2", new Day12InterpretInitialized()}
        };

        static void Main(string[] args)
        {
            var puzzleId = GetPuzzleIdFromArgOrLatest(args);
            var inputName = puzzleId.TrimStart('0');
            if (!File.Exists($"input/{puzzleId}.txt"))
                inputName = puzzleId.Substring(0, puzzleId.Length - 2);
            var input = File.ReadAllLines($"input/{inputName}.txt");
            var solver = Puzzles[puzzleId];
            var result = solver.Solve(input);
            Console.WriteLine(result);
        }

        private static string GetPuzzleIdFromArgOrLatest(string[] args)
        {
            if (args.Length > 0)
                return args[0];
            return Puzzles.Keys.OrderByDescending(x => x).First();
        }
    }
}
