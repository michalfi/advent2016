using System.Diagnostics;
using System.Linq;
using Advent2016.Bunny.Grid;

namespace Advent2016.Bunny.Cluster
{
    public class GridState
    {
        public GridState(Node[,] nodes, Position dataLocation, ClusterSetup cluster)
        {
            Debug.Assert(nodes.GetLength(0) == cluster.XDim && nodes.GetLength(1) == cluster.YDim);
            Nodes = nodes;
            DataLocation = dataLocation;
            Cluster = cluster;
        }

        public Node[,] Nodes { get; }

        public Position DataLocation { get; }

        public ClusterSetup Cluster { get; }

        public GridState MoveData(Position source, Position destination)
        {
            var sourceNode = Nodes[source.X, source.Y];
            var destNode = Nodes[destination.X, destination.Y];
            Debug.Assert(sourceNode.UsedSpace <= destNode.AvailableSpace);
            var nodesAfter = (Node[,]) Nodes.Clone();
            nodesAfter[source.X, source.Y] = new Node(source, 0, sourceNode.UsedSpace + sourceNode.AvailableSpace);
            nodesAfter[destination.X, destination.Y] = new Node(destination, destNode.UsedSpace + sourceNode.UsedSpace,
                destNode.AvailableSpace - sourceNode.UsedSpace);
            var dataLocAfter = source.Equals(DataLocation) ? destination : DataLocation;
            return new GridState(nodesAfter, dataLocAfter, Cluster);
        }

        public override string ToString()
        {
            return $"GridState: data@{DataLocation}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as GridState;
            return other != null && this.Equals(other);
        }

        protected bool Equals(GridState other)
        {
            return Equals(Cluster, other.Cluster) && Equals(DataLocation, other.DataLocation) &&
                   Nodes.Cast<Node>().SequenceEqual(other.Nodes.Cast<Node>());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (DataLocation != null ? DataLocation.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Cluster != null ? Cluster.GetHashCode() : 0);
                for (int i = 0; i < Nodes.GetLength(0); i++)
                    hashCode = (hashCode*397) ^ Nodes[i, 0].GetHashCode();
                return hashCode;
            }
        }
    }
}
