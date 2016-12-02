namespace Advent2016.Bunny.Keypad
{
    public class PadKey
    {
        public PadKey(char key, char up, char right, char down, char left)
        {
            this.Key = key;
            this.Neighbors = new[] {up, right, down, left};
        }

        public char Key { get; }

        private char[] Neighbors { get; }

        public char Neighbor(Direction dir)
        {
            return this.Neighbors[(int) dir];
        }
    }
}
