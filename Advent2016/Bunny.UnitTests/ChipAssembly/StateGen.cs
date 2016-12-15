using System;
using System.Collections;
using System.Linq;
using Advent2016.Bunny.ChipAssembly;
using FsCheck;

namespace Bunny.UnitTests.ChipAssembly
{
    public static class StateGen
    {
        public static Gen<State> ValidStates()
        {
            return from len in Gen.Elements(Enumerable.Range(1, 30))
                from elevator in Gen.Elements(Enumerable.Range(0, InitialStateParser.FloorCount))
                from chips in DistributionGen(len, InitialStateParser.FloorCount)
                from generators in DistributionGen(len, InitialStateParser.FloorCount)
                select new State(elevator, chips, generators);
        }

        public static Gen<BitArray[]> DistributionGen(int len, int buckets)
        {
            return Gen.Elements(Enumerable.Range(0, buckets)).ArrayOf(len).Select(choices => Buckets(buckets, choices));
        }

        private static BitArray[] Buckets(int bucketCount, int[] bucketChoices)
        {
            var buckets = Enumerable.Range(0, bucketCount).Select(_ => new BitArray(bucketChoices.Length)).ToArray();
            for (int i = 0; i < bucketChoices.Length; i++)
                buckets[bucketChoices[i]].Set(i, true);
            return buckets;
        }

        /* Way too slow
         * public static Gen<State> ValidStates()
        {
            var states =
                AllStates()
                    .Where(
                        s =>
                            Enumerable.Range(0, s.ChipFloors[0].Length)
                                .All(
                                    i =>
                                        s.ChipFloors.Sum(f => f[i] ? 1 : 0) == 1 &&
                                        s.GeneratorFloors.Sum(f => f[i] ? 1 : 0) == 1));
            return states;
        }*/
    }
}
