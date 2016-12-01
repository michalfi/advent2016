using System;
using Advent2016.Bunny.Grid;
using FsCheck;
using Xunit;
using FsCheck.Xunit;

namespace Bunny.UnitTests.Grid
{
    public class PositionTests
    {
        [Property]
        public Property DistanceToSelfIsZero()
        {
            return Prop.ForAll<Position>(pos => pos.DistanceFrom(pos) == 0);
        }

        [Property]
        public Property DistanceInRowIsXDiff()
        {
            var pairs = PositionsWithSameY();
            return Prop.ForAll(pairs, tup => tup.Item1.DistanceFrom(tup.Item2) == Math.Abs(tup.Item1.X - tup.Item2.X));
        }

        [Property]
        public Property DistanceInColumnIsYDiff()
        {
            var pairs = PositionsWithSameX();
            return Prop.ForAll(pairs, tup => tup.Item1.DistanceFrom(tup.Item2) == Math.Abs(tup.Item1.Y - tup.Item2.Y));
        }

        private static Arbitrary<Tuple<Position, Position>> PositionsWithSameY()
        {
            var shortTriples = Gen.Three(Arb.Generate<short>());
            var pairs =
                shortTriples.Select(
                    tup =>
                        new Tuple<Position, Position>(new Position(tup.Item1, tup.Item3),
                            new Position(tup.Item2, tup.Item3))).ToArbitrary();
            return pairs;
        }

        private static Arbitrary<Tuple<Position, Position>> PositionsWithSameX()
        {
            var shortTriples = Gen.Three(Arb.Generate<short>());
            var pairs =
                shortTriples.Select(
                    tup =>
                        new Tuple<Position, Position>(new Position(tup.Item1, tup.Item2),
                            new Position(tup.Item1, tup.Item3))).ToArbitrary();
            return pairs;
        }

        [Property]
        public Property DistanceIsSymetric()
        {
            return Prop.ForAll<Position, Position>((a, b) => a.DistanceFrom(b) == b.DistanceFrom(a));
        }

        [Fact]
        public void DistanceBetweenArbitraryPointsIsAccurate()
        {
            var a = new Position(2, 1);
            var b = new Position(-3, 5);
            var c = new Position(9,-4);

            Assert.Equal(9, a.DistanceFrom(b));
            Assert.Equal(12, a.DistanceFrom(c));
            Assert.Equal(21, b.DistanceFrom(c));
        }
    }
}
