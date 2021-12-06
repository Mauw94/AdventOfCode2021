using AdventOfCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day6 : IBase
    {
        private List<int> _initialState;
        private List<int> _initialStateCopy;
        private const int _daysSol1 = 80;
        private const int _daysSol2 = 256;

        public Day6()
        {
            _initialState = new List<int>();
            string[] input = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day6.txt");
            foreach (var line in input)
            {
                var numbers = line.Split(",").Select(x => int.Parse(x)).ToList();
                foreach (var number in numbers)
                    _initialState.Add(number);
            }
            _initialStateCopy = new List<int>(_initialState);
        }

        public override void Solution1()
        {
            PopulateLaternFish(_initialState);
            _initialState = _initialStateCopy;
        }

        public override void Solution2()
        {
            PopulateLaternFishThatNeverDie(_initialState);
        }

        private void PopulateLaternFish(List<int> fishes)
        {
            for (int i = 0; i < _daysSol1; i++)
            {
                for (int j = 0; j < fishes.Count; j++)
                {
                    if (fishes[j] - 1 < 0)
                    {
                        fishes.Add(9);
                        fishes[j] = 6;
                    }
                    else
                    {
                        fishes[j] -= 1;
                    }
                }
            }

            base.LogResults(6, 1, fishes.Count.ToString());
        }

        private void PopulateLaternFishThatNeverDie(List<int> fishes)
        {
            Dictionary<long, long> fishLives = new Dictionary<long, long>();

            foreach (var f in fishes)
            {
                UpdateDictionary(fishLives, f, 1);
            }

            for (int i = 1; i <= _daysSol2; i++)
            {
                Dictionary<long, long> updatedFishLives = new Dictionary<long, long>();

                foreach (var daysToReproduce in fishLives.Keys.ToArray())
                {
                    var fishThisAge = fishLives[daysToReproduce];
                    var nextAge = daysToReproduce - 1;

                    if (nextAge >= 0)
                    {
                        UpdateDictionary(updatedFishLives, nextAge, fishThisAge);
                    }
                    else
                    {
                        UpdateDictionary(updatedFishLives, 8, fishThisAge);
                        UpdateDictionary(updatedFishLives, 6, fishThisAge);
                    }
                }

                fishLives = updatedFishLives;
            }

            long sum = 0;
            foreach (var fishAgeGroup in fishLives.Values)
            {
                sum += fishAgeGroup;
            }

            base.LogResults(6, 2, fishLives.Values.Sum().ToString());
        }

        private void UpdateDictionary(Dictionary<long, long> fishLives, long key, long amount)
        {
            if (fishLives.ContainsKey(key))
                fishLives[key] += amount;
            else
                fishLives.Add(key, amount);
        }
    }
}
