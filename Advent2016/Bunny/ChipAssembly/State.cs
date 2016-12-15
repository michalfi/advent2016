using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent2016.Bunny.ChipAssembly
{
    public class State
    {
        public State(int elevatorFloor, BitArray[] chipFloors, BitArray[] generatorFloors)
        {
            Debug.Assert(chipFloors.Length == generatorFloors.Length);
            Debug.Assert(chipFloors.Select(f => f.Length).Distinct().Single() == generatorFloors.Select(f => f.Length).Distinct().Single());
            Debug.Assert(Enumerable.Range(0, chipFloors[0].Length).All(i => chipFloors.Sum(f => f[i] ? 1 : 0) <= 1));
            Debug.Assert(elevatorFloor >= 0 && elevatorFloor < chipFloors.Length);

            ElevatorFloor = elevatorFloor;
            ChipFloors = chipFloors;
            GeneratorFloors = generatorFloors;
        }

        public int ElevatorFloor { get; }

        public BitArray[] ChipFloors { get; }

        public BitArray[] GeneratorFloors { get; }

        public int MaterialCount => this.ChipFloors[0].Length;

        public int FloorCount => this.ChipFloors.Length;

        public bool IsSafe()
        {
            for (int i = 0; i < FloorCount; i++)
            {
                if (!FloorIsSafe(ChipFloors[i], GeneratorFloors[i]))
                    return false;
            }
            return true;
        }

        public IEnumerable<State> PossibleMoves()
        {
            var indices = Enumerable.Range(0, this.MaterialCount);
            var possibleCargo =
                indices.Where(i => this.ChipFloors[this.ElevatorFloor][i])
                    .Concat(
                        indices.Where(i => this.GeneratorFloors[this.ElevatorFloor][i])
                            .Select(i => i + this.MaterialCount)).ToArray();
            foreach (var load in possibleCargo.Subsets(2))
            {
                if (this.ElevatorFloor < this.FloorCount - 1)
                    yield return this.AfterMove(load, 1);
                if (this.ElevatorFloor > 0)
                    yield return this.AfterMove(load, -1);
            }
        }

        private State AfterMove(int[] loadIndices, int elevatorDirection)
        {
            var nextFloor = this.ElevatorFloor + elevatorDirection;
            var chipFloors = this.ChipFloors.Select(f => new BitArray(f)).ToArray();
            foreach (int movedChip in loadIndices.Where(i => i < this.MaterialCount))
            {
                chipFloors[this.ElevatorFloor].Set(movedChip, false);
                chipFloors[nextFloor].Set(movedChip, true);
            }

            var genFloors = this.GeneratorFloors.Select(f => new BitArray(f)).ToArray();
            foreach (int movedGen in loadIndices.Where(i => i >= this.MaterialCount).Select(i => i - this.MaterialCount))
            {
                genFloors[this.ElevatorFloor].Set(movedGen, false);
                genFloors[nextFloor].Set(movedGen, true);
            }

            return new State(nextFloor, chipFloors, genFloors);
        }

        private static bool FloorIsSafe(BitArray chips, BitArray generators)
        {
            return generators.Cardinality() <= 0 || chips.MakeNot().Or(generators).Cardinality() == chips.Length;
        }

        public override string ToString()
        {
            var chips = string.Join("><", this.ChipFloors.AsEnumerable().Select(ba => ba.ToDigitString()));
            var gens = string.Join("><", this.GeneratorFloors.AsEnumerable().Select(ba => ba.ToDigitString()));
            return $"State[{this.ElevatorFloor}[<{chips}>][<{gens}>]]";
        }

        public override bool Equals(object obj)
        {
            var other = obj as State;
            return other != null && this.Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ElevatorFloor;
                for (int i = 0; i < this.FloorCount; i++)
                {
                    hashCode = (hashCode*397) ^ ChipFloors[i].ToDigitString().GetHashCode();
                    hashCode = (hashCode*397) ^ GeneratorFloors[i].ToDigitString().GetHashCode();
                }
                return hashCode;
            }
        }

        protected bool Equals(State other)
        {
            return ElevatorFloor == other.ElevatorFloor &&
                   Enumerable.Range(0, FloorCount)
                       .All(
                           i =>
                               ChipFloors[i].ValueEquals(other.ChipFloors[i]) &&
                               GeneratorFloors[i].ValueEquals(other.GeneratorFloors[i]));
        }
    }
}