using System.Collections.Generic;
using System.Text;

namespace Savidiy.Utils
{
    public static class ArrayExtensions
    {
        private static readonly StringBuilder Builder = new StringBuilder();

        public static int[,] CloneArray(this int[,] fromArray)
        {
            int countA = fromArray.GetLength(0);
            int countB = fromArray.GetLength(1);
            var ints = new int [countA, countB];

            for (int i = 0; i < countA; i++)
            for (int j = 0; j < countB; j++)
                ints[i, j] = fromArray[i, j];

            return ints;
        }
        
        public static string ToStringLine<T>(this IEnumerable<T> enumerable, string separator = ", ")
        {
            Builder.Clear();
            
            var firstElementAdded = false;
            foreach (var element in enumerable)
            {
                if (firstElementAdded)
                {
                    Builder.Append(separator);
                }

                Builder.Append(element);
                firstElementAdded = true;
            }

            return Builder.ToString();
        }
    }
}