using System.IO;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    public class Day1 : AdventBase
    {
        private int[] _depthNumbers;

        public Day1()
        {
            string[] fileContent = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day1.txt");
            _depthNumbers = fileContent.Select(x => int.Parse(x)).ToArray();
        }

        public override void Solution1()
        {
            int increased = 0;

            for (int j = 0; j < _depthNumbers.Length; j++)
            {
                if (j != 0)
                {
                    var previousDepth = _depthNumbers[j - 1];
                    var currentDepth = _depthNumbers[j];
                    if (previousDepth < currentDepth)
                        increased++;
                }
            }

            LogResults(1, 1, increased);
        }

        public override void Solution2()
        {
            int increased = 0;
            int j = 0;
            int previousSum = 0;
            int nextSum = 0;
            for (int i = 0; i <= _depthNumbers.Length; i++)
            {
                if (j == 3)
                {
                    if (previousSum != 0)
                        if (previousSum < nextSum)
                            increased++;
                    previousSum = nextSum;
                    nextSum = 0;
                    j = 0;
                    i -= 2;
                }
                if (i != _depthNumbers.Length)
                {
                    nextSum += _depthNumbers[i];
                    j++;
                }
            }

            LogResults(1, 2, increased);
        }
    }
}
