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
        private List<int> _locationPoints;

        public Day9()
        {
            _locationPoints = new();
            string[] input = File.ReadAllLines("C:\\Projects\\AdventOfCode2021\\input\\day9.txt");
            foreach (var line in input)
            {
                foreach (var point in line)
                {
                    _locationPoints.Add(int.Parse(point.ToString()));
                }
            }
        }

        public override void Solution1()
        {
            var lowPoints = 0;
            for (int i = 0; i < _locationPoints.Count; i++)
            {
                if (i == 0 || i == _locationPoints.Count) continue;

                if (_locationPoints[i] < _locationPoints[i - 1] && _locationPoints[i] < _locationPoints[i + 1])
                    lowPoints++;
            }
        }

        public override void Solution2()
        {

        }
    }
}
