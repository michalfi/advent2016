using System;

namespace Advent2016.Bunny.Display
{
    public class Bitmap
    {
        public Bitmap(bool[,] pixelState)
        {
            this.PixelState = pixelState;
            this.Width = (byte) pixelState.GetLength(0);
            this.Height = (byte) pixelState.GetLength(1);
        }

        public bool[,] PixelState { get; }

        public byte Width { get; }

        public byte Height { get; }
    }
}
