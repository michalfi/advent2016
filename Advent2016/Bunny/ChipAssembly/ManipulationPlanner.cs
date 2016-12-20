using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kts.AStar;

namespace Advent2016.Bunny.ChipAssembly
{
    public class ManipulationPlanner
    {
        public IEnumerable<State> PlanAssembly(State initial)
        {
            var target = new State(initial.FloorCount - 1, TargetFloors(initial.FloorCount, initial.MaterialCount),
                TargetFloors(initial.FloorCount, initial.MaterialCount));
            Func<State, double> heuristic =
                s =>
                    Enumerable.Range(0, s.FloorCount - 1)
                        .Sum(i => s.ChipFloors[i].Cardinality() + s.GeneratorFloors[i].Cardinality());
            return
                AStarUtilities.FindMinimalPath(initial, target, s => s.PossibleMoves().Where(move => move.IsSafe()),
                    (s1, s2) => 1, heuristic).Result;
        }

        private BitArray[] TargetFloors(int floorCount, int materialCount)
        {
            var floors = Enumerable.Range(0, floorCount).Select(_ => new BitArray(materialCount)).ToArray();
            floors[floorCount - 1].SetAll(true);
            return floors;
        }
    }
}
