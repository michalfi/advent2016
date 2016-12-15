using System;
using System.Collections;
using System.Linq;
using Advent2016.Bunny.ChipAssembly;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Bunny.UnitTests.ChipAssembly
{
    public class StateSafetyTests
    {
        [Property]
        public Property WithNoGeneratorChipsAreSafe()
        {
            var bitarrayGen = BitarrayGen();
            var stateGen = bitarrayGen.Select(ba => new State(0, new[] {ba}, new[] {new BitArray(ba.Count)}));
            return Prop.ForAll(stateGen.ToArbitrary(), state => state.IsSafe() && state.GeneratorFloors[0].Cardinality() == 0);
        }

        [Property]
        public Property ChipWithMatchingGeneratorIsSafe()
        {
            var bitGen = Arb.Generate<bool>();
            var chipsGensPairGen = from chips in BitarrayGen()
                from generators in bitGen.ArrayOf(chips.Count).Select(bits => new BitArray(bits))
                select new {chips, generators};
            var stateGen =
                from pair in
                chipsGensPairGen.Where(x => x.chips.MakeNot().MakeOr(x.generators).Cardinality() == x.generators.Length)
                select new State(0, new[] {pair.chips}, new[] {pair.generators});
            return Prop.ForAll(stateGen.ToArbitrary(), state => state.IsSafe());
        }

        /* Gets stuck in the first 'chips'
         * [Property]
        public Property ChipWithMatchingGeneratorIsSafe()
        {
            var bitGen = Arb.Generate<bool>();
            var stateGen = from chips in BitarrayGen()
                           from generators in
                           bitGen.ArrayOf(chips.Count)
                               .Select(bits => new BitArray(bits))
                               .Where(gens => chips.Not().Or(gens).Cardinality() == gens.Length)
                    select new State(0, new[] { chips }, new[] { generators });
            return Prop.ForAll(stateGen.ToArbitrary(), state => state.IsSafe());
        }*/

        [Property]
        public Property ChipWithoutGeneratorGetsFriedByAnother()
        {
            var bitGen = Arb.Generate<bool>();
            var chipsGensPairGen = from chips in BitarrayGen()
                from generators in bitGen.ArrayOf(chips.Count).Select(bits => new BitArray(bits))
                select new {chips, generators};
            var stateGen =
                from pair in
                chipsGensPairGen.Where(
                    x => x.chips.MakeAnd(x.generators.MakeNot()).Cardinality() > 0 && x.generators.Cardinality() > 0 && Check(x.chips, x.generators))
                select new State(0, new[] {pair.chips}, new[] {pair.generators});
            return Prop.ForAll(stateGen.ToArbitrary(), state => !state.IsSafe());
        }

        [Property]
        public Property CompleteSafetyMeansEveryFloorSafety()
        {
            return Prop.ForAll(StateGen.ValidStates().ToArbitrary(),
                    state => CompleteSafetyIsEqualToEveryFloorSafety(state))
                .And(Prop.ForAll(StateGen.ValidStates().ToArbitrary(),
                    state => CompleteSafetyIsEqualToEveryFloorSafety(state)));
        }

        private bool CompleteSafetyIsEqualToEveryFloorSafety(State state)
        {
            return state.IsSafe() ==
                   Enumerable.Range(0, state.ChipFloors.Length)
                       .Select(
                           floor => new State(0, new[] {state.ChipFloors[floor]}, new[] {state.GeneratorFloors[floor]}))
                       .All(floorState => floorState.IsSafe());
        }

        private static bool Check(BitArray chips, BitArray gens)
        {
            return true;
        }

        private static Gen<BitArray> BitarrayGen()
        {
            var bitGen = Arb.Generate<bool>();
            var bitarrayGen = bitGen.ArrayOf().Select(bits => new BitArray(bits));
            return bitarrayGen;
        }
    }
}
