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
        private Paper[,] _paper;

        record Coord(int X, int Y);
        record FoldAt(char Axis, int Position);

        class Paper
        {
            public Coord Coord { get; set; }
            public bool IsFolded { get; set; }
            public bool Remove { get; set; }

            public Paper(Coord coord, bool isFolded)
            {
                Coord = coord;
                IsFolded = isFolded;
                Remove = false;
            }
        }

        public Day13()
        {
            _coordinates = new();
            _folds = new();

            List<string> input = Common.GetInput(13);

            ParseAllCoordinates(input);
            ParseAllFolds(input);
            CreatePaper();
            Fold();
        }

        void Fold()
        {
            foreach (var fold in _folds)
            {
                if (fold.Axis == 'y')
                    FoldUp(fold.Position);
                else
                    FoldLeft(fold.Position);
            }
        }

        void FoldUp(int position)
        {
            var foldCount = 0;
            for (int i = 0; i < _paper.GetLength(0); i++)
            {
                if (i > position) foldCount += 2;
                for (int j = 0; j < _paper.GetLength(1); j++)
                {
                    if (i > position)
                    {
                        if (!(_paper[i - foldCount, j].IsFolded) && _paper[i, j].IsFolded)
                            _paper[i - foldCount, j].IsFolded = true;

                        _paper[i, j].Remove = true;
                    }
                }
            }

            // Add 1 cos we also want to remove at the current position.
            CleanUpPaper(position, true);
            PrintFolderPaper();

        }

        void FoldLeft(int position)
        {
            var foldCount = 0;
            for (int i = 0; i < _paper.GetLength(0); i++)
            {
                foldCount = 0;
                for (int j = 0; j < _paper.GetLength(1); j++)
                {
                    foldCount += 2;
                    if (j > position)
                    {
                        if (!(_paper[i, j - foldCount].IsFolded) && _paper[i, j].IsFolded)
                            _paper[i, j - foldCount].IsFolded = true;

                        _paper[i, j].Remove = true;
                    }
                }
            }

            // Add 1 cos we also want to remove at the current position.
            CleanUpPaper(position + 1, false);
            PrintFolderPaper();

        }

        void CleanUpPaper(int position, bool foldUp)
        {
            Paper[,] newPaper;

            if (foldUp)
                newPaper = new Paper[_paper.GetLength(0) - position, _paper.GetLength(1)];
            else
                newPaper = new Paper[_paper.GetLength(0), _paper.GetLength(1) - position];

            for (int i = 0; i < newPaper.GetLength(0); i++)
            {
                for (int j = 0; j < newPaper.GetLength(1); j++)
                {
                    newPaper[i, j] = _paper[i, j];
                }
            }

            _paper = newPaper;
        }

        void PrintFolderPaper()
        {
            for (int i = 0; i < _paper.GetLength(0); i++)
            {
                for (int j = 0; j < _paper.GetLength(1); j++)
                {
                    if (_paper[i, j].IsFolded)
                        Console.Write(string.Format("{0} ", "#"));
                    else
                        Console.Write(string.Format("{0} ", "."));
                }
                Console.Write(Environment.NewLine);
            }
        }

        void CreatePaper()
        {
            int rowCount = _coordinates.Max(x => x.Y);
            int colCount = _coordinates.Max(x => x.X);
            _paper = new Paper[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                {
                    Coord newPaperCoord = new(j, i);
                    bool isFolded = false;

                    if (_coordinates.Contains(newPaperCoord))
                        isFolded = true;

                    _paper[i, j] = new Paper(new Coord(j, i), isFolded);
                }
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
