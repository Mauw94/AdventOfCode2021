using System.Collections.Generic;

namespace AdventOfCode2021.AoC
{
    public static class Extensions
    {
        /// <summary>
        /// Return the product of the numbers in the passed IEnumerable&lt;int&gt;
        /// </summary>
        /// <param name="factors"></param>
        public static long Product(this IEnumerable<int> factors)
        {
            long product = 1;

            foreach (int factor in factors)
                product *= factor;

            return product;
        }

        /// <summary>
        /// Return the product of the numbers in the passed IEnumerable&lt;long&gt;
        /// </summary>
        /// <param name="factors"></param>
        public static long Product(this IEnumerable<long> factors)
        {
            long product = 1;

            foreach (long factor in factors)
                product *= factor;

            return product;
        }
    }
}
