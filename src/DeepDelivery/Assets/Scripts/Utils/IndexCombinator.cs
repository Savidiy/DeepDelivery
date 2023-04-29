using System;
using System.Collections.Generic;
using System.Linq;

namespace Savidiy.Utils
{
    public sealed class IndexCombinator
    {
        private readonly Dictionary<int, Dictionary<int, List<IReadOnlyList<int>>>> _precalculatedResults = new();

        public List<IReadOnlyList<int>> GetIndexCombinations(int resultCount, int indexQuantity)
        {
            if (resultCount > indexQuantity)
                throw new Exception(
                    $"Founded index count '{resultCount}' must be least or equal index quantity '{indexQuantity}'");

            if (!_precalculatedResults.ContainsKey(resultCount))
                _precalculatedResults.Add(resultCount, new Dictionary<int, List<IReadOnlyList<int>>>());

            if (!_precalculatedResults[resultCount].ContainsKey(indexQuantity))
            {
                var indexes = new List<int>(indexQuantity);
                for (int i = 0; i < indexQuantity; i++)
                    indexes.Add(i);

                var result = GetIndexCombinations(resultCount, indexes).ToList();
                _precalculatedResults[resultCount].Add(indexQuantity, result);
            }

            return _precalculatedResults[resultCount][indexQuantity];
        }

        private IEnumerable<IReadOnlyList<int>> GetIndexCombinations(int count, List<int> indexes)
        {
            if (count == 0)
            {
                yield return Array.Empty<int>();
            }
            else
            {
                foreach (var index in indexes)
                {
                    var nextIndexes = new List<int>(indexes);
                    nextIndexes.Remove(index);
                    foreach (var nextCombination in GetIndexCombinations(count - 1, nextIndexes))
                    {
                        var ints = new List<int>(count) {index};
                        ints.AddRange(nextCombination);
                        yield return ints;
                    }
                }
            }
        }
    }
}