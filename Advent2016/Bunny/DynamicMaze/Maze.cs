using System.Collections.Generic;
using System.Linq;

namespace Advent2016.Bunny.DynamicMaze
{
    public class Maze
    {
        private const int XDimension = 4, YDimension = 4;

        private static Move[] Moves =
        {
            new Move('U', 0, -1),
            new Move('D', 0, 1),
            new Move('L', -1, 0),
            new Move('R', 1, 0)
        };

        public Maze(string passcode, Dynamizer dynamizer)
        {
            Passcode = passcode;
            Dynamizer = dynamizer;
        }

        public string Passcode { get; }

        private Dynamizer Dynamizer { get; }

        public Position Vault { get; } = new Position(3, 3, "*");

        public Position Init { get; } = new Position(0, 0, "");

        public IEnumerable<Position> ContinueFrom(Position current)
        {
            var open = Dynamizer.DoorsState(Passcode, current);
            var possible = Moves.Where((move, i) => open[i]).Select(move => ResultingPosition(current, move));
            var valid = possible.Where(pos => pos.X >= 0 && pos.X < XDimension && pos.Y >= 0 && pos.Y < YDimension);
            return valid;
        }

        private Position ResultingPosition(Position original, Move move)
        {
            int x = original.X + move.XChange;
            int y = original.Y + move.YChange;
            return new Position(x, y, original.Path + move.Marker);
        }

        private struct Move
        {
            public char Marker;
            public int XChange;
            public int YChange;

            public Move(char marker, int xChange, int yChange)
            {
                Marker = marker;
                XChange = xChange;
                YChange = yChange;
            }
        }
    }
}
