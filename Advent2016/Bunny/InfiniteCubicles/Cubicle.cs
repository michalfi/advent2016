using System;

namespace Advent2016.Bunny.InfiniteCubicles
{
    public class Cubicle
    {
        public Cubicle(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }

        public ushort X { get; }

        public ushort Y { get; }

        public ushort ManhattanDistance(Cubicle other)
        {
            return (ushort) (Math.Abs(this.X - other.X) + Math.Abs(this.Y - other.Y));
        }

        public override string ToString()
        {
            return $"[{X},{Y}]";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Cubicle;
            return other != null && this.Equals(other);
        }

        protected bool Equals(Cubicle other)
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
