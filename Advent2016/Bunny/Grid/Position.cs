using System;

namespace Advent2016.Bunny.Grid
{
    public class Position
    {
        public Position(short x, short y)
        {
            X = x;
            Y = y;
        }

        public short X { get; }

        public short Y { get; }

        public int DistanceFrom(Position other)
        {
            return Math.Abs(this.X - other.X) + Math.Abs(this.Y - other.Y);
        }

        public override string ToString()
        {
            return $"[{this.X},{this.Y}]";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Position;
            return other != null && this.Equals(other);
        }

        protected bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode()*397) ^ Y.GetHashCode();
            }
        }
    }
}
