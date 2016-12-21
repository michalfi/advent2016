using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MoreLinq;

namespace Advent2016.Bunny.Comms
{
    public class KeyFinder
    {
        private const int FutureLength = 1000;

        public KeyFinder(DataGenerator source)
        {
            Source = source;
        }

        private DataGenerator Source { get; }

        private NletFinder TripletFinder { get; } = new NletFinder(3);

        public IEnumerable<int> KeyIndices()
        {
            var future = Enumerable.Range(0, FutureLength).Select(i => new FutureEntry(i, Source.Get(i))).ToArray();
            for (int index = 0;; index++)
            {
                int futureIndex = index % FutureLength;
                Debug.Assert(future[futureIndex].Index == index);
                var current = future[futureIndex].Data;
                future[futureIndex] = new FutureEntry(index + FutureLength, Source.Get(index + FutureLength));

                var c = TripletFinder.Find(current);
                if (!c.HasValue)
                    continue;
                string confirm = string.Join("", Enumerable.Repeat(c.Value, 5));
                if (future.Any(fe => fe.Data.Contains(confirm)))
                    yield return index;
            }
        }

        private class FutureEntry
        {
            public FutureEntry(int index, string data)
            {
                Index = index;
                Data = data;
            }

            public int Index { get; }
            public string Data { get; }
        }
    }
}
