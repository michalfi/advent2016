using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent2016.Bunny.Keypad
{
    public static class PadCatalogue
    {
        static PadCatalogue()
        {
            Numeric = LoadModel(new[] {"123", "456", "789"});
            Bathroom = LoadModel(new[] {"  1  ", " 234 ", "56789", " ABC ", "  D  "});
            Models = new List<Pad>
            {
                Numeric,
                Bathroom
            };
        }

        public static Pad Numeric { get; }

        public static Pad Bathroom { get; }

        public static IEnumerable<Pad> Models { get; }

        private const char NonKey = ' ';

        private static Pad LoadModel(string[] layout)
        {
            Debug.Assert(layout.Select(row => row.Length).Distinct().Count() == 1);
            Debug.Assert(layout.SelectMany(s => s).GroupBy(c => c).All(g => g.Key == NonKey || g.Count() == 1));
            return new Pad(ExtractKeys(layout));
        }

        private static IEnumerable<PadKey> ExtractKeys(string[] layout)
        {
            int rows = layout.Length;
            int cols = layout[0].Length;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    char key = layout[row][col];
                    if (key == NonKey)
                        continue;
                    char up = layout[Math.Max(row - 1, 0)][col];
                    char right = layout[row][Math.Min(col + 1, cols - 1)];
                    char down = layout[Math.Min(row + 1, rows - 1)][col];
                    char left = layout[row][Math.Max(col - 1, 0)];
                    yield return new PadKey(layout[row][col], Or(up, key), Or(right, key), Or(down, key), Or(left, key));
                }
            }
        }

        private static char Or(char neighbor, char key)
        {
            return neighbor != NonKey ? neighbor : key;
        }
    }
}
