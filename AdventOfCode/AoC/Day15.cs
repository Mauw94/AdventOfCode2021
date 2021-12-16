using System;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    public class Day15 : AdventBase
    {
        private int[][] _cavern;

        public Day15()
        {
            _cavern = Common.GetInput(15)
                .Select(ToArray)
                .ToArray();
        }

        int[] ToArray(string riskLevels)
        {
            return riskLevels.ToCharArray().Select(c => c - 48).ToArray();
        }

        /// <summary>
        /// Get the minimum distance from a level.
        /// </summary>
        static int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int levelsCount)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int i = 0; i < levelsCount; i++)
            {
                if (shortestPathTreeSet[i] == false && distance[i] < min)
                {
                    min = distance[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }

        static int[] Dijkstra(int[][] cavern, int source, int levelsCount)
        {
            int[] distance = new int[levelsCount];
            bool[] shortestPathTreeSet = new bool[levelsCount];

            for (int i = 0; i < levelsCount; i++)
            {
                distance[i] = int.MaxValue;
                shortestPathTreeSet[i] = false;
            }

            distance[source] = 0;

            for (int count = 0; count < levelsCount - 1; count++)
            {
                int minDist = MinimumDistance(distance, shortestPathTreeSet, levelsCount);
                shortestPathTreeSet[minDist] = true;

                for (int j = 0; j < levelsCount; j++)
                    if (!shortestPathTreeSet[j]
                        && Convert.ToBoolean(cavern[minDist][j])
                        && distance[minDist] != int.MaxValue
                        && distance[minDist] + cavern[minDist][j] < distance[j])
                    {
                        distance[j] = distance[minDist] + cavern[minDist][j];
                    }
            }

            return distance;
        }

        public override void Solution1()
        {
            var result = Dijkstra(_cavern, 1, _cavern.Length).Sum().ToString();

            LogResults(15, 1, result);
        }

        public override void Solution2()
        {
            base.Solution2();
        }
    }
}
