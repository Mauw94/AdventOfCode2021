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
            string[] input = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day9.txt");

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
            int lowPoints = 0;
            int riskLevel = 0;

            for (int i = 0; i < _locationPoints.GetLength(0); i++)
            {
                for (int j = 0; j < _locationPoints.GetLength(1); j++)
                {
                }
            }
        }

        public override void Solution2()
        {

        }
    }
}
