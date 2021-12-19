using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    public class Day13 : AdventBase
    {
        private List<Dot> _dots;
        private readonly List<FoldAt> _folds;

        record Dot(int Col, int Row);
        record FoldAt(bool X, int Position);

        public Day13()
        {
            _dots = new();
            _folds = new();

            List<string> input = Common.GetInput(13);

            ParseAllCoordinates(input);
            ParseAllFolds(input);
        }

        void DoFold(FoldAt fold)
        {
            _dots = _dots
                .Select(dot => Fold(fold, dot))
                .Distinct()
                .ToList();
        }

        static Dot Fold(FoldAt fold, Dot dot)
        {
            if (fold.X)
            {
                if (dot.Col < fold.Position)
                    return dot;

                return new Dot(fold.Position - (dot.Col - fold.Position), dot.Row);
            }
            else
            {
                if (dot.Row < fold.Position)
                    return dot;

                return new Dot(dot.Col, fold.Position - (dot.Row - fold.Position));
            }
        }

        void ParseAllCoordinates(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == "")
                    break;
                var coords = input[i].Split(",");
                _dots.Add(new Dot(int.Parse(coords[0]), int.Parse(coords[1])));
            }
        }

        void ParseAllFolds(List<string> input)
        {
            var foldsIndex = input.FindIndex(x => x == "");

            for (int j = 1; j < input.Count - foldsIndex; j++)
            {
                var foldInfo = input[j + foldsIndex].Remove(0, "fold along ".Length);
                var foldAt = foldInfo.Split("=");
                _folds.Add(new FoldAt(foldAt[0] == "x", int.Parse(foldAt[1])));
            }
        }

        public override void Solution1()
        {
            var fold = _folds.First();
            DoFold(fold);

            LogResults(13, 1, _dots.Count);
        }

        public override void Solution2()
        {
            foreach (var fold in _folds)
                DoFold(fold);

            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 40; j++)
                {
                    char dot = _dots.Contains(new Dot(j, i))
                        ? '#'
                        : '.';
                    Console.Write(dot);
                }
                Console.WriteLine();
            }

            LogResults(13, 2, _dots.Count);
        }
    }
}
