using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    class Polymer
    {
        Dictionary<string, long> _pairs;
        readonly Dictionary<string, Tuple<string, string>> _pairsFromRule;
        readonly Dictionary<char, long> _quantities;

        public Polymer(List<string> input)
        {
            _pairsFromRule = new();
            _pairs = new();
            _quantities = new();

            CalculateQuantityFromPolymer(input.First());
            CreateRules(input);
            AddPairCounts(input.First());
        }

        void CalculateQuantityFromPolymer(string polymer)
        {
            for (int i = 0; i < polymer.Length; i++)
            {
                if (!_quantities.ContainsKey(polymer[i]))
                    _quantities.Add(polymer[i], 1);
                else
                    _quantities[polymer[i]]++;
            }
        }

        void CreateRules(List<string> input)
        {
            for (int i = 2; i < input.Count; i++)
            {
                char element = input[i].Substring(input[i].Length - 1, 1).ToCharArray()[0];

                _pairs.Add(input[i][..2], 0);
                _pairsFromRule.Add(input[i][..2],
                    new(input[i][..1] + element, element + input[i].Substring(1, 1)));
            }
        }

        void AddPairCounts(string polymer)
        {
            for (int i = 1; i < polymer.Length; i++)
            {
                _pairs[polymer[i - 1].ToString() + polymer[i].ToString()]++;
            }
        }

        public (long Min, long Max) Run(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                var currentPairs = new Dictionary<string, long>(_pairs);
                foreach (var pair in _pairs)
                {
                    if (pair.Value > 0)
                    {
                        var generates = _pairsFromRule[pair.Key];
                        currentPairs[generates.Item1] += pair.Value;
                        currentPairs[generates.Item2] += pair.Value;

                        var character = generates.Item1[1];
                        if (_quantities.ContainsKey(character))
                            _quantities[character] += pair.Value;
                        else
                            _quantities.Add(character, pair.Value);

                        currentPairs[pair.Key] -= pair.Value;
                    }
                }
                _pairs = currentPairs;
            }

            return (_quantities.Values.Min(), _quantities.Values.Max());
        }
    }

    public class Day14P2 : AdventBase
    {
        private readonly List<string> _input;

        public Day14P2()
        {
            _input = Common.GetInput(14);
        }

        public override void Solution1()
        {
            var polymer = new Polymer(_input);
            var (min, max) = polymer.Run(40);

            LogResults(14, 2, max - min);
        }
    }
}
