using System.Text.RegularExpressions;
using Advent2016.Bunny.Grid;
using Advent2016.Bunny.Utils;

namespace Advent2016.Bunny.Cluster
{
    public class Node
    {
        public Node(Position position, int usedSpace, int availableSpace)
        {
            Position = position;
            UsedSpace = usedSpace;
            AvailableSpace = availableSpace;
        }

        public Position Position { get; }

        public int UsedSpace { get; }

        public int AvailableSpace { get; }

        public override string ToString()
        {
            return Position.ToString();
        }

        public static Regex NodeRe = new Regex(@"/dev/grid/node-x(\d+)-y(\d+)\s+\d+T\s+(\d+)T\s+(\d+)T.*");

        public static Node FromString(string description)
        {
            var match = NodeRe.Match(description);
            return new Node(new Position(match.Short(1), match.Short(2)), match.Int(3), match.Int(4));
        }

        public override bool Equals(object obj)
        {
            var other = obj as Node;
            return other != null && this.Equals(other);
        }

        protected bool Equals(Node other)
        {
            return Equals(Position, other.Position) && UsedSpace == other.UsedSpace && AvailableSpace == other.AvailableSpace;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Position != null ? Position.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ UsedSpace;
                hashCode = (hashCode*397) ^ AvailableSpace;
                return hashCode;
            }
        }
    }
}
