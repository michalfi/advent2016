using System.Collections.Generic;
using System.Linq;

namespace Advent2016.Puzzle
{
    public class Day19StealingAcross : IPuzzle
    {
        public string Solve(string[] input)
        {
            int count = int.Parse(input[0]);
            var firstHalf = new Queue<int>(Enumerable.Range(1, count/2));
            var secondHalf = new Queue<int>(Enumerable.Range(count/2 + 1, count%2 == 0 ? count/2 : count/2 + 1));
            while (firstHalf.Count > 0)
            {
                secondHalf.Dequeue(); // just lost
                secondHalf.Enqueue(firstHalf.Dequeue()); // just stole

                if (secondHalf.Count > firstHalf.Count + 1)
                    firstHalf.Enqueue(secondHalf.Dequeue()); // balance
            }
            return secondHalf.Dequeue().ToString();
        }
    }
}
