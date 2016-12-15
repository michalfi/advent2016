using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2016.Bunny.ChipAssembly
{
    public class InitialStateParser
    {
        public const int FloorCount = 4;

        private static Regex ChipRe = new Regex(@"(\w+)-compatible microchip");

        private static Regex GeneratorRe = new Regex(@"(\w+) generator");

        public State Parse(string[] description)
        {
            Debug.Assert(description.Length == FloorCount);

            var ctx = new Context();
            for (int floor = 0; floor < FloorCount; floor++)
                this.ParseFloor(floor, description[floor], ctx);

            var s = new State(0, this.MakeArrays(ctx.ChipFloors, ctx.Materials),
                this.MakeArrays(ctx.GeneratorFloors, ctx.Materials));
            Debug.Assert(s.IsSafe());
            return s;
        }

        private BitArray[] MakeArrays(List<int>[] floors, Dictionary<string, int> materials)
        {
            return Enumerable.Range(0, FloorCount).Select(floor => MakeArray(floors[floor], materials)).ToArray();
        }

        private BitArray MakeArray(List<int> floor, Dictionary<string, int> materials)
        {
            var array = new BitArray(materials.Count);
            foreach (var present in floor)
                array.Set(present, true);
            return array;
        }

        private void ParseFloor(int floor, string description, Context ctx)
        {
            foreach (var chipMatch in ChipRe.Matches(description).Cast<Match>())
                ctx.ChipFloors[floor].Add(ctx.MaterialIndex(chipMatch.Groups[1].Value));
            foreach (var genMatch in GeneratorRe.Matches(description).Cast<Match>())
                ctx.GeneratorFloors[floor].Add(ctx.MaterialIndex(genMatch.Groups[1].Value));
        }

        private class Context
        {
            public Dictionary<string, int> Materials { get; } = new Dictionary<string, int>();

            public List<int>[] ChipFloors { get; } = Enumerable.Range(0, FloorCount).Select(_ => new List<int>()).ToArray();

            public List<int>[] GeneratorFloors { get; } = Enumerable.Range(0, FloorCount).Select(_ => new List<int>()).ToArray();

            public int MaterialIndex(string material)
            {
                int index;
                if (this.Materials.TryGetValue(material, out index))
                    return index;

                index = this.Materials.Count;
                this.Materials.Add(material, index);
                return index;
            }
        }
    }
}
