using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day9 : Base
    {
        private int[,] _locationPoints;

        public Day9()
        {
            string[] input = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day9-real.txt");

            _locationPoints = new int[input.Length, input[0].Length];

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    _locationPoints[i, j] = Convert.ToInt32(input[i][j].ToString());
                }
            }
        }

        public override void Solution1()
        {
            List<int> lowPoints = new();
            int riskLevel = 0;

            for (int i = 0; i < _locationPoints.GetLength(0); i++)
            {
                for (int j = 0; j < _locationPoints.GetLength(1); j++)
                {
                    var currentPoint = _locationPoints[i, j];

                    var isTopRight = i == 0 && j == _locationPoints.GetLength(1) - 1;
                    var isTopLeft = i == 0 && j == 0;
                    var isBottomLeft = i == _locationPoints.GetLength(0) - 1 && j == 0;
                    var isBottomRight = i == _locationPoints.GetLength(0) - 1 && j == _locationPoints.GetLength(1) - 1;

                    var firstRow = i == 0;
                    var lastRow = i == _locationPoints.GetLength(0) - 1;
                    var leftSide = j == 0;
                    var rightSide = j == _locationPoints.GetLength(1) - 1;

                    // top left
                    if (isTopLeft)
                    {
                        if (currentPoint < _locationPoints[i, j + 1] && currentPoint < _locationPoints[i + 1, j])
                        {
                            lowPoints.Add(_locationPoints[i, j]);
                        }
                    }
                    /// top right
                    if (isTopRight)
                    {
                        if (currentPoint < _locationPoints[i, j - 1] && currentPoint < _locationPoints[i + 1, j])
                        {
                            lowPoints.Add(_locationPoints[i, j]);
                        }
                    }
                    // bottom left
                    if (isBottomLeft)
                    {
                        if (currentPoint < _locationPoints[i - 1, j] && currentPoint < _locationPoints[i, j + 1])
                        {
                            lowPoints.Add(_locationPoints[i, j]);
                        }
                    }
                    // bottom right
                    if (isBottomRight)
                    {
                        if (currentPoint < _locationPoints[i, j - 1] && currentPoint < _locationPoints[i - 1, j])
                        {
                            lowPoints.Add(_locationPoints[i, j]);
                        }
                    }
                    if (firstRow && !isTopLeft && !isTopRight)
                    {
                        if (currentPoint < _locationPoints[i, j - 1]
                            && currentPoint < _locationPoints[i, j + 1]
                            && currentPoint < _locationPoints[i + 1, j])
                        {
                            lowPoints.Add(_locationPoints[i, j]);
                        }
                    }
                    if (lastRow && !isBottomLeft && !isBottomRight)
                    {
                        if (currentPoint < _locationPoints[i, j - 1]
                            && currentPoint < _locationPoints[i, j + 1]
                            && currentPoint < _locationPoints[i - 1, j])
                        {
                            lowPoints.Add(_locationPoints[i, j]);
                        }
                    }
                    if (leftSide && !isTopLeft && !isBottomLeft)
                    {
                        if (currentPoint < _locationPoints[i - 1, j]
                            && currentPoint < _locationPoints[i + 1, j]
                            && currentPoint < _locationPoints[i, j + 1])
                        {
                            lowPoints.Add(_locationPoints[i, j]);
                        }
                    }
                    if (rightSide && !isTopRight && !isBottomRight)
                    {
                        if (currentPoint < _locationPoints[i - 1, j]
                            && currentPoint < _locationPoints[i + 1, j]
                            && currentPoint < _locationPoints[i, j - 1])
                        {
                            lowPoints.Add(_locationPoints[i, j]);
                        }
                    }
                    if (!firstRow && !lastRow && !leftSide && !rightSide && !isTopLeft && !isTopRight && !isBottomRight && !isBottomLeft)
                    {
                        if (currentPoint < _locationPoints[i - 1, j]
                            && currentPoint < _locationPoints[i + 1, j]
                            && currentPoint < _locationPoints[i, j - 1]
                            && currentPoint < _locationPoints[i, j + 1])
                        {
                            lowPoints.Add(_locationPoints[i, j]);
                        }
                    }
                }
            }

            foreach (var point in lowPoints)
            {
                riskLevel += point + 1;
            }

            LogResults(9, 1, riskLevel.ToString());
        }

        public override void Solution2()
        {

        }
    }
}
