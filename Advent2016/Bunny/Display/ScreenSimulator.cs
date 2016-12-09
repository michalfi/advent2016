namespace Advent2016.Bunny.Display
{
    public class ScreenSimulator
    {
        public ScreenSimulator(byte screenWidth, byte screenHeight)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public byte ScreenWidth { get; }

        public byte ScreenHeight { get; }

        public Bitmap InitialState() => new Bitmap(new bool[ScreenWidth, ScreenHeight]);

        public Bitmap Simulate(Instruction instruction, Bitmap currentState)
        {
            return currentState;
        }
    }
}
