using System;
using System.Linq;

namespace Advent2016.Bunny.RoomList
{
    public class Shifter
    {
        public static char[] Alphabet = ChecksumComputer.Alphabet;

        public string Shift(string original, int count)
        {
            return string.Join("", original.Select(c => Shift(c, count)));
        }

        public char Shift(char original, int count)
        {
            var index = Array.IndexOf(Alphabet, original);
            if (index < 0)
                return ' ';
            int shifted = (index + count)%Alphabet.Length;
            return Alphabet[shifted];
        }
    }
}
