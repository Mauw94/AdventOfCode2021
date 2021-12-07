using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode;

namespace AdventOfCode2021
{
    public class Day7 : IBase
    {
        List<int> _crabPositions;
        private bool _foundBestPosition = false;
        private int _resultSol2 = 0;

        public Day7()
        {
            _crabPositions = new List<int>();
            var input = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day7.txt");
            foreach (var inp in input)
            {
                inp.Split(",").Select(x => int.Parse(x)).ToList()
                .ForEach(x => _crabPositions.Add(x));
            }
        }

        public override void Solution1()
        {
            int median = GetMedian(_crabPositions.ToArray());
            int totalFuelCost = 0;

            for (int i = 0; i < _crabPositions.Count; i++)
            {
                var fuel = _crabPositions[i] - median;
                if (fuel < 0) fuel *= -1;

                totalFuelCost += fuel;
            }

            base.LogResults(7, 1, totalFuelCost.ToString());
        }

        public override void Solution2()
        {
            var possibleFuelCosts = new List<int>();
            var medianFuelCost = GetMedian(_crabPositions.ToArray());

            CalculateFuelCost(_crabPositions, medianFuelCost, possibleFuelCosts);
            base.LogResults(7, 2, _resultSol2.ToString());
        }

        private void CalculateFuelCost(List<int> crabPositions, int medianFuelCost, List<int> possibleFuelCosts)
        {
            var totalFuelCost = 0;
            while (!_foundBestPosition)
            {
                for (int i = 0; i < _crabPositions.Count; i++)
                {
                    var fuelCost = _crabPositions[i] - medianFuelCost;
                    if (fuelCost < 0) fuelCost *= -1;
                    var adjustFuelCost = 0;
                    var previousAdded = 0;
                    for (int j = 0; j < fuelCost; j++)
                    {
                        var temp = previousAdded;
                        previousAdded = temp + 1;
                        adjustFuelCost += previousAdded;
                    }

                    totalFuelCost += adjustFuelCost;
                }
                if (possibleFuelCosts.Count > 0 && possibleFuelCosts.Last() < totalFuelCost)
                {
                    _resultSol2 = possibleFuelCosts.Last();
                    _foundBestPosition = true;
                    return;
                }
                possibleFuelCosts.Add(totalFuelCost);
                medianFuelCost++;
                CalculateFuelCost(crabPositions, medianFuelCost, possibleFuelCosts);
            }
        }

        private int GetMedian(int[] source)
        {
            int[] sorted = (int[])source.Clone();
            Array.Sort(sorted);

            int size = sorted.Length;
            int mid = size / 2;
            int median = (size % 2 != 0) ? sorted[mid] : (sorted[mid] + sorted[mid - 1]) / 2;

            return median;
        }

    }
}
