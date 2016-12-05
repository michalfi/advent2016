using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2016.Bunny.Keypad;
using Advent2016.Bunny.SecurityDoor;
using Advent2016.Puzzle;

namespace Advent2016
{
    class Program
    {
        private static readonly Dictionary<string, IPuzzle> Puzzles = new Dictionary<string, IPuzzle>
        {
            {"1", new Day1DistanceToHq()},
            {"1.2", new Day1DistanceToActualHq()},
            {"2", new Day2BathroomCode(PadCatalogue.Numeric)},
            {"2.2", new Day2BathroomCode(PadCatalogue.Bathroom)},
            {"3", new Day3PossibleTriangles() },
            {"3.2", new Day3VerticalTriangles() },
            {"4", new Day4EncryptedRooms() },
            {"4.2", new Day4FindNorthPole() },
            {"5", new Day5SecurityDoor(new PasswordCracker()) },
            {"5.2", new Day5SecurityDoor(new PositionalCracker()) }
        };

        static void Main(string[] args)
        {
            var puzzleId = GetPuzzleIdFromArgOrLatest(args);
            var inputName = puzzleId;
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
