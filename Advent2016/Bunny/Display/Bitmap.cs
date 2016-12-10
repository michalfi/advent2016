using System;

namespace Advent2016.Bunny.Display
{
    using System.Linq;

    public class Bitmap
    {
        public Bitmap(bool[,] pixels)
        {
            this.Pixels = pixels;
            this.Width = (byte) pixels.GetLength(0);
            this.Height = (byte) pixels.GetLength(1);
        }

        public bool[,] Pixels { get; }

        public byte Width { get; }

        public byte Height { get; }

        public override string ToString()
        {
            return string.Join("\n",
                Enumerable.Range(0, this.Height)
                    .Select(
                        row =>
                            string.Join("",
                                Enumerable.Range(0, this.Width).Select(col => this.Pixels[col, row] ? '#' : ' '))));
        }
    }
}
