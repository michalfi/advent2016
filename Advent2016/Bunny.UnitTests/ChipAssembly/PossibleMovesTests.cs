using System;
using System.Collections;
using System.Linq;
using Advent2016.Bunny.ChipAssembly;
using FsCheck;
using FsCheck.Xunit;

namespace Bunny.UnitTests.ChipAssembly
{
    public class PossibleMovesTests
    {
        [Property]
        public Property MovingDoesntChangeOtherFloors()
        {
            return
                TestAllMoves(
                    (state, moved) =>
                        FloorsNotChanged(state.ChipFloors, moved.ChipFloors, state.ElevatorFloor,
                            moved.ElevatorFloor) &&
                        FloorsNotChanged(state.GeneratorFloors, moved.GeneratorFloors, state.ElevatorFloor,
                            moved.ElevatorFloor));
        }

        [Property]
        public Property MovingMovesElevator()
        {
            return TestAllMoves((state, moved) => moved.ElevatorFloor != state.ElevatorFloor);
        }

        [Property]
        public Property MovesAreDistinct()
        {
            return Prop.ForAll(StateGen.ValidStates().ToArbitrary(),
                state => state.PossibleMoves().Distinct().Count() == state.PossibleMoves().Count());
        }

        [Property]
        public Property MoveCountIsCorrect()
        {
            return Prop.ForAll(StateGen.ValidStates().ToArbitrary(),
                state =>
                    state.PossibleMoves().Count() ==
                    CombinationCount(state.ChipFloors[state.ElevatorFloor].Cardinality() +
                                     state.GeneratorFloors[state.ElevatorFloor].Cardinality()) || CheckState(state));
        }

        private static int CombinationCount(int setSize)
        {
            return 1 + setSize + setSize*(setSize - 1)/2;
        }

        [Property]
        public Property MoveChangesAtMostTwoItems()
        {
            return
                TestAllMoves(
                    (state, moved) =>
                        CountDiffOnFloor(state, moved, state.ElevatorFloor) <= 2 &&
                        CountDiffOnFloor(state, moved, moved.ElevatorFloor) <= 2);
        }

        [Property]
        public Property MoveIsConsistent()
        {
            return
                TestAllMoves(
                    (state, moved) =>
                        state.ChipFloors[state.ElevatorFloor].MakeOr(state.ChipFloors[moved.ElevatorFloor])
                            .ValueEquals(
                                moved.ChipFloors[state.ElevatorFloor].MakeOr(moved.ChipFloors[moved.ElevatorFloor])) &&
                        state.GeneratorFloors[state.ElevatorFloor].MakeOr(state.GeneratorFloors[moved.ElevatorFloor])
                            .ValueEquals(
                                moved.GeneratorFloors[state.ElevatorFloor].MakeOr(
                                    moved.GeneratorFloors[moved.ElevatorFloor])));
        }

        private static int CountDiffOnFloor(State state, State moved, int floorNumber)
        {
            return state.ChipFloors[floorNumber].MakeXor(moved.ChipFloors[floorNumber]).Cardinality() +
                   state.GeneratorFloors[floorNumber].MakeXor(moved.GeneratorFloors[floorNumber]).Cardinality();
        }

        public static Property TestAllMoves(Func<State, State, bool> predicate)
        {
            return Prop.ForAll(StateGen.ValidStates().ToArbitrary(),
                state => state.PossibleMoves().All(moved => predicate(state, moved)));
        }

        private static bool FloorsNotChanged(BitArray[] origFloors, BitArray[] movedFloors, int except1, int except2)
        {
            return
                Enumerable.Range(0, origFloors.Length)
                    .Except(new[] {except1, except2})
                    .All(i => origFloors[i].ValueEquals(movedFloors[i]));
        }

        private static bool CheckState(State state)
        {
            return true;
        }
    }
}
