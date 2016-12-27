using Advent2016.Bunny.Grid;

namespace Advent2016.Bunny.Cluster
{
    public class ClusterSetup
    {
        public ClusterSetup(short xDim, short yDim, Position accessNode)
        {
            XDim = xDim;
            YDim = yDim;
            AccessNode = accessNode;
        }

        public short XDim { get; }

        public short YDim { get; }

        public Position AccessNode { get; }

        public override string ToString()
        {
            return $"Cluster {XDim}*{YDim} accessed @{AccessNode}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as ClusterSetup;
            return other != null && this.Equals(other);
        }

        protected bool Equals(ClusterSetup other)
        {
            return XDim == other.XDim && YDim == other.YDim && Equals(AccessNode, other.AccessNode);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = XDim.GetHashCode();
                hashCode = (hashCode*397) ^ YDim.GetHashCode();
                hashCode = (hashCode*397) ^ (AccessNode != null ? AccessNode.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}