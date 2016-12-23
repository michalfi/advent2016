using Advent2016.Bunny.Scrambling;

namespace Bunny.UnitTests.Scrambling
{
    public static class SampleOps
    {
        public static IOperation Swap4With0 = new SwapPosition(4, 0);
        public static IOperation SwapDWithB = new SwapLetter('d', 'b');
        public static IOperation Reverse0Through4 = new Reverse(0, 4);
        public static IOperation Rotate1Left = new Rotate(Rotate.RotateDirection.left, 1);
        public static IOperation Rotate8Right = new Rotate(Rotate.RotateDirection.right, 8);
        public static IOperation Move1To4 = new Move(1, 4);
        public static IOperation Move3To0 = new Move(3, 0);
        public static IOperation RotateByB = new RotateByLetter('b');
        public static IOperation RotateByD = new RotateByLetter('d');
        public static IOperation RotateByH = new RotateByLetter('h');

        public static IOperation[] Sequence =
        {
            Swap4With0, SwapDWithB, Reverse0Through4, Rotate1Left, Move1To4, Move3To0,
            RotateByB, RotateByD
        };
    }
}
