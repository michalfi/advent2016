using System.Collections.Generic;
using System.Linq;

namespace Advent2016.Bunny.ChipAssembly
{
    public static class ArrayExtensions
    {
        public static IEnumerable<T[]> Subsets<T>(this T[] source, int maxSize)
            => SubsetsImpl(source, 0, maxSize - 1);

        private static IEnumerable<T[]> SubsetsImpl<T>(T[] source, int start, int iteration, List<int> indices = null)
        {
            indices = indices ?? new List<int>();
            for (int i = start; i < source.Length; i++)
            {
                indices.Add(i);
                yield return SubsetArray(source, indices);

                if (iteration > 0)
                {
                    foreach (var array in SubsetsImpl(source, i + 1, iteration - 1, indices))
                    {
                        yield return array;
                    }
                }
                indices.RemoveAt(indices.Count - 1);
            }
        }

        private static T[] SubsetArray<T>(T[] source, List<int> indices)
        {
            var array = new T[indices.Count];
            for (int j = 0; j < indices.Count; j++)
            {
                array[j] = source[indices[j]];
            }

            return array;
        }
    }
}
