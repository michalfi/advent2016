using System;
using System.Collections.Generic;
using System.Linq;
using Advent2016.Bunny.Grid;
using Dnum;

namespace Advent2016.Puzzle
{
    public class Day1DistanceToActualHq : IPuzzle
    {
        public string Solve(string[] input)
        {
            var initialPosition = new Position(0, 0);
            var position = initialPosition;
            var direction = Direction.North;
            var moves =
                input[0].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(move => new { turn = Dnum<Turn.Dir>.Parse(move.Substring(0, 1)), distance = ushort.Parse(move.Substring(1)) });

            ISet<Position> visited = new HashSet<Position> {initialPosition};
            foreach (var instruction in moves)
            {
                direction = new Turn(instruction.turn).ResultingDirection(direction);
                foreach (var step in new Move(direction, instruction.distance).StepsToResulting(position))
                {
                    position = step;
                    if (visited.Contains(position))
                        return position.DistanceFrom(initialPosition).ToString();
                    visited.Add(position);
                }
            }

            return "No luck";
        }
    }
}
