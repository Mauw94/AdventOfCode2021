using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    class Polymerizer
    {
        private string _startingTemplate;
        private readonly Dictionary<string, char> _pairInsertions;
        private readonly Dictionary<char, long> _occurences;

        public Polymerizer(List<string> input)
        {
            _startingTemplate = input.First();
            _pairInsertions = new();
            _occurences = new();

            Setup(input);
        }

        public long Run(int steps)
        {
            var morphedTemplate = _startingTemplate;
            for (int i = 0; i < steps; i++)
            {
                morphedTemplate = InsertBetweenPairs(CreateInsertionPairs(morphedTemplate));
            }

            var (min, max) = FindMinMax(morphedTemplate);

            return max - min;
        }

        (long Min, long Max) FindMinMax(string morphedTemplate)
        {
            foreach (var c in morphedTemplate.ToCharArray())
            {
                if (!_occurences.ContainsKey(c))
                    _occurences.Add(c, 0);

                _occurences[c]++;
            }

            var max = _occurences.Values.Max();
            var min = _occurences.Values.Min();

            return (min, max);
        }

        static List<(char, char)> CreateInsertionPairs(string template)
        {
            List<(char, char)> pairs = new();
            char[] chars = template.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i + 1 == chars.Length)
                    break;

                pairs.Add((chars[i], chars[i + 1]));
            }

            return pairs;
        }

        string InsertBetweenPairs(List<(char, char)> insertionPairs)
        {
            var newTemplate = string.Empty;

            for (int i = 0; i < insertionPairs.Count; i++)
            {
                var pair = insertionPairs[i];
                bool last = false;
                if (i == insertionPairs.Count - 1)
                    last = true;

                string pairInsertionKey = $"{pair.Item1}{pair.Item2}";

                if (!_pairInsertions.ContainsKey(pairInsertionKey))
                    throw new ArgumentOutOfRangeException($"Can not find item with key: {pairInsertionKey}");

                var insert = _pairInsertions[pairInsertionKey];

                if (!last)
                    newTemplate += pair.Item1.ToString() + insert;
                else
                    newTemplate += pair.Item1.ToString() + insert + pair.Item2.ToString();
            }

            return newTemplate;
        }

        void Setup(List<string> input)
        {
            foreach (var pair in input.Where(line => line.Contains(" -> ")))
            {
                var pairs = pair.Split(" -> ");
                _pairInsertions.Add(pairs[0], char.Parse(pairs[1]));
            }
        }
    }

    public class Day14P1 : AdventBase
    {
        private readonly List<string> _input;

        public Day14P1()
        {
            _input = Common.GetInput(14);
        }

        public override void Solution1()
        {
            var polymer = new Polymerizer(_input);
            var result = polymer.Run(10);

            LogResults(14, 1, result);
        }
    }
}
