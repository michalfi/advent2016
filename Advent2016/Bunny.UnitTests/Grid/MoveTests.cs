using System.Linq;
using Advent2016.Bunny.Grid;
using FsCheck;
using FsCheck.Xunit;

namespace Bunny.UnitTests.Grid
{
    public class MoveTests
    {
        [Property]
        public Property VerticalMoveKeepsX()
        {
            return
                Prop.ForAll<Position, Move>(
                    (position, move) =>
                        (move.ResultingPosition(position).X == position.X)
                        .When(move.Direction == Direction.North || move.Direction == Direction.South));
        }

        [Property]
        public Property HorizontalMoveKeepsY()
        {
            return
                Prop.ForAll<Position, Move>(
                    (position, move) =>
                        (move.ResultingPosition(position).Y == position.Y)
                        .When(move.Direction == Direction.East || move.Direction == Direction.West));
        }

        [Property]
        public Property MoveNorthIncreasesY()
        {
            return
                Prop.ForAll<Position, Move>(
                    (position, move) =>
                        (move.ResultingPosition(position).Y > position.Y)
                        .When(move.Direction == Direction.North && move.Distance > 0));
        }

        [Property]
        public Property MoveSouthDecreasesY()
        {
            return
                Prop.ForAll<Position, Move>(
                    (position, move) =>
                        (move.ResultingPosition(position).Y < position.Y)
                        .When(move.Direction == Direction.South && move.Distance > 0));
        }

        [Property]
        public Property MoveEastIncreasesX()
        {
            return
                Prop.ForAll<Position, Move>(
                    (position, move) =>
                        (move.ResultingPosition(position).X > position.X)
                        .When(move.Direction == Direction.East && move.Distance > 0));
        }

        [Property]
        public Property MoveWestDecreasesX()
        {
            return
                Prop.ForAll<Position, Move>(
                    (position, move) =>
                        (move.ResultingPosition(position).X < position.X)
                        .When(move.Direction == Direction.West && move.Distance > 0));
        }

        [Property]
        public Property CountOfStepsIsEqualToDistance()
        {
            return
                Prop.ForAll<Position, Move>(
                    (position, move) => move.StepsToResulting(position).Count() == move.Distance);
        }

        [Property]
        public Property LastStepIsTheResult()
        {
            return
                Prop.ForAll<Position, Move>(
                    (position, move) =>
                        (move.StepsToResulting(position).LastOrDefault() ?? position).Equals(move.ResultingPosition(position)));
        }

        public Property StepsHaveUnitDistance()
        {
            return
                Prop.ForAll<Position, Move>(
                    (position, move) =>
                        new[] {position}.Concat(move.StepsToResulting(position))
                            .Zip(move.StepsToResulting(position), (a, b) => a.DistanceFrom(b))
                            .All(x => x == 1));
        }
    }
}
