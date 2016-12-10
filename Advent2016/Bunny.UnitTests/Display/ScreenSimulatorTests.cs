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
                pixel => pixel.rect.Resulting.Pixels[pixel.col, pixel.row]);
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
                    pixel.rect.Resulting.Pixels[pixel.col, pixel.row] ==
                    pixel.rect.Original.Pixels[pixel.col, pixel.row]);
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
                    pixel.rect.Resulting.Pixels[pixel.col, pixel.row] ==
                    pixel.rect.Original.Pixels[pixel.col, pixel.row]);
        }

        [Property]
        public Property RowRotationRotatesPixelsInRow()
        {
            var pixelsInside = from rot in RowRotationInstructions()
                from col in Gen.Elements(Enumerable.Range(0, rot.Original.Width))
                select new {rot, col, shifted = (col + rot.Instruction.ParamB) % rot.Original.Width };
            return Prop.ForAll(pixelsInside.ToArbitrary(),
                pixel =>
                    pixel.rot.Resulting.Pixels[pixel.shifted, pixel.rot.Instruction.ParamA] ==
                    pixel.rot.Original.Pixels[pixel.col, pixel.rot.Instruction.ParamA]);
        }

        [Property]
        public Property RowRotationKeepsOtherRowsIntact()
        {
            var pixelsOutside = from rot in RowRotationInstructions().Where(r => r.Original.Height > 1)
                from col in Gen.Elements(Enumerable.Range(0, rot.Original.Width))
                from row in
                    Gen.Elements(Enumerable.Range(0, rot.Original.Height)).Where(r => r != rot.Instruction.ParamA)
                select new {rot, col, row};
            return Prop.ForAll(pixelsOutside.ToArbitrary(),
                pixel =>
                    pixel.rot.Resulting.Pixels[pixel.col, pixel.row] == pixel.rot.Original.Pixels[pixel.col, pixel.row]);
        }

        [Property]
        public Property ColRotationRotatesPixelsInColumn()
        {
            var pixelsInside = from rot in ColRotationInstructions()
                               from row in Gen.Elements(Enumerable.Range(0, rot.Original.Height))
                               select new { rot, row, shifted = (row + rot.Instruction.ParamB) % rot.Original.Height };
            return Prop.ForAll(pixelsInside.ToArbitrary(),
                pixel =>
                    pixel.rot.Resulting.Pixels[pixel.rot.Instruction.ParamA, pixel.shifted] ==
                    pixel.rot.Original.Pixels[pixel.rot.Instruction.ParamA, pixel.row]);
        }

        [Property]
        public Property ColRotationKeepsOtherColumnsIntact()
        {
            var pixelsOutside = from rot in ColRotationInstructions().Where(r => r.Original.Width > 1)
                                from row in Gen.Elements(Enumerable.Range(0, rot.Original.Height))
                                from col in
                                    Gen.Elements(Enumerable.Range(0, rot.Original.Width)).Where(c => c != rot.Instruction.ParamA)
                                select new { rot, col, row };
            return Prop.ForAll(pixelsOutside.ToArbitrary(),
                pixel =>
                    pixel.rot.Resulting.Pixels[pixel.col, pixel.row] == pixel.rot.Original.Pixels[pixel.col, pixel.row]);
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

        private static Gen<BitmapModification> RowRotationInstructions()
        {
            return from bitmap in Bitmaps().Where(b => b.Width > 1)
                from row in Gen.Elements(Enumerable.Range(0, bitmap.Height))
                from shift in Gen.Elements(Enumerable.Range(1, bitmap.Width - 1))
                select
                    new BitmapModification(bitmap,
                        new Instruction(Instruction.Operation.RowRotation, (byte)row, (byte)shift));
        }

        private static Gen<BitmapModification> ColRotationInstructions()
        {
            return from bitmap in Bitmaps().Where(b => b.Height > 1)
                   from col in Gen.Elements(Enumerable.Range(0, bitmap.Width))
                   from shift in Gen.Elements(Enumerable.Range(1, bitmap.Height - 1))
                   select
                       new BitmapModification(bitmap,
                           new Instruction(Instruction.Operation.ColRotation, (byte)col, (byte)shift));
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
