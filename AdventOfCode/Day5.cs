using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day5 : IBase
    {
        private readonly List<Point> _hydroThermalLines;
        private int _gridSize;
        public Day5()
        {
            string[] input = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day5.txt");
            _hydroThermalLines = new List<Point>();
            _gridSize = input.Length;

            foreach (var i in input)
            {
                var middle = i.IndexOf("->");
                var firstCoordinates = i.Substring(0, middle).Split(",").Select(x => int.Parse(x)).ToList();
                var point = new Point(firstCoordinates[0], firstCoordinates[1]);
                _hydroThermalLines.Add(point);

                var secondCoordinates = i.Substring(middle + 2, i.Length - middle - 2).Split(",").Select(x => int.Parse(x)).ToList();
                point = new Point(secondCoordinates[0], secondCoordinates[1]);
                _hydroThermalLines.Add(point);
            }
        }

        public override void Solution1()
        {
            GridPoint[,] hydroThermalGrid = new GridPoint[_gridSize * 2, _gridSize * 2];
            for (int i = 0; i < _hydroThermalLines.Count; i += 2)
            {
                if (i + 1 >= _hydroThermalLines.Count) continue;
                FillGrid(_hydroThermalLines[i], _hydroThermalLines[i + 1], hydroThermalGrid);
            }

            PrintGrid(hydroThermalGrid);
        }

        private void FillGrid(Point point1, Point point2, GridPoint[,] grid)
        {
            if (point1.X == point2.X)
            {
                if (point1.Y < point2.Y)
                {
                    for (int i = point1.Y; i <= point2.Y; i++)
                    {
                        if (grid[point1.X, i] == null) grid[point1.X, i] = new GridPoint(point1.X, i);
                        else
                        {
                            grid[point1.X, i].Overlaps++;
                        }
                    }
                }
                else
                {
                    for (int i = point1.Y; i >= point2.Y; i--)
                    {
                        if (grid[point1.X, i] == null) grid[point1.X, i] = new GridPoint(point1.X, i);
                        else
                        {
                            grid[point1.X, i].Overlaps++;
                        }
                    }
                }
            }
            if (point1.Y == point2.Y)
            {
                if (point1.X < point2.X)
                {
                    for (int i = point1.X; i <= point2.X; i++)
                    {
                        if (grid[i, point1.Y] == null) grid[i, point1.Y] = new GridPoint(i, point1.Y);
                        else
                        {
                            grid[i, point1.Y].Overlaps++;
                        }
                    }
                }
                else
                {
                    for (int i = point1.X; i >= point2.X; i--)
                    {
                        if (grid[i, point1.Y] == null) grid[i, point1.Y] = new GridPoint(i, point1.Y);
                        else
                        {
                            grid[i, point1.Y].Overlaps++;
                        }
                    }
                }
            }
        }

        private void PrintGrid(GridPoint[,] grid)
        {
            int totalOverlaps = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[j, i] == null || grid[j, i].Overlaps == 0)
                        Console.Write(".");

                    if (grid[j, i] != null && grid[j, i].Overlaps > 0)
                        Console.Write(grid[j, i].Overlaps.ToString());

                    if (grid[j, i] != null && grid[j, i].Overlaps >= 2)
                        totalOverlaps++;
                }
                Console.Write("\n");
            }

            Console.Write("\n");
            base.LogResults(5, 1, totalOverlaps.ToString());
        }

        public override void Solution2()
        {
            base.Solution2();
        }
    }
}
