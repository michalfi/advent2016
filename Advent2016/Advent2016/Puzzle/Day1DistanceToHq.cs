using System;
using System.Linq;
using Advent2016.Bunny.Grid;
using Dnum;

namespace Advent2016.Puzzle
{
    public class Day1DistanceToHq : IPuzzle
    {
        public string Solve(string[] input)
        {
            var initialPosition = new Position(0, 0);
            var position = initialPosition;
            var direction = Direction.North;
            var moves =
                input[0].Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(move => new { turn = Dnum<Turn.Dir>.Parse(move.Substring(0, 1)), distance = ushort.Parse(move.Substring(1))});
            foreach (var instruction in moves)
            {
                direction = new Turn(instruction.turn).ResultingDirection(direction);
                position = new Move(direction, instruction.distance).ResultingPosition(position);
            }

            var distanceFromInitial = position.DistanceFrom(initialPosition);

            return distanceFromInitial.ToString();
        }
    }
}
