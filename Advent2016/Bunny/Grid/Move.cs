using System.Collections.Generic;
using System.Linq;

namespace Advent2016.Bunny.Grid
{
    public class Move
    {
        public Move(Direction direction, ushort distance)
        {
            Direction = direction;
            Distance = distance;
        }

        public Direction Direction { get; }

        public ushort Distance { get; }

        private static Dictionary<Direction, Position> UnitMovement = new Dictionary<Direction, Position>
        {
            {Direction.North, new Position(0, 1)},
            {Direction.East, new Position(1, 0)},
            {Direction.South, new Position(0, -1)},
            {Direction.West, new Position(-1, 0)}
        };

        public Position ResultingPosition(Position original)
        {
            var unit = UnitMovement[this.Direction];
            return new Position((short) (original.X + this.Distance*unit.X), (short) (original.Y + this.Distance*unit.Y));
        }

        public IEnumerable<Position> StepsToResulting(Position original)
        {
            var unit = UnitMovement[this.Direction];
            return
                Enumerable.Range(1, this.Distance)
                    .Select(i => new Position((short) (original.X + i*unit.X), (short) (original.Y + i*unit.Y)));
        }

        public override string ToString()
        {
            return $"<{this.Direction},{this.Distance}>";
        }
    }
}
