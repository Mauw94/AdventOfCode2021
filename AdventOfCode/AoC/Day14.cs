using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.AoC
{
    public class Day14 : AdventBase
    {
        private readonly string _template;
        private readonly Dictionary<string, char> _pairInsertions;
        private readonly Dictionary<char, long> _occurences;

        public Day14()
        {
            var input = Common.GetInput(14);

            _template = input.First();
            _pairInsertions = new();
            _occurences = new();

            foreach (var pair in input.Where(line => line.Contains(" -> ")))
            {
                var pairs = pair.Split(" -> ");
                _pairInsertions.Add(pairs[0], char.Parse(pairs[1]));
            }

            CreateOccurences(_template);
        }

        public override void Solution1()
        {
        }

        public override void Solution2()
        {
            base.Solution2();
        }

        void CreateOccurences(string template)
        {
            var chars = template.ToCharArray();
            foreach (var c in chars)
            {
                if (!_occurences.ContainsKey(c))
                    _occurences.Add(c, 0);
            }
        }

    }
}
