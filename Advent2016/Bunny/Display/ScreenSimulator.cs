namespace Advent2016.Bunny.Display
{
    using System;

    public class ScreenSimulator
    {
        public ScreenSimulator(byte screenWidth, byte screenHeight)
        {
            this.ScreenWidth = screenWidth;
            this.ScreenHeight = screenHeight;
        }

        public byte ScreenWidth { get; }

        public byte ScreenHeight { get; }

        public Bitmap InitialState() => new Bitmap(new bool[this.ScreenWidth, this.ScreenHeight]);

        public Bitmap Simulate(Instruction instruction, Bitmap currentState)
        {
            switch (instruction.Op)
            {
                case Instruction.Operation.Rectangle:
                    return this.ApplyRectangle(currentState, instruction.ParamA, instruction.ParamB);
                case Instruction.Operation.RowRotation:
                    return this.ApplyRowRotation(currentState, instruction.ParamA, instruction.ParamB);
                case Instruction.Operation.ColRotation:
                    return this.ApplyColRotation(currentState, instruction.ParamA, instruction.ParamB);
                default:
                    return currentState;
            }
        }

        private Bitmap ApplyOperation(Func<int, int, bool> pixelFunc)
        {
            var pixels = new bool[this.ScreenWidth, this.ScreenHeight];
            for (int col = 0; col < this.ScreenWidth; col++)
                for (int row = 0; row < this.ScreenHeight; row++)
                    pixels[col, row] = pixelFunc(col, row);
            return new Bitmap(pixels);
        }

        private Bitmap ApplyColRotation(Bitmap original, byte rotatedCol, byte shift)
            => this.ApplyOperation((col, row) => col == rotatedCol
                ? original.Pixels[col, (row - shift + this.ScreenHeight)%this.ScreenHeight]
                : original.Pixels[col, row]);

        private Bitmap ApplyRowRotation(Bitmap original, byte rotatedRow, byte shift)
            => this.ApplyOperation((col, row) => row == rotatedRow
                ? original.Pixels[(col - shift + this.ScreenWidth)%this.ScreenWidth, row]
                : original.Pixels[col, row]);

        private Bitmap ApplyRectangle(Bitmap original, byte rectWidth, byte rectHeight)
            => this.ApplyOperation((col, row) => col < rectWidth && row < rectHeight || original.Pixels[col, row]);
    }
}