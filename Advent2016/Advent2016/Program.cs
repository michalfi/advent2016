using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2016.Puzzle;

namespace Advent2016
{
    class Program
    {
        private static Dictionary<string, IPuzzle> Puzzles = new Dictionary<string, IPuzzle>
        {
            {"1", new Day1DistanceToHq()},
            {"1.2", new Day1DistanceToActualHq()}
        };

        static void Main(string[] args)
        {
            var puzzleId = GetPuzzleIdFromArgOrLatest(args);
            var input = File.ReadAllLines($"input/{puzzleId}.txt");
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
