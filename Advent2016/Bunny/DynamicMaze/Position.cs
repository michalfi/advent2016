using System;

namespace Advent2016.Bunny.DynamicMaze
{
    public class Position
    {
        public Position(int x, int y, string path)
        {
            X = x;
            Y = y;
            Path = path;
        }

        public int X { get; }

        public int Y { get; }

        public string Path { get; }

        public override bool Equals(object obj)
        {
            var other = obj as Position;
            return other != null && this.Equals(other);
        }

        protected bool Equals(Position other)
        {
            return X == other.X && Y == other.Y && string.Equals(Path, other.Path);
        }

        public override int GetHashCode() => Path.GetHashCode();

        public override string ToString()
        {
            return $"[{X},{Y}:{Path}]";
        }
    }
}
