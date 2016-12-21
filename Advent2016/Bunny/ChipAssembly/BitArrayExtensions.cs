using System.Collections;
using System.Linq;

namespace Advent2016.Bunny.ChipAssembly
{
    public static class BitArrayExtensions
    {
        public static BitArray MakeNot(this BitArray array)
        {
            return new BitArray(array).Not();
        }

        public static BitArray MakeAnd(this BitArray array, BitArray other)
        {
            return new BitArray(array).And(other);
        }

        public static BitArray MakeOr(this BitArray array, BitArray other)
        {
            return new BitArray(array).Or(other);
        }

        public static BitArray MakeXor(this BitArray array, BitArray other)
        {
            return new BitArray(array).Xor(other);
        }

        public static int Cardinality(this BitArray array)
        {
            int[] ints = new int[(array.Count >> 5) + 1];
            array.CopyTo(ints, 0);
            int count = 0;

            // fix for not truncated bits in last integer that may have been set to true with SetAll()
            ints[ints.Length - 1] &= ~(-1 << (array.Count % 32));

            for (int i = 0; i < ints.Length; i++)
            {
                int c = ints[i];

                // magic (http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetParallel)
                unchecked
                {
                    c = c - ((c >> 1) & 0x55555555);
                    c = (c & 0x33333333) + ((c >> 2) & 0x33333333);
                    c = ((c + (c >> 4) & 0xF0F0F0F) * 0x1010101) >> 24;
                }

                count += c;
            }

            return count;
        }

        public static string ToDigitString(this BitArray array)
        {
            var bitChars = array.Cast<bool>().Select(b => b ? '1' : '0').ToArray();
            return new string(bitChars);
        }

        public static BitArray FromDigitString(string digits)
        {
            return new BitArray(digits.Select(d => d == '1').ToArray());
        }

        public static bool ValueEquals(this BitArray array, BitArray other)
        {
            return new BitArray(array).Xor(other).Cardinality() == 0;
        }
    }
}
