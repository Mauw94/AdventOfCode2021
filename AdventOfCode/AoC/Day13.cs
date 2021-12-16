using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.AoC
{
    public class Day13 : AdventBase
    {
        private readonly List<Coord> _coordinates;
        private readonly List<FoldAt> _folds;

        record Coord(int Row, int Col);
        record FoldAt(char Axis, int Position);

        public Day13()
        {
            _coordinates = new();
            _folds = new();

            List<string> input = Common.GetInput(13);

            ParseAllCoordinates(input);

            ParseAllFolds(input);
        }

        void ParseAllCoordinates(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == "")
                    break;
                var coords = input[i].Split(",");
                _coordinates.Add(new Coord(int.Parse(coords[0]), int.Parse(coords[1])));
            }
        }

        void ParseAllFolds(List<string> input)
        {
            var foldsIndex = input.FindIndex(x => x == "");

            for (int j = 1; j < input.Count - foldsIndex; j++)
            {
                var foldInfo = input[j + foldsIndex].Remove(0, "fold along ".Length);
                var foldAt = foldInfo.Split("=");
                _folds.Add(new FoldAt(char.Parse(foldAt[0]), int.Parse(foldAt[1])));
            }
        }

        public override void Solution1()
        {

        }

        public override void Solution2()
        {

        }
    }
}
