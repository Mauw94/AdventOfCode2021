using System.Collections.Generic;

namespace AdventOfCode2021
{
    public static class Extensions
    {
        public static long Product(this IEnumerable<int> factors)
        {
            long product = 1;

            foreach (int factor in factors)
                product *= factor;

            return product;
        }
    }
}
