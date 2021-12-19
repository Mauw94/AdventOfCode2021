using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    public record Coord(int Row, int Col);

    public class Day9 : AdventBase
    {
        readonly int[][] _heights;
        readonly int _floorRows;
        readonly int _floorCols;

        public Day9()
        {
            _heights = Common.GetInput(9)
                .Select(ToDigitArray)
                .ToArray();

            _floorRows = _heights.Length;
            _floorCols = _heights[0].Length;
        }

        int[] ToDigitArray(string digits)
        {
            return digits
                .ToCharArray()
                .Select(c => c - 48)
                .ToArray();
        }

        int HeightAt(Coord point)
        {
            return _heights[point.Row][point.Col];
        }

        public override void Solution1()
        {
            int risk = 0;

            foreach (var point in FindLowPoints())
                risk += HeightAt(point) + 1;

            LogResults(9, 1, risk);
        }

        public override void Solution2()
        {
            List<List<Coord>> basins = new();

            foreach (var point in FindLowPoints())
                basins.Add(FloodBasin(point));

            var biggestThree = basins
                .OrderByDescending(b => b.Count)
                .Take(3);

            long size = biggestThree
                .Select(b => b.Count)
                .Product();

            LogResults(9, 2, size);
        }

        List<Coord> FindLowPoints()
        {
            List<Coord> points = new();

            for (int row = 0; row < _floorRows; row++)
                for (int col = 0; col < _floorCols; col++)
                {
                    int height = _heights[row][col];
                    var neighbours = Neighbours(row, col);

                    bool isLowest = neighbours
                        .All(n => HeightAt(n) > height);

                    if (isLowest)
                        points.Add(new Coord(row, col));
                }

            return points;
        }

        List<Coord> Neighbours(int row, int col)
        {
            List<Coord> neighbours = new()
            {
                new Coord(row - 1, col),
                new Coord(row, col + 1),
                new Coord(row + 1, col),
                new Coord(row, col - 1),
            };

            return neighbours
                .Where(n => n.Row >= 0 && n.Row < _floorRows
                         && n.Col >= 0 && n.Col < _floorCols)
                .ToList();
        }

        List<Coord> FloodBasin(Coord point)
        {
            Stack<Coord> frontier = new();
            List<Coord> basin = new();

            frontier.Push(point);
            basin.Add(point);

            while (frontier.Any())
            {
                var next = frontier.Pop();
                var neighbours = Neighbours(next.Row, next.Col);

                var upwards = neighbours
                    .Where(n => HeightAt(n) > HeightAt(next))
                    .Where(n => HeightAt(n) < 9);

                foreach (var coord in upwards)
                {
                    if (basin.Contains(coord))
                        continue;

                    frontier.Push(coord);
                    basin.Add(coord);
                }
            }

            return basin;
        }

    }
}
