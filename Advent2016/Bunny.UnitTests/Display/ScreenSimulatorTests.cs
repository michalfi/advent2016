using System.Linq;
using System.Net.Sockets;
using Advent2016.Bunny.Display;
using FsCheck;
using FsCheck.Xunit;

namespace Bunny.UnitTests.Display
{
    public class ScreenSimulatorTests
    {
        [Property]
        public Property RectangleLightsPixelsInsideIt()
        {
            var pixelsInRectangle = from rect in RectangleInstructions()
                from col in Gen.Elements(Enumerable.Range(0, rect.Instruction.ParamA))
                from row in Gen.Elements(Enumerable.Range(0, rect.Instruction.ParamB))
                select new {rect, col, row};
            return Prop.ForAll(pixelsInRectangle.ToArbitrary(),
                pixel => pixel.rect.Resulting.PixelState[pixel.col, pixel.row]);
        }

        [Property]
        public Property RectangleKeepsPixelsRightOfItIntact()
        {
            var pixelsOutside = from rect in RectangleInstructions().Where(r => r.Original.Width > r.Instruction.ParamA)
                from col in Gen.Elements(Enumerable.Range(rect.Instruction.ParamA, rect.Original.Width - rect.Instruction.ParamA))
                from row in Gen.Elements(Enumerable.Range(0, rect.Original.Height))
                select new {rect, col, row};
            return Prop.ForAll(pixelsOutside.ToArbitrary(),
                pixel =>
                    pixel.rect.Resulting.PixelState[pixel.col, pixel.row] ==
                    pixel.rect.Original.PixelState[pixel.col, pixel.row]);
        }

        [Property]
        public Property RectangleKeepsPixelsUnderItIntact()
        {
            var pixelsOutside = from rect in RectangleInstructions().Where(r => r.Original.Height > r.Instruction.ParamB)
                from col in Gen.Elements(Enumerable.Range(0, rect.Original.Width))
                from row in Gen.Elements(Enumerable.Range(rect.Instruction.ParamB, rect.Original.Height - rect.Instruction.ParamB))
                select new {rect, col, row};
            return Prop.ForAll(pixelsOutside.ToArbitrary(),
                pixel =>
                    pixel.rect.Resulting.PixelState[pixel.col, pixel.row] ==
                    pixel.rect.Original.PixelState[pixel.col, pixel.row]);
        }

        private static Gen<Bitmap> Bitmaps()
        {
            return from width in Arb.Generate<byte>().Where(w => w > 0)
                from height in Arb.Generate<byte>().Where(h => h > 0)
                from pixels in Gen.Array2DOf(height, width, Arb.Generate<bool>())
                select new Bitmap(pixels);
        }

        private static Gen<BitmapModification> RectangleInstructions()
        {
            return from bitmap in Bitmaps()
                from width in Gen.Elements(Enumerable.Range(1, bitmap.Width))
                from height in Gen.Elements(Enumerable.Range(1, bitmap.Height))
                select
                new BitmapModification(bitmap,
                    new Instruction(Instruction.Operation.Rectangle, (byte) width, (byte) height));
        }

        private class BitmapModification
        {
            public BitmapModification(Bitmap original, Instruction instruction)
            {
                this.Original = original;
                this.Instruction = instruction;
                var simulator = new ScreenSimulator(original.Width, original.Height);
                this.Resulting = simulator.Simulate(instruction, original);
            }

            public Bitmap Original { get; }

            public Bitmap Resulting { get; }

            public Instruction Instruction { get; }
        }
    }
}
