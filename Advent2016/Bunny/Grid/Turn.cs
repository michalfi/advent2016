namespace Advent2016.Bunny.Grid
{
    public class Turn
    {
        public Turn(Dir turnDir)
        {
            this.TurnDir = turnDir;
        }

        public Direction ResultingDirection(Direction original)
        {
            return (Direction) (((int) original + (int) this.TurnDir + 4) % 4);
        }

        private Dir TurnDir { get; }

        public enum Dir
        {
            L = -1,
            R = 1
        }
    }
}
